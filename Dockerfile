FROM mcr.microsoft.com/dotnet/runtime:8.0
COPY App /App 
COPY packages /root/.nuget/packages
WORKDIR /App 
