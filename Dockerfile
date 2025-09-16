# 1. Use the official ASP.NET runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. Build stage with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore
COPY TodoListAPI.sln .
COPY TodoListAPI/ TodoListAPI/

RUN dotnet restore TodoListAPI/TodoListAPI.csproj

# Build and publish
RUN dotnet publish TodoListAPI/TodoListAPI.csproj -c Release -o /app/publish

# 3. Final stage - runtime only
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TodoListAPI.dll"]
