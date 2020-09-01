# Cleanup Files
Get-ChildItem Published -Include *.* -Recurse | ForEach { $_.Delete()}
# Cleanup Directories
Remove-Item 'Published/*' -Recurse
# SDK Version
$file_data = Get-Content .sdk.version
$version = [int]::Parse($file_data) + 1
Set-Content -Path .sdk.version -Value $version
# Build Release Packages
dotnet build --configuration Release -p:VersionPrefix="0.0.1" --version-suffix preview$($version) -o Published;

Get-ChildItem "Published/EventHorizon.*.nupkg" -Recurse | Select-Object FullName | %{dotnet nuget push $($_.FullName) -k $($env:NUGET_ORG_KEY) --source "https://api.nuget.org/v3/index.json"}
Get-ChildItem "Published/EventHorizon.*.nupkg" -Recurse | Select-Object FullName