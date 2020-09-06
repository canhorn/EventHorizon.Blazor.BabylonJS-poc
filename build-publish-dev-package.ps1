# Cleanup Files
Get-ChildItem Published -Include *.* -Recurse | ForEach-Object { $_.Delete() }
# Cleanup Directories
Remove-Item 'Published/*' -Recurse

# Cleanup Local NuGet Cache for the local source feed
# This helps to clear the http metadata for the nuget versions.
Remove-Item "$($env:LOCALAPPDATA)/NuGet/v3-cache/*_localhost_1313*/list_eventhorizon*" -Recurse

# dotnet build --configuration Release; 
$nuget_source_feed = "http://localhost:1313/v3/index.json"
$file_data = Get-Content .dev.version
$version = [int]::Parse($file_data) + 1
Set-Content -Path .dev.version -Value $version
dotnet build --configuration Release /p:PackageIdPostfix=-Dev -p:PackageVersion="0.0.$($version)" -o Published;

Get-ChildItem "Published/EventHorizon.*-Dev*$($version)*.nupkg" -Recurse | Select-Object FullName | ForEach-Object { dotnet nuget push $($_.FullName) -k $($env:NUGET_ORG_KEY) --source $nuget_source_feed }
Get-ChildItem "Published/EventHorizon.*-Dev*$($version)*.nupkg" -Recurse | Select-Object FullName
