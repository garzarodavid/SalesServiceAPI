#!/bin/bash
set -e

# Parar todos os contêineres em execução
echo "Parando todos os contêineres em execução..."
docker stop $(docker ps -a -q) || true

# Remover todos os contêineres
echo "Removendo todos os contêineres..."
docker rm $(docker ps -a -q) || true

# Construir e iniciar os contêineres
echo "Construindo e iniciando os contêineres..."
docker-compose up --build