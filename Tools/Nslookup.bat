@echo off

title NSLookup %*
cls
echo.
echo ------ NSLookup result for %* ------
echo.
echo.

%SystemRoot%\system32\nslookup.exe %*

echo.
echo ------ NSLookup results end ------

pause
@echo on

exit