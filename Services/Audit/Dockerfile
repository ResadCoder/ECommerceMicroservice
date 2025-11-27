FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Services/Audit/Audit.csproj ./Audit.csproj
RUN dotnet restore ./Audit.csproj
COPY Services/Audit/. .
RUN dotnet publish ./Audit.csproj -c Release -o /app/publish

# Use a valid functions runtime image tag for .NET 8 or whichever supported version
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
WORKDIR /home/site/wwwroot
COPY --from=build /app/publish .
