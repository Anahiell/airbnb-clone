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
COPY ["Airbnb.SharedKernel/Airbnb.Connection/Airbnb.Connection.csproj", "../Airbnb.SharedKernel/Airbnb.Connection/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/Airbnb.MongoRepository.csproj", "../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.UserManagement/Airbnb.UserManagement.API/Airbnb.UserManagement.API.csproj", "Airbnb.UserManagement.API/"]
COPY ["Airbnb.UserManagement/Airbnb.UserManagement.Application/Airbnb.UserManagement.Application.csproj", "Airbnb.UserManagement.Application/"]
COPY ["Airbnb.UserManagement/Airbnb.UserManagement.Infrastructure/Airbnb.UserManagement.Infrastructure.csproj", "Airbnb.UserManagement.Infrastructure/"]
COPY ["Airbnb.UserManagement/Airbnb.UserManagement.Domain/Airbnb.UserManagement.Domain.csproj", "Airbnb.UserManagement.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.UserManagement.API/Airbnb.UserManagement.API.csproj"

# Копируем весь исходный код
COPY ["Airbnb.SharedKernel/Airbnb.Domain/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Domain/"]
COPY ["Airbnb.SharedKernel/Airbnb.Application/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Application/"]
COPY ["Airbnb.SharedKernel/Airbnb.Infrastructure/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Infrastructure/"]
COPY ["Airbnb.SharedKernel/Airbnb.Cache/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Cache/"]
COPY ["Airbnb.SharedKernel/Airbnb.Connection/", "./Airbnb/../Airbnb.SharedKernel/Airbnb.Connection/"]

COPY ["Airbnb.QueryRepository/Airbnb.MongoRepository/.", "./Airbnb/../Airbnb.QueryRepository/Airbnb.MongoRepository/"]

COPY ["Airbnb.UserManagement/Airbnb.UserManagement.API/", "./Airbnb/Airbnb.UserManagement.API/"]
COPY ["Airbnb.UserManagement/Airbnb.UserManagement.Application/", "./Airbnb/Airbnb.UserManagement.Application/"]
COPY ["Airbnb.UserManagement/Airbnb.UserManagement.Infrastructure/", "./Airbnb/Airbnb.UserManagement.Infrastructure/"]
COPY ["Airbnb.UserManagement/Airbnb.UserManagement.Domain/", "./Airbnb/Airbnb.UserManagement.Domain/"]

# Сборка проекта
WORKDIR "/app/Airbnb/Airbnb.UserManagement.API"
RUN ls -la /app
RUN dotnet publish -c ${BUILD_CONFIG} -o /out

# Создание финального образа для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./ 

# Указываем порт
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.UserManagement.API.dll"]