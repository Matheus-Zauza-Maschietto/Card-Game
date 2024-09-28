FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["app/app.csproj", "app/"]
COPY ["app.Tests/app.Tests.csproj", "app.Tests/"]
COPY ["app.Integration/app.Integration.csproj", "app.Integration/"]

RUN dotnet restore "app/app.csproj"

COPY . .
WORKDIR "/src/app"
RUN dotnet build "app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "app.dll"]