
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

- If using NuGet.org, you will need to setup the an environment variable of 'NUGET_ORG_KEY' for the key
  - This is used during the push to add the packages to NuGet for referencing by the Zone/Game Server.
- You can also setup a local NuGet registry, baget configuration is included.
  - Checkout https://loic-sharma.github.io/BaGet/installation/docker/ for docker installation instructions
  - Using Docker to host Baget NuGet server
    ~~~ bash
    # Startup local baget server
    docker run --rm --name nuget-server -p 1313:80 --env-file baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest
    ~~~
- The packages will be have versions of 0.0.*.
- The file .dev.version and .sdk.version are used to keep track of the next versions to publish.
- Scripting Configuration on the Zone/Game server should be using the Development settings.
- Run "build-publish-dev-package.sh" in PowerShell Core.
    ~~~ bash
    # Using PowerShell Core
    pwsh.exe -File .\build-publish-dev-package.ps1
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

## Docker Commands

~~~ 
# Build Client Image
docker build --build-arg Version=0.1.0 -t canhorn/ehz-platform-server-client:dev .

# Run the image, ignoring normal startup
docker run -it --rm --entrypoint /bin/bash canhorn/ehz-platform-server-client-dotnet-build:dev
~~~

## Client - Docker Build

~~~
# Build Runtime Docker Image
docker build --build-arg Version=0.1.0 --target client-runtime -t ehz/game/client:0.1.0 .

# Build NuGet Push Docker Image
docker build --target dotnet-nuget-push --build-arg Version=0.1.0 -f Dockerfile -t ehz/game/client/packages:0.1.0 .

# Run Image (uses appsettings.Production.json for settings, not included by default)
docker run -it --rm --name nu-client -v "$(PWD)/src/EventHorizon.Blazor.BabylonJS/wwwroot/appsettings.Production.json:/app/wwwroot/appsettings.json" -p 5000:80 ehz/game/nu_client:latest

# Run NuGet Push
docker run --rm --name push-packages ehz/game/client/packages:0.1.0 --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_ORG_KEY

# Build Runtime Docker Image
docker build --build-arg Version=0.1.0 -f Dockerfile -t canhorn/ehz-platform-server-client:dev .

# Build Restore Stage of Docker Image
docker build --build-arg Version=0.1.0 --target dotnet-restore -f Dockerfile -t canhorn/ehz-platform-server-dotnet-restore:dev .

# Build Build Stage of Docker Image
docker build --build-arg Version=0.1.0 --target dotnet-build -f Dockerfile -t canhorn/ehz-platform-server-dotnet-build:dev .
~~~

## Engine - Docker Build

~~~
# Build Restore Stage
docker build --build-arg Version=0.1.0 --target dotnet-restore -t canhorn/ehz-platform-server-engine_dotnet-restore:dev .

# Build Build Stage
docker build --build-arg Version=0.1.0 --target dotnet-build-editor -t canhorn/ehz-platform-server-engine_dotnet-build:dev .

# Build Runtime Stage
docker build --build-arg Version=1.2.3 --target editor-runtime -t canhorn/ehz-platform-server-editor:dev .
~~~

## Editor - Docker Build 

~~~ bash
# Build Editor Runtime Stage
docker build --build-arg Version=0.1.0 -t canhorn/ehz-platform-server-editor:dev .
~~~

~~~ bash
# Debugging Build Runs in Docker
docker build --progress plain # <-- This will give you a better debugging exp by not collapsing layers
~~~

## Azure Pipelines Build Steps
~~~ bash 
# Restore Repository
docker build --build-arg Version=0.0.0-dev --target dotnet-restore -f Dockerfile -t canhorn/ehz-platform-client-base:dev .

# Build Client
docker build --build-arg Version=0.0.0-dev --target client-runtime -f Dockerfile -t canhorn/ehz-platform-server-client:dev .
# Build Editor
docker build --build-arg Version=0.0.0-dev --target editor-runtime -t canhorn/ehz-platform-server-editor:dev .
# Build Packages
docker build --build-arg Version=0.0.0-dev --target dotnet-nuget-push -t canhorn/ehz-client-packages:dev .

# Push Client
docker push canhorn/ehz-platform-server-client:dev
# Push Editor
docker push canhorn/ehz-platform-server-editor:dev
# Push Packages
docker push canhorn/ehz-client-packages:dev

# NuGet Package Push
docker run --rm --name push-packages canhorn/ehz-client-packages:dev --source https://api.nuget.org/v3/index.json --api-key $NUGET_API_KEY
~~~