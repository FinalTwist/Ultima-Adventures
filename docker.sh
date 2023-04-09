#!/bin/bash
docker build /home/mythos/repos/test2/Ultima-Adventures -t adventures/uaserver:1.0 &&
	docker run -itd adventures/uaserver:1.0
