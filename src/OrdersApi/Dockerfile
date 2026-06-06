FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OrdersApi/OrdersApi.csproj", "src/OrdersApi/"]
RUN dotnet restore "src/OrdersApi/OrdersApi.csproj"
COPY src/OrdersApi/ src/OrdersApi/
WORKDIR /src/src/OrdersApi
RUN dotnet publish "OrdersApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrdersApi.dll"]