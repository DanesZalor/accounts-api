FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src
COPY src .
RUN dotnet restore "Something.Api/Something.Api.csproj"
RUN dotnet build "Something.Api/Something.Api.csproj" -c Release -o /app/build
RUN dotnet dev-certs https

FROM build AS publish
RUN dotnet publish "/src/Something.Api/Something.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Something.Api.dll"]