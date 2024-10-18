FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/DeliveryMenService/DeliveryMenService.csproj", "src/Services/DeliveryMenService/"]
RUN dotnet restore "./src/Services/DeliveryMenService/DeliveryMenService.csproj"
COPY . .
WORKDIR "/src/src/Services/DeliveryMenService"
RUN dotnet build "./DeliveryMenService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./DeliveryMenService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryMenService.dll"]