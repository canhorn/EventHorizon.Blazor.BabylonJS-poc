using NUnit.Framework;

// We limit the running browser to 2.
// The loading of WebAssembly resources are very intensive for CPU and RAM.
[assembly: LevelOfParallelism(2)]
