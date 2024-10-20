FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/RentService/RentService.Api/RentService.Api.csproj", "src/Services/RentService/RentService.Api/"]
RUN dotnet restore "./src/Services/RentService/RentService.Api/RentService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/RentService/RentService.Api"
RUN dotnet build "./RentService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./RentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentService.Api.dll"]