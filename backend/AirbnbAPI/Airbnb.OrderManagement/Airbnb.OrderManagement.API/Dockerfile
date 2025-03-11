FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.API/Airbnb.OrderManagement.API.csproj", "Airbnb.OrderManagement/Airbnb.OrderManagement.API/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Infrastructure/Airbnb.OrderManagement.Infrastructure.csproj", "Airbnb.OrderManagement/Airbnb.OrderManagement.Infrastructure/"]
RUN dotnet restore "Airbnb.OrderManagement/Airbnb.OrderManagement.API/Airbnb.OrderManagement.API.csproj"
COPY . .
WORKDIR "/src/Airbnb.OrderManagement/Airbnb.OrderManagement.API"
RUN dotnet build "Airbnb.OrderManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Airbnb.OrderManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Airbnb.OrderManagement.API.dll"]
