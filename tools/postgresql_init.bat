@ECHO ON
REM Dieses Skript initialisiert das Datenverzeichnis für einen PostgreSQL Datenbankserver

REM Konfiguration einbinden
call "%~dp0\postgresql_config.bat"

REM Datenverzeichnis initialisieren
"%~dp0\bin\initdb" -U postgres -A trust --encoding=UTF8

REM PostgreSQL Server starten
"%~dp0\bin\pg_ctl" -D "%~dp0/data" -l logfile start

REM Benutzer und Datenbank anlegen
"%~dp0\bin\createuser" -U postgres ava_storagebackend
"%~dp0\bin\createdb" -U postgres --owner=ava_storagebackend --encoding=UTF8 --locale=de_DE.UTF-8 --template=template0 ava

REM PostgreSQL Server stoppen
"%~dp0\bin\pg_ctl" -D "%~dp0/data" stop

ECHO "PostgreSQL Datenverzeichnis initialisiert"
ECHO "Enter druecken um fortzufahren"
pause
