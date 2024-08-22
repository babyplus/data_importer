docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/runtime:8.0 ./bin/Debug/net8.0/App -i /App/test/data.json
