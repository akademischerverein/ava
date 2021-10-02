@ECHO ON
REM Dieses Skript startet einen PostgreSQL Datenbankserver

REM Konfiguration einbinden
call "%~dp0\postgresql_config.bat"

"%~dp0\bin\pg_ctl" -D "%~dp0/data" -l logfile start
ECHO "Enter druecken, um PostgreSQL zu beenden"
pause
"%~dp0\bin\pg_ctl" -D "%~dp0/data" stop
