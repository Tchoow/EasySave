# Choisir une image de base de .Net Core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine

# Définir le répertoire de travail
WORKDIR /app

# Copier les fichiers de l'application dans le répertoire de travail
COPY . .

# Compiler l'application
RUN dotnet build "EasySave.csproj" -c Release -o /app

# Exposer le port 5000 pour l'application
EXPOSE 5000

# Définir la commande par défaut pour lancer l'application
CMD ["dotnet", "run", "--project", "EasySave.csproj"]