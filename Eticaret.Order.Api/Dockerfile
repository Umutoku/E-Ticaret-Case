#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eticaret.Order.Api/Eticaret.Order.Api.csproj", "Eticaret.Order.Api/"]
COPY ["Eticaret.Order.Application/Eticaret.Order.Application.csproj", "Eticaret.Order.Application/"]
COPY ["Eticaret.Order.Domain/Eticaret.Order.Domain.csproj", "Eticaret.Order.Domain/"]
COPY ["Eticaret.Order.Domain.Core/Eticaret.Order.Domain.Core.csproj", "Eticaret.Order.Domain.Core/"]
COPY ["ETicaret.Order.Infrastructure/ETicaret.Order.Infrastructure.csproj", "ETicaret.Order.Infrastructure/"]
COPY ["Order.Shared/Order.Shared.csproj", "Order.Shared/"]
RUN dotnet restore "./Eticaret.Order.Api/Eticaret.Order.Api.csproj"
COPY . .
WORKDIR "/src/Eticaret.Order.Api"
RUN dotnet build "./Eticaret.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Eticaret.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eticaret.Order.Api.dll"]