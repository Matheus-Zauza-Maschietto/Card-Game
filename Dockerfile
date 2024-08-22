# Use a imagem oficial do .NET como imagem base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Use a imagem oficial do SDK do .NET como imagem de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie o arquivo de solução e arquivos de projeto
COPY ["app/app.csproj", "app/"]
COPY ["app.Tests/app.Tests.csproj", "app.Tests/"]
COPY ["app.Integration/app.Integration.csproj", "app.Integration/"]

# Restaure as dependências de todos os projetos
RUN dotnet restore "app/app.csproj"

# Copie o restante dos arquivos e compile a aplicação
COPY . .
WORKDIR "/src/app"
RUN dotnet build "app.csproj" -c Release -o /app/build

# Publique a aplicação
FROM build AS publish
RUN dotnet publish "app.csproj" -c Release -o /app/publish

# Use a imagem base e copie a publicação da build
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Defina o comando de entrada
ENTRYPOINT ["dotnet", "app.dll"]