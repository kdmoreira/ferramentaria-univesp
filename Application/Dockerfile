#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infra.Data/Infra.Data.csproj", "Infra.Data/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infra.CrossCutting/Infra.CrossCutting.csproj", "Infra.CrossCutting/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
RUN dotnet restore "Application/Application.csproj"
COPY . .
WORKDIR "/src/Application"
RUN dotnet build "Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]