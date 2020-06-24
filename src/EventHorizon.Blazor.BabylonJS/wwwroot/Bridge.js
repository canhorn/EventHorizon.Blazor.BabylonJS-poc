window.bridge = {
    send: (event) => {
        console.log("send: ", { event });
    },
    jsonParse: (json) => {
        const obj = JSON.parse(json);
        console.log({ obj })
    },
    jsonParseRaw: (args) => {
        var json = Blazor.platform.readStringField(args);
        const obj = JSON.parse(json);
        console.log({ obj })
    },
    jsonParseRaw: (args) => {
        var json = Blazor.platform.readStringField(args);
        const obj = JSON.parse(json);
        console.log({ obj })
    }
};

window["FuncCallbackClass"] = function () {
    const callbackActions = [];

    this.register = (callbackAction) => {
        if (typeof callbackAction === "function") {
            callbackActions.push(callbackAction);
        }
    };
    this.trigger = (times) => {
        for (var i = 0; i < times; i++) {
            callbackActions.forEach(
                action => action()
            );
        }
    }
}