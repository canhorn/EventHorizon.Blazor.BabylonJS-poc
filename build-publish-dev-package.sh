# Cleanup Files
Get-ChildItem Published -Include *.* -Recurse | ForEach { $_.Delete()}
# Cleanup Directories
Remove-Item 'Published/*' -Recurse

# dotnet build --configuration Release; 
$file_data = Get-Content .dev.version
$version = [int]::Parse($file_data) + 1
Set-Content -Path .dev.version -Value $version
dotnet build --configuration Release /p:PackageIdPostfix=-Dev -p:PackageVersion="0.0.$($version)" -o Published;

Get-ChildItem "Published/EventHorizon.*-Dev*$($version)*.nupkg" -Recurse | Select-Object FullName | %{dotnet nuget push $($_.FullName) -k $($env:NUGET_ORG_KEY) --source "https://api.nuget.org/v3/index.json"}
Get-ChildItem "Published/EventHorizon.*-Dev*$($version)*.nupkg" -Recurse | Select-Object FullName
