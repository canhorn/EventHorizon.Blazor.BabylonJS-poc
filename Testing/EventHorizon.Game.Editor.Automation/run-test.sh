#!/bin/bash
# Setting -e will cause any error code to stop execution
set -e

dotnet test --configuration $BUILD_CONFIGURATION \
    --no-restore --no-build \
    --logger:"trx;LogFilePrefix=TestResult" --results-directory /TestResults/Reports /nodeReuse:false /maxCpuCount:6
