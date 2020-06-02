# Mannschaftsverwaltung (.NET Framework) + GUI (ASP)	 
Dieses Projekt ist Teil einer Lernsituation im Unterrichtsfach Anwendungsentwicklung.



### Vorbereitung der Applikation

#### Die Datenbank muss konfiguriert werden
Die Anwendung funktioniert nur mit angeschlossener Datenbank. 
Beim Starten der Anwendung wird das Datenbankschema automatisch eingerichtet. 
Der erste Stat kann dadurch etwas länger dauern.
```
+ user
|-- username:    mvw_app
|-- password:    mvw_app
|-- hostname:    localhost     (Wildcard funktioniert ggf. nicht)
|-- permissions: SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, GRANT

+ database
|-- name:        mannschaftsverwaltung
```

Bei Änderungen des Datenbankusers muss die Datei "Application.properties" angepasst werden. Diese befindet sich im Root-Path des Projektordners.
```
DB_SERVER=127.0.0.1
DB_DATABASE=mannschaftsverwaltung
DB_UID=mvw_app
DB_PASSWORD=mvw_app
```

#### Datenbankstatus
Auf einigen Seiten ist ersichtlich, wenn die Verbindung zur Datenbank fehlgeschlagen ist. Die initale Konfiguration bzw. deren Funktionalität können Sie unter dem Menüpunkt Informationen ablesen.