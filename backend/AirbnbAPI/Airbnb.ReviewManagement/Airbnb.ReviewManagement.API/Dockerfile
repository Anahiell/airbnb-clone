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

COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.API/Airbnb.ReviewManagement.API.csproj", "Airbnb.ReviewManagement.API/"]
COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.Application/Airbnb.ReviewManagement.Application.csproj", "Airbnb.ReviewManagement.Application/"]
COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.Infrastructure/Airbnb.ReviewManagement.Infrastructure.csproj", "Airbnb.ReviewManagement.Infrastructure/"]
COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.Domain/Airbnb.ReviewManagement.Domain.csproj", "Airbnb.ReviewManagement.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.ReviewManagement.API/Airbnb.ReviewManagement.API.csproj"

# Копируем весь исходный код
COPY ["Airbnb.SharedKernel/Airbnb.Domain/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Cache/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/", "./Airbnb/../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.API/.", "./Airbnb/Airbnb.ReviewManagement.API/"]
COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.Application/", "./Airbnb/Airbnb.ReviewManagement.Application/"]
COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.Infrastructure/.", "./Airbnb/Airbnb.ReviewManagement.Infrastructure/"]
COPY ["Airbnb.ReviewManagement/Airbnb.ReviewManagement.Domain/", "./Airbnb/Airbnb.ReviewManagement.Domain/"]

# Сборка проекта
WORKDIR "/app/Airbnb/Airbnb.ReviewManagement.API"
RUN ls -la /app
RUN dotnet publish -c ${BUILD_CONFIG} -o /out

# Создание финального образа для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Указываем порт
EXPOSE 8082

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.ReviewManagement.API.dll"]
