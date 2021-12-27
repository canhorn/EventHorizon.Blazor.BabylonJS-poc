# Simple .NET Test Runner
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-restore
WORKDIR /source

COPY . .

RUN dotnet test
