using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Repositories;
using My.Function.Services;
using My.Function.Time;

namespace My.Function;

public class CleanupOldBlobs
{
    // NCRONTAB with seconds field: {second} {minute} {hour} {day} {month} {day-of-week}
    private const string MondaysAt4AmUtc = "0 0 4 * * 1";

    // Blobs last modified more than this long ago are deleted.
    private static readonly TimeSpan MaxAge = TimeSpan.FromDays(2);

    private readonly ILogger<CleanupOldBlobs> _logger;
    private readonly AppTimeZone _appTimeZone;

    public CleanupOldBlobs(ILogger<CleanupOldBlobs> logger, AppTimeZone appTimeZone)
    {
        _logger = logger;
        _appTimeZone = appTimeZone;
    }

    [Function("CleanupOldBlobs")]
    public async Task Run(
        [TimerTrigger(MondaysAt4AmUtc)] TimerInfo timer,
        [BlobInput(BlobUploadService.ContainerName)] BlobContainerClient container)
    {
        var cutoff = DateTimeOffset.UtcNow - MaxAge;

        _logger.LogInformation(
            "CleanupOldBlobs started at {Timestamp:O} ({TimeZone}, container: {Container}, cutoff: {Cutoff:O}, isPastDue: {IsPastDue})",
            _appTimeZone.Now,
            _appTimeZone.Id,
            BlobUploadService.ContainerName,
            _appTimeZone.Convert(cutoff),
            timer.IsPastDue);

        if (!await container.ExistsAsync())
        {
            _logger.LogInformation("Container {Container} does not exist yet; nothing to clean up.", BlobUploadService.ContainerName);
            return;
        }

        var scanned = 0;
        var deleted = 0;

        await foreach (BlobItem blob in container.GetBlobsAsync())
        {
            scanned++;

            if (string.Equals(blob.Name, BlobTodoRepository.BlobName, StringComparison.Ordinal))
            {
                continue;
            }

            var lastModified = blob.Properties.LastModified;
            if (lastModified is null || lastModified > cutoff)
            {
                continue;
            }

            try
            {
                await container.DeleteBlobIfExistsAsync(blob.Name, DeleteSnapshotsOption.IncludeSnapshots);
                deleted++;
                _logger.LogInformation(
                    "Deleted blob {BlobName} (last modified {LastModified:O})",
                    blob.Name,
                    _appTimeZone.Convert(lastModified.Value));
            }
            catch (Azure.RequestFailedException ex)
            {
                // Keep going so one bad blob doesn't abort the whole sweep.
                _logger.LogWarning(ex, "Failed to delete blob {BlobName}", blob.Name);
            }
        }

        _logger.LogInformation(
            "CleanupOldBlobs finished: scanned {Scanned} blob(s), deleted {Deleted} older than {MaxAgeDays} days.",
            scanned,
            deleted,
            MaxAge.TotalDays);

        if (timer.ScheduleStatus is not null)
        {
            _logger.LogInformation(
                "Next cleanup scheduled for {Next:O} ({TimeZone})",
                _appTimeZone.Convert(timer.ScheduleStatus.Next),
                _appTimeZone.Id);
        }
    }
}
