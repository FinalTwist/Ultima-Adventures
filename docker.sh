#!/bin/bash
docker build $(pwd) -t adventures/uaserver:1.0 &&
	docker run -itd adventures/uaserver:1.0
