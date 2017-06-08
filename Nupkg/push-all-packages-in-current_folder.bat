@echo off
setlocal enabledelayedexpansion
set filepath=%cd%
FOR /R %filepath% %%i IN (*.nupkg) DO (
set file=%%i
call ./nugetpush.bat !file!

)
pause
exit