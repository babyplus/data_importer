# Verify the JSON file.
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/runtime:8.0 ./bin/Debug/net8.0/App json_verify -i /App/test/data.json
# Parse the JSON file and store the data in the influxDB.
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/runtime:8.0 ./bin/Debug/net8.0/App influxdb -i /App/test/data.json -t "9OyYqdvvpKlIewQr1U4jrNOMufKw5vfM2rpkakQ1Jf34JFjP0gX7Jzc9j4rOQcXS1cDIGPXhtQXpOlDd6o4WIg==" -b "recordings" -O "HUANG" -a "http://192.168.56.1:8086" -T "Asia/Shanghai" -m "weather"
