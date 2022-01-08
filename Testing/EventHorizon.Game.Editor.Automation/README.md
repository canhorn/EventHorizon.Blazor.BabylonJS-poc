
# Development

## Configuration 

- WebHost:SlowMo 
    - Add a delay to each action during a test, helps with debugging by slowing down the tests.
- WebHost:SlowMoDelay
    - The seconds to delay before each action.
- (Checkout Config.json for defaults and more values)

# Build

~~~ bash
docker build -t ehz/editor/automation:dev .

docker run --mount type=bind,source=$(pwd)/Config.Local.json,target=/source/bin/Release/net6.0/Config.Override.json ehz/editor/automation:dev
~~~