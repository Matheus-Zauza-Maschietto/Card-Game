FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "app.csproj"


RUN dotnet publish app.csproj -c Release -o /output
RUN ls /output
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build ./output .
RUN ls /app
EXPOSE 5000

ENTRYPOINT ["dotnet", "app.dll"]