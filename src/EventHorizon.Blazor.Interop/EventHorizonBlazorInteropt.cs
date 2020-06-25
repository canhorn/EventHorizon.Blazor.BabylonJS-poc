namespace EventHorizon.Blazor.Interop
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using Mono.WebAssembly.Interop;

    public static class EventHorizonBlazorInteropt
    {
        public static MonoWebAssemblyJSRuntime RUNTIME = new MonoWebAssemblyJSRuntime();
        public static IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// 
        /// Example:
        /// EventHorizonBlazorInteropt.Call(
        ///     /* CachedEntity */ this, 
        ///     "attachControl",
        ///     canvas,
        ///     preventDefault
        /// );
        /// </summary>
        /// <param name="args"></param>
        public static void Call(
            params object[] args
        )
        {
            RUNTIME.InvokeVoid(
                "blazorInterop.call",
                args
            );
        }

        public static CachedEntity Func(
            params object[] args
        )
        {
            // This might need to be JSRuntime
            return RUNTIME.Invoke<CachedEntity>(
                "blazorInterop.func",
                args
            );
        }

        /// https://github.com/jhwcn/BlazorUnmarshalledCanvas/blob/master/UmCanvas/Canvas.cs
        public static T Get<T>(
            string root,
            string identifier
        )
        {
            return RUNTIME.InvokeUnmarshalled<ValueTuple<string, string>, T>(
                "blazorInterop.get",
                ValueTuple.Create(
                    root,
                    identifier
                )
            );
        }

        public static CachedEntity New(
            params object[] args
        )
        {
            return RUNTIME.Invoke<CachedEntity>(
                "blazorInterop.new",
                args
            );
        }

        public static ValueTask RunScript(
            string methodName,
            string script,
            object args
        )
        {
            return JSRuntime.InvokeVoidAsync(
                "blazorInterop.runScript",
                new JavaScriptMethodRunner
                {
                    MethodName = methodName,
                    Script = script,
                    Args = args
                }
            );
        }

        public static ValueTask FuncCallback<T>(
            CachedEntity entity,
            string funcCallbackName,
            string referenceMethod,
            DotNetObjectReference<T> invokableReference
        ) where T : class
        {
            return JSRuntime.InvokeVoidAsync(
                "blazorInterop.funcCallback",
                entity,
                funcCallbackName,
                referenceMethod,
                invokableReference
            );
        }

        public static ValueTask<CachedEntity> Set(
            params object[] args
        )
        {
            return JSRuntime.InvokeAsync<CachedEntity>(
                "blazorInterop.set",
                args
            );
        }
    }
    public struct JavaScriptMethodRunner
    {
        public string MethodName { get; set; }
        public string Script { get; set; }
        public object Args { get; set; }
    }
    //public struct FuncCallbackArgs
    //{
    //    public CachedEntity entity { get; set; }
    //    public string funcCallbackName { get; set; }
    //    public string referenceMethod { get; set; }
    //    public object _invokableReference { get; set; }
    //}
}
