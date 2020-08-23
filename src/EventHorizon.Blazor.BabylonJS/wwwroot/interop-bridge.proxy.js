(function () {
    const enabled = false;
    window["ENABLED"] = false;
    const loggingProxy = (functionIdentifier) => {
        // An anonymous function wrapper helps you keep oldSomeFunction private
        var realFunction = window["blazorInterop"][functionIdentifier];

        window["blazorInterop"][functionIdentifier] = function () {
            if (arguments.length > 2
                && arguments[1] == "text") {
                console.log(functionIdentifier, JSON.stringify(arguments));
            }
            if (window["ENABLED"]) {
                console.log(functionIdentifier, JSON.stringify(arguments));
            }
            return realFunction(...arguments);
        };
    };
    const loggingPlaformProxy = (functionIdentifier) => {
        // An anonymous function wrapper helps you keep oldSomeFunction private
        var realFunction = window["blazorInterop"][functionIdentifier];

        window["blazorInterop"][functionIdentifier] = function (args) {
            if (window["ENABLED"]) {
                var root = Blazor.platform.readStringField(args);
                var identifier = Blazor.platform.readStringField(args, 4);
                console.log(functionIdentifier, { root, identifier });
            }
            return realFunction(args);
        };
    };
    if (enabled) {
        loggingPlaformProxy("get");
        loggingPlaformProxy("getClass");
        loggingPlaformProxy("getArrayClass");
        loggingProxy("getArrayClassSlow");
        loggingProxy("getArraySlow");
        loggingProxy("set");
        loggingProxy("call");
        loggingProxy("new");
        loggingProxy("func");
        loggingProxy("funcClass");
        loggingProxy("funcArray");
        loggingProxy("runScript");
        loggingProxy("funcCallback");
        loggingProxy("assemblyFuncCallback");
    }

})();