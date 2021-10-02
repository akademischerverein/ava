@ECHO ON
REM Dieses Skript initialisiert das Datenverzeichnis für einen PostgreSQL Datenbankserver

REM Konfiguration einbinden
call "%~dp0\postgresql_config.bat"

"%~dp0\bin\initdb" -U postgres -A trust --encoding=UTF8

ECHO "PostgreSQL Datenverzeichnis initialisiert"
ECHO "Enter druecken um fortzufahren"
pause
