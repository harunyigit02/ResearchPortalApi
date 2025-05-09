# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7237

# Base image for building the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# .csproj dosyasını kopyalıyoruz
COPY FinalProjectWebApi.csproj FinalProjectWebApi/
WORKDIR /src/FinalProjectWebApi
RUN dotnet restore

# Uygulamanın tüm dosyalarını kopyalıyoruz
COPY . . 
RUN dotnet build -c Release -o /app/build

# Yayınlama işlemi
RUN dotnet publish -c Release -o /app/publish

# Final imaj
FROM base AS final
WORKDIR /app

# appsettings.Development.json dosyasını kopyalıyoruz
COPY appsettings.Development.json /app/appsettings.Development.json

# Yayınlanan uygulamayı kopyalıyoruz
COPY --from=build /app/publish .

# Ortam değişkenini ayarlıyoruz
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "FinalProjectWebApi.dll"] 