using System.Runtime.Serialization;

namespace My.Function.Models;

public enum GreetLanguage
{
    [EnumMember(Value = "en")]
    En,

    [EnumMember(Value = "fr")]
    Fr,

    [EnumMember(Value = "es")]
    Es,

    [EnumMember(Value = "de")]
    De
}
