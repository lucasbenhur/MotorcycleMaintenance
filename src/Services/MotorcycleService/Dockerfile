FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/MotorcycleService/MotorcycleService.csproj", "src/Services/MotorcycleService/"]
RUN dotnet restore "./src/Services/MotorcycleService/MotorcycleService.csproj"
COPY . .
WORKDIR "/src/src/Services/MotorcycleService"
RUN dotnet build "./MotorcycleService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./MotorcycleService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleService.dll"]