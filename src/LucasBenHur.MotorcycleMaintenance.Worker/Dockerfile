FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/LucasBenHur.MotorcycleMaintenance.Worker/LucasBenHur.MotorcycleMaintenance.Worker.csproj", "src/LucasBenHur.MotorcycleMaintenance.Worker/"]
RUN dotnet restore "./src/LucasBenHur.MotorcycleMaintenance.Worker/LucasBenHur.MotorcycleMaintenance.Worker.csproj"
COPY . .
WORKDIR "/src/src/LucasBenHur.MotorcycleMaintenance.Worker"
RUN dotnet build "./LucasBenHur.MotorcycleMaintenance.Worker.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LucasBenHur.MotorcycleMaintenance.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LucasBenHur.MotorcycleMaintenance.Worker.dll"]