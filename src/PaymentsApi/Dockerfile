FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/PaymentsApi/PaymentsApi.csproj", "src/PaymentsApi/"]
RUN dotnet restore "src/PaymentsApi/PaymentsApi.csproj"
COPY src/PaymentsApi/ src/PaymentsApi/
WORKDIR /src/src/PaymentsApi
RUN dotnet publish "PaymentsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PaymentsApi.dll"]