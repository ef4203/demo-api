FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contonso.SampleApi.Web/Contonso.SampleApi.Web.csproj", "Contonso.SampleApi.Web/"]
RUN dotnet restore "Contonso.SampleApi.Web/Contonso.SampleApi.Web.csproj"
COPY . .
WORKDIR "/src/Contonso.SampleApi.Web"
RUN dotnet build "Contonso.SampleApi.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contonso.SampleApi.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contonso.SampleApi.Web.dll"]
