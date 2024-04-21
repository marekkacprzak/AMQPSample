#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Receive/Receive.csproj", "Receive/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["ServiceContract/ServiceContract.csproj", "ServiceContract/"]
RUN dotnet restore "./Receive/Receive.csproj"
COPY . .
RUN dotnet build "./Receive/Receive.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "./Receive/Receive.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#ENTRYPOINT ["tail", "-f", "/dev/null"]

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Receive.dll"]

