docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/sdk:8.0 bash
