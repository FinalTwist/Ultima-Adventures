SET DOTNET=C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
SET PATH=%DOTNET%
csc.exe /debug /nowarn:0618 /nologo /out:.\test.exe /unsafe /recurse:*.cs
