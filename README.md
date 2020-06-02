# Mannschaftsverwaltung (.NET Framework) + GUI (ASP)	 
Dieses Projekt ist Teil einer Lernsituation im Unterrichtsfach Anwendungsentwicklung.



### Vorbereitung der Applikation

Die Anwendung funktioniert nur mit angeschlossener Datenbank.
1. Ein User muss in der Datenbank angelegt werden:
```
username:    mvw_app
password:    mvw_app
database:    mannschaftsverwaltung

Permissions: SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER
```
Bei Abweichungen muss die Datei "Application.properties" angepasst werden. Diese befindet sich im Root-Path des Projektordners.
```
DB_SERVER=127.0.0.1
DB_DATABASE=mannschaftsverwaltung
DB_UID=mvw_app
DB_PASSWORD=mvw_app
```

2. Die Datenbank "mannschaftsverwaltung" muss angelegt werden.

Beim Starten der Anwendung wird das Datenbankschema automatisch eingerichtet. 
Der erste Stat kann dadurch etwas länger dauern.
