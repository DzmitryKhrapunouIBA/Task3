@echo off
setlocal

if '%1' == '' (
 echo Migration name not set
 goto :eof
)

dotnet ef migrations add %1 --project ..\IBA.Task3.Migration -s ..\IBA.Task3
dotnet ef migrations list --project ..\IBA.Task3.Migration -s ..\IBA.Task3
 
