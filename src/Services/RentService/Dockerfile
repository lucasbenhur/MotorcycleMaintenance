FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/RentService/RentService.csproj", "src/Services/RentService/"]
RUN dotnet restore "./src/Services/RentService/RentService.csproj"
COPY . .
WORKDIR "/src/src/Services/RentService"
RUN dotnet build "./RentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./RentService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentService.dll"]