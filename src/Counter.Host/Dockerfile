FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build

COPY "src/Counter.Host/" "src/Counter.Host/"

RUN dotnet restore "src/Counter.Host/Counter.Host.csproj"
RUN dotnet build "src/Counter.Host/Counter.Host.csproj"

FROM build as publish
RUN dotnet publish -c Release "src/Counter.Host/Counter.Host.csproj" -o /app/publish /p:UseAppHost=false -f net8.0

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Counter.Host.dll" ]