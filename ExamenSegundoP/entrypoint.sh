#!/usr/bin/env bash
set -e

echo "Aplicando migraciones (si existen)..."
dotnet ef database update --no-build || echo "No se pudieron aplicar migraciones o no existen, continuando..."

echo "Iniciando aplicación..."
dotnet ExamenSegundoP.dll
