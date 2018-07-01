@echo off
cd %~dp0
nuget sources remove -name "hase-local"
nuget sources add -name "hase-local" -source "c:\ProgramData\hase\local-nuget-repo"