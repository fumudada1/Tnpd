@echo off
set /p var=請輸入此次更新說明:
echo %var%
git add .
git commit -m %var%
pause
