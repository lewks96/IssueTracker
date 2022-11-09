# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /IssueTracker
    
COPY . .
WORKDIR /IssueTracker/IssueTracker
RUN dotnet restore 

# Copy everything else and build
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /DockerRun
COPY --from=build-env /IssueTracker/IssueTracker/out .
ENTRYPOINT ["dotnet", "IssueTracker-WebApp.dll"]