FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:latest AS build

COPY "src/Something.Api/" "src/Something.Api/"
COPY "src/Something.Core/" "src/Something.Core/"
COPY "src/Something.Infra/" "src/Something.Infra/"
RUN dotnet restore "src/Something.Api/Something.Api.csproj" 
RUN dotnet build "src/Something.Api/Something.Api.csproj" -f net8.0 -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Something.Api/Something.Api.csproj" -f net8.0 -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Something.Api.dll"]