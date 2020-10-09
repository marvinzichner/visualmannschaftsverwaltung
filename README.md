# Mannschaftsverwaltung (.NET Framework) + GUI (ASP)	 
Dieses Projekt ist Teil einer Lernsituation im Unterrichtsfach Anwendungsentwicklung.

### Vorbereitung der Applikation

#### Die Datenbank muss konfiguriert werden
Die Anwendung funktioniert nur mit einer angeschlossenen Datenbank. 
Beim Starten der Anwendung wird das Datenbankschema automatisch eingerichtet. 
Der erste Start kann dadurch wenige Sekunden dauern.

In dem Datenbanksystem sollte ein User angelegt werden, der nur die nötigsten Rechte erhält. Dazu kann folgende Matrix betrachtet werden:
```
+ user
|-- username:    mvw_app
|-- password:    mvw_app
|-- access from hostname:    localhost     (Wildcard funktioniert ggf. nicht)
|-- permissions: SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, GRANT

+ database (wird automatisch erstellt)
|-- name:        mannschaftsverwaltung
```

Bei Änderungen muss die Datei "Application.properties" angepasst werden. Hier wird auch der Hostname angegeben. Diese Datei befindet sich im Root-Path des Projektordners.
```
DB_SERVER=127.0.0.1
DB_DATABASE=mannschaftsverwaltung
DB_UID=mvw_app
DB_PASSWORD=mvw_app
```