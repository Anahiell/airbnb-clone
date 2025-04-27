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
COPY ["Airbnb.SharedKernel/Airbnb.Infrastructure/Airbnb.Infrastructure.csproj", "../Airbnb.SharedKernel/Airbnb.Infrastructure/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/Airbnb.MongoRepository.csproj", "../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.API/Airbnb.PictureManagement.API.csproj", "Airbnb.PictureManagement.API/"]
COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.Application/Airbnb.PictureManagement.Application.csproj", "Airbnb.PictureManagement.Application/"]
COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.Infrastructure/Airbnb.PictureManagement.Infrastructure.csproj", "Airbnb.PictureManagement.Infrastructure/"]
COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.Domain/Airbnb.PictureManagement.Domain.csproj", "Airbnb.PictureManagement.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.PictureManagement.API/Airbnb.PictureManagement.API.csproj"

# Копируем весь исходный код
COPY ["Airbnb.SharedKernel/Airbnb.Domain/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Cache/"]
COPY ["Airbnb.SharedKernel/Airbnb.Infrastructure/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Infrastructure/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/", "./Airbnb/../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.API/", "./Airbnb/Airbnb.PictureManagement.API/"]
COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.Application/", "./Airbnb/Airbnb.PictureManagement.Application/"]
COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.Infrastructure/", "./Airbnb/Airbnb.PictureManagement.Infrastructure/"]
COPY ["Airbnb.PictureManagement/Airbnb.PictureManagement.Domain/", "./Airbnb/Airbnb.PictureManagement.Domain/"]

# Сборка проекта
WORKDIR "/app/Airbnb/Airbnb.PictureManagement.API"
RUN ls -la /app
RUN dotnet publish -c ${BUILD_CONFIG} -o /out /p:UseAppHost=false

# Создание финального образа для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Указываем порт
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.PictureManagement.API.dll"]
