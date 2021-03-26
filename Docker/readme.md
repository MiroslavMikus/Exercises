## Deploy angular app in to the docker

Windows container [Docu](https://docs.microsoft.com/en-us/virtualization/windowscontainers/manage-docker/manage-windows-dockerfile)

> ng new angular-test

> npm start

> ng build --prod

```docker
# Container to build the app

FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

# Run container
FROM nginx:alpine
COPY --from=node /app/dist/angular-app /usr/share/nginx/html
```

> docker build --pull --rm -f "Dockerfile2" -t angulartest:v3 "."

> docker run --rm -d -p 80:80 angulartest:v3