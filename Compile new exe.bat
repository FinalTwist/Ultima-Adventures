@echo off
REM
REM -- RunUO compile script for .NET 4.0 --
REM
REM The full .NET framework needs to be installed for this script.
REM i.e. not the "Client Profile", as it is missing several required DLLs.
REM
set targetfile=Adventures Server.exe

if exist "%targetfile%" (
	echo Deleting binary...
	del "%targetfile%" 1>NUL 2>NUL
	
	if exist "%targetfile%" (
		echo Failed!
		echo.
		echo Is "%targetfile%" in use?
		echo.
		goto end
	) else (
		echo Success.
		echo.
	)
)

echo Recompiling...
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /unsafe /out:"%targetfile%" /recurse:Server\*.cs /win32icon:Server\runuo.ico /optimize /main:Server.Core

:end
pause
