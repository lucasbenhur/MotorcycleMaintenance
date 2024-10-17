FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/LucasBenHur.MotorcycleMaintenance.Api/LucasBenHur.MotorcycleMaintenance.Api.csproj", "src/LucasBenHur.MotorcycleMaintenance.Api/"]
RUN dotnet restore "./src/LucasBenHur.MotorcycleMaintenance.Api/LucasBenHur.MotorcycleMaintenance.Api.csproj"
COPY . .
WORKDIR "/src/src/LucasBenHur.MotorcycleMaintenance.Api"
RUN dotnet build "./LucasBenHur.MotorcycleMaintenance.Api.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LucasBenHur.MotorcycleMaintenance.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LucasBenHur.MotorcycleMaintenance.Api.dll"]