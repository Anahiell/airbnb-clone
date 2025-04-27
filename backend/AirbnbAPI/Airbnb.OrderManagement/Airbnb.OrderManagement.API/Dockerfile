# Используем образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию
WORKDIR /app

ARG BUILD_CONFIG=Production
ENV ASPNETCORE_ENVIRONMENT=${BUILD_CONFIG}

# Копируем .sln файл
COPY AirbnbAPI.sln ./AirbnbAPI.sln

# Копируем все необходимые .csproj файлы
COPY ["Airbnb.SharedKernel/Airbnb.Domain/Airbnb.Domain.csproj", "../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/Airbnb.Application.csproj", "../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/Airbnb.Cache.csproj", "../Airbnb.SharedKernel/Airbnb.Cache/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/Airbnb.MongoRepository.csproj", "../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.API/Airbnb.OrderManagement.API.csproj", "Airbnb.OrderManagement.API/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Application/Airbnb.OrderManagement.Application.csproj", "Airbnb.OrderManagement.Application/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Infrastructure/Airbnb.OrderManagement.Infrastructure.csproj", "Airbnb.OrderManagement.Infrastructure/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Domain/Airbnb.OrderManagement.Domain.csproj", "Airbnb.OrderManagement.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.OrderManagement.API/Airbnb.OrderManagement.API.csproj"

# Копируем весь исходный код
COPY ["Airbnb.SharedKernel/Airbnb.Domain/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Cache/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/", "./Airbnb/../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.API/.", "./Airbnb/Airbnb.OrderManagement.API/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Application/", "./Airbnb/Airbnb.OrderManagement.Application/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Infrastructure/.", "./Airbnb/Airbnb.OrderManagement.Infrastructure/"]
COPY ["Airbnb.OrderManagement/Airbnb.OrderManagement.Domain/", "./Airbnb/Airbnb.OrderManagement.Domain/"]

# Сборка проекта
WORKDIR "/app/Airbnb/Airbnb.OrderManagement.API"
RUN ls -la /app
RUN dotnet publish -c ${BUILD_CONFIG} -o /out

# Создание финального образа для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Указываем порт
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.OrderManagement.API.dll"]
