# Используем базовый образ для ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копируем все файлы из локальной папки bin/Release/net6.0/publish в контейнер
COPY ./bin/Release/net8.0/ .

# Убедимся, что файл StorageAPI.dll существует
RUN ls -la /app

# Задаем команду запуска приложения
ENTRYPOINT ["dotnet", "StorageAPI.dll"]

# Expose порт 8080 для доступа к приложению
EXPOSE 8080
