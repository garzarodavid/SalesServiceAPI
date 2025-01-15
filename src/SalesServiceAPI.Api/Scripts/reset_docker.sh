#!/bin/bash
set -e

# Parar todos os cont�ineres em execu��o
echo "Parando todos os cont�ineres em execu��o..."
docker stop $(docker ps -a -q) || true

# Remover todos os cont�ineres
echo "Removendo todos os cont�ineres..."
docker rm $(docker ps -a -q) || true

# Construir e iniciar os cont�ineres
echo "Construindo e iniciando os cont�ineres..."
docker-compose up --build