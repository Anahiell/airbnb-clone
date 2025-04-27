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

COPY ["Airbnb.TagManagement/Airbnb.TagManagement.API/Airbnb.TagManagement.API.csproj", "Airbnb.TagManagement.API/"]
COPY ["Airbnb.TagManagement/Airbnb.TagManagement.Application/Airbnb.TagManagement.Application.csproj", "Airbnb.TagManagement.Application/"]
COPY ["Airbnb.TagManagement/Airbnb.TagManagement.Infrastructure/Airbnb.TagManagement.Infrastructure.csproj", "Airbnb.TagManagement.Infrastructure/"]
COPY ["Airbnb.TagManagement/Airbnb.TagManagement.Domain/Airbnb.TagManagement.Domain.csproj", "Airbnb.TagManagement.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.TagManagement.API/Airbnb.TagManagement.API.csproj"

# Копируем весь исходный код
COPY ["Airbnb.SharedKernel/Airbnb.Domain/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Cache/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/", "./Airbnb/../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.TagManagement/Airbnb.TagManagement.API/", "./Airbnb/Airbnb.TagManagement.API/"]
COPY ["Airbnb.TagManagement/Airbnb.TagManagement.Application/", "./Airbnb/Airbnb.TagManagement.Application/"]
COPY ["Airbnb.TagManagement/Airbnb.TagManagement.Infrastructure/", "./Airbnb/Airbnb.TagManagement.Infrastructure/"]
COPY ["Airbnb.TagManagement/Airbnb.TagManagement.Domain/", "./Airbnb/Airbnb.TagManagement.Domain/"]

# Сборка проекта
WORKDIR "/app/Airbnb/Airbnb.TagManagement.API"
RUN ls -la /app
RUN dotnet publish -c ${BUILD_CONFIG} -o /out /p:UseAppHost=false

# Создание финального образа для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Указываем порт
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.TagManagement.API.dll"]
