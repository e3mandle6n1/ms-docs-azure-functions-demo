# ms-docs-azure-functions-demo

[![Deploy Azure Functions](https://github.com/e3mandle6n1/ms-docs-azure-functions-demo/actions/workflows/azure-dev.yml/badge.svg)](https://github.com/e3mandle6n1/ms-docs-azure-functions-demo/actions/workflows/azure-dev.yml)

[![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff)](#)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![Azure Functions](https://img.shields.io/badge/Azure_Functions-v4-0062AD?logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyBpZD0iYTJjODgzMDYtZmEwMy00ZTViLWIxOTItNDAxZjBiNzc4MDhiIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxOCIgaGVpZ2h0PSIxOCIgdmlld0JveD0iMCAwIDE4IDE4Ij48ZGVmcz48bGluZWFyR3JhZGllbnQgaWQ9ImI0MDNhY2E3LWYzODctNDQzNC05NmI0LWFlMTU3ZWRjODM1ZiIgeDE9Ii0xNzUuOTkzIiB5MT0iLTM0My43MjMiIHgyPSItMTc1Ljk5MyIgeTI9Ii0zNTkuMjMyIiBncmFkaWVudFRyYW5zZm9ybT0idHJhbnNsYXRlKDIxMi41NzMgMzcwLjU0OCkgc2NhbGUoMS4xNTYgMS4wMjkpIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI%2BPHN0b3Agb2Zmc2V0PSIwIiBzdG9wLWNvbG9yPSIjZmVhMTFiIiAvPjxzdG9wIG9mZnNldD0iMC4yODQiIHN0b3AtY29sb3I9IiNmZWE1MWEiIC8%2BPHN0b3Agb2Zmc2V0PSIwLjU0NyIgc3RvcC1jb2xvcj0iI2ZlYjAxOCIgLz48c3RvcCBvZmZzZXQ9IjAuOCIgc3RvcC1jb2xvcj0iI2ZmYzMxNCIgLz48c3RvcCBvZmZzZXQ9IjEiIHN0b3AtY29sb3I9IiNmZmQ3MGYiIC8%2BPC9saW5lYXJHcmFkaWVudD48L2RlZnM%2BPHRpdGxlPkljb24tY29tcHV0ZS0yOTwvdGl0bGU%2BPGc%2BPHBhdGggZD0iTTIuMzcsNy40NzVIMy4yYS4yNjcuMjY3LDAsMCwxLC4yNjcuMjY3djYuMTQ4YS41MzMuNTMzLDAsMCwxLS41MzMuNTMzSDIuMWEwLDAsMCwwLDEsMCwwVjcuNzQxYS4yNjcuMjY3LDAsMCwxLC4yNjctLjI2N1oiIHRyYW5zZm9ybT0idHJhbnNsYXRlKDEyLjUwNyAxNi43MDUpIHJvdGF0ZSgxMzQuOTE5KSIgZmlsbD0iIzUwZTZmZiIgLz48cGF0aCBkPSJNMi4zMjUsMy42aC44MzNhLjI2Ny4yNjcsMCwwLDEsLjI2Ny4yNjd2Ni41ODNhMCwwLDAsMCwxLDAsMEgyLjU5MWEuNTMzLjUzMywwLDAsMS0uNTMzLS41MzNWMy44NjVBLjI2Ny4yNjcsMCwwLDEsMi4zMjUsMy42WiIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoNS43NTkgMC4xMTQpIHJvdGF0ZSg0NC45MTkpIiBmaWxsPSIjMTQ5MGRmIiAvPjwvZz48Zz48cGF0aCBkPSJNMTQuNTMsNy40NzVoLjgzM2EuNTMzLjUzMywwLDAsMSwuNTMzLjUzM3Y2LjE0OGEuMjY3LjI2NywwLDAsMS0uMjY3LjI2N0gxNC44YS4yNjcuMjY3LDAsMCwxLS4yNjctLjI2N1Y3LjQ3NWEwLDAsMCwwLDEsMCwwWiIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoMTIuMjIzIC03LjU1NSkgcm90YXRlKDQ1LjA4MSkiIGZpbGw9IiM1MGU2ZmYiIC8%2BPHBhdGggZD0iTTE1LjEwOCwzLjZoLjgzM2EwLDAsMCwwLDEsMCwwdjYuNTgzYS4yNjcuMjY3LDAsMCwxLS4yNjcuMjY3aC0uODMzYS4yNjcuMjY3LDAsMCwxLS4yNjctLjI2N1Y0LjEzMWEuNTMzLjUzMywwLDAsMSwuNTMzLS41MzNaIiB0cmFuc2Zvcm09InRyYW5zbGF0ZSgzMS4wMjIgMS4yMjIpIHJvdGF0ZSgxMzUuMDgxKSIgZmlsbD0iIzE0OTBkZiIgLz48L2c%2BPHBhdGggZD0iTTguNDU5LDkuOUg0Ljg3YS4xOTMuMTkzLDAsMCwxLS4yLS4xODEuMTY2LjE2NiwwLDAsMSwuMDE4LS4wNzVMOC45OTEsMS4xM2EuMjA2LjIwNiwwLDAsMSwuMTg2LS4xMDZoNC4yNDVhLjE5My4xOTMsMCwwLDEsLjIuMTgxLjE2NS4xNjUsMCwwLDEtLjAzNS4xTDguNTM0LDcuOTY2aDQuOTI4YS4xOTMuMTkzLDAsMCwxLC4yLjE4MS4xNzYuMTc2LDAsMCwxLS4wNTIuMTIyTDUuNDIxLDE2Ljc4OGMtLjA3Ny4wNDYtLjYyNC41LS4zNTYtLjE4OWgwWiIgZmlsbD0idXJsKCNiNDAzYWNhNy1mMzg3LTQ0MzQtOTZiNC1hZTE1N2VkYzgzNWYpIiAvPjwvc3ZnPg%3D%3D)
![C#](https://img.shields.io/badge/C%23-Isolated_Worker-239120?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-HTTP-512BD4?logo=dotnet&logoColor=white)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=black)](#interactive-api-explorer-swagger)
![Azure Developer CLI](https://img.shields.io/badge/Azure_Developer_CLI-azd-0078D4?logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyBpZD0idXVpZC1mYmJiZWVkZi03ZjAzLTRkNjItYjBmMC1hNGI2ODljMDQxZDkiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjE4IiBoZWlnaHQ9IjE4IiB2aWV3Qm94PSIwIDAgMTggMTgiPjxkZWZzPjxsaW5lYXJHcmFkaWVudCBpZD0idXVpZC03Y2QwYWU0Ny02NDE0LTRiOTMtYWJjOS1hNzA1MTk3MDEzN2EiIHgxPSI5IiB5MT0iMTYuNjg2IiB4Mj0iOSIgeTI9IjEzLjM3NyIgZ3JhZGllbnRUcmFuc2Zvcm09Im1hdHJpeCgxLCAwLCAwLCAxLCAwLCAwKSIgZ3JhZGllbnRVbml0cz0idXNlclNwYWNlT25Vc2UiPjxzdG9wIG9mZnNldD0iLjAwMSIgc3RvcC1jb2xvcj0iIzM3YzJiMSIgLz48c3RvcCBvZmZzZXQ9IjEiIHN0b3AtY29sb3I9IiMzY2Q0YzIiIC8%2BPC9saW5lYXJHcmFkaWVudD48bGluZWFyR3JhZGllbnQgaWQ9InV1aWQtYjEwMWFlNDUtNDYxYS00ZGU3LWIxYTAtZjNlMjllYTk2Y2M3IiB4MT0iOC45ODUiIHkxPSI3NzkuMTU4IiB4Mj0iOC45ODUiIHkyPSI3OTEuMTA4IiBncmFkaWVudFRyYW5zZm9ybT0idHJhbnNsYXRlKDAgNzkxLjUxNikgc2NhbGUoMSAtMSkiIGdyYWRpZW50VW5pdHM9InVzZXJTcGFjZU9uVXNlIj48c3RvcCBvZmZzZXQ9IjAiIHN0b3AtY29sb3I9IiMwMDc4ZDQiIC8%2BPHN0b3Agb2Zmc2V0PSIuMTU2IiBzdG9wLWNvbG9yPSIjMTM4MGRhIiAvPjxzdG9wIG9mZnNldD0iLjUyOCIgc3RvcC1jb2xvcj0iIzNjOTFlNSIgLz48c3RvcCBvZmZzZXQ9Ii44MjIiIHN0b3AtY29sb3I9IiM1NTljZWMiIC8%2BPHN0b3Agb2Zmc2V0PSIxIiBzdG9wLWNvbG9yPSIjNWVhMGVmIiAvPjwvbGluZWFyR3JhZGllbnQ%2BPC9kZWZzPjxnPjxwYXRoIGQ9Ik0xOCwxNi42ODZIMGwzLjMzMi0zLjE0OWMuMTA4LS4xMDIsLjI1MS0uMTYsLjQtLjE2SDE0LjY4Yy4wODMsMCwuMTY1LC4wMTksLjI0LC4wNTMsLjA3NiwuMDM1LC4xNDMsLjA4NSwuMTk4LC4xNDdsMi44ODIsMy4xMDlaIiBmaWxsPSJ1cmwoI3V1aWQtN2NkMGFlNDctNjQxNC00YjkzLWFiYzktYTcwNTE5NzAxMzdhKSIgLz48cGF0aCBkPSJNMCwxNi42ODZIMTh2LjQxYzAsLjE1NS0uMDYyLC4zMDUtLjE3MiwuNDE0cy0uMjU5LC4xNzItLjQxNCwuMTcySC41ODZjLS4xNTUsMC0uMzA0LS4wNjItLjQxNC0uMTcyLS4xMS0uMTEtLjE3Mi0uMjU5LS4xNzItLjQxNHYtLjQxWiIgZmlsbD0iIzNjZDRjMiIgLz48cGF0aCBkPSJNMTcuNDMsOC42MThjLS4wMjYtLjg5OS0uMzcxLTEuNzU5LS45NzQtMi40MjYtLjYwMy0uNjY3LTEuNDI0LTEuMDk3LTIuMzE2LTEuMjE0LS4wNTItMS4yNTYtLjU5OC0yLjQ0Mi0xLjUyLTMuMjk3LS45MjEtLjg1Ni0yLjE0NC0xLjMxMy0zLjQtMS4yNzMtMS4wMTQtLjAxNy0yLjAwOCwuMjgxLTIuODQ2LC44NTItLjgzOCwuNTcxLTEuNDc5LDEuMzg4LTEuODM0LDIuMzM4LTEuMDc4LC4xMjQtMi4wNzUsLjYzMi0yLjgwOSwxLjQzMS0uNzM0LC43OTktMS4xNTcsMS44MzUtMS4xOTEsMi45MTksLjAxOCwuNjA4LC4xNTYsMS4yMDYsLjQwNywxLjc2LC4yNTEsLjU1NCwuNjA4LDEuMDUzLDEuMDUzLDEuNDY3LC40NDQsLjQxNSwuOTY3LC43MzcsMS41MzcsLjk0OSwuNTcsLjIxMiwxLjE3NiwuMzA4LDEuNzg0LC4yODRIMTMuNjNjMS4wMDMtLjAxLDEuOTYyLS40MTMsMi42NzItMS4xMjEsLjcxLS43MDgsMS4xMTUtMS42NjYsMS4xMjgtMi42NjlaIiBmaWxsPSJ1cmwoI3V1aWQtYjEwMWFlNDUtNDYxYS00ZGU3LWIxYTAtZjNlMjllYTk2Y2M3KSIgLz48cGF0aCBkPSJNNi4zNiw2Ljg2OGwyLjY0LTIuNTljLjAyOC0uMDI5LC4wNjEtLjA1MiwuMDk4LS4wNjcsLjAzNy0uMDE2LC4wNzctLjAyNCwuMTE3LS4wMjRzLjA4LC4wMDgsLjExNywuMDI0Yy4wMzcsLjAxNiwuMDcsLjAzOCwuMDk4LC4wNjdsMi41NywyLjU5Yy4wMjIsLjAxOCwuMDM4LC4wNDMsLjA0NSwuMDcsLjAwNywuMDI4LC4wMDQsLjA1Ny0uMDA3LC4wODMtLjAxMSwuMDI2LS4wMzEsLjA0OC0uMDU2LC4wNjItLjAyNSwuMDE0LS4wNTQsLjAxOS0uMDgyLC4wMTVoLTEuNjJjLS4wMzYsLjAwMi0uMDcxLC4wMTgtLjA5NiwuMDQ0LS4wMjYsLjAyNi0uMDQxLC4wNi0uMDQ0LC4wOTZsLS4wMjEsNS4wNzJjMCwuMDI5LS4wMTIsLjA1Ny0uMDMyLC4wNzgtLjAyMSwuMDIxLS4wNDksLjAzMi0uMDc4LC4wMzJoLTEuNzRjLS4wMjksMC0uMDU3LS4wMTItLjA3OC0uMDMyLS4wMjEtLjAyMS0uMDMyLS4wNDktLjAzMi0uMDc4bC4wMjEtNS4wNzJjMC0uMDM1LS4wMTMtLjA3LS4wMzctLjA5Ni0uMDI0LS4wMjYtLjA1Ny0uMDQyLS4wOTMtLjA0NGgtMS42Yy0uMDI3LC4wMDItLjA1NC0uMDA2LS4wNzctLjAycy0uMDQxLS4wMzYtLjA1MS0uMDYyYy0uMDEtLjAyNS0uMDEyLS4wNTMtLjAwNS0uMDgsLjAwNy0uMDI2LC4wMjItLjA1LC4wNDMtLjA2OFoiIGZpbGw9IiNmMmYyZjIiIC8%2BPC9nPjwvc3ZnPg%3D%3D)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-Enabled-000000?logo=opentelemetry&logoColor=white)
![Application Insights](https://img.shields.io/badge/Application_Insights-Monitoring-68217A?logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyBpZD0iYjZmNmQ5OWUtZjMzMC00ODFkLTgzNmYtZWE1OGNjNDIyMTdmIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxOCIgaGVpZ2h0PSIxOCIgdmlld0JveD0iMCAwIDE4IDE4Ij48ZGVmcz48cmFkaWFsR3JhZGllbnQgaWQ9ImE3YTFjNDMxLTZjNmQtNGE4Zi05YTY5LThkYTQzN2U1YjBjNSIgY3g9IjkiIGN5PSI3LjM1IiByPSI2LjQyIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI%2BPHN0b3Agb2Zmc2V0PSIwIiBzdG9wLWNvbG9yPSIjYjc3YWY0IiAvPjxzdG9wIG9mZnNldD0iMC4yMSIgc3RvcC1jb2xvcj0iI2IzNzhmMiIgLz48c3RvcCBvZmZzZXQ9IjAuNDMiIHN0b3AtY29sb3I9IiNhNjcyZWQiIC8%2BPHN0b3Agb2Zmc2V0PSIwLjY1IiBzdG9wLWNvbG9yPSIjOTI2N2U0IiAvPjxzdG9wIG9mZnNldD0iMC44OCIgc3RvcC1jb2xvcj0iIzc1NTlkOCIgLz48c3RvcCBvZmZzZXQ9IjEiIHN0b3AtY29sb3I9IiM2MjRmZDAiIC8%2BPC9yYWRpYWxHcmFkaWVudD48bGluZWFyR3JhZGllbnQgaWQ9ImVjMGM0ZjBkLTVjOGUtNDg4Mi05NmExLTg5ZDYxODA4ZWI0OSIgeDE9IjkuMDIiIHkxPSIzLjkxIiB4Mj0iOS4wOCIgeTI9IjExLjQ5IiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI%2BPHN0b3Agb2Zmc2V0PSIwIiBzdG9wLWNvbG9yPSIjZjJmMmYyIiAvPjxzdG9wIG9mZnNldD0iMC4yMyIgc3RvcC1jb2xvcj0iI2YxZjFmMiIgc3RvcC1vcGFjaXR5PSIwLjk5IiAvPjxzdG9wIG9mZnNldD0iMC4zNyIgc3RvcC1jb2xvcj0iI2VkZWRmMSIgc3RvcC1vcGFjaXR5PSIwLjk1IiAvPjxzdG9wIG9mZnNldD0iMC40OCIgc3RvcC1jb2xvcj0iI2U3ZTVmMCIgc3RvcC1vcGFjaXR5PSIwLjg5IiAvPjxzdG9wIG9mZnNldD0iMC41OCIgc3RvcC1jb2xvcj0iI2RlZGJlZSIgc3RvcC1vcGFjaXR5PSIwLjgxIiAvPjxzdG9wIG9mZnNldD0iMC42NyIgc3RvcC1jb2xvcj0iI2QzY2VlYiIgc3RvcC1vcGFjaXR5PSIwLjciIC8%2BPHN0b3Agb2Zmc2V0PSIwLjc2IiBzdG9wLWNvbG9yPSIjYzRiZWU4IiBzdG9wLW9wYWNpdHk9IjAuNTciIC8%2BPHN0b3Agb2Zmc2V0PSIwLjg0IiBzdG9wLWNvbG9yPSIjYjRhYmU1IiBzdG9wLW9wYWNpdHk9IjAuNDEiIC8%2BPHN0b3Agb2Zmc2V0PSIwLjkyIiBzdG9wLWNvbG9yPSIjYTA5NWUxIiBzdG9wLW9wYWNpdHk9IjAuMjIiIC8%2BPHN0b3Agb2Zmc2V0PSIwLjk5IiBzdG9wLWNvbG9yPSIjOGI3ZGRjIiBzdG9wLW9wYWNpdHk9IjAuMDIiIC8%2BPHN0b3Agb2Zmc2V0PSIxIiBzdG9wLWNvbG9yPSIjODk3YmRjIiBzdG9wLW9wYWNpdHk9IjAiIC8%2BPC9saW5lYXJHcmFkaWVudD48L2RlZnM%2BPHRpdGxlPkljb24tbWFuYWdlLTMxMDwvdGl0bGU%2BPHBhdGggZD0iTTEwLjIzLDE3LjM5bC44MS0uODdWMTQuMkg3djIuMzJsLjgxLjg3QS4zMi4zMiwwLDAsMCw4LDE3LjVoMkEuMzIuMzIsMCwwLDAsMTAuMjMsMTcuMzlaIiBmaWxsPSIjY2VjZWNlIiAvPjxwYXRoIGQ9Ik05LC41QTUuODksNS44OSwwLDAsMCwzLjA5LDcuMDdjLjI3LDIuNDcsMi42MiwzLjYyLDMuMjksNi43NWEuNDkuNDksMCwwLDAsLjQ3LjM4aDQuM2EuNDkuNDksMCwwLDAsLjQ3LS4zOGMuNjctMy4xMywzLTQuMjgsMy4yOS02Ljc1QTUuODksNS44OSwwLDAsMCw5LC41Wk03LDE0LjIiIGZpbGw9InVybCgjYTdhMWM0MzEtNmM2ZC00YThmLTlhNjktOGRhNDM3ZTViMGM1KSIgLz48cGF0aCBkPSJNMTEuNDYsMy43OWExLjQsMS40LDAsMCwwLTEuMzUsMS40NFY2SDhWNS4yM0ExLjQxLDEuNDEsMCwwLDAsNi41OSwzLjc5LDEuNCwxLjQsMCwwLDAsNS4yNCw1LjIzLDEuNDEsMS40MSwwLDAsMCw2LjU5LDYuNjhoLjY0djZhLjM2LjM2LDAsMCwwLC43Miwwdi02aDIuMTZ2NmEuMzYuMzYsMCwxLDAsLjcyLDB2LTZoLjYzYTEuNCwxLjQsMCwwLDAsMS4zNS0xLjQ1QTEuNCwxLjQsMCwwLDAsMTEuNDYsMy43OVpNNy4yMyw2SDYuNTVhLjc0Ljc0LDAsMCwxLS42OC0uNzcuNzQuNzQsMCwwLDEsLjY4LS43Ny43NC43NCwwLDAsMSwuNjguNzdabTQuMjgsMGgtLjY4VjUuMTlhLjY4LjY4LDAsMSwxLDEuMzUsMEEuNzMuNzMsMCwwLDEsMTEuNTEsNloiIGZpbGw9InVybCgjZWMwYzRmMGQtNWM4ZS00ODgyLTk2YTEtODlkNjE4MDhlYjQ5KSIgLz48cG9seWdvbiBwb2ludHM9IjYuOTYgMTUuOCAxMS4wNCAxNS4wMSAxMS4wNCAxNC41NiA2Ljk2IDE1LjM2IDYuOTYgMTUuOCIgZmlsbD0iIzk5OSIgLz48cG9seWdvbiBwb2ludHM9IjExLjA0IDE2LjExIDExLjA0IDE1LjY3IDYuOTYgMTYuNDggNi45NiAxNi41MiA3LjI3IDE2Ljg2IDExLjA0IDE2LjExIiBmaWxsPSIjOTk5IiAvPjwvc3ZnPg%3D%3D)
![Azure Storage](https://img.shields.io/badge/Azure_Storage-Managed_Identity-0078D4?logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyBpZD0iZjJmMDQzNDktOGFlZS00NDEzLTg0YzktYTkwNTM2MTFiMzE5IiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxOCIgaGVpZ2h0PSIxOCIgdmlld0JveD0iMCAwIDE4IDE4Ij48ZGVmcz48bGluZWFyR3JhZGllbnQgaWQ9ImFkNGM0Zjk2LTA5YWEtNGY5MS1iYTEwLTVjYjhhZDUzMGY3NCIgeDE9IjkiIHkxPSIxNS44MyIgeDI9IjkiIHkyPSI1Ljc5IiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI%2BPHN0b3Agb2Zmc2V0PSIwIiBzdG9wLWNvbG9yPSIjYjNiM2IzIiAvPjxzdG9wIG9mZnNldD0iMC4yNiIgc3RvcC1jb2xvcj0iI2MxYzFjMSIgLz48c3RvcCBvZmZzZXQ9IjEiIHN0b3AtY29sb3I9IiNlNmU2ZTYiIC8%2BPC9saW5lYXJHcmFkaWVudD48L2RlZnM%2BPHRpdGxlPkljb24tc3RvcmFnZS04NjwvdGl0bGU%2BPHBhdGggZD0iTS41LDUuNzloMTdhMCwwLDAsMCwxLDAsMHY5LjQ4YS41Ny41NywwLDAsMS0uNTcuNTdIMS4wN2EuNTcuNTcsMCwwLDEtLjU3LS41N1Y1Ljc5QTAsMCwwLDAsMSwuNSw1Ljc5WiIgZmlsbD0idXJsKCNhZDRjNGY5Ni0wOWFhLTRmOTEtYmExMC01Y2I4YWQ1MzBmNzQpIiAvPjxwYXRoIGQ9Ik0xLjA3LDIuMTdIMTYuOTNhLjU3LjU3LDAsMCwxLC41Ny41N1Y1Ljc5YTAsMCwwLDAsMSwwLDBILjVhMCwwLDAsMCwxLDAsMFYyLjczQS41Ny41NywwLDAsMSwxLjA3LDIuMTdaIiBmaWxsPSIjMzdjMmIxIiAvPjxwYXRoIGQ9Ik0yLjgxLDYuODlIMTUuMThhLjI3LjI3LDAsMCwxLC4yNi4yN3YxLjRhLjI3LjI3LDAsMCwxLS4yNi4yN0gyLjgxYS4yNy4yNywwLDAsMS0uMjYtLjI3VjcuMTZBLjI3LjI3LDAsMCwxLDIuODEsNi44OVoiIGZpbGw9IiNmZmYiIC8%2BPHBhdGggZD0iTTIuODIsOS42OEgxNS4xOWEuMjcuMjcsMCwwLDEsLjI2LjI3djEuNDFhLjI3LjI3LDAsMCwxLS4yNi4yN0gyLjgyYS4yNy4yNywwLDAsMS0uMjYtLjI3VjEwQS4yNy4yNywwLDAsMSwyLjgyLDkuNjhaIiBmaWxsPSIjMzdjMmIxIiAvPjxwYXRoIGQ9Ik0yLjgyLDEyLjVIMTUuMTlhLjI3LjI3LDAsMCwxLC4yNi4yN3YxLjQxYS4yNy4yNywwLDAsMS0uMjYuMjdIMi44MmEuMjcuMjcsMCwwLDEtLjI2LS4yN1YxMi43N0EuMjcuMjcsMCwwLDEsMi44MiwxMi41WiIgZmlsbD0iIzI1ODI3NyIgLz48L3N2Zz4%3D)
![Bicep](https://img.shields.io/badge/Bicep-IaC-0078D4?logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyBpZD0iZTRiYTVkMmItZTZlNC00MTAxLTkzYWMtN2M2YjNkNTY4YTI3IiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxOCIgaGVpZ2h0PSIxOCIgdmlld0JveD0iMCAwIDE4IDE4Ij48ZGVmcz48bGluZWFyR3JhZGllbnQgaWQ9ImJkMmY5NDk0LWQzZmYtNDNiZC1iNTk5LTRlYzE3MGYwOTBiZCIgeDE9IjguNjMiIHkxPSIxNy41OSIgeDI9IjguNjMiIHkyPSIwLjU5IiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSI%2BPHN0b3Agb2Zmc2V0PSIwIiBzdG9wLWNvbG9yPSIjMDA3OGQ0IiAvPjxzdG9wIG9mZnNldD0iMC44MiIgc3RvcC1jb2xvcj0iIzVlYTBlZiIgLz48L2xpbmVhckdyYWRpZW50PjwvZGVmcz48dGl0bGU%2BSWNvbi1nZW5lcmFsLTk8L3RpdGxlPjxwYXRoIGQ9Ik05LjU5LjcySDIuMzZhLjU2LjU2LDAsMCwwLS41Ny41N3YxNS42YS41Ni41NiwwLDAsMCwuNTcuNTdIMTQuODlhLjU3LjU3LDAsMCwwLC41OC0uNTdWNi41NkEuNTguNTgsMCwwLDAsMTQuODksNkgxMC43M2EuNTcuNTcsMCwwLDEtLjU3LS41N1YxLjI5QS41Ni41NiwwLDAsMCw5LjU5LjcyWiIgZmlsbD0iI2ZmZiIgLz48cGF0aCBkPSJNOS4zMywxLjQ1VjUuMzdBMS40MywxLjQzLDAsMCwwLDEwLjc2LDYuOGg0djkuOTNIMi41NFYxLjQ1SDkuMzNNOS42LjU5SDIuMjZhLjU4LjU4LDAsMCwwLS41OC41OFYxN2EuNTguNTgsMCwwLDAsLjU4LjU4SDE1YS41OC41OCwwLDAsMCwuNTgtLjU4VjYuNTNBLjU4LjU4LDAsMCwwLDE1LDZIMTAuNzZhLjU4LjU4LDAsMCwxLS41OC0uNThWMS4xN0EuNTguNTgsMCwwLDAsOS42LjU5WiIgZmlsbD0idXJsKCNiZDJmOTQ5NC1kM2ZmLTQzYmQtYjU5OS00ZWMxNzBmMDkwYmQpIiAvPjxwYXRoIGQ9Ik0xNS4zNSw2LjA2LDEwLC43MlY1LjA3YTEsMSwwLDAsMCwxLDFaIiBmaWxsPSIjMDA3OGQ0IiAvPjxwYXRoIGQ9Ik01LjI3LDEwLjU2YTEuMjEsMS4yMSwwLDAsMS0uMjYuODMuODkuODksMCwwLDEtLjcyLjI5LDEsMSwwLDAsMS0uMzktLjA4VjExYS41NS41NSwwLDAsMCwuMzUuMTJjLjI3LDAsLjQxLS4yMS40MS0uNjJWOC45M2guNjFabS40Niwxdi0uNmExLjMxLDEuMzEsMCwwLDAsLjM2LjIxLDEuMjIsMS4yMiwwLDAsMCwuMzguMDdsLjIsMCwuMTUtLjA2QS4zNC4zNCwwLDAsMCw2LjksMTFhLjIuMiwwLDAsMCwwLS4xMS4yMy4yMywwLDAsMCwwLS4xNC40Ny40NywwLDAsMC0uMTMtLjExLjc3Ljc3LDAsMCwwLS4xOC0uMWwtLjIzLS4xYTEuMjUsMS4yNSwwLDAsMS0uNDctLjMyLjY4LjY4LDAsMCwxLS4xNi0uNDYuODcuODcsMCwwLDEsLjA4LS4zN0EuOTIuOTIsMCwwLDEsNiw5LjA3YTEuMTYsMS4xNiwwLDAsMSwuMzQtLjE0LDEuODQsMS44NCwwLDAsMSwuNDEsMCwyLjA1LDIuMDUsMCwwLDEsLjM3LDAsMS4zMiwxLjMyLDAsMCwxLC4zLjA4di41NmEuNTMuNTMsMCwwLDAtLjE1LS4wOC40NC40NCwwLDAsMC0uMTYtLjA2TDcsOS4zN0g2LjgxbC0uMTksMGEuNTkuNTksMCwwLDAtLjE0LjA2LjIxLjIxLDAsMCwwLS4xMi4yLjI3LjI3LDAsMCwwLDAsLjEyLjU1LjU1LDAsMCwwLC4xMS4xbC4xNi4wOS4yMS4xYTIsMiwwLDAsMSwuMjkuMTQsMS4yOCwxLjI4LDAsMCwxLC4yMi4xNy43NC43NCwwLDAsMSwuMTQuMjIuODguODgsMCwwLDEsLjA1LjI5Ljc0Ljc0LDAsMCwxLS4wOS4zOC42OC42OCwwLDAsMS0uMjMuMjUuOTMuOTMsMCwwLDEtLjM0LjEzLDEuODQsMS44NCwwLDAsMS0uNDEsMCwyLjcyLDIuNzIsMCwwLDEtLjQyLDBBMS4xMSwxLjExLDAsMCwxLDUuNzMsMTEuNTJabTMuNDIuMTZhMS4yNiwxLjI2LDAsMCwxLS45NC0uMzgsMS4zOCwxLjM4LDAsMCwxLS4zNy0xLDEuNDgsMS40OCwwLDAsMSwuMzctMSwxLjMxLDEuMzEsMCwwLDEsMS0uNCwxLjI0LDEuMjQsMCwwLDEsLjk0LjM4LDEuNCwxLjQsMCwwLDEsLjM1LDEsMS40MywxLjQzLDAsMCwxLS4zNywxQTEuMjQsMS4yNCwwLDAsMSw5LjE1LDExLjY4Wm0wLTIuMjdhLjYxLjYxLDAsMCwwLS41MS4yNCwxLjE2LDEuMTYsMCwwLDAsMCwxLjI3LjYyLjYyLDAsMCwwLC40OS4yMy42MS42MSwwLDAsMCwuNS0uMjMsMSwxLDAsMCwwLC4xOC0uNjMsMSwxLDAsMCwwLS4xNy0uNjVBLjYxLjYxLDAsMCwwLDkuMTgsOS40MVptNC4xOCwyLjIyaC0uNjJsLTEuMTEtMS43YTIuMzQsMi4zNCwwLDAsMS0uMTMtLjIyaDBjMCwuMDksMCwuMjQsMCwuNDR2MS40OGgtLjU3VjguOTNoLjY2bDEuMDYsMS42NC4xNC4yMmgwYTIuNTEsMi41MSwwLDAsMSwwLS4zN1Y4LjkzaC41OFoiIGZpbGw9IiM1ZWEwZWYiIC8%2BPC9zdmc%2B)
![KQL](https://img.shields.io/badge/KQL-Log_Queries-0078D4)

Azure Functions (.NET 10 isolated worker) demo project deployed with [Azure Developer CLI](https://learn.microsoft.com/azure/developer/azure-developer-cli/) (`azd`).

## Run locally

```bash
dotnet run
```

When the host starts, **check the terminal output** — it lists every function URL and the port the host is listening on (often **7071**, not necessarily the value in `launchSettings.json`). All HTTP routes are prefixed with `/api`.

## Interactive API explorer (Swagger)

This project serves a [Swagger UI](https://swagger.io/tools/swagger-ui/) at **`/api/swagger`**, wired up in `Program.cs` via [AzureFunctions.Worker.Extensions.Swashbuckle](https://www.nuget.org/packages/AzureFunctions.Worker.Extensions.Swashbuckle). Use it to browse HTTP endpoints, fill in parameters, send requests, and read responses without writing `curl` commands.

| Environment | URL |
|-------------|-----|
| Local       | `http://localhost:<port>/api/swagger` |
| Azure       | `https://<function-app-name>.azurewebsites.net/api/swagger` |

Replace `<port>` with the port printed in the terminal when you run `dotnet run` (see the `Functions:` block). After `azd deploy`, use your Function App hostname from `azd show`.

### How to use it

1. Start the host with `dotnet run` and leave that terminal visible — **function invocations and errors are logged there**.
2. Open `/api/swagger` in your browser.
3. Expand an operation (e.g. `GET /api/greet` or `POST /api/todos`).
4. Fill in inputs:
   - **GET** — use the **Parameters** section for query strings (`name`, `lang`, `filter`, …).
   - **POST** — use the **Request body** JSON editor (`title` items in an array, `message`, …). For **SaveToBlob** and **UploadAndProcess**, use **Choose File** to pick a file (`multipart/form-data`).
5. Click **Execute** and inspect the response body and status code below.
6. Switch back to the `dotnet run` terminal to confirm the function ran (`Executing 'Functions.GreetUser'`, log lines, errors, etc.).

Timer and queue triggers (`Heartbeat`, `ProcessQueueMessage`, `CleanupOldBlobs`) do not appear in Swagger — they have no HTTP endpoint. `HttpExample` is hidden from the docs (`[ApiExplorerSettings(IgnoreApi = true)]`) but still works at `/api/HttpExample`.

OpenAPI JSON is also available at `/api/swagger/v1/swagger.json` if you want the raw spec.

## Run in Docker

Build the image and run it (port 8080 → container port 80):

```bash
docker build -t functions-demo .
docker run --rm --name functions-demo -p 8080:80 functions-demo
```

Endpoints are then at `http://localhost:8080/api/...`. HTTP-only functions (`greet`, `todos`, `echo-headers`) work out of the box; the storage-backed functions (`save`, queue trigger) need `AzureWebJobsStorage` passed as an environment variable, e.g. pointing at [Azurite](https://learn.microsoft.com/azure/storage/common/storage-use-azurite):

```bash
docker run --rm --name functions-demo -p 8080:80 \
  -e AzureWebJobsStorage="<connection-string>" \
  functions-demo
```

### Stop the container

Press `Ctrl+C` in the terminal running the container, or from another terminal:

```bash
docker stop functions-demo
```

`--rm` in the run commands above deletes the container automatically when it stops, so nothing is left behind in `docker ps -a`.

### Push to Azure Container Registry

```bash
az acr login --name <registry-name>
docker tag functions-demo <registry-url>/functions-demo:latest
docker push <registry-url>/functions-demo:latest
```

Alternatively, build on ACR's infrastructure (natively amd64, no local emulation) and push in one step:

```bash
az acr build --registry <registry-name> --image functions-demo:latest .
```

### Notes

- The runtime stage in the `Dockerfile` is pinned to `linux/amd64` because the Azure Functions base image has no arm64 variant. On Apple Silicon it runs under Rosetta emulation (you'll see a harmless platform-mismatch warning).
- The container is for local development and registry practice only. The `azd deploy` flow below deploys to a **Flex Consumption** Function App, which uses package deployment and does not run custom containers. To deploy this image, you'd need a Dedicated/Elastic Premium plan Function App or Azure Container Apps instead.

## Functions

<details>
<summary><strong>GreetUser</strong> — <code>GET /api/greet</code></summary>

Returns a JSON greeting for the given `name` query parameter.

| Query param | Required | Description |
|-------------|----------|-------------|
| `name`      | Yes      | Name to greet |
| `lang`      | No       | Language (`en`, `fr`, `es`, `de`). Defaults to `en`. In Swagger UI, pick from the dropdown. |

### Examples

```bash
# Basic greeting (English)
curl "http://localhost:7137/api/greet?name=Emandleni"
# {"message":"Hello, Emandleni!"}

# French greeting
curl "http://localhost:7137/api/greet?name=Emandleni&lang=fr"
# {"message":"Bonjour, Emandleni!"}

# Missing name (400 Bad Request)
curl "http://localhost:7137/api/greet"
# {"error":"Query parameter 'name' is required."}
```

</details>

<details>
<summary><strong>CreateTodo</strong> — <code>POST /api/todos</code></summary>

Creates one or more todos from a JSON array and returns the created items.

| Field (per item) | Required | Description |
|------------------|----------|-------------|
| `title`          | Yes      | Todo title (max 200 characters) |

### Examples

```bash
# Create a single todo (201 Created)
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d '[{"title":"Learn Azure Functions"}]'
# {"todos":[{"id":"...","title":"Learn Azure Functions","createdAt":"..."}]}

# Create multiple todos in one request
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d '[{"title":"Buy milk"},{"title":"Write docs"}]'
# {"todos":[{"id":"...","title":"Buy milk",...},{"id":"...","title":"Write docs",...}]}

# Empty array (400 Bad Request)
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d '[]'
# {"error":"Request body must be a non-empty JSON array of todos."}
```

</details>

<details>
<summary><strong>EchoHeaders</strong> — <code>GET /api/echo-headers</code></summary>

Returns the request headers as a JSON map, sorted alphabetically. Useful for seeing what proxies and Azure's front-end add to a request (`X-Forwarded-For`, `X-ARR-LOG-ID`, etc.).

| Query param | Required | Description |
|-------------|----------|-------------|
| `filter`    | No       | Set to `interesting` to only return notable headers (`User-Agent`, `Host`, `X-Forwarded-*`, `CLIENT-IP`, ...). Defaults to all headers. |

### Examples

```bash
# Echo all request headers
curl "http://localhost:7137/api/echo-headers"
# {"count":3,"headers":{"Accept":"*/*","Host":"localhost:7137","User-Agent":"curl/8.7.1"}}

# Only the "interesting" headers
curl "http://localhost:7137/api/echo-headers?filter=interesting"
# {"count":3,"headers":{"Accept":"*/*","Host":"localhost:7137","User-Agent":"curl/8.7.1"}}

# Send a custom header and see it echoed back
curl -H "X-Demo: hello" "http://localhost:7137/api/echo-headers"
# {"count":4,"headers":{...,"X-Demo":"hello"}}
```

Locally the two variants look similar; the difference shows up in Azure, where the proxy layer adds many extra headers.

</details>

<details>
<summary><strong>ProcessQueueMessage</strong> — queue trigger on <code>demo-queue</code></summary>

Triggered whenever a message lands on the `demo-queue` storage queue. Logs the message body along with its id, dequeue count, and insertion time. There is no HTTP endpoint; you drive it by enqueuing messages.

The queue lives in the same storage account the Functions host uses (`AzureWebJobsStorage`), and access is via managed identity, so no connection string is needed.

### Examples

```bash
# Enqueue a message (uses your azd storage account and Azure AD auth)
az storage message put \
  --queue-name demo-queue \
  --content "hello from the queue" \
  --account-name <storage-account-name> \
  --auth-mode login

# Watch the function pick it up
func azure functionapp logstream <function-app-name>
# ...Processing queue message <id> (dequeue count: 1, inserted: ...): hello from the queue
```

Locally, `dotnet run` connects to the same storage account through `local.settings.json` (`AzureWebJobsStorage__queueServiceUri`), so enqueuing a message triggers the function on your machine too. Your Azure CLI login needs the **Storage Queue Data Contributor** role on the account.

</details>

<details>
<summary><strong>SaveToBlob</strong> — <code>POST /api/save</code></summary>

Accepts a file upload (`multipart/form-data`) and writes it to the `uploads` blob container via `IBlobUploadService` (managed identity). Returns `201 Created` with blob metadata. The blob name comes from the uploaded file's name; MIME type is taken from the file when the client sends one.

| Form field | Required | Description |
|------------|----------|-------------|
| `file`     | Yes      | File to store (max 8 MB). In Swagger UI, use **Choose File**. |

Uses the same storage account and managed identity as the queue functions (`AzureWebJobsStorage__blobServiceUri`).

### Examples

```bash
# Save a text file (201 Created)
curl -i -X POST "http://localhost:7137/api/save" \
  -F "file=@notes.txt"
# {"filename":"notes.txt","blobUri":"uploads/notes.txt","contentType":"text/plain","sizeBytes":28}

# Missing file (400 Bad Request)
curl -i -X POST "http://localhost:7137/api/save"
# {"error":"Field 'file' is required."}
```

</details>

<details>
<summary><strong>CleanupOldBlobs</strong> — timer trigger, Mondays at 04:00 UTC</summary>

Maintenance job that sweeps the `uploads` container every Monday at 04:00 UTC and deletes blobs last modified more than **2 days** ago. There is no HTTP endpoint; it demonstrates combining a timer trigger with a `BlobContainerClient` input binding (the Blob SDK) for housekeeping work.

Uses the same storage account and managed identity as the other storage functions (`AzureWebJobsStorage__blobServiceUri`), so no connection string is needed. Failed deletes are logged and skipped so one bad blob doesn't abort the sweep.

### Examples

```bash
# Watch the cleanup run in Azure
func azure functionapp logstream <function-app-name>
# ...CleanupOldBlobs started at ... (container: uploads, cutoff: ...)
# ...Deleted blob notes.txt (last modified ...)
# ...CleanupOldBlobs finished: scanned 5 blob(s), deleted 2 older than 2 days.
```

To watch it locally without waiting for Monday 04:00 UTC, temporarily change the NCRONTAB expression in `functions/CleanupOldBlobs.cs` (e.g. to `*/30 * * * * *`) and run `dotnet run`.

</details>

<details>
<summary><strong>GetWeather</strong> — <code>GET /api/weather/{city}</code></summary>

Returns a weather report for the given city via [WeatherAPI.com](https://www.weatherapi.com/). Demonstrates constructor injection of a custom service: `IWeatherService` is registered in `Program.cs` and injected into the function alongside the logger.

When `WeatherApi__ApiKey` is configured, `WeatherApiWeatherService` calls the real API. Without a key, the app falls back to `FakeWeatherService` (deterministic fake data).

| Route param | Required | Description |
|-------------|----------|-------------|
| `city`      | Yes      | City name (part of the URL path). Use `Paris,FR` for disambiguation. |

### Configuration

**Local development** — paste your API key into `local.settings.json` (gitignored):

```json
"WeatherApi__ApiKey": "your-key-here"
```

**Azure** — set the same app setting on the Function App after deploy:

```bash
az functionapp config appsettings set \
  --name <function-app-name> \
  --resource-group <resource-group-name> \
  --settings "WeatherApi__ApiKey=your-key-here"
```

Alternatively, use .NET user secrets (also gitignored):

```bash
dotnet user-secrets set "WeatherApi:ApiKey" "your-key-here"
```

### Examples

```bash
# Get the weather for a city (200 OK)
curl -i "http://localhost:7137/api/weather/johannesburg" | jq .
# {
#  "city": "Johannesburg",
#  "temperatureC": 9,
#  "condition": "Severe smog",
#  "observedAt": "2026-07-05T05:30:00+00:00",
#  "temperatureF": 48
# }

# Disambiguate common city names
curl -s "http://localhost:7137/api/weather/Paris,FR" | jq .

# Unknown city (404)
curl -i "http://localhost:7137/api/weather/NotARealCity12345"
```

</details>

<details>
<summary><strong>UploadAndProcess</strong> — <code>POST /api/upload</code> (capstone)</summary>

Accepts the same `multipart/form-data` file upload as `SaveToBlob` via `IBlobUploadService`, then enqueues an upload job on `demo-queue` using a queue output binding. Returns `202 Accepted` immediately; `ProcessQueueMessage` picks up the job asynchronously.

| Form field | Required | Description |
|------------|----------|-------------|
| `file`     | Yes      | File to store (max 8 MB). In Swagger UI, use **Choose File**. |

### Examples

```bash
# Upload a file and enqueue processing (202 Accepted)
curl -i -X POST "http://localhost:7137/api/upload" \
  -F "file=@report.txt"
# {"jobId":"...","queue":"demo-queue","blob":{"filename":"report.txt","blobUri":"uploads/report.txt","contentType":"text/plain","sizeBytes":17}}

# Watch ProcessQueueMessage handle the job in the dotnet run terminal:
# ...Processing upload job ... for report.txt at uploads/report.txt ...
```

</details>

### Deploy and test in Azure

```bash
azd deploy
```

After deploy, replace `localhost:<port>` in the examples below with your Function App URL from `azd show`. Swagger UI is at `https://<function-app-name>.azurewebsites.net/api/swagger`.

## Monitoring (KQL)

Telemetry flows through OpenTelemetry to Application Insights (ingestion lags ~2–5 minutes). Run these in **Application Insights → Logs** in the portal.

Find recent heartbeats:

```kusto
traces
| where message startswith "Heartbeat at"
| project timestamp, message, severityLevel, operation_Name, cloud_RoleName
| order by timestamp desc
```

All logs from the Heartbeat function, including the "next scheduled" lines:

```kusto
traces
| where customDimensions.CategoryName == "My.Function.Heartbeat"
   or operation_Name == "Heartbeat"
| order by timestamp desc
```

Chart execution counts to verify the timer schedule is firing at the expected rate:

```kusto
traces
| where message startswith "Heartbeat at"
| summarize executions = count() by bin(timestamp, 10m)
| render timechart
```

> Querying the Log Analytics workspace directly instead of the Application Insights resource? Use the `AppTraces` table with `TimeGenerated` / `Message` in place of `traces` / `timestamp` / `message`.

## Troubleshooting

### Clean slate checklist

When a build, `dotnet run`, or `azd deploy` feels stuck:

```bash
# Stop leftover local Functions hosts and builds
pkill -f 'dotnet run' 2>/dev/null
pkill -f 'func host' 2>/dev/null
pkill -f 'dotnet publish' 2>/dev/null

# Release MSBuild / Roslyn compiler locks from interrupted builds
dotnet build-server shutdown

# Confirm default Functions ports are free (7071, 7137)
lsof -i :7071 -i :7137

# List any remaining deploy/build processes
pgrep -fl 'azd|func |dotnet|MSBuild|VBCSCompiler'

# Clean artifacts and verify Release build completes
rm -rf bin obj && dotnet build -c Release

# Redeploy to Azure
azd deploy
```

Run the `pkill` / `build-server shutdown` lines after any interrupted build or deploy. If `pgrep` or `lsof` still show processes or ports in use, kill those PIDs before retrying.

### `azd deploy` hangs forever at "Packaging (Publishing .NET project)"

**Symptoms**

- `azd deploy` sits on `api: Packaging (Publishing .NET project)` indefinitely with no progress.
- Azure resources look provisioned, but no functions appear in the Function App.
- A plain `dotnet build` also hangs (stuck on `CoreCompile` for minutes).

**Cause**

The packaging phase is just `dotnet publish -c Release` running locally, it never reaches Azure. If a previous `dotnet run`, `dotnet publish`, or `azd deploy` was interrupted (Ctrl+C) or left running, its leftover processes keep holding the shared Roslyn compiler (`VBCSCompiler`) and MSBuild locks. Every new build then deadlocks waiting on them. Because nothing was ever published, no code reaches the Function App and no functions show up, that part is a downstream effect, not a separate problem.

**Fix**

1. Cancel the hung deploy with `Ctrl+C`.
2. Run the [clean slate checklist](#clean-slate-checklist) above.
3. Re-run the deploy (add `--debug` if you want to see what azd is doing behind the spinner):

```bash
azd deploy
```

**Prevention**

- Never run two builds/deploys of this project at the same time.
- After interrupting any `dotnet run` / `azd deploy` with Ctrl+C, run `dotnet build-server shutdown` before starting the next one.
- If a function is missing after deploy, check `git status` first.

## Project structure

```
ms-docs-azure-functions-demo/
├── Program.cs
├── functions/
│   ├── CleanupOldBlobs.cs
│   ├── CreateTodo.cs
│   ├── EchoHeaders.cs
│   ├── EnqueueMessage.cs
│   ├── GetWeather.cs
│   ├── Greetuser.cs
│   ├── Heartbeat.cs
│   ├── HttpExample.cs
│   ├── ProcessQueueMessage.cs
│   ├── SaveToBlob.cs
│   └── UploadAndProcess.cs
├── models/
│   ├── QueueMessage.cs
│   ├── SaveBlob.cs
│   ├── Todo.cs
│   ├── UploadJob.cs
│   └── WeatherReport.cs
├── services/
│   ├── BlobUploadService.cs
│   ├── BlobUploadStorage.cs
│   ├── BlobUploadValidation.cs
│   ├── IBlobUploadService.cs
│   ├── IWeatherService.cs
│   ├── FakeWeatherService.cs
│   ├── WeatherApiOptions.cs
│   └── WeatherApiWeatherService.cs
├── Properties/
│   └── launchSettings.json
├── infra/
│   ├── main.bicep
│   ├── main.parameters.json
│   ├── abbreviations.json
│   └── app/
│       ├── api.bicep
│       ├── rbac.bicep
│       ├── storage-PrivateEndpoint.bicep
│       └── vnet.bicep
├── .github/
│   └── workflows/
│       └── azure-dev.yml
├── azure.yaml
├── Dockerfile
├── .dockerignore
├── host.json
├── local.settings.json
└── ms-docs-azure-functions-demo.csproj
```
