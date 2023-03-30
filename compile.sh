#!/bin/bash
mcs -optimize+ -unsafe -t:exe -out:AdvServ.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:MONO -recurse:"Server/*.cs" "/reference:System.Drawing.dll"
