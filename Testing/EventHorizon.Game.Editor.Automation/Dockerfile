# Stage 1 - Restore .NET Project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-restore
WORKDIR /source

COPY . .

RUN dotnet test
