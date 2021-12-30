# Simple .NET Test Runner
FROM mcr.microsoft.com/dotnet/sdk:6.0
ARG BUILD_CONFIGURATION=Release
ENV BUILD_CONFIGURATION=${BUILD_CONFIGURATION}

WORKDIR /source

COPY . .

RUN dotnet restore

RUN dotnet build --configuration $BUILD_CONFIGURATION --no-restore

RUN chmod +x run-test.sh

ENTRYPOINT ["/source/run-test.sh"]