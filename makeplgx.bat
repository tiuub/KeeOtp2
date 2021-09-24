@@ -1,117 +0,0 @@
@echo off
setlocal enableextensions enabledelayedexpansion

set arg1=%1
echo %1
cd %~dp0
set SolutionName=KeeOtp2
set ProjectName=KeeOtp2
set SolutionPath=%~dp0
set PlgXPath=%SolutionPath%PlgX
set ReleasesPath=%SolutionPath%Releases
set DebugPath=%SolutionPath%Debug
set PlgXExculdeDirectoriesPath=%SolutionPath%PlgXExcludeDirectories.txt
set PlgXExculdeFilesPath=%SolutionPath%PlgXExcludeFiles.txt
set PackagesPath=%SolutionPath%packages

set PlgXExculdeDirectories=
for /f "usebackq tokens=*" %%D in ("%PlgXExculdeDirectoriesPath%") do (
    If NOT "!PlgXExculdeDirectories!"=="" (
        Set PlgXExculdeDirectories=!PlgXExculdeDirectories! "%%D"
    ) Else (
        Set PlgXExculdeDirectories="%%D"
    )
)

set PlgXExculdeFiles=
for /f "usebackq tokens=*" %%F in ("%PlgXExculdeFilesPath%") do (
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
robocopy "%SolutionPath%." "%PlgXPath%" "%SolutionName%.sln" /NDL /NFL /NJH /NJS /NP /NS /NC
robocopy "%SolutionPath%%ProjectName%" "%PlgXPath%\%ProjectName%" /MIR /E /NDL /NFL /NJH /NJS /NP /NS /NC /XF !PlgXExculdeFiles! /XD !PlgXExculdeDirectories!
robocopy "%SolutionPath%Dependencies" "%PlgXPath%\Dependencies" /MIR /E /NDL /NFL /NJH /NJS /NP /NS /NC /XF !PlgXExculdeFiles! /XD !PlgXExculdeDirectories!

for /F "tokens=3 delims=<>" %%a in ( 'find /i "<HintPath>" ^< "%SolutionPath%%ProjectName%\%ProjectName%.csproj"' ) do (
    if %%~xa == .dll (
        echo [+] %%~na%%~xa
        FOR %%i IN ("%SolutionPath%%ProjectName%\%%a") DO (
            set origin=%%~dpi
        )
        
        FOR %%i IN ("%PlgXPath%\%ProjectName%\%%a") DO (
            set destination=%%~dpi
        )
        
        robocopy "!origin!\" "!destination!\" "%%~na%%~xa" /MIR /NDL /NFL /NJH /NJS /NP /NS /NC /XF > nul
    ) else (
        echo [-] %%~na%%~xa
    )
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

move /y PlgX.plgx "%ReleasesPath%\%SolutionName%.plgx"
If "%arg1%" == "--visualstudiomode" (
    if exist "%ReleasesPath%\Others\TESTBUILDS" (
        echo Releases\Others\TESTBUILDS folder already exist
    ) ELSE (
        echo Create Releases\Others\TESTBUILDS folder
        mkdir "%ReleasesPath%\Others\TESTBUILDS"
    )
    echo Copying PlgX
    copy "%ReleasesPath%\%SolutionName%.plgx" "%ReleasesPath%\Others\TESTBUILDS\%SolutionName%-%stamp%.plgx"
    robocopy "%ReleasesPath%" "%DebugPath%/Plugins" "%SolutionName%.plgx" /NDL /NFL /NJH /NJS /NP /NS /NC
) ELSE (
    echo Copying PlgX
    copy "%ReleasesPath%\%SolutionName%.plgx" "%ReleasesPath%\Others\%SolutionName%-%stamp%.plgx"
)

echo Cleaning up
rmdir /s /q "%PlgXPath%"

If "%arg1%" == "--visualstudiomode" start "" "%DebugPath%\KeePass.exe" --debug