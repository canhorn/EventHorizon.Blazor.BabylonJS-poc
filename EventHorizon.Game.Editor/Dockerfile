# Stage 1 - Restore .NET Project
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-restore
WORKDIR /source

ENV PATH="$PATH:/root/.dotnet/tools"

COPY ./EventHorizon.Game.Editor/EventHorizon.Game.Editor.sln ./EventHorizon.Game.Editor/EventHorizon.Game.Editor.sln

# Generated Source
## Generated - Blazor.BabylonJS.WASM
COPY src/Shared/_generated/Blazor.BabylonJS.WASM/Blazor.BabylonJS.WASM.csproj ./src/Shared/_generated/Blazor.BabylonJS.WASM/Blazor.BabylonJS.WASM.csproj

# Game Client
## Main
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
COPY src/SDK/EventHorizon.Game.Client.Systems.Api/EventHorizon.Game.Client.Systems.Api.csproj ./src/SDK/EventHorizon.Game.Client.Systems.Api/EventHorizon.Game.Client.Systems.Api.csproj

COPY src/SDK/Server/EventHorizon.Game.Server.Api/EventHorizon.Game.Server.Api.csproj ./src/SDK/Server/EventHorizon.Game.Server.Api/EventHorizon.Game.Server.Api.csproj
COPY src/SDK/Server/EventHorizon.Game.Server.ServerModule.Api/EventHorizon.Game.Server.ServerModule.Api.csproj ./src/SDK/Server/EventHorizon.Game.Server.ServerModule.Api/EventHorizon.Game.Server.ServerModule.Api.csproj

## Server
COPY src/Server/EventHorizon.Game.Server/EventHorizon.Game.Server.csproj ./src/Server/EventHorizon.Game.Server/EventHorizon.Game.Server.csproj

# Editor
## Editor - Main Source
COPY EventHorizon.Game.Editor/Client/EventHorizon.Game.Editor.Client.csproj ./EventHorizon.Game.Editor/Client/EventHorizon.Game.Editor.Client.csproj
COPY EventHorizon.Game.Editor/Api/EventHorizon.Game.Editor.Api/EventHorizon.Game.Editor.Api.csproj ./EventHorizon.Game.Editor/Api/EventHorizon.Game.Editor.Api/EventHorizon.Game.Editor.Api.csproj

COPY EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core/EventHorizon.Game.Editor.Core.csproj ./EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core/EventHorizon.Game.Editor.Core.csproj
COPY EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core.Api/EventHorizon.Game.Editor.Core.Api.csproj ./EventHorizon.Game.Editor/Core/EventHorizon.Game.Editor.Core.Api/EventHorizon.Game.Editor.Core.Api.csproj

COPY EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone/EventHorizon.Game.Editor.Zone.csproj ./EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone/EventHorizon.Game.Editor.Zone.csproj
COPY EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone.Api/EventHorizon.Game.Editor.Zone.Api.csproj ./EventHorizon.Game.Editor/Zone/EventHorizon.Game.Editor.Zone.Api/EventHorizon.Game.Editor.Zone.Api.csproj

COPY EventHorizon.Game.Editor/Shared/EventHorizon.Game.Editor.Shared.csproj ./EventHorizon.Game.Editor/Shared/EventHorizon.Game.Editor.Shared.csproj

## Editor - Server
COPY EventHorizon.Game.Editor/Server/EventHorizon.Game.Editor.Server.csproj ./EventHorizon.Game.Editor/Server/EventHorizon.Game.Editor.Server.csproj

RUN dotnet restore ./EventHorizon.Game.Editor/EventHorizon.Game.Editor.sln


# Stage 2 - Build/Publish Client/Editor/SDK .NET Project
FROM dotnet-restore AS dotnet-build

ARG Version 

WORKDIR /source

COPY ./src ./src
COPY ./EventHorizon.Game.Editor ./EventHorizon.Game.Editor

## Single folder publish of whole project
RUN dotnet publish --output /app/ --configuration Release --no-restore ./EventHorizon.Game.Editor


# Stage 3 - Editor Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=dotnet-build /app .
ENTRYPOINT ["dotnet", "EventHorizon.Game.Editor.Server.dll"]
