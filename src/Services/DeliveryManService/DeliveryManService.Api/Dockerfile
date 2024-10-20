FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/DeliveryManService/DeliveryManService.Api/DeliveryManService.Api.csproj", "src/Services/DeliveryManService/DeliveryManService.Api/"]
RUN dotnet restore "./src/Services/DeliveryManService/DeliveryManService.Api/DeliveryManService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/DeliveryManService/DeliveryManService.Api"
RUN dotnet build "./DeliveryManService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./DeliveryManService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryManService.Api.dll"]