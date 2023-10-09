param(
    [string]
    [ValidateSet(
        "setup",
        "clean",
        "restore",
        "build",
        "format",
        "client:run",
        "client:watch",
        "client:test",
        "client:test:automation",
        "client:publish",
        "editor:run",
        "editor:watch",
        "editor:test",
        "editor:test:automation",
        "editor:publish",
        "pre",
        "generate:babylonjs"
    )]
    $Command,
    [string] $Configuration = "Release"
)

$clientProject = "./src/EventHorizon.Blazor.BabylonJS/EventHorizon.Blazor.BabylonJS.csproj"
$editorProject = "./EventHorizon.Game.Editor/Client/EventHorizon.Game.Editor.Client.csproj"
$editorTestingAutomationProject = "./Testing/EventHorizon.Game.Editor.Automation/EventHorizon.Game.Editor.Automation.csproj"

switch ($Command) {
    "setup" {
        dotnet tool update --global csharpier
    }
    "clean" {
        dotnet clean
    }
    "restore" {
        dotnet restore --no-cache
    }
    "build" {
        dotnet build
    }
    "format" { 
        dotnet csharpier .
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
        Write-Ouput "Publishing Client"
        # TODO: Add client publish command
        # dotnet publish -c $Configuration -o ./published $clientProject
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
        Write-Ouput "Publishing Editor"
        # TODO: Add editor publish command
        # dotnet publish -c $Configuration -o ./published $editorProject
    }
    "pre" {
        ./entry.ps1 -Command format
        ./entry.ps1 -Command build
        ./entry.ps1 -Command client:publish
        ./entry.ps1 -Command client:test 
        ./entry.ps1 -Command client:test:automation
        ./entry.ps1 -Command editor:publish
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
    Default {
        Write-Output "Invalid Command"
    }
}
