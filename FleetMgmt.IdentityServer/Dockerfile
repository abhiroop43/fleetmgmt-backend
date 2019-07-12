FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["FleetMgmt.IdentityServer/FleetMgmt.IdentityServer.csproj", "FleetMgmt.IdentityServer/"]
RUN dotnet restore "FleetMgmt.IdentityServer/FleetMgmt.IdentityServer.csproj"
COPY . .
# COPY ["FleetMgmt.IdentityServer/entrypoint.sh", "/app/"]
WORKDIR "/src/FleetMgmt.IdentityServer"
RUN dotnet build "FleetMgmt.IdentityServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FleetMgmt.IdentityServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FleetMgmt.IdentityServer.dll", "/seed"]
# RUN chmod +x ./entrypoint.sh
# ENTRYPOINT ["./entrypoint.sh"]
