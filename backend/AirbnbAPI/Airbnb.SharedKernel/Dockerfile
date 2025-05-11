FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# копируем текущую папку (где лежит Dockerfile и все проекты)
COPY . .

# restore и build все проекты (или через решение, если есть)
RUN for proj in $(find . -name '*.csproj'); do \
    dotnet restore "$proj" && \
    dotnet build "$proj" -c Release --no-restore; \
  done
