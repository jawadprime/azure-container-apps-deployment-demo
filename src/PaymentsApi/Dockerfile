FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PaymentsApi/PaymentsApi.csproj", "PaymentsApi/"]
RUN dotnet restore "PaymentsApi/PaymentsApi.csproj"
COPY . .
WORKDIR "/src/PaymentsApi"
RUN dotnet publish "PaymentsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PaymentsApi.dll"]
