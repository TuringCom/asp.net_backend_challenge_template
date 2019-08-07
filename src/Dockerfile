FROM mysql:5.7

MAINTAINER Malisa Ncube <malisa.n@turing.com>

ENV MYSQL_DATABASE 'turing'
ENV MYSQL_ROOT_PASSWORD 'root'
ENV MYSQL_USER 'turing'
ENV MYSQL_PASSWORD 'turing'

COPY ./sql/database.sql /docker-entrypoint-initdb.d/dump.sql


# Install .NET CLI dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
	    apt-utils \
		curl \
		supervisor \
		wget \
		apt-transport-https \
		lsb-release \
		ca-certificates \
		less \
        libc6 \
        libgcc1 \
        libgssapi-krb5-2 \
        libicu57 \
        liblttng-ust0 \
        libssl1.0.2 \
        libstdc++6 \
        zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Install .NET Core SDK
ENV DOTNET_SDK_VERSION 2.2.301

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_VERSION-linux-x64.tar.gz \
    && dotnet_sha512='63c54261b58b8d5e56326d0efb2ef3b25f120ae16e49f7bd470537da9cdddf96b1e0b6288c159ec808bd0b7e2cc9c93d0df2e4122948995e74a797c04098c599' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -zxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet


# Configure web servers to bind to port 8000 when present
ENV ASPNETCORE_URLS=http://+:80 \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

# Trigger first run experience by running arbitrary cmd to populate local package cache

# Project Files and Settings
ARG PROJECT=turing_backend
ARG PROJECT_DIR=/var/www/${PROJECT}

WORKDIR $PROJECT_DIR

COPY . .

RUN dotnet publish -c Release -o bin

WORKDIR $PROJECT_DIR/TuringBackend.Api/bin/

EXPOSE 80/tcp

COPY turing-entrypoint.sh /turing-entrypoint.sh

RUN ["chmod", "+x", "/turing-entrypoint.sh"]
ENTRYPOINT ["/turing-entrypoint.sh"]

