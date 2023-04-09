FROM ubuntu:22.04
ARG DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y wget && \
  wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
  dpkg -i packages-microsoft-prod.deb && \
  apt-get update && \
  apt-get install -y apt-transport-https && \
  apt-get install -y dotnet-sdk-6.0 zlib1g-dev mono-complete make 

RUN mkdir /opt/runuo
WORKDIR /opt/runuo
COPY ./ /opt/runuo
EXPOSE 2593
RUN /opt/runuo/compile.sh
