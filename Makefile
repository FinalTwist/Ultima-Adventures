.PHONY: run stop

compile:
	mcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:NEWTIMERS -d:NEWPARENT -d:MONO -reference:System.Drawing -r:System.Drawing.Common.dll -r:System.Runtime.Remoting.dll -r:UOArchitectInterface.dll -r:OrbServerSDK.dll -recurse:Server/*.cs

build:
	docker buildx build --tag ultima-adventures --progress auto .

run:
	docker run -it -p 2593:2593 -v ./Backups:/opt/Ultima-Adventures/Backups -v ./Logs:/opt/Ultima-Adventures/Logs -v ./Saves:/opt/Ultima-Adventures/Saves ultima-adventures

stop:
	docker stop ultima-adventures

dev-deps:
	brew bundle install

all: dev-deps compile build run

rebuild: stop compile build run