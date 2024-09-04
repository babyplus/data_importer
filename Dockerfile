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
