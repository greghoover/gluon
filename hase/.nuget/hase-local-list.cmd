@echo off
cd %~dp0
nuget list %1 -source "hase-local"