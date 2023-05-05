# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV DOTNET_URLS=http://*:8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Something.Api/Something.Api.csproj", "Something.Api/"]
COPY ["Something.Core/Something.Core.csproj", "Something.Core/"]
COPY ["Something.Infra/Something.Infra.csproj", "Something.Infra/"]
RUN dotnet restore "Something.Api/Something.Api.csproj"
COPY . .
WORKDIR "/src/Something.Api"
RUN dotnet build "Something.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Something.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Something.Api.dll"]

