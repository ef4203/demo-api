# .NET REST API Demo

> Demo to showcase best practices for developing REST APIs with .NET, clean code examples and good approaches to architecture.

[![Build](https://api.travis-ci.org/ef4203/serve.svg?branch=master)](https://travis-ci.org/ef4203/demo-api/builds)
[![DotNet](https://img.shields.io/badge/.NET-5.0-blue)](https://dotnet.microsoft.com/download/dotnet/5.0)
[![License](https://img.shields.io/github/license/ef4203/demo-api)](https://github.com/ef4203/demo-api/blob/master/LICENSE)
[![Lines of Code](https://img.shields.io/tokei/lines/github/ef4203/api-demo)](https://github.com/ef4203/demo-api)

This project contains a complete REST API base, which can be extended with further functionality.

## Building the project

On Microsoft Windows, download [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) and
open the `.sln` file. The Community Edition of Visual Studio is free of charge.

On Mac OS, download [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/) and open
the `.sln` file.

On Linux based System, download the [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
and install it like instructed. The project is then compiled and run with:

```
dotnet restore
dotnet publish
dotnet run --project Contonso.API.Web
```
