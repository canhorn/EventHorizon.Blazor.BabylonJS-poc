
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
