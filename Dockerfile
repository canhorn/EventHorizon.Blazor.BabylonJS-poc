# Stage 1 - Restore .NET Project
# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-restore
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS dotnet-restore
WORKDIR /source

ENV PATH="$PATH:/root/.dotnet/tools"

COPY ./*.sln ./

# Install Source Code Generation Tool
# RUN dotnet tool install --global EventHorizon.Blazor.TypeScript.Interop.Tool --version 0.1.12

### Build _generated/Blazor.BabylonJS.WASM from scratch
# RUN mkdir -p ./src/Shared && \ 
#     cd ./src/Shared && \
#     ehz-generate -f -a Blazor.BabylonJS.WASM -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/babylon.d.ts -s https://raw.githubusercontent.com/BabylonJS/Babylon.js/master/dist/gui/babylon.gui.d.ts -c Button -c MeshBuilder -c PointLight -c StandardMaterial -c HemisphericLight -c UniversalCamera -c Grid -c StackPanel -c SceneLoader -c BoundingBoxGizmo -c ArcFollowCamera -c ScrollViewer

# Copy the source project files
## Generated - Blazor.BabylonJS.WASM
COPY src/Shared/_generated/Blazor.BabylonJS.WASM/Blazor.BabylonJS.WASM.csproj ./src/Shared/_generated/Blazor.BabylonJS.WASM/Blazor.BabylonJS.WASM.csproj

## Main
COPY src/EventHorizon.Blazor.BabylonJS/EventHorizon.Blazor.BabylonJS.csproj ./src/EventHorizon.Blazor.BabylonJS/EventHorizon.Blazor.BabylonJS.csproj
COPY src/EventHorizon.Blazor.BabylonJS.Server/EventHorizon.Blazor.BabylonJS.Server.csproj ./src/EventHorizon.Blazor.BabylonJS.Server/EventHorizon.Blazor.BabylonJS.Server.csproj
COPY src/EventHorizon.Common/EventHorizon.Common.csproj ./src/EventHorizon.Common/EventHorizon.Common.csproj
COPY src/EventHorizon.Game.Client/EventHorizon.Game.Client.csproj ./src/EventHorizon.Game.Client/EventHorizon.Game.Client.csproj
COPY src/EventHorizon.Game.Client.Systems/EventHorizon.Game.Client.Systems.csproj ./src/EventHorizon.Game.Client.Systems/EventHorizon.Game.Client.Systems.csproj
COPY src/EventHorizon.Html.Interop/EventHorizon.Html.Interop.csproj ./src/EventHorizon.Html.Interop/EventHorizon.Html.Interop.csproj
COPY src/EventHorizon.Observer/EventHorizon.Observer.csproj ./src/EventHorizon.Observer/EventHorizon.Observer.csproj

## SDK
COPY src/SDK/EventHorizon.Game.Client.Core.Api/EventHorizon.Game.Client.Core.Api.csproj ./src/SDK/EventHorizon.Game.Client.Core.Api/EventHorizon.Game.Client.Core.Api.csproj
COPY src/SDK/EventHorizon.Game.Client.Engine.Api/EventHorizon.Game.Client.Engine.Api.csproj ./src/SDK/EventHorizon.Game.Client.Engine.Api/EventHorizon.Game.Client.Engine.Api.csproj
COPY src/SDK/EventHorizon.Game.Client.Engine.Gui.Api/EventHorizon.Game.Client.Engine.Gui.Api.csproj ./src/SDK/EventHorizon.Game.Client.Engine.Gui.Api/EventHorizon.Game.Client.Engine.Gui.Api.csproj
COPY src/SDK/EventHorizon.Game.Client.Scripts.SDK/EventHorizon.Game.Client.Scripts.SDK.csproj ./src/SDK/EventHorizon.Game.Client.Scripts.SDK/EventHorizon.Game.Client.Scripts.SDK.csproj
COPY src/SDK/EventHorizon.Game.Client.Scripts.SDK.Generator/EventHorizon.Game.Client.Scripts.SDK.Generator.csproj ./src/SDK/EventHorizon.Game.Client.Scripts.SDK.Generator/EventHorizon.Game.Client.Scripts.SDK.Generator.csproj
COPY src/SDK/EventHorizon.Game.Client.Systems.Api/EventHorizon.Game.Client.Systems.Api.csproj ./src/SDK/EventHorizon.Game.Client.Systems.Api/EventHorizon.Game.Client.Systems.Api.csproj

COPY src/SDK/Server/EventHorizon.Game.Server.Api/EventHorizon.Game.Server.Api.csproj ./src/SDK/Server/EventHorizon.Game.Server.Api/EventHorizon.Game.Server.Api.csproj
COPY src/SDK/Server/EventHorizon.Game.Server.ServerModule.Api/EventHorizon.Game.Server.ServerModule.Api.csproj ./src/SDK/Server/EventHorizon.Game.Server.ServerModule.Api/EventHorizon.Game.Server.ServerModule.Api.csproj

## Server
COPY src/Server/EventHorizon.Game.Server/EventHorizon.Game.Server.csproj ./src/Server/EventHorizon.Game.Server/EventHorizon.Game.Server.csproj

RUN dotnet restore


# Stage 2 - Build/Publish Client/SDK .NET Project
FROM dotnet-restore AS dotnet-build

ARG Version 

WORKDIR /source

COPY ./src ./src

RUN dotnet build /p:Version=$Version -c Release --no-restore

## Single folder publish of whole solution
RUN dotnet publish /p:Version=$Version --output /app/ --configuration Release --no-restore --no-build

## Create NuGet Artifacts
RUN dotnet build /p:Version=$Version -c Release --no-restore --output /artifacts/


# Stage 3 - Publish to NuGet
# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-nuget-push
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS dotnet-nuget-push
WORKDIR /app
COPY --from=dotnet-build /artifacts .
RUN find . -name '*.nupkg' -ls
ENTRYPOINT ["dotnet", "nuget", "push", "/app/*.nupkg"]
CMD ["--source", "https://api.nuget.org/v3/index.json"]


# Stage 4 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=dotnet-build /app .
ENTRYPOINT ["dotnet", "EventHorizon.Blazor.BabylonJS.Server.dll"]
