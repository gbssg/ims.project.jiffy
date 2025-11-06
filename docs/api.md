
# API

## Inhaltsverzeichnis


- [1 User](#1-user)
   - [1.1 PATCH ClassId](#11-patch-classid)
      - [1.1.1 Parameter](#111-parameter)
      - [1.1.2 Antworten](#112-antworten)
   - [1.2 GET Users](#12-get-users)
      - [1.2.1 Antworten](#121-antworten)
   - [1.3 POST User](#13-post-user)
      - [1.3.1 Request Body](#131-request-body)
      - [1.3.2 Antworten](#132-antworten)
   - [1.4 DELETE User](#14-delete-user)
      - [1.4.1 Parameter](#141-parameter)
      - [1.4.2 Antworten](#142-antworten)
   - [1.5 PATCH User](#15-patch-user)
      - [1.5.1 Parameter](#151-parameter)
      - [1.5.2 Request Body](#152-request-body)
      - [1.5.3 Antworten](#153-antworten)

## 1 User

### 1.1 PATCH ClassId

> Route: `/api/v1/users/set-class/{id}`
> Authorization Level: **Authentifiziert**

Setzt die Klasse des aktuell angemeldeten Benutzers.

#### 1.1.1 Parameter

  | Name | Typ  | Beschreibung  |
  | ---- | ---- | ------------- |
  | id   | Guid | Id der Klasse |

#### 1.1.2 Antworten

  | Code | Beschreibung                   |
  | ---- | ------------------------------ |
  | 200  | Klasse erfolgreich gesetzt     |
  | 401  | Benutzer nicht authentifiziert |
  | 404  | Klasse nicht gefunden          |


### 1.2 GET Users

> Route: `/api/v1/users`
> Authorization Level: **Administrator**

Gibt alle Benutzer im System zurück.

#### 1.2.1 Antworten

  | Code | Beschreibung                          |
  | ---- | ------------------------------------- |
  | 200  | Liste von Benutzer-DTOs zurückgegeben |


### 1.3 POST User

> Route: `/api/v1/users`
> Authorization Level: **Administrator**

Erstellt einen neuen Benutzer.

#### 1.3.1 Request Body

  ```json
  {
    "userName": "max.mustermann",
    "email": "max@example.com",
    "password": "Str0ngP@ss!",
    "phoneNumber": "0791234567",
    "twoFactorEnabled": false,
    "lockoutEnabled": false,
    "lockoutEnd": "2025-12-31T23:59:59Z",
    "accessFailedCount": 0,
    "emailConfirmed": false
  }
  ```

#### 1.3.2 Antworten

  | Code | Beschreibung                                     |
  | ---- | ------------------------------------------------ |
  | 200  | Benutzer erfolgreich erstellt, DTO zurückgegeben |
  | 400  | Passwort ungültig oder Fehler                    |
  | 409  | Benutzer mit dieser Email existiert bereits      |


### 1.4 DELETE User

> Route: `/api/v1/users/{id}`
> Authorization Level: **Administrator**

Löscht einen Benutzer anhand seiner Id.

#### 1.4.1 Parameter

  | Name | Typ    | Beschreibung |
  | ---- | ------ | ------------ |
  | id   | string | Benutzer-Id  |

#### 1.4.2 Antworten

  | Code | Beschreibung                  |
  | ---- | ----------------------------- |
  | 204  | Benutzer erfolgreich gelöscht |
  | 404  | Benutzer nicht gefunden       |
  | 400  | Fehler beim Löschen           |


### 1.5 PATCH User

> Route: `/api/v1/users/{id}`
> Authorization Level: **Administrator**

Aktualisiert die Daten eines Benutzers.

#### 1.5.1 Parameter

  | Name | Typ    | Beschreibung |
  | ---- | ------ | ------------ |
  | id   | string | Benutzer-Id  |

#### 1.5.2 Request Body

  ```json
  {
    "userName": "new.username",
    "email": "new@example.com",
    "password": "NeuesStr0ngP@ss!",
    "phoneNumber": "0787654321",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnabled": true,
    "lockoutEnd": "2026-01-01T12:00:00Z",
    "accessFailedCount": 1,
    "emailConfirmed": true
  }
  ```

#### 1.5.3 Antworten

  | Code | Beschreibung                                         |
  | ---- | ---------------------------------------------------- |
  | 200  | Benutzer erfolgreich aktualisiert, DTO zurückgegeben |
  | 404  | Benutzer nicht gefunden                              |
  | 400  | Fehler beim Aktualisieren oder Passwort ändern       |
