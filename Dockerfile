# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar y restaurar dependencias
COPY *.slnx .
COPY src/WargearTracker.Core/*.csproj src/WargearTracker.Core/
COPY src/WargearTracker.Data/*.csproj src/WargearTracker.Data/
COPY src/WargearTracker.Api/*.csproj src/WargearTracker.Api/
COPY tests/WargearTracker.Tests/*.csproj tests/WargearTracker.Tests/
RUN dotnet restore

# Copiar el resto y compilar
COPY src/ src/
RUN dotnet publish src/WargearTracker.Api -c Release -o /app/publish

# Fase de runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WargearTracker.Api.dll"]