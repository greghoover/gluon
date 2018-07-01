@echo off
cd %~dp0
nuget.exe install %1 -o "C:\ProgramData\hase\vhosts\default" -ExcludeVersion -PreRelease