# containerization

<sub><sup>atm i'm using `rancher-desktop`, `containerd` and `nerdctl` because the vanilla `docker-desktop` doesn't work on my machine; when it comes to dotnet restore, it complains about some SSL errors.</sup></sub>

## Docker file
Setup environment, expose port and set environment variables
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app 
EXPOSE 80 
ENV DOTNET_URLS=http://*:8080
ENV ASPNETCORE_URLS=http://*:8080
```

building and publishing the project
```dockerfile
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
```

Running the app
```dockerfile
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Something.Api.dll"]
```
<sub><sup>I wish i could explain these line by line lmao</sup></sub>


### **build the docker image**

```bash
docker build -t image-name:tag path/to/dockerfile
```
<sub><sup>e.g: `docker build -t something-api:testing .` </sup></sub><br>
you can then view your built images using `docker image ls`

### **running an image**
to run an image, you need to instantiate it. An instanced image is called a container.
```bash
docker run -d -p container-port:host-port --name container-name image-name:tag
```
<sub><sup>e.g: `docker run -d -p 8080:8080 --name contained-api something-api:testing`</sup></sub>

> <sub>The `-d` specifies to be run on detached mode. If so, it will run in parallel. If you want to view the logs, use <br>`docker container logs container-name`.

### containers
|command||
|-|-|
|<sup><sub>`container ls`|view containers.<br>add `-a` flag to include downed containers|
|<sup><sub>`container start container-name`|startup a down container.<br> add `-a` before the name to attach mode|
|<sup><sub>`container logs container-name`|view a container's logs.<br> add `-f` before the name to attach mode|
|<sup><sub>`container stop container-name`|stop a running container|


