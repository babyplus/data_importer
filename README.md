# 准备编译和运行环境

```
[root@archlinux tool]# docker pull registry.cn-hangzhou.aliyuncs.com/babyplus/get:a231129dc4b6.sdk.8_0
a231129dc4b6.sdk.8_0: Pulling from babyplus/get
Digest: sha256:54290a4faddce14b1a3fcbc3b61176acea84c2ec4c739311ec00741d3f22a08b
Status: Image is up to date for registry.cn-hangzhou.aliyuncs.com/babyplus/get:a231129dc4b6.sdk.8_0
registry.cn-hangzhou.aliyuncs.com/babyplus/get:a231129dc4b6.sdk.8_0
[root@archlinux tool]# 
[root@archlinux tool]# docker pull registry.cn-hangzhou.aliyuncs.com/babyplus/get:a23113098cc3.runtime.8_0
a23113098cc3.runtime.8_0: Pulling from babyplus/get
Digest: sha256:690bcef83b764e37aa07e913a6826957a89d9bf2e1c177505364214824c0ab68
Status: Image is up to date for registry.cn-hangzhou.aliyuncs.com/babyplus/get:a23113098cc3.runtime.8_0
registry.cn-hangzhou.aliyuncs.com/babyplus/get:a23113098cc3.runtime.8_0
[root@archlinux tool]# 
```

# 项目

## donet版本

```
root@d7738967c706:/App# dotnet --version
8.0.100

```

## 创建项目

```
root@d7738967c706:/App# dotnet new create console
The template "Console App" was created successfully.

Processing post-creation actions...
Restoring /App/App.csproj:
  Determining projects to restore...
  Restored /App/App.csproj (in 1.15 sec).
Restore succeeded.

```

### 运行生成的模板程序

```
root@d7738967c706:/App# ls
App.csproj  Program.cs  obj
root@d7738967c706:/App# dotnet build
MSBuild version 17.8.3+195e7f5a3 for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  App -> /App/bin/Debug/net8.0/App.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:06.11
root@d7738967c706:/App# ls
App.csproj  Program.cs  bin  obj
root@d7738967c706:/App# ./bin/Debug/net8.0/App 
Hello, World!

```

## 准备依赖

```
root@d7738967c706:/App# ls
App.csproj  Program.cs  obj
root@d7738967c706:/App# dotnet add package CommandLineParser --version 2.9.1
  Determining projects to restore...
  Writing /tmp/tmpbBv2Fo.tmp
info : X.509 certificate chain validation will use the fallback certificate bundle at '/usr/share/dotnet/sdk/8.0.100/trustedroots/codesignctl.pem'.
info : X.509 certificate chain validation will use the fallback certificate bundle at '/usr/share/dotnet/sdk/8.0.100/trustedroots/timestampctl.pem'.
info : Adding PackageReference for package 'CommandLineParser' into project '/App/App.csproj'.
info : Restoring packages for /App/App.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/commandlineparser/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/commandlineparser/index.json 1500ms
info :   GET https://api.nuget.org/v3-flatcontainer/commandlineparser/2.9.1/commandlineparser.2.9.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/commandlineparser/2.9.1/commandlineparser.2.9.1.nupkg 122ms
info : Installed CommandLineParser 2.9.1 from https://api.nuget.org/v3/index.json with content hash OE0sl1/sQ37bjVsPKKtwQlWDgqaxWgtme3xZz7JssWUzg5JpMIyHgCTY9MVMxOg48fJ1AgGT3tgdH5m/kQ5xhA==.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2024.08.14.06.06.01/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2024.08.14.06.06.01/2024.08.14.06.06.01/vulnerability.update.json
info : Package 'CommandLineParser' is compatible with all the specified frameworks in project '/App/App.csproj'.
info : PackageReference for package 'CommandLineParser' version '2.9.1' added to file '/App/App.csproj'.
info : Writing assets file to disk. Path: /App/obj/project.assets.json
log  : Restored /App/App.csproj (in 3.49 sec).
root@d7738967c706:/App# dotnet add package YamlDotNet --version 16.0.0
  Determining projects to restore...
  Writing /tmp/tmpau2mkw.tmp
info : X.509 certificate chain validation will use the fallback certificate bundle at '/usr/share/dotnet/sdk/8.0.100/trustedroots/codesignctl.pem'.
info : X.509 certificate chain validation will use the fallback certificate bundle at '/usr/share/dotnet/sdk/8.0.100/trustedroots/timestampctl.pem'.
info : Adding PackageReference for package 'YamlDotNet' into project '/App/App.csproj'.
info : Restoring packages for /App/App.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/yamldotnet/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/yamldotnet/index.json 327ms
info :   GET https://api.nuget.org/v3-flatcontainer/yamldotnet/16.0.0/yamldotnet.16.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/yamldotnet/16.0.0/yamldotnet.16.0.0.nupkg 108ms
info : Installed YamlDotNet 16.0.0 from https://api.nuget.org/v3/index.json with content hash kZ4jR5ltFhnjaUqK9x81zXRIUTH4PTXTTEmJDNQdkDLQhcv+2Nl19r0dCSvPW1mstOYBfXTnjdieRbUO6gHMDw==.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2024.08.14.06.06.01/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2024.08.14.06.06.01/2024.08.14.06.06.01/vulnerability.update.json
info : Package 'YamlDotNet' is compatible with all the specified frameworks in project '/App/App.csproj'.
info : PackageReference for package 'YamlDotNet' version '16.0.0' added to file '/App/App.csproj'.
info : Writing assets file to disk. Path: /App/obj/project.assets.json
log  : Restored /App/App.csproj (in 2.9 sec).
root@d7738967c706:/App# 

```

## 开发环境脚本

```
[root@archlinux tool]# cat develop.sh 
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/sdk:8.0 bash

```

## 编译脚本

```
[root@archlinux tool]# cat compile.sh 
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/sdk:8.0 dotnet build

```

## 运行脚本

```
[root@archlinux tool]# cat run.sh 
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/runtime:8.0 ./bin/Debug/net8.0/App -i /App/test/data.json

``` 
