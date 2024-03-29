# .NET REST API Demo

> Demo to showcase best practices for developing REST APIs with .NET, 
> clean code examples and good approaches to architecture.

[![Build](https://img.shields.io/github/actions/workflow/status/ef4203/demo-api/dotnet.yml)](https://github.com/ef4203/demo-api/actions/workflows/dotnet.yml)
[![DotNet](https://img.shields.io/badge/.NET-7.0-blue)](https://dotnet.microsoft.com/download/dotnet/7.0)
[![License](https://img.shields.io/github/license/ef4203/demo-api)](https://github.com/ef4203/demo-api/blob/master/LICENSE)

This project contains a complete REST API base, which can be extended 
with further functionality. It showcases the hexagonal architecture,
described by Martin Fowler.

## Building the project

### Using Rider or Visual Studio
Download [Rider](https://www.jetbrains.com/rider/) or 
[Visual Studio](https://visualstudio.microsoft.com) and open the projects 
`.sln` file. All launch profiles should be detected automatically and you 
can directly start or debug the application.

### Using the CLI
If you're not using an IDE you can download and install the latest
[.NET SDK](https://dotnet.microsoft.com/download/dotnet/7.0).

```
dotnet restore
dotnet run --project Contonso.API.Web
```

## License
Permission to modify and redistribute is granted, free of charge, under the
MIT license. See [LICENSE](LICENSE) file for the full license. This license
is applicable for all files in this repository, unless a different license
is explicitly specified.

## Contributing
All contributions are welcome, if you have ideas, improvements or suggestions
feel free to open an issue or a pull request on GitHub.