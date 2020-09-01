
# (POC) Blazor BabylonJS 

This project is a proof of concept project that shows off how a Blazor project can integrate with BabylonJS, with a focus on using very little JavaScript.

## Disclaimer

This project has no way to run, it has dependencies on other parts of my private infrastructure, a Game Server, an Identity Server, and others. 
Checkout <a href="https://ehzgames.studio/game-development-platform.html">Game Development Platform</a> for more details of the architecture this project is integrating with. This project is just the Client Server, the others are not publicly available, yet.

## Blazor.BabylonJS.WASM

The Blazor.BabylonJS.WASM project is built on top of a few other projects I built that are available here: 

- <a href="https://github.com/canhorn/EventHorizon.Blazor.Interop">canhorn/EventHorizon.Blazor.Interop</a> -> Used to help with interfacing with the BabylonJS JavaScript layer.
- <a href="https://github.com/canhorn/EventHorizon.Blazor.TypeScript.Interop.Generator">httpscanhorn/EventHorizon.Blazor.TypeScript.Interop.Generator</a> -> Used to generate C# that matches the BabylonJS API, built from TypeScript defintion files.

## Generate new Blazor.BabylonJS.WASM

Create a new Blazor.BabylonJS.WASM using the tool provided by <a href="https://github.com/canhorn/EventHorizon.Blazor.TypeScript.Interop.Generator">scanhorn/EventHorizon.Blazor.TypeScript.Interop.Generator</a>.

~~~ bash
ehz-generate -f -a Blazor.BabylonJS.WASM -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/babylon.d.ts -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/gui/babylon.gui.d.ts -c Button -c MeshBuilder -c PointLight -c StandardMaterial -c HemisphericLight -c UniversalCamera -c Grid -c StackPanel -c SceneLoader -c BoundingBoxGizmo -c ArcFollowCamera -c ScrollViewer
~~~

## Development - Building the SDK

The Zone/Game Server will use the SDK project provided by this project for it compiling the script.
Because of this connection between the two services any work being done with the SDK will need extra consideration when developing.

The Zone/Game Server architecture in setup to use the SDK project hosted in a NuGet repository, because of this any new functionality added to the SDK will need to be pushed to NuGet.
Below is the details about what needs to be done, with a CLI script that can be ran from PowerShell Core to Build/Package/Push the SDK package.

- You will need to setup the an environment variable of 'NUGET_ORG_KEY'
  - This is used during the push to add the packages to NuGet for referencing by the Zone/Game Server.
- The packages will be have versions of 0.0.*.
- The file .dev.version and .sdk.version are used to keep track of the next versions to publish.
- Scripting Configuration on the Zone/Game server should be using the Development settings.
- Run "build-publish-dev-package.sh" in PowerShell Core.
    ~~~ bash
    # Using PowerShell Core
    pwsh.exe -File .\build-publish-dev-package.sh  
    ~~~

### Adding New SDK Package References

The SDK project is the single project that pulls all of the packages together for distribution, follow the steps below to register new APIs to the SDK.

- Create a new Class Library for dotnet core, using cli or any GUI is fine.
- Take the snippet of XML below and add it to your csproj.
  - Update these tokens:
    - [[PROJECT]] -> This token is the name of your project. eg. EventHorizon.Game.Server.ServerModule.Api
    - [[HOSTED_REPOSITORY]] -> Where your source is hosted. eg. https://github.com/canhorn/EventHorizon.Blazor.BabylonJS-poc
    - [[PROJECT_DESCRIPTION]] -> A NuGet friendly description of this package. eg. This project is the Server ServerModule Api/Model/Events for the EventHorizon Game and Server.
- Details about snippet:
  - PackageIdPostfix -> Helps the Build/Package/Push scripts tag for Release or Dev.
  - GeneratePackageOnBuild -> Helps to limit the 'dotnet build' with only what should go up to NuGet.
- (Optional) Add the "RootNamespace" to PropertyGroup
  - This hints to the editor that new files should start at the root of 'EventHorizon.Game.Server.ServerModule' for example, instead of 'EventHorizon.Game.Server.ServerModule.Api' that the folder and csproj could be named after.

 ~~~ xml
    <PropertyGroup>
        <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
        <PackageId>[[PROJECT]]$(PackageIdPostfix)</PackageId>
        <RepositoryUrl>[[HOSTED_REPOSITORY]]</RepositoryUrl>
        <Description>[[PROJECT_DESCRIPTION]]</Description>
    </PropertyGroup>
 ~~~