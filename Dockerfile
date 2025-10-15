# ====== Build ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo e restaura
COPY . .
RUN dotnet restore

# Publica em Release (sem AppHost para reduzir tamanho)
RUN dotnet publish ESG.Compliance.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# ====== Runtime ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Garanta que a API escute na 8080
ENV ASPNETCORE_URLS=http://+:8080

# Exponha a porta do container (o mapeamento externo é pelo compose)
EXPOSE 8080

# Copie o build publicado
COPY --from=build /app/publish .

# Start
ENTRYPOINT ["dotnet", "ESG.Compliance.Api.dll"]