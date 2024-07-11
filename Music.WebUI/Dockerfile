# stage 1
FROM node:alpine as node
WORKDIR /app
COPY angular.json package.json karma.conf.js ./
COPY tsconfig.json tsconfig.app.json tsconfig.spec.json ./
COPY src/ src/
ENV PATH /app/node_modules/.bin:$PATH
RUN npm install --dd
EXPOSE 4200
CMD ["npm", "start"]

