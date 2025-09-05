# Inhaltsverzeichnis

- [Inhaltsverzeichnis](#inhaltsverzeichnis)
- [Generell](#generell)
  - [Repo-Aufbau](#repo-aufbau)
- [Sourcecode](#sourcecode)
  - [Instanzvariablen](#instanzvariablen)
    - [Private Instanzvariablen](#private-instanzvariablen)
    - [Public Instanzvariablen](#public-instanzvariablen)
    - [Klassenvariablen](#klassenvariablen)
    - [Konstanten](#konstanten)
    - [Methoden namen](#methoden-namen)
    - [Argumente](#argumente)
    - [Namespaces](#namespaces)
    - [Klassen](#klassen)
      - [Normale Klassen](#normale-klassen)
      - [Abstrakte Klassen](#abstrakte-klassen)
      - [Interfaces](#interfaces)
        - [Dateinamen](#dateinamen)
- [Datenmodell](#datenmodell)
  - [Entitäten](#entitäten)
  - [Schlüssel](#schlüssel)
    - [Primärschlussel](#primärschlussel)
    - [Fremdschlüssel](#fremdschlüssel)
- [API](#api)
  - [Struktur](#struktur)
  - [Namenskoventionen](#namenskoventionen)
    - [HTTP-Methoden](#http-methoden)
    - [Response-Statuscodes](#response-statuscodes)



# Generell
In Jiffy gilt generell für alles ausser der Dokumentation, dass nur Englisch zulässig ist.

## Repo-Aufbau
```
Root
├─assets
│ └─ Hier befinden sich die Assets für die Dokumentation.
├─docs
│ └─ Hier befinden sich die Dokumentationen.
└─Zeiterfassungssoftware
  ├─Zeiterfassungssoftware
  │ ├─Components
  │ │ └─ Hier befinden sich die ASP.NET-Komponenten und Seiten.
  │ ├─Data
  │ │ └─ Hier sind die Daten und der Datenbankkontext zu finden.
  │ ├─Mapper
  │ │ └─ In diesem Verzeichnis befinden sich die Mapper, die aus den Daten DTOs erzeugen.
  │ └─Controller
  │   └─ Verzeichnis mit allen API-Controllern.
  ├─Zeiterfassungssoftware.Client
  │ ├─Components
  │ ├─Pages
  │ │ └─ Hier befinden sich alle Webseiten, die auf dem Client liegen.
  │ └─Services
  │ │ └─ Hier befinden sich alle Services, die dafür zuständig sind, die API-Endpunkte anzusprechen.
  ├─Zeiterfassungssoftware.SharedData
  │  └─ Hier befinden sich Dateien, die von beiden Projekten genutzt werden, wie z. B. DTOs.
  └─Zeiterfassungssoftware.sln
```


# Sourcecode
- Für alles muss ein sinnvoller, klarer und aussagekräftiger Name vorhanden sein. 
- Pro Datei darf maximal eine Klasse vorhanden sein.

## Instanzvariablen
### Private Instanzvariablen
- Immer **camelCase**.
- Immer davor ein Unterstrich `_`.

### Public Instanzvariablen
- Immer** **PascalCase**

## Klassenvariablen
Bei Klassenvariablen wird **PascalCase** verwendet.

## Konstanten
- Konstanten werden mit dem **SCREAMING_SNAKE_CASE** benannt.


## Methoden namen
- Methoden werden **immer** in **PascalCase** benannt.
- Methoden sollten **beschreibende** Namen haben.

## Argumente
Argumente werden **immer** in **camelCase** benannt.

## Namespaces
Namespaces werden **immer** im **PascalCase** benannt.

## Klassen
### Normale Klassen
Normale Klassen müssen mit **PascalCase** benannt werden.

### Abstrakte Klassen 
Abstrakte Klassen müssen das Schlüsselwort `Abstract` vor ihrem Namen haben und befolgen ansonsten die gleichen Konventionen wie eine Klasse.

### Interfaces
Interfaces beginnen immer mit einem grossen `I` und folgen ansonsten den gleichen Naming-Conventions wie eine normale Klasse.

## Dateinamen
Dateinamen sind **immer gleich** dem **Klassennamen**


# Datenmodell
## Entitäten
- Entitäten **immer** im **Singular** benennen.
- Entitäten haben immer im PascalCase zu sein.

## Schlüssel
### Primärschlussel
- Jeder Datensatz muss über einen eindeutigen Primärschlüssel verfügen.
- Der Primärschlüssel **muss** eine **GUID** sein.
- Der Primärschlüssel **muss** immer den Namen `Id` tragen.

### Fremdschlüssel
- Fremdschlüssel müssen **PascalCase** verwenden.
- Es dürfen **keine Sonderzeichen** oder Trennzeichen verwendet werden.
- Fremdschlüssel müssen immer folgendes schema befolgen: `{TABELLEN_NAME}Id`

| Korrekt    | Falsch     |
|------------|------------|
| UserId     | User_Id    |
| EntryId    | entryid    |
| ActivityId | activityId |

# API

### Struktur

Alle Endpunkte folgen diesem Schema:

```https://{BASE_URL}/api/{API_VERSION}/{CONTROLLER}/```

| Platzhalter      | Beschreibung                       | Beispiel           |
|------------------|------------------------------------|--------------------|
| `BASE_URL`       | Host abhängig                      | `localhost:5252`   |
| `API_VERSION`    | Aktuelle API-Version               | `v1`               |
| `CONTROLLER`     | Ressource, **immer** im **Plural** | `entries`          |

## Namenskoventionen
Controller sind **immer** im Plural und werden kleingeschrieben. Müssen zwei Wörter benutzt werden, werden diese mit einem Bindestrich getrennt.

| Datenmodell | Controller          |
|----------   |---------------------|
| `Entry`     | `api/v1/entries`    | 
| `Activity`  | `api/v1/activities` |
| `Class`     | `api/v1/classes`    | 

### HTTP-Methoden  
In Jiffy's api sind folgende HTTP-Methoden zulässig:

| Methode  | Zweck                               |
|----------|-------------------------------------|
| `GET`    | Resourcen abrufen                   | 
| `POST`   | Neue Ressourcen erstellen           |
| `PATCH`  | Anderungen an Resourcen vornehmen   | 
| `DELETE` | Ressourcen löschen                  | 


### Response-Statuscodes
In Jiffy's API dürfen grundsätzlich alle HTTP-Statuscodes verwendet werden, sofern die hier aufgelisteten für die zu gebende Antwort keinen Sinn ergeben.


| Code | Bedeutung   | Verwendung               |
|------|-------------|--------------------------|
| 200  | OK          | Erfolgreicher Abruf      |
| 201  | Created     | Erfolgreiche Erstellung  |
| 204  | No Content  | Erfolgreiches Löschen    |
| 403  | Forbidden   | Keine Berechtigung       |
| 400  | Bad Request | Ungültige Daten          |
| 404  | Not Found   | Ressource nicht gefunden |
