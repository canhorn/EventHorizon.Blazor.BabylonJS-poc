## Build

~~~ bash
docker build -t ehz/editor/automation:dev .

docker run --mount type=bind,source=$(pwd)/Config.Local.json,target=/source/bin/Release/net6.0/Config.Override.json ehz/editor/automation:dev
~~~