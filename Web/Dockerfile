FROM microsoft/aspnet:4.6.2
# disable DNS caching to account for services changing their IP address
RUN reg add HKLM\SYSTEM\CurrentControlSet\Services\DNS\Parameters\MaxCacheTtl /v 0
ARG source
WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .
