@echo on

set servicepath=%cd%\bin\x86\Debug\DataUtils.v1.1.dll
echo %servicepath%
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
c:

Installutil %servicepath%
pause