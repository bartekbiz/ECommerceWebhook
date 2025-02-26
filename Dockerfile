FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerceWebhook.Api/ECommerceWebhook.Api.csproj", "ECommerceWebhook.Api/"]
RUN dotnet restore "ECommerceWebhook.Api/ECommerceWebhook.Api.csproj"
COPY . .
WORKDIR "/src/ECommerceWebhook.Api"
RUN dotnet build "ECommerceWebhook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerceWebhook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceWebhook.Api.dll"]