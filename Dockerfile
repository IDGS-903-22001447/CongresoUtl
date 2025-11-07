# ========================
# ETAPA 1: Construcción
# ========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar el archivo del proyecto y restaurar dependencias
COPY ["ExamenSegundoP/ExamenSegundoP.csproj", "ExamenSegundoP/"]
RUN dotnet restore "ExamenSegundoP/ExamenSegundoP.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/ExamenSegundoP"
RUN dotnet publish "ExamenSegundoP.csproj" -c Release -o /app/publish /p:UseAppHost=false


# ========================
# ETAPA 2: Ejecución
# ========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

# (Opcional) agregar script de migraciones
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["/entrypoint.sh"]
