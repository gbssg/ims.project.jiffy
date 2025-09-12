# Inhaltsverzeichnis

- [1 Generell](#1-generell)
  - [1.1 Repo-Aufbau](#11-repo-aufbau)
- [2 Sourcecode](#2-sourcecode)
  - [2.1 Instanzvariablen](#21-instanzvariablen)
    - [2.1.1 Private Instanzvariablen](#211-private-instanzvariablen)
    - [2.1.2 Public Instanzvariablen](#212-public-instanzvariablen)
    - [2.2 Klassenvariablen](#22-klassenvariablen)
    - [2.3 Konstanten](#23-konstanten)
    - [2.4 Methoden namen](#24-methoden-namen)
    - [2.5 Argumente](#25-argumente)
    - [2.6 Namespaces](#26-namespaces)
    - [2.7 Klassen](#27-klassen)
      - [2.7.1 Normale Klassen](#271-normale-klassen)
      - [2.7.2 Abstrakte Klassen](#272-abstrakte-klassen)
      - [2.7.3 Interfaces](#273-interfaces)
        - [2.8 Dateinamen](#28-dateinamen)
- [3 Datenmodell](#3-datenmodell)
  - [3.1 Entitäten](#31-entitäten)
  - [3.2 Schlüssel](#32-schlüssel)
    - [3.2.1 Primärschlussel](#321-primärschlussel)
    - [3.2.2 Fremdschlüssel](#322-fremdschlüssel)
- [4 API](#4-api)
  - [4.1 Struktur](#41-struktur)
  - [4.2 Namenskoventionen](#42-namenskoventionen)
  - [4.3 HTTP-Methoden](#43-http-methoden)
  - [4.4 Response-Statuscodes](#44-response-statuscodes)

# 1 Generell
In Jiffy gilt generell für alles ausser der Dokumentation, dass nur Englisch zulässig ist.

## 1.1 Repo-Aufbau
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


# 2 Sourcecode
- Für alles muss ein sinnvoller, klarer und aussagekräftiger Name vorhanden sein. 
- In einer Datei darf maximal eine Klasse vorhanden sein.

## 2.1 Instanzvariablen
### 2.1.1 Private Instanzvariablen
- Immer **camelCase**.
- Immer davor ein Unterstrich `_`.

### 2.1.2 Public Instanzvariablen
- Immer **PascalCase**

## 2.2 Klassenvariablen
- Bei Klassenvariablen wird **PascalCase** verwendet.

## 2.3 Konstanten
- Konstanten werden mit dem **SCREAMING_SNAKE_CASE** benannt.


## 2.4 Methoden namen
- Methoden werden **immer** in **PascalCase** benannt.
- Methoden sollten **beschreibende** Namen haben.

## 2.5 Argumente
- Argumente werden **immer** in **camelCase** benannt.

## 2.6 Namespaces
- Namespaces werden **immer** im **PascalCase** benannt.

## 2.7 Klassen
### 2.7.1 Normale Klassen
- Normale Klassen müssen mit **PascalCase** benannt werden.

### 2.7.2 Abstrakte Klassen 
- Abstrakte Klassen müssen das Schlüsselwort `Abstract` vor ihrem Namen haben und befolgen ansonsten die gleichen Konventionen wie eine Klasse.

### 2.7.3 Interfaces
- Interfaces beginnen immer mit einem grossen `I` und folgen ansonsten den gleichen Naming-Conventions wie eine normale Klasse.

## 2.8 Dateinamen
- Dateinamen sind **immer gleich** dem **Klassennamen**


# 3 Datenmodell
## 3.1 Entitäten
- Entitäten **immer** im **Plural** benennen.
- Entitäten haben immer im PascalCase zu sein.

## 3.2 Schlüssel
### 3.2.1 Primärschlussel
- Jeder Datensatz muss über einen eindeutigen Primärschlüssel verfügen.
- Der Primärschlüssel **muss** eine **GUID** sein.
- Der Primärschlüssel **muss** immer den Namen `Id` tragen.

### 3.2.2 Fremdschlüssel
- Fremdschlüssel müssen **PascalCase** verwenden.
- Es dürfen **keine Sonderzeichen** oder Trennzeichen verwendet werden.
- Fremdschlüssel müssen immer folgendes schema befolgen: `{TABELLEN_NAME}Id`

| Korrekt    | Falsch     |
|------------|------------|
| UserId     | User_Id    |
| EntryId    | entryid    |
| ActivityId | activityId |

# 4 API

## 4.1 Struktur

Alle Endpunkte folgen diesem Schema:

```https://{BASE_URL}/api/{API_VERSION}/{CONTROLLER}/```

| Platzhalter      | Beschreibung                       | Beispiel           |
|------------------|------------------------------------|--------------------|
| `BASE_URL`       | Host abhängig                      | `localhost:5252`   |
| `API_VERSION`    | Aktuelle API-Version               | `v1`               |
| `CONTROLLER`     | Ressource, **immer** im **Plural** | `entries`          |

## 4.2 Namenskoventionen
Controller sind **immer** im Plural und werden kleingeschrieben. Müssen zwei Wörter benutzt werden, werden diese mit einem Bindestrich getrennt.

| Datenmodell | Controller          |
|----------   |---------------------|
| `Entry`     | `api/v1/entries`    | 
| `Activity`  | `api/v1/activities` |
| `Class`     | `api/v1/classes`    | 

## 4.3 HTTP-Methoden  
In Jiffy's api sind folgende HTTP-Methoden zulässig:

| Methode  | Zweck                               |
|----------|-------------------------------------|
| `GET`    | Resourcen abrufen                   | 
| `POST`   | Neue Ressourcen erstellen           |
| `PATCH`  | Anderungen an Resourcen vornehmen   | 
| `DELETE` | Ressourcen löschen                  | 


## 4.4 Response-Statuscodes
In Jiffy's API dürfen grundsätzlich alle HTTP-Statuscodes verwendet werden, sofern die hier aufgelisteten für die zu gebende Antwort keinen Sinn ergeben.


| Code | Bedeutung   | Verwendung               |
|------|-------------|--------------------------|
| 200  | OK          | Erfolgreicher Abruf      |
| 201  | Created     | Erfolgreiche Erstellung  |
| 204  | No Content  | Erfolgreiches Löschen    |
| 403  | Forbidden   | Keine Berechtigung       |
| 400  | Bad Request | Ungültige Daten          |
| 404  | Not Found   | Ressource nicht gefunden |
