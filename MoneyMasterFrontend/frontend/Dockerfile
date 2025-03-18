# Этап 1: Сборка приложения с использованием Node.js
FROM node:18 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем файлы с зависимостями
COPY package.json package-lock.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем исходный код приложения
COPY . .

# Строим приложение для продакшн
RUN npm run build

# Этап 2: Используем Nginx для обслуживания статических файлов
FROM nginx:alpine

# Копируем сгенерированные файлы из предыдущего этапа в директорию Nginx
# Меняем /app/build на /app/dist
COPY --from=build /app/dist /usr/share/nginx/html

# Открываем порт 80 для доступа к приложению
EXPOSE 80

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]
