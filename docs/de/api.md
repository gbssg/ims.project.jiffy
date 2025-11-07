# Jiffy
Eine ASP.NET Core Web-API zur Erfassung der Arbeitszeit

## Version: v1

---

### [GET] /api/v1/Activities/descriptions
**Ruft eine Liste aller Beschreibungen ab, die dem Benutzer gehören.**

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ActivityDescriptionDto](#activitydescriptiondto) ]<br>**application/json**: [ [ActivityDescriptionDto](#activitydescriptiondto) ]<br>**text/json**: [ [ActivityDescriptionDto](#activitydescriptiondto) ]<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [POST] /api/v1/Activities/descriptions
**Erstellt eine neue Beschreibung für den aktuellen Benutzer.**

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Activities/descriptions/{id}
**Löscht eine Beschreibung, wenn sie dem aktuellen Benutzer gehört.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Activities/descriptions/{id}
**Aktualisiert eine Beschreibung, wenn sie dem aktuellen Benutzer gehört.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Activities/titles
**Ruft eine Liste aller Titel ab, die ein Benutzer besitzt.**

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ActivityTitleDto](#activitytitledto) ]<br>**application/json**: [ [ActivityTitleDto](#activitytitledto) ]<br>**text/json**: [ [ActivityTitleDto](#activitytitledto) ]<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [POST] /api/v1/Activities/titles
**Erstellt einen neuen Titel für den aktuellen Benutzer.**

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityTitleDto](#activitytitledto)<br>**application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Activities/titles/{id}
**Löscht einen Titel, wenn er dem aktuellen Benutzer gehört.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Activities/titles/{id}
**Aktualisiert einen Titel, wenn er sich im Besitz des aktuellen Benutzers befindet.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityTitleDto](#activitytitledto)<br>**application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/Classes
**Ruft eine Liste mit allen verfügbaren Klassen ab.**

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ClassDto](#classdto) ]<br>**application/json**: [ [ClassDto](#classdto) ]<br>**text/json**: [ [ClassDto](#classdto) ]<br> |

### [POST] /api/v1/Classes
**Erstellt eine neue Klasse (nur für Administratoren).**

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ClassDto](#classdto)<br>**application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Classes/{id}
**Ruft eine bestimmte Klasse ab.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ClassDto](#classdto)<br>**application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Classes/{id}
**Löscht eine Klasse (nur für Administratoren).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Classes/{id}
**Aktualisiert eine Klasse (nur für Administratoren).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ClassDto](#classdto)<br>**application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/Entries
**Wenn der aktuelle Benutzer ein Administrator ist, wird eine Liste aller Einträge und ihrer Eigentümer zurückgegeben. Ist der Benutzer jedoch kein Administrator, wird nur eine Liste seiner eigenen Einträge zurückgegeben.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| start | query |  | Nein | integer |
| limit | query |  | Nein | integer |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [TimeEntryDto](#timeentrydto) ]<br>**application/json**: [ [TimeEntryDto](#timeentrydto) ]<br>**text/json**: [ [TimeEntryDto](#timeentrydto) ]<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [POST] /api/v1/Entries
**Erstellt einen neuen Eintrag, der mit dem aktuellen Benutzer verknüpft ist.**

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [TimeEntryDto](#timeentrydto)<br>**application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Entries/{id}
**Ruft einen bestimmten Eintrag ab (beschränkt auf eigene Einträge für Nicht-Administratoren).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [TimeEntryDto](#timeentrydto)<br>**application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Entries/{id}
**Löscht einen Eintrag (beschränkt auf eigene Einträge für Nicht-Administratoren).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Entries/{id}
**Aktualisiert einen Eintrag (für Nicht-Administratoren beschränkt auf eigene Einträge).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [TimeEntryDto](#timeentrydto)<br>**application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/ShouldTimes
**Ruft eine Liste aller Sollzeiten ab.**

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ShouldTimeDto](#shouldtimedto) ]<br>**application/json**: [ [ShouldTimeDto](#shouldtimedto) ]<br>**text/json**: [ [ShouldTimeDto](#shouldtimedto) ]<br> |

### [GET] /api/v1/ShouldTimes/{Id}
**Ruft eine bestimmte Sollzeit ab.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ShouldTimeDto](#shouldtimedto)<br>**application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/ShouldTimes/{Id}
**Aktualisiert eine bestimmte Sollzeit.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br>**application/*+json**: [ShouldTimeDto](#shouldtimedto)<br> | **application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br>**application/*+json**: [ShouldTimeDto](#shouldtimedto)<br> | **application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br>**application/*+json**: [ShouldTimeDto](#shouldtimedto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ShouldTimeDto](#shouldtimedto)<br>**application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/ShouldTimes/{id}
**Löscht eine bestimmte Sollzeit.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string (uuid) |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/Users
**Ruft eine Liste aller Benutzer ab (nur für Administratoren).**

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [UserDto](#userdto) ]<br>**application/json**: [ [UserDto](#userdto) ]<br>**text/json**: [ [UserDto](#userdto) ]<br> |

### [POST] /api/v1/Users
**Erstellt einen neuen Benutzer (nur für Administratoren).**

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [UserDto](#userdto)<br>**application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 409 | Conflict | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Users/{id}
**Es ruft die Informationen eines Benutzers ab (nur für Administratoren).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| Id | path |  | Ja | string |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [UserDto](#userdto)<br>**application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Users/{id}
**Löscht einen Benutzer anhand seiner ID (nur für Administratoren).**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Users/{id}
**Aktualisiert die Informationen eines bestimmten Benutzers. Wenn der aktuelle Benutzer ein Administrator ist, kann er jedes Feld jedes Benutzers bearbeiten. Ist der Benutzer jedoch kein Administrator, kann er nur seine eigene Klassen-ID bearbeiten.**

#### Parameters

| Name | Ort | Beschreibung | Benötigt | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Ja | string |

#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Ja | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> |

#### Responses

| Code | Beschreibung | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [UserDto](#userdto)<br>**application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [POST] /Account/PerformExternalLogin
#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Nein | **multipart/form-data**: { **"provider"**: string, **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string, **"returnUrl"**: string }<br> | **multipart/form-data**: { **"provider"**: string, **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string, **"returnUrl"**: string }<br> |

#### Responses

| Code | Beschreibung |
| ---- | ----------- |
| 200 | OK |

### [POST] /Account/Logout
#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Nein | **multipart/form-data**: { **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"returnUrl"**: string }<br> | **multipart/form-data**: { **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"returnUrl"**: string }<br> |

#### Responses

| Code | Beschreibung |
| ---- | ----------- |
| 200 | OK |

### [POST] /Account/Manage/LinkExternalLogin
#### Request Body

| Benötigt | Schema |
| -------- | ------ |
|  Nein | **multipart/form-data**: { **"provider"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string }<br> | **multipart/form-data**: { **"provider"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string }<br> |

#### Responses

| Code | Beschreibung |
| ---- | ----------- |
| 200 | OK |

### [POST] /Account/Manage/DownloadPersonalData
#### Responses

| Code | Beschreibung |
| ---- | ----------- |
| 200 | OK |

---
### Schemas

#### ActivityDescriptionDto

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | Nein |
| value | string |  | Nein |
| favorite | boolean |  | Nein |

#### ActivityTitleDto

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | Nein |
| value | string |  | Nein |
| favorite | boolean |  | Nein |

#### ClassDto

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | Nein |
| name | string |  | Nein |
| shouldTimes | [ [ShouldTimeDto](#shouldtimedto) ] |  | Nein |

#### DayOfWeek

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| DayOfWeek | integer |  |  |

#### ProblemDetails

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| type | string |  | Nein |
| title | string |  | Nein |
| status | integer |  | Nein |
| detail | string |  | Nein |
| instance | string |  | Nein |

#### ShouldTimeDto

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | Nein |
| classId | string (uuid) |  | Nein |
| dayOfWeek | [DayOfWeek](#dayofweek) |  | Nein |
| should | string (date-span) |  | Nein |

#### TimeEntryDto

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | Nein |
| start | dateTime |  | Nein |
| end | dateTime |  | Nein |
| title | string |  | Nein |
| Beschreibung | string |  | Nein |
| username | string |  | Nein |
| time | string (date-span) |  | Nein |
| shouldTime | string (date-span) |  | Nein |
| ovetime | string (date-span) |  | Nein |
| sick | boolean |  | Nein |

#### UserDto

| Name | Type | Beschreibung | Benötigt |
| ---- | ---- | ----------- | -------- |
| id | string |  | Nein |
| classId | string (uuid) |  | Nein |
| userName | string |  | Nein |
| normalizedUserName | string |  | Nein |
| email | string |  | Nein |
| normalizedEmail | string |  | Nein |
| emailConfirmed | boolean |  | Nein |
| password | string |  | Nein |
| phoneNumber | string |  | Nein |
| phoneNumberConfirmed | boolean |  | Nein |
| twoFactorEnabled | boolean |  | Nein |
| lockoutEnd | dateTime |  | Nein |
| lockoutEnabled | boolean |  | Nein |
| accessFailedCount | integer |  | Nein |
