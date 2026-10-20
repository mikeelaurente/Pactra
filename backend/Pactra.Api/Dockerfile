# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file first for dependency caching
COPY Pactra.Api.csproj ./

# Restore NuGet packages
RUN dotnet restore Pactra.Api.csproj

# Copy remaining source code
COPY . .

# Publish application
RUN dotnet publish Pactra.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "Pactra.Api.dll"]