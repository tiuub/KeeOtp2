@echo off
setlocal enableextensions enabledelayedexpansion

set arg1=%1
echo %1
cd %~dp0
set PlgXPath=%~dp0PlgX
set ReleasesPath=%~dp0Releases
set DebugPath=%~dp0Debug
set PlgXExculdeDirectoriesPath=%~dp0PlgXExcludeDirectories.txt
set PlgXExculdeFilesPath=%~dp0PlgXExcludeFiles.txt
set PackagesPath=%~dp0packages

set PlgXExculdeDirectories=
for /f "usebackq tokens=*" %%D in (%PlgXExculdeDirectoriesPath%) do (
    If NOT "!PlgXExculdeDirectories!"=="" (
        Set PlgXExculdeDirectories=!PlgXExculdeDirectories! "%%D"
    ) Else (
        Set PlgXExculdeDirectories="%%D"
    )
)

set PlgXExculdeFiles=
for /f "usebackq tokens=*" %%F in (%PlgXExculdeFilesPath%) do (
    If NOT "!PlgXExculdeFiles!"=="" (
        Set PlgXExculdeFiles=!PlgXExculdeFiles! "%%F"
    ) Else (
        Set PlgXExculdeFiles="%%F"
    )
)

echo %PlgXPath%

for /f "delims=" %%a in ('wmic OS Get localdatetime  ^| find "."') do set dt=%%a
set datestamp=%dt:~0,8%
set timestamp=%dt:~8,6%
set YYYY=%dt:~0,4%
set MM=%dt:~4,2%
set DD=%dt:~6,2%
set HH=%dt:~8,2%
set Min=%dt:~10,2%
set Sec=%dt:~12,2%

set stamp=%YYYY%-%MM%-%DD%_%HH%-%Min%-%Sec%

echo Start compiling process - %stamp%

echo Deleting existing PlgX folder
rmdir /s /q "%PlgXPath%"

echo Creating PlgX folder
mkdir "%PlgXPath%"

echo Copying files
robocopy "%~dp0." "%PlgXPath%" "KeeOtp2.sln" /NDL /NJH /NJS /NP /NS /NC
robocopy "%~dp0KeeOtp2" "%PlgXPath%\KeeOtp2" /MIR /E /NDL /NJH /NJS /NP /NS /NC /XF !PlgXExculdeFiles! /XD !PlgXExculdeDirectories!
robocopy "%~dp0Dependencies" "%PlgXPath%\Dependencies" /MIR /E /NDL /NJH /NJS /NP /NS /NC /XF !PlgXExculdeFiles! /XD !PlgXExculdeDirectories!

for /f "usebackq delims=|" %%f in (`dir /b "%PackagesPath%"`) do (
    echo Copying Dependencie: %%f
    robocopy "%PackagesPath%\%%f\lib" "%PlgXPath%\packages\%%f\lib" *.dll /MIR /E /NDL /NJH /NJS /NP /NS /NC /XF !PlgXExculdeFiles!
    
)

if exist "%ReleasesPath%" (
    echo Releases folder already exist
) ELSE (
    echo Create Releases folder
    mkdir "%ReleasesPath%"
)

if exist "%ReleasesPath%\Others" (
    echo Relases\Others folder already exist
) ELSE (
    echo Create Releases\Others folder
    mkdir "%ReleasesPath%\Others"
)

echo Compiling PlgX
"%PROGRAMFILES(X86)%\KeePass Password Safe 2\KeePass.exe" --plgx-create "%PlgXPath%" --plgx-prereq-net:4.0

echo Releasing PlgX

move /y PlgX.plgx "%ReleasesPath%\KeeOtp2.plgx"
If "%arg1%" == "--visualstudiomode" (
    if exist "%ReleasesPath%\Others\TESTBUILDS" (
        echo Releases\Others\TESTBUILDS folder already exist
    ) ELSE (
        echo Create Releases\Others\TESTBUILDS folder
        mkdir "%ReleasesPath%\Others\TESTBUILDS"
    )
    echo Copying PlgX
    copy "%ReleasesPath%\KeeOtp2.plgx" "%ReleasesPath%\Others\TESTBUILDS\KeeOtp2-%stamp%.plgx"
    robocopy "%ReleasesPath%" "%DebugPath%/Plugins" "KeeOtp2.plgx" /NDL /NJH /NJS /NP /NS /NC
) ELSE (
    echo Copying PlgX
    copy "%ReleasesPath%\KeeOtp2.plgx" "%ReleasesPath%\Others\KeeOtp2-%stamp%.plgx"
)

echo Cleaning up
rmdir /s /q "%PlgXPath%"

If "%arg1%" == "--visualstudiomode" start "" "%DebugPath%\KeePass.exe" --debug