#!/bin/bash
set -e

# Executa as migrações do Entity Framework Core
echo "Applying migrations..."
dotnet ef database update --project src/SalesServiceAPI.Infrastructure --startup-project src/SalesServiceAPI.Api

# Inicia o aplicativo
echo "Starting application..."
exec dotnet SalesServiceAPI.Api.dll
