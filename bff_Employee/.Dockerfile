FROM node:alpine
RUN mkdir -p /home/node/app && chown -R node:node /home/node/app

USER node

WORKDIR /home/node/app
COPY --chown=node:node ./package*.json ./

RUN npm install
COPY --chown=node:node . .

CMD [ "node", "."]