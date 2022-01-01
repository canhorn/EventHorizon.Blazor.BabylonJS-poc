#!/bin/bash
# Setting -e will cause any error code to stop execution
set -e

dotnet test --configuration $BUILD_CONFIGURATION \
    --no-restore --no-build \
    --filter Name~Displays_Login_Page_When_Opening_Home_Page_On_Fresh_Window \
    --logger:"trx;LogFilePrefix=TestResult" --results-directory /TestResults/Reports \
    /nodeReuse:false /maxCpuCount:6
