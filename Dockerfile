# Stage 1 - Restore .NET Project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-restore
WORKDIR /source

ENV PATH="$PATH:/root/.dotnet/tools"

# Solutions
COPY ./*.sln .
COPY ./EventHorizon.Game.Editor/*.sln ./EventHorizon.Game.Editor/

COPY Directory.Build.props ./Directory.Build.props

# Install Source Code Generation Tool
# RUN dotnet tool install --global EventHorizon.Blazor.TypeScript.Interop.Tool --version 0.1.12

### Build _generated/Blazor.BabylonJS.WASM from scratch
# RUN mkdir -p ./src/Shared && \ 
#     cd ./src/Shared && \
#     ehz-generate -f -a Blazor.BabylonJS.WASM -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/babylon.d.ts -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/gui/babylon.gui.d.ts -c Button -c MeshBuilder -c PointLight -c StandardMaterial -c HemisphericLight -c UniversalCamera -c Grid -c StackPanel -c SceneLoader -c BoundingBoxGizmo -c ArcFollowCamera -c ScrollViewer


# Copy the source project files
## Generated - Blazor.BabylonJS.WASM
COPY src/Shared/_generated/Blazor.BabylonJS.WASM/*.csproj ./src/Shared/_generated/Blazor.BabylonJS.WASM/

## Main
COPY src/EventHorizon.Common/*.csproj ./src/EventHorizon.Common/
COPY src/EventHorizon.Connection.Shared/*.csproj ./src/EventHorizon.Connection.Shared/
COPY src/EventHorizon.Game.Client/*.csproj ./src/EventHorizon.Game.Client/
COPY src/EventHorizon.Game.Client.Systems/*.csproj ./src/EventHorizon.Game.Client.Systems/
COPY src/EventHorizon.Html.Interop/*.csproj ./src/EventHorizon.Html.Interop/
COPY src/EventHorizon.Observer/*.csproj ./src/EventHorizon.Observer/

## Platform 
COPY src/Platform/EventHorizon.Platform.LogProvider/*.csproj ./src/Platform/EventHorizon.Platform.LogProvider/
COPY src/Platform/EventHorizon.Platform.Integration/*.csproj ./src/Platform/EventHorizon.Platform.Integration/

## SDK
COPY src/SDK/EventHorizon.Game.Client.Core.Api/*.csproj ./src/SDK/EventHorizon.Game.Client.Core.Api/
COPY src/SDK/EventHorizon.Game.Client.Engine.Api/*.csproj ./src/SDK/EventHorizon.Game.Client.Engine.Api/
COPY src/SDK/EventHorizon.Game.Client.Engine.Gui.Api/*.csproj ./src/SDK/EventHorizon.Game.Client.Engine.Gui.Api/
COPY src/SDK/EventHorizon.Game.Client.Scripts.SDK/*.csproj ./src/SDK/EventHorizon.Game.Client.Scripts.SDK/
COPY src/SDK/EventHorizon.Game.Client.Scripts.SDK.Generator/*.csproj ./src/SDK/EventHorizon.Game.Client.Scripts.SDK.Generator/
COPY src/SDK/EventHorizon.Game.Client.Systems.Api/*.csproj ./src/SDK/EventHorizon.Game.Client.Systems.Api/

COPY src/SDK/Server/EventHorizon.Game.Server.Api/*.csproj ./src/SDK/Server/EventHorizon.Game.Server.Api/
COPY src/SDK/Server/EventHorizon.Game.Server.ServerModule.Api/*.csproj ./src/SDK/Server/EventHorizon.Game.Server.ServerModule.Api/

## Server
COPY src/Server/EventHorizon.Game.Server/*.csproj ./src/Server/EventHorizon.Game.Server/

## Shared
COPY src/Shared/EventHorizon.Activity/*.csproj ./src/Shared/EventHorizon.Activity/
COPY src/Shared/EventHorizon.Activity.Api/*.csproj ./src/Shared/EventHorizon.Activity.Api/
COPY src/Shared/EventHorizon.ApplicationDetails.Component/*.csproj ./src/Shared/EventHorizon.ApplicationDetails.Component/

# Client
## Client - Main Source
COPY src/EventHorizon.Blazor.BabylonJS/*.csproj ./src/EventHorizon.Blazor.BabylonJS/
COPY src/EventHorizon.Blazor.BabylonJS.Server/*.csproj ./src/EventHorizon.Blazor.BabylonJS.Server/
## END Client

# Editor
## Editor - Main Source
COPY EventHorizon.Game.Editor/Client/*.csproj ./EventHorizon.Game.Editor/Client/
COPY EventHorizon.Game.Editor/Api/EventHorizon.Game.Editor.Api/*.csproj ./EventHorizon.Game.Editor/Api/EventHorizon.Game.Editor.Api/

COPY EventHorizon.Game.Editor/Servers/Asset/EventHorizon.Game.Server.Asset/*.csproj ./EventHorizon.Game.Editor/Servers/Asset/EventHorizon.Game.Server.Asset/
COPY EventHorizon.Game.Editor/Servers/Asset/EventHorizon.Game.Server.Asset.Api/*.csproj ./EventHorizon.Game.Editor/Servers/Asset/EventHorizon.Game.Server.Asset.Api/

COPY EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core/*.csproj ./EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core/
COPY EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core.Api/*.csproj ./EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core.Api/

COPY EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone/*.csproj ./EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone/
COPY EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone.Api/*.csproj ./EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone.Api/

COPY EventHorizon.Game.Editor/Shared/*.csproj ./EventHorizon.Game.Editor/Shared/

## Zone Systems
COPY EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.System.Server.Scripts/*.csproj ./EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.System.Server.Scripts/
COPY EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.ArtifactManagement/*.csproj ./EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.ArtifactManagement/
COPY EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.ClientAssets/*.csproj ./EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.ClientAssets/
COPY EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.DataStorage/*.csproj ./EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.DataStorage/
COPY EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.Wizard/*izard.csproj ./EventHorizon.Game.Editor/Zone/Systems/EventHorizon.Zone.Systems.Wizard/

## Editor - Server
COPY EventHorizon.Game.Editor/Server/*.csproj ./EventHorizon.Game.Editor/Server/

## Editor - Cache Source Generators
COPY EventHorizon.Game.Editor/Generator/EventHorizon.Cache.Api/*.csproj ./EventHorizon.Game.Editor/Generator/EventHorizon.Cache.Api/
COPY EventHorizon.Game.Editor/Generator/EventHorizon.Cache.Generator/*.csproj ./EventHorizon.Game.Editor/Generator/EventHorizon.Cache.Generator/
# END Editor

# Testing
COPY Testing/EventHorizon.Game.Editor.Automation/*.csproj ./Testing/EventHorizon.Game.Editor.Automation/
COPY Testing/EventHorizon.Game.Editor.Component.Tests/*.csproj ./Testing/EventHorizon.Game.Editor.Component.Tests/
# END Testing

RUN dotnet restore ./EventHorizon.Blazor.BabylonJS.sln

RUN dotnet restore ./EventHorizon.Game.Editor/EventHorizon.Game.Editor.sln


# Stage 2.1 - Build Client
FROM dotnet-restore AS dotnet-build-client
ARG Version=0.0.0

WORKDIR /source

COPY ./src ./src

RUN dotnet build /p:Version=$Version --configuration Release --no-restore


# Stage 2.2 - Publish Client
FROM dotnet-build-client AS dotnet-publish-client
ARG Version=0.0.0

WORKDIR /source

## Single folder publish of whole solution
RUN dotnet publish /p:Version=$Version --output /app/client/ --configuration Release --no-build --no-restore


# Stage 3.1 - Build Editor
FROM dotnet-build-client AS dotnet-build-editor
ARG Version=0.0.0

WORKDIR /source

COPY ./EventHorizon.Game.Editor ./EventHorizon.Game.Editor

RUN dotnet build /p:Version=$Version --configuration Release --no-restore ./EventHorizon.Game.Editor


# Stage 3.2 - Publish Editor
FROM dotnet-build-editor AS dotnet-publish-editor
ARG Version=0.0.0

WORKDIR /source

## Single folder publish of whole project
RUN dotnet publish /p:Version=$Version --output /app/editor/ --configuration Release --no-build --no-restore ./EventHorizon.Game.Editor


# Stage 4.1 - Build NuGet
FROM dotnet-restore AS dotnet-nuget-build
ARG Version=0.0.0

WORKDIR /source

COPY ./src ./src

RUN dotnet build --output /artifacts /p:Version=$Version --configuration Release --no-restore


# Stage 4.2 - Publish to NuGet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-nuget-push
WORKDIR /artifacts
COPY --from=dotnet-nuget-build /artifacts .
RUN find . -name '*.nupkg' -ls
ENTRYPOINT ["dotnet", "nuget", "push", "/artifacts/*.nupkg"]
CMD ["--source", "https://api.nuget.org/v3/index.json"]


# Stage 4.2 - Editor Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS editor-runtime
ARG Version=0.0.0
ENV APPLICATION_VERSION=$Version

WORKDIR /app
COPY --from=dotnet-publish-editor /app/editor .

RUN echo "export const APPLICATION_VERSION = () => \"$Version\";" > /app/wwwroot/version.js

ENTRYPOINT ["dotnet", "EventHorizon.Game.Editor.Server.dll"]


# Stage 4.3 - Client Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS client-runtime
ARG Version=0.0.0
ENV APPLICATION_VERSION=$Version

WORKDIR /app
COPY --from=dotnet-publish-client /app/client .

RUN echo "export const APPLICATION_VERSION = () => \"$Version\";" > /app/wwwroot/version.js

ENTRYPOINT ["dotnet", "EventHorizon.Blazor.BabylonJS.Server.dll"]
