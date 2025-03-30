# Используем образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию
WORKDIR /app

ARG BUILD_CONFIG=Production
ENV ASPNETCORE_ENVIRONMENT=${BUILD_CONFIG}

# Копируем .sln файл
COPY AirbnbAPI.sln ./AirbnbAPI.sln

# Копируем все необходимые .csproj файлы в соответствующие директории
COPY ["Airbnb.SharedKernel/Airbnb.Domain/Airbnb.Domain.csproj", "../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/Airbnb.Application.csproj", "../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/Airbnb.Cache.csproj", "../Airbnb.SharedKernel/Airbnb.Cache/"]


COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/Airbnb.MongoRepository.csproj", "../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.API/Airbnb.ProductManagement.API.csproj", "Airbnb.ProductManagement.API/"]
COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.Application/Airbnb.ProductManagement.Application.csproj", "Airbnb.ProductManagement.Application/"]
COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.Infrastructure/Airbnb.ProductManagement.Infrastructure.csproj", "Airbnb.ProductManagement.Infrastructure/"]
COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.Domain/Airbnb.ProductManagement.Domain.csproj", "Airbnb.ProductManagement.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.ProductManagement.API/Airbnb.ProductManagement.API.csproj"

# Копируем весь исходный код
COPY ["Airbnb.SharedKernel/Airbnb.Domain/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Cache/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/", "./Airbnb/../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.API/.", "./Airbnb/Airbnb.ProductManagement.API/"]
COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.Application/", "./Airbnb/Airbnb.ProductManagement.Application/"]
COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.Infrastructure/.", "./Airbnb/Airbnb.ProductManagement.Infrastructure/"]
COPY ["Airbnb.ProductManagement/Airbnb.ProductManagement.Domain/", "./Airbnb/Airbnb.ProductManagement.Domain/"]


# Сборка проекта
WORKDIR "/app/Airbnb/Airbnb.ProductManagement.API"
RUN ls -la /app
RUN dotnet publish -c ${BUILD_CONFIG} -o /out

# Создание финального образа для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Указываем порт
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.ProductManagement.API.dll"]