# Tested on mono:6.12.0.182
FROM mono:6.12

# Save time
RUN echo "force-unsafe-io" > /etc/dpkg/dpkg.cfg.d/02apt-speedup
# Save space
RUN echo "Acquire::http {No-Cache=True;};" > /etc/apt/apt.conf.d/no-cache
# Add the zlib1g-dev package
RUN echo "deb http://security.debian.org/debian-security buster/updates main" >> /etc/apt/sources.list
RUN apt-get -qq update && apt-get -qq --yes install zlib1g-dev

# Take only what the server needs
COPY ./Data/ /opt/Ultima-Adventures/Data/
COPY ./Files/ /opt/Ultima-Adventures/Files/
# If you'd like to be able to modify the scripts without having to rebuild the image, then remove the copy of the scripts
# and bind mount your scripts folder from the host - like the Logs & Saves.
COPY ./Scripts/ /opt/Ultima-Adventures/Scripts/
COPY ./Server/ /opt/Ultima-Adventures/Server/
COPY ./*.dll /opt/Ultima-Adventures/
COPY ./*.exe /opt/Ultima-Adventures/

WORKDIR /opt/Ultima-Adventures

CMD ["mono", "./RunUO.exe"]

# Port is set in "Scripts/Server Functions/Misc/SocketOptions.cs"
EXPOSE 2593/tcp
