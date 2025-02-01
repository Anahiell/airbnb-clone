# Используем образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем .csproj файлы отдельно (для кеширования зависимостей)
COPY ["Airbnb.API/Airbnb.API.csproj", "Airbnb.API/"]
COPY ["Airbnb.Infrastructure/Airbnb.Infrastructure.csproj", "Airbnb.Infrastructure/"]

# Восстанавливаем зависимости
RUN dotnet restore "Airbnb.API/Airbnb.API.csproj"

# Копируем весь исходный код
COPY . .

# Собираем проект
WORKDIR "/app/Airbnb.API"
RUN dotnet publish "Airbnb.API.csproj" -c Release -o /out

# Используем ASP.NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копируем собранное приложение
COPY --from=build /out ./

# Указываем порт
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "Airbnb.API.dll"]
