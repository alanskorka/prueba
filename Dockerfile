# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo de solución y el código fuente
COPY SimuladorDeObjetos.sln ./
COPY src/ ./src/
RUN dotnet restore ./src/SimuladorDeObjetos.WebApi/SimuladorDeObjetos.WebApi.csproj

# Publica la app
RUN dotnet publish ./src/SimuladorDeObjetos.WebApi/SimuladorDeObjetos.WebApi.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "SimuladorDeObjetos.WebApi.dll"] 