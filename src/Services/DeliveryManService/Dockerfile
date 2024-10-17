FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/DeliveryManService/DeliveryManService.csproj", "src/Services/DeliveryManService/"]
RUN dotnet restore "./src/Services/DeliveryManService/DeliveryManService.csproj"
COPY . .
WORKDIR "/src/src/Services/DeliveryManService"
RUN dotnet build "./DeliveryManService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./DeliveryManService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryManService.dll"]