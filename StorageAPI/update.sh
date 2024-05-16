echo "Остановка и удаление текущего контейнера..."
docker compose stop
echo "Выполнение git pull..."
git pull
echo "Выполнение сборки"
docker compose build
echo "Запуск"
docker compose up -d