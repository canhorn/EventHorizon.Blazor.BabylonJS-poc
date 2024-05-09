param(
    [string]
    [ValidateSet(
        "setup",
        "format",
        "client:clean",
        "client:restore",
        "client:build",
        "client:run",
        "client:watch",
        "client:test",
        "client:test:automation",
        "client:publish",
        "client:serve",
        "client:docker",
        "editor:clean",
        "editor:restore",
        "editor:build",
        "editor:run",
        "editor:watch",
        "editor:test",
        "editor:test:automation",
        "editor:publish",
        "editor:serve",
        "editor:docker",
        "pre",
        "fix:formatting",
        "generate:babylonjs"
    )]
    $Command,
    [string] $Configuration = "Release"
)

$clientSolution = "./EventHorizon.Blazor.BabylonJS.sln"
$clientProject = "./src/EventHorizon.Blazor.BabylonJS/EventHorizon.Blazor.BabylonJS.csproj"
$editorSolution = "./EventHorizon.Game.Editor/EventHorizon.Game.Editor.sln"
$editorProject = "./EventHorizon.Game.Editor/Client/EventHorizon.Game.Editor.Client.csproj"
$editorTestingAutomationProject = "./Testing/EventHorizon.Game.Editor.Automation/EventHorizon.Game.Editor.Automation.csproj"

switch ($Command) {
    "setup" {
        dotnet tool update --global csharpier
        dotnet tool install --global dotnet-serve
    }
    "format" { 
        dotnet csharpier .
    }
    "client:clean" {
        dotnet clean $clientSolution
    }
    "client:restore" {
        dotnet restore --no-cache $clientSolution
    }
    "client:build" {
        dotnet clean $clientSolution
        dotnet build $clientSolution
    }
    "client:run" {
        $Env:ASPNETCORE_ENVIRONMENT = "$Configuration"
        $Env:ASPNETCORE_URLS = "https://localhost:5001"

        dotnet run --project $clientProject
    }
    "client:watch" {
        $Env:ASPNETCORE_ENVIRONMENT = "$Configuration"
        $Env:ASPNETCORE_URLS = "https://localhost:5001"

        dotnet watch --project $clientProject
    }
    "client:test" { 
        Write-Output "Client Testing Not Implemented"
    }
    "client:test:automation" { 
        Write-Output "Client Automation Testing Not Implemented"
    }
    "client:publish" {
        Write-Output "Publishing Client"
        # TODO: Add client publish command
        # dotnet publish -c $Configuration -o ./published $clientProject
    }
    "client:docker" {
        docker build --build-arg Version=0.1.0 `
            --target dotnet-publish-client `
            -t canhorn/ehz-platform-server-client_dotnet-publish:dev .
    }
    "editor:clean" {
        dotnet clean $editorSolution
    }
    "editor:restore" {
        dotnet restore --no-cache $editorSolution
    }
    "editor:build" {
        dotnet clean $editorSolution
        dotnet build $editorSolution --no-cache
    }
    "editor:run" {
        $Env:ASPNETCORE_ENVIRONMENT = "$Configuration"
        $Env:ASPNETCORE_URLS = "https://localhost:5888"

        dotnet run --project $editorProject
    }
    "editor:watch" {
        $Env:ASPNETCORE_ENVIRONMENT = "$Configuration"
        $Env:ASPNETCORE_URLS = "https://localhost:5888"

        dotnet watch --project $editorProject
    }
    "editor:test" { 
        Write-Output "Editor Testing Not Implemented"
    }
    "editor:test:automation" { 
        dotnet test $editorTestingAutomationProject
    }
    "editor:publish" {
        dotnet publish -c $Configuration -o ./published $editorProject
    }
    "editor:serve" {
        dotnet serve --port 5001 -S -d="./published/wwwroot" --fallback-file "./published/wwwroot/index.html"
    }
    "editor:docker" {
        docker build --build-arg Version=0.1.0 `
            --target dotnet-publish-editor `
            -t canhorn/ehz-platform-server-engine_dotnet-publish:dev .
    }
    "pre" {
        ./entry.ps1 -Command format
        ./entry.ps1 -Command client:build
        ./entry.ps1 -Command client:publish
        ./entry.ps1 -Command client:test 
        ./entry.ps1 -Command client:test:automation
        ./entry.ps1 -Command editor:build
        ./entry.ps1 -Command editor:publish
        ./entry.ps1 -Command editor:test
        ./entry.ps1 -Command editor:test:automation
    }
    "generate:babylonjs" {
        ehz-generate -f `
            -a Blazor.BabylonJS.WASM `
            -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/babylon.d.ts `
            -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/gui/babylon.gui.d.ts `
            -c Button `
            -c MeshBuilder `
            -c PointLight `
            -c StandardMaterial `
            -c HemisphericLight `
            -c UniversalCamera `
            -c Grid `
            -c StackPanel `
            -c SceneLoader `
            -c BoundingBoxGizmo `
            -c ArcFollowCamera `
            -c ScrollViewer
    }
    "fix:formatting" {
        # Get-ChildItem -Path $PWD -Filter *.csproj | ForEach-Object { dotnet format $_.DirectoryName }
        # Get-ChildItem -Recurse -Path $PWD -Filter *.csproj | ForEach-Object { Write-Output $_.FullName }
        Get-ChildItem -Recurse -Path $PWD -Filter *.csproj | ForEach-Object { 
            Write-Output $_.FullName
            dotnet outdated --upgrade $_.FullName }
    }
    Default {
        Write-Output "Invalid Command"
    }
}
