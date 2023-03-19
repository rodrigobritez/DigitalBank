# Imagem de compilação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copiar projeto e restaurar pacotes
COPY DigitalBank.API/*.csproj ./DigitalBank.API/
COPY DigitalBank.Domain/*.csproj ./DigitalBank.Domain/
COPY DigitalBank.Data/*.csproj ./DigitalBank.Data/
COPY DigitalBank.Shared/*.csproj ./DigitalBank.Shared/
COPY DigitalBank.GraphQL/*.csproj ./DigitalBank.GraphQL/
COPY DigitalBank.Tests/*.csproj ./DigitalBank.Tests/
RUN dotnet restore DigitalBank.API/DigitalBank.API.csproj

# Copiar todo o código
COPY . .

# Compilar e publicar a aplicação
RUN dotnet publish DigitalBank.API/DigitalBank.API.csproj -c Release -o out

# Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out ./

# Expor porta
EXPOSE 80

# Comando para iniciar o container
ENTRYPOINT ["dotnet", "DigitalBank.API.dll"]