FROM node:13.10.1-alpine AS build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --prod

FROM nginx:1.17.9-alpine
COPY --from=build /usr/src/app/dist/Spa /usr/share/nginx/html