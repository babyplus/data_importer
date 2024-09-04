# 准备编译和运行环境

```
[root@archlinux ~]# docker pull mcr.microsoft.com/dotnet/sdk:8.0
8.0: Pulling from dotnet/sdk
e4fff0779e6d: Already exists 
88496bad2e81: Pull complete 
c82d150f73e8: Pull complete 
1d2f51cd947a: Pull complete 
dfde04fc2ec3: Pull complete 
a870a55f5dde: Pull complete 
1c7123b4e8b3: Pull complete 
6a80a6870f65: Pull complete 
a70678546d2f: Pull complete 
Digest: sha256:8c6beed050a602970c3d275756ed3c19065e42ce6ca0809f5a6fcbf5d36fd305
Status: Downloaded newer image for mcr.microsoft.com/dotnet/sdk:8.0
mcr.microsoft.com/dotnet/sdk:8.0
[root@archlinux ~]# docker pull mcr.microsoft.com/dotnet/runtime:8.0
8.0: Pulling from dotnet/runtime
e4fff0779e6d: Already exists 
88496bad2e81: Already exists 
c82d150f73e8: Already exists 
1d2f51cd947a: Already exists 
dfde04fc2ec3: Already exists 
Digest: sha256:39b7ffd4f2fe8522aeef9fa57705ffc6a8123a857458e25403e2c8e39dd40167
Status: Downloaded newer image for mcr.microsoft.com/dotnet/runtime:8.0
mcr.microsoft.com/dotnet/runtime:8.0
```

# 准备数据库

## influxdb:2.7.9

```
[root@archlinux data_importer]# cat ../influxdb/start_influxdb.sh 
docker run -d -p 8086:8086 \
  -v "$PWD/data:/var/lib/influxdb2" \
  -v "$PWD/config:/etc/influxdb2" \
  -e DOCKER_INFLUXDB_INIT_MODE=setup \
  -e DOCKER_INFLUXDB_INIT_USERNAME=root \
  -e DOCKER_INFLUXDB_INIT_PASSWORD=rXX8Gra8 \
  -e DOCKER_INFLUXDB_INIT_ORG=HUANG \
  -e DOCKER_INFLUXDB_INIT_BUCKET=recordings \
  influxdb:2.7.9
```

# 项目

## donet版本

```
root@d7738967c706:/App# dotnet --version
8.0.401

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

### 添加命令行解析和JSON解析依赖

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

### 添加InfluxDB客户端依赖

```
root@ace21eabdcdb:/App# dotnet add package InfluxDB.Client
  Determining projects to restore...
  Writing /tmp/tmpmIyk4j.tmp
info : X.509 certificate chain validation will use the fallback certificate bundle at '/usr/share/dotnet/sdk/8.0.100/trustedroots/codesignctl.pem'.
info : X.509 certificate chain validation will use the fallback certificate bundle at '/usr/share/dotnet/sdk/8.0.100/trustedroots/timestampctl.pem'.
info : Adding PackageReference for package 'InfluxDB.Client' into project '/App/App.csproj'.
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/index.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/index.json 905ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/1.0.0/1.12.0-dev.1466.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/1.0.0/1.12.0-dev.1466.json 858ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/1.12.0/1.18.0.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/1.12.0/1.18.0.json 860ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/1.19.0-dev.3008/4.0.0-dev.5180.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/1.19.0-dev.3008/4.0.0-dev.5180.json 895ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.0.0-dev.5188/4.5.0-dev.7011.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.0.0-dev.5188/4.5.0-dev.7011.json 888ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.5.0-dev.7040/4.8.0-dev.8899.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.5.0-dev.7040/4.8.0-dev.8899.json 856ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.8.0-dev.8928/4.13.0-dev.11479.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.8.0-dev.8928/4.13.0-dev.11479.json 880ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.13.0-dev.11676/4.17.0-dev.14291.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.13.0-dev.11676/4.17.0-dev.14291.json 866ms
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.17.0-dev.14300/4.18.0-dev.14446.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/influxdb.client/page/4.17.0-dev.14300/4.18.0-dev.14446.json 863ms
info : Restoring packages for /App/App.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/influxdb.client/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/influxdb.client/index.json 907ms
info :   GET https://api.nuget.org/v3-flatcontainer/influxdb.client/4.17.0/influxdb.client.4.17.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/influxdb.client/4.17.0/influxdb.client.4.17.0.nupkg 899ms
info :   GET https://api.nuget.org/v3-flatcontainer/influxdb.client.core/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/jsonsubtypes/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.objectpool/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.net.http.headers/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.collections.immutable/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.configuration.configurationmanager/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.reactive/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/influxdb.client.core/index.json 280ms
info :   GET https://api.nuget.org/v3-flatcontainer/influxdb.client.core/4.17.0/influxdb.client.core.4.17.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.configuration.configurationmanager/index.json 500ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.configuration.configurationmanager/8.0.0/system.configuration.configurationmanager.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.objectpool/index.json 560ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.objectpool/8.0.7/microsoft.extensions.objectpool.8.0.7.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.configuration.configurationmanager/8.0.0/system.configuration.configurationmanager.8.0.0.nupkg 96ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.reactive/index.json 607ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.reactive/6.0.1/system.reactive.6.0.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.collections.immutable/index.json 612ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.collections.immutable/8.0.0/system.collections.immutable.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.net.http.headers/index.json 633ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.net.http.headers/2.2.8/microsoft.net.http.headers.2.2.8.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.objectpool/8.0.7/microsoft.extensions.objectpool.8.0.7.nupkg 82ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.reactive/6.0.1/system.reactive.6.0.1.nupkg 118ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.collections.immutable/8.0.0/system.collections.immutable.8.0.0.nupkg 142ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.net.http.headers/2.2.8/microsoft.net.http.headers.2.2.8.nupkg 149ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.buffers/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.security.cryptography.protecteddata/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/jsonsubtypes/index.json 1185ms
info :   GET https://api.nuget.org/v3-flatcontainer/jsonsubtypes/2.0.1/jsonsubtypes.2.0.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.buffers/index.json 291ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.buffers/4.5.0/system.buffers.4.5.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/influxdb.client.core/4.17.0/influxdb.client.core.4.17.0.nupkg 975ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/index.json 324ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/2.2.0/microsoft.extensions.primitives.2.2.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.security.cryptography.protecteddata/index.json 326ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.security.cryptography.protecteddata/8.0.0/system.security.cryptography.protecteddata.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/jsonsubtypes/2.0.1/jsonsubtypes.2.0.1.nupkg 122ms
info :   GET https://api.nuget.org/v3-flatcontainer/newtonsoft.json/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/system.buffers/4.5.0/system.buffers.4.5.0.nupkg 122ms
info :   GET https://api.nuget.org/v3-flatcontainer/csvhelper/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/nodatime/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/nodatime.serialization.jsonnet/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/restsharp/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.extensions.primitives/2.2.0/microsoft.extensions.primitives.2.2.0.nupkg 164ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.security.cryptography.protecteddata/8.0.0/system.security.cryptography.protecteddata.8.0.0.nupkg 171ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.memory/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.runtime.compilerservices.unsafe/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/newtonsoft.json/index.json 316ms
info :   GET https://api.nuget.org/v3-flatcontainer/newtonsoft.json/13.0.1/newtonsoft.json.13.0.1.nupkg
info :   GET https://api.nuget.org/v3-flatcontainer/newtonsoft.json/13.0.3/newtonsoft.json.13.0.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/nodatime.serialization.jsonnet/index.json 331ms
info :   GET https://api.nuget.org/v3-flatcontainer/nodatime.serialization.jsonnet/3.1.0/nodatime.serialization.jsonnet.3.1.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/newtonsoft.json/13.0.3/newtonsoft.json.13.0.3.nupkg 79ms
info :   OK https://api.nuget.org/v3-flatcontainer/newtonsoft.json/13.0.1/newtonsoft.json.13.0.1.nupkg 123ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/index.json 870ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/8.0.0/system.diagnostics.eventlog.8.0.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/restsharp/index.json 429ms
info :   GET https://api.nuget.org/v3-flatcontainer/restsharp/111.4.0/restsharp.111.4.0.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/nodatime.serialization.jsonnet/3.1.0/nodatime.serialization.jsonnet.3.1.0.nupkg 121ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.memory/index.json 425ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.memory/4.5.1/system.memory.4.5.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.diagnostics.eventlog/8.0.0/system.diagnostics.eventlog.8.0.0.nupkg 110ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.runtime.compilerservices.unsafe/index.json 484ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.runtime.compilerservices.unsafe/4.5.1/system.runtime.compilerservices.unsafe.4.5.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/restsharp/111.4.0/restsharp.111.4.0.nupkg 188ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.memory/4.5.1/system.memory.4.5.1.nupkg 154ms
info :   OK https://api.nuget.org/v3-flatcontainer/system.runtime.compilerservices.unsafe/4.5.1/system.runtime.compilerservices.unsafe.4.5.1.nupkg 175ms
info :   OK https://api.nuget.org/v3-flatcontainer/nodatime/index.json 935ms
info :   GET https://api.nuget.org/v3-flatcontainer/nodatime/3.1.11/nodatime.3.1.11.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/csvhelper/index.json 942ms
info :   GET https://api.nuget.org/v3-flatcontainer/csvhelper/33.0.1/csvhelper.33.0.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/nodatime/3.1.11/nodatime.3.1.11.nupkg 156ms
info :   OK https://api.nuget.org/v3-flatcontainer/csvhelper/33.0.1/csvhelper.33.0.1.nupkg 172ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.runtime.compilerservices.unsafe/4.7.1/system.runtime.compilerservices.unsafe.4.7.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.runtime.compilerservices.unsafe/4.7.1/system.runtime.compilerservices.unsafe.4.7.1.nupkg 113ms
info : Installed System.Runtime.CompilerServices.Unsafe 4.7.1 from https://api.nuget.org/v3/index.json with content hash zOHkQmzPCn5zm/BH+cxC1XbUS3P4Yoi3xzW7eRgVpDR2tPGSzyMZ17Ig1iRkfJuY0nhxkQQde8pgePNiA7z7TQ==.
info : Installed System.Memory 4.5.1 from https://api.nuget.org/v3/index.json with content hash sDJYJpGtTgx+23Ayu5euxG5mAXWdkDb4+b0rD0Cab0M1oQS9H0HXGPriKcqpXuiJDTV7fTp/d+fMDJmnr6sNvA==.
info : Installed System.Runtime.CompilerServices.Unsafe 4.5.1 from https://api.nuget.org/v3/index.json with content hash Zh8t8oqolRaFa9vmOZfdQm/qKejdqz0J9kr7o2Fu0vPeoH3BL1EOXipKWwkWtLT1JPzjByrF19fGuFlNbmPpiw==.
info : Installed System.Buffers 4.5.0 from https://api.nuget.org/v3/index.json with content hash pL2ChpaRRWI/p4LXyy4RgeWlYF2sgfj/pnVMvBqwNFr5cXg7CXNnWZWxrOONLg8VGdFB8oB+EG2Qw4MLgTOe+A==.
info : Installed Microsoft.Extensions.Primitives 2.2.0 from https://api.nuget.org/v3/index.json with content hash azyQtqbm4fSaDzZHD/J+V6oWMFaf2tWP4WEGIYePLCMw3+b2RQdj9ybgbQyjCshcitQKQ4lEDOZjmSlTTrHxUg==.
info : Installed System.Diagnostics.EventLog 8.0.0 from https://api.nuget.org/v3/index.json with content hash fdYxcRjQqTTacKId/2IECojlDSFvp7LP5N78+0z/xH7v/Tuw5ZAxu23Y6PTCRinqyu2ePx+Gn1098NC6jM6d+A==.
info : Installed System.Security.Cryptography.ProtectedData 8.0.0 from https://api.nuget.org/v3/index.json with content hash +TUFINV2q2ifyXauQXRwy4CiBhqvDEDZeVJU7qfxya4aRYOKzVBpN+4acx25VcPB9ywUN6C0n8drWl110PhZEg==.
info : Installed RestSharp 111.4.0 from https://api.nuget.org/v3/index.json with content hash GcOIOyYYFm3oCFHbGBwGtDawC9yqvv6m7Ixyt+qeI+/UrPNyCJK31VOPRfEgj5M+3AfigSP6a8jZyFtBZxouTA==.
info : Installed System.Configuration.ConfigurationManager 8.0.0 from https://api.nuget.org/v3/index.json with content hash JlYi9XVvIREURRUlGMr1F6vOFLk7YSY4p1vHo4kX3tQ0AGrjqlRWHDi66ImHhy6qwXBG3BJ6Y1QlYQ+Qz6Xgww==.
info : Installed CsvHelper 33.0.1 from https://api.nuget.org/v3/index.json with content hash fev4lynklAU2A9GVMLtwarkwaanjSYB4wUqO2nOJX5hnzObORzUqVLe+bDYCUyIIRQM4o5Bsq3CcyJR89iMmEQ==.
info : Installed InfluxDB.Client.Core 4.17.0 from https://api.nuget.org/v3/index.json with content hash M19x/C/cP1/3/30dDyA5YJVTMy27mq+D4w6HP0iaFyqSrbhwjTKtQaDUaeRYV2GWgGR13W5wYZ1URjOfi0cU5Q==.
info : Installed InfluxDB.Client 4.17.0 from https://api.nuget.org/v3/index.json with content hash EpgVIc1cgeUvUWglKkHdK8KK4Yf7m/cj9B0eankBc4feq3ECn1m9m2AgMR4hBmasd9/5PYCXMDh5F4njbbCM/w==.
info : Installed JsonSubTypes 2.0.1 from https://api.nuget.org/v3/index.json with content hash 1Po+Ypf0SjCeEKx5+C89Nb5OgTcqNvfS3uTI46MUM+KEp6Rq/M0h+vVsTUt/6DFRwZMTpsAJM4yJrZmEObVANA==.
info : Installed Microsoft.Net.Http.Headers 2.2.8 from https://api.nuget.org/v3/index.json with content hash wHdwMv0QDDG2NWDSwax9cjkeQceGC1Qq53a31+31XpvTXVljKXRjWISlMoS/wZYKiqdqzuEvKFKwGHl+mt2jCA==.
info : Installed Microsoft.Extensions.ObjectPool 8.0.7 from https://api.nuget.org/v3/index.json with content hash 2yLweyqmpuuFSRo+I3sLHMxmnAgNcI537kBJiyv49U2ZEqo00jZcG8lrnD8uCiOJp9IklYyTZULtbsXoFVzsjQ==.
info : Installed Newtonsoft.Json 13.0.1 from https://api.nuget.org/v3/index.json with content hash ppPFpBcvxdsfUonNcvITKqLl3bqxWbDCZIzDWHzjpdAHRFfZe0Dw9HmA0+za13IdyrgJwpkDTDA9fHaxOrt20A==.
info : Installed System.Collections.Immutable 8.0.0 from https://api.nuget.org/v3/index.json with content hash AurL6Y5BA1WotzlEvVaIDpqzpIPvYnnldxru8oXJU2yFxFUy3+pNXjXd1ymO+RA0rq0+590Q8gaz2l3Sr7fmqg==.
info : Installed Newtonsoft.Json 13.0.3 from https://api.nuget.org/v3/index.json with content hash HrC5BXdl00IP9zeV+0Z848QWPAoCr9P3bDEZguI+gkLcBKAOxix/tLEAAHC+UvDNPv4a2d18lOReHMOagPa+zQ==.
info : Installed System.Reactive 6.0.1 from https://api.nuget.org/v3/index.json with content hash rHaWtKDwCi9qJ3ObKo8LHPMuuwv33YbmQi7TcUK1C264V3MFnOr5Im7QgCTdLniztP3GJyeiSg5x8NqYJFqRmg==.
info : Installed NodaTime 3.1.11 from https://api.nuget.org/v3/index.json with content hash AYSiCHp1PLzWKVf7hEL3MJ0q9kzOWMNIaTVysXk4XKrDBzK5PF2wpd4LsAl+EIQ2Hbvu+vw4oFaexcXzCuY1lQ==.
info : Installed NodaTime.Serialization.JsonNet 3.1.0 from https://api.nuget.org/v3/index.json with content hash eEr9lXUz50TYr4rpeJG4TDAABkpxjIKr5mDSi/Zav8d6Njy6fH7x4ZtNwWFj0Vd+vIvEZNrHFQ4Gfy8j4BqRGg==.
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 120ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.08.17.05.29.13/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.08.17.05.29.13/2024.08.22.23.29.27/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.08.17.05.29.13/vulnerability.base.json 112ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.08.17.05.29.13/2024.08.22.23.29.27/vulnerability.update.json 128ms
info : Package 'InfluxDB.Client' is compatible with all the specified frameworks in project '/App/App.csproj'.
info : PackageReference for package 'InfluxDB.Client' version '4.17.0' added to file '/App/App.csproj'.
info : Writing assets file to disk. Path: /App/obj/project.assets.json
log  : Restored /App/App.csproj (in 10.3 sec).
root@ace21eabdcdb:/App# ls
App.csproj  Program.cs  bin  jobs  models  obj  test
root@ace21eabdcdb:/App# cat App.csproj 
﻿<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="CommandLineParser" Version="2.9.1" />
    <PackageReference Include="InfluxDB.Client" Version="4.17.0" />
    <PackageReference Include="NuGet.CommandLine" Version="6.11.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="YamlDotNet" Version="16.0.0" />
  </ItemGroup>

</Project>

```

## 开发环境脚本

```
[root@archlinux data_importer]# cat develop.sh 
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/sdk:8.0 bash

```

## 编译脚本

```
[root@archlinux data_importer]# cat compile.sh 
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/sdk:8.0 dotnet build

```

## 运行脚本

```
[root@archlinux data_importer]# cat run.sh 
# Verify the JSON file.
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/runtime:8.0 ./bin/Debug/net8.0/App json_verify -i /App/test/data.json
# Parse the JSON file and store the data in the influxDB.
docker run -it -v `pwd`/App:/App -v `pwd`/packages:/root/.nuget/packages -w /App --rm mcr.microsoft.com/dotnet/runtime:8.0 ./bin/Debug/net8.0/App influxdb -i /App/test/data.json -t "9OyYqdvvpKlIewQr1U4jrNOMufKw5vfM2rpkakQ1Jf34JFjP0gX7Jzc9j4rOQcXS1cDIGPXhtQXpOlDd6o4WIg==" -b "recordings" -O "HUANG" -a "http://192.168.56.1:8086" -T "Asia/Shanghai" -m "weather"
```

## 查看数据库

### InfluxDB

```
root@8dad910f2557:/# influx query "from(bucket:\"recordings\") |> range(start: 2024-08-30T03:56:00Z, stop: 2024-08-30T03:58:00Z)" | head
Result: _result
Table: keys: [_start, _stop, _field, _measurement, id, province]
                   _start:time                      _stop:time           _field:string     _measurement:string               id:string         province:string                      _time:time                  _value:float
------------------------------  ------------------------------  ----------------------  ----------------------  ----------------------  ----------------------  ------------------------------  ----------------------------
2024-08-30T03:56:00.000000000Z  2024-08-30T03:58:00.000000000Z                Humidity                 weather                ChangSha                   HuNan  2024-08-30T03:56:26.000000000Z                         0.668
2024-08-30T03:56:00.000000000Z  2024-08-30T03:58:00.000000000Z                Humidity                 weather                ChangSha                   HuNan  2024-08-30T03:56:27.000000000Z                         0.696
2024-08-30T03:56:00.000000000Z  2024-08-30T03:58:00.000000000Z                Humidity                 weather                ChangSha                   HuNan  2024-08-30T03:56:28.000000000Z                         0.674
2024-08-30T03:56:00.000000000Z  2024-08-30T03:58:00.000000000Z                Humidity                 weather                ChangSha                   HuNan  2024-08-30T03:56:29.000000000Z                         0.676
2024-08-30T03:56:00.000000000Z  2024-08-30T03:58:00.000000000Z                Humidity                 weather                ChangSha                   HuNan  2024-08-30T03:56:30.000000000Z                         0.662
2024-08-30T03:56:00.000000000Z  2024-08-30T03:58:00.000000000Z                Humidity                 weather                ChangSha                   HuNan  2024-08-30T03:56:31.000000000Z                          0.68

```

## 镜像打包脚本

```
[root@archlinux data_importer]# cat Dockerfile 
FROM registry.cn-hangzhou.aliyuncs.com/babyplus/get:a2211240e2f7.git.2_37_1 as SourceDownloader
RUN mkdir -p /tmp/src
WORKDIR /tmp/src
RUN git clone https://github.com/babyplus/data_importer.git

FROM mcr.microsoft.com/dotnet/sdk:8.0 as compiler
COPY --from=SourceDownloader /tmp/src/data_importer /tmp/src/data_importer
WORKDIR /tmp/src/data_importer/App
RUN dotnet build

FROM mcr.microsoft.com/dotnet/runtime:8.0
COPY --from=compiler /tmp/src/data_importer/App /App
COPY --from=compiler /tmp/src/data_importer/packages /root/.nuget/packages
WORKDIR /App 

```

# 快速开始

1. 准备数据库（略）
2. 下载镜像压缩包
3. 上传镜像压缩包到服务器
4. 解压镜像压缩包
5. 导入镜像
6. 运行
 
