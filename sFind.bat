@SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION
@ECHO OFF
COLOR 0F
SET fString=%1

FOR /F "tokens=*" %%A IN (patch_notes.txt) DO CALL :PARSE "%%A"

:PARSE
SET str1=%*
IF NOT "x%str1:update number=%"=="x%str1%" ECHO %str1%
IF NOT "x!str1:%fString%=!"=="x%str1%" ECHO %str1%
GOTO :EOF