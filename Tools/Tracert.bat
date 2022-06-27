@echo off

title Tracert %*
cls
echo.
echo ------ Tracert result for %* ------

%SystemRoot%\system32\Tracert.exe %*


echo.
echo ------ Tracert results end ------

pause
@echo on

exit