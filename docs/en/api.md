# Jiffy
An ASP.NET Core Web API for tracking work time

## Version: v1

---

### [GET] /api/v1/Activities/descriptions
**Gets a list of all the descriptions owned by the user.**

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ActivityDescriptionDto](#activitydescriptiondto) ]<br>**application/json**: [ [ActivityDescriptionDto](#activitydescriptiondto) ]<br>**text/json**: [ [ActivityDescriptionDto](#activitydescriptiondto) ]<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [POST] /api/v1/Activities/descriptions
**Creates a new description for the current user.**

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Activities/descriptions/{id}
**Deletes a description if it is ownd by the current user.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Activities/descriptions/{id}
**Updates a description if it is owned by the current user.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> | **application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/*+json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**application/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br>**text/json**: [ActivityDescriptionDto](#activitydescriptiondto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Activities/titles
**Gets a list of all the titles owned by a user.**

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ActivityTitleDto](#activitytitledto) ]<br>**application/json**: [ [ActivityTitleDto](#activitytitledto) ]<br>**text/json**: [ [ActivityTitleDto](#activitytitledto) ]<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [POST] /api/v1/Activities/titles
**Creates a new title for the current user.**

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityTitleDto](#activitytitledto)<br>**application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Activities/titles/{id}
**Deletes a title if it is owned by the current user.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Activities/titles/{id}
**Updates a title if it is owned by the current user.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> | **application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br>**application/*+json**: [ActivityTitleDto](#activitytitledto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ActivityTitleDto](#activitytitledto)<br>**application/json**: [ActivityTitleDto](#activitytitledto)<br>**text/json**: [ActivityTitleDto](#activitytitledto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/Classes
**Gets a list containing all available classes.**

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ClassDto](#classdto) ]<br>**application/json**: [ [ClassDto](#classdto) ]<br>**text/json**: [ [ClassDto](#classdto) ]<br> |

### [POST] /api/v1/Classes
**Creates a new class (for administrator only).**

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ClassDto](#classdto)<br>**application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Classes/{id}
**Gets a specific class.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ClassDto](#classdto)<br>**application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Classes/{id}
**Deletes a class (for administrator only).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Classes/{id}
**Updates a class (for administrator only).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> | **application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br>**application/*+json**: [ClassDto](#classdto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ClassDto](#classdto)<br>**application/json**: [ClassDto](#classdto)<br>**text/json**: [ClassDto](#classdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/Entries
**If the current user is an administrator, a list of all entries and their owners will be returned. However, if the user is not an administrator, only a list of their own entries will be returned.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| start | query |  | No | integer |
| limit | query |  | No | integer |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [TimeEntryDto](#timeentrydto) ]<br>**application/json**: [ [TimeEntryDto](#timeentrydto) ]<br>**text/json**: [ [TimeEntryDto](#timeentrydto) ]<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [POST] /api/v1/Entries
**Creates a new entry that is linked to the current user.**

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [TimeEntryDto](#timeentrydto)<br>**application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Entries/{id}
**Gets a specific entry (limited to own entries for non-administrators).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [TimeEntryDto](#timeentrydto)<br>**application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Entries/{id}
**Deletes an entry (limited to own entries for non-administrators).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Entries/{id}
**Updates an entry (limited to own entry for non-administrators).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> | **application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br>**application/*+json**: [TimeEntryDto](#timeentrydto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [TimeEntryDto](#timeentrydto)<br>**application/json**: [TimeEntryDto](#timeentrydto)<br>**text/json**: [TimeEntryDto](#timeentrydto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 401 | Unauthorized | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/ShouldTimes
**Gets a list of all the shouldtimes.**

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [ShouldTimeDto](#shouldtimedto) ]<br>**application/json**: [ [ShouldTimeDto](#shouldtimedto) ]<br>**text/json**: [ [ShouldTimeDto](#shouldtimedto) ]<br> |

### [GET] /api/v1/ShouldTimes/{Id}
**Gets a specific shouldtime.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ShouldTimeDto](#shouldtimedto)<br>**application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/ShouldTimes/{Id}
**Updates a specific shouldtime.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br>**application/*+json**: [ShouldTimeDto](#shouldtimedto)<br> | **application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br>**application/*+json**: [ShouldTimeDto](#shouldtimedto)<br> | **application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br>**application/*+json**: [ShouldTimeDto](#shouldtimedto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ShouldTimeDto](#shouldtimedto)<br>**application/json**: [ShouldTimeDto](#shouldtimedto)<br>**text/json**: [ShouldTimeDto](#shouldtimedto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/ShouldTimes/{id}
**Deletes a specific shouldtime.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string (uuid) |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [GET] /api/v1/Users
**Gets a list of all users (for administrators only).**

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [ [UserDto](#userdto) ]<br>**application/json**: [ [UserDto](#userdto) ]<br>**text/json**: [ [UserDto](#userdto) ]<br> |

### [POST] /api/v1/Users
**Creates a new user (for administrators only).**

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [UserDto](#userdto)<br>**application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 409 | Conflict | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [GET] /api/v1/Users/{id}
**It gets a user's information (for administrators only).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| Id | path |  | Yes | string |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [UserDto](#userdto)<br>**application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [DELETE] /api/v1/Users/{id}
**Deletes a user by ID (for administrators only).**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 204 | No Content |  |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

### [PUT] /api/v1/Users/{id}
**Updates a specific user's information. If the current user is an administrator, they can edit every field of every user. If the user is not an administrator, however, they can only edit their own class ID.**

#### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| id | path |  | Yes | string |

#### Request Body

| Required | Schema |
| -------- | ------ |
|  Yes | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> | **application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br>**application/*+json**: [UserDto](#userdto)<br> |

#### Responses

| Code | Description | Schema |
| ---- | ----------- | ------ |
| 200 | OK | **text/plain**: [UserDto](#userdto)<br>**application/json**: [UserDto](#userdto)<br>**text/json**: [UserDto](#userdto)<br> |
| 400 | Bad Request | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |
| 404 | Not Found | **text/plain**: [ProblemDetails](#problemdetails)<br>**application/json**: [ProblemDetails](#problemdetails)<br>**text/json**: [ProblemDetails](#problemdetails)<br> |

---

### [POST] /Account/PerformExternalLogin
#### Request Body

| Required | Schema |
| -------- | ------ |
|  No | **multipart/form-data**: { **"provider"**: string, **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string, **"returnUrl"**: string }<br> | **multipart/form-data**: { **"provider"**: string, **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string, **"returnUrl"**: string }<br> |

#### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### [POST] /Account/Logout
#### Request Body

| Required | Schema |
| -------- | ------ |
|  No | **multipart/form-data**: { **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"returnUrl"**: string }<br> | **multipart/form-data**: { **"returnUrl"**: string }<br>**application/x-www-form-urlencoded**: { **"returnUrl"**: string }<br> |

#### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### [POST] /Account/Manage/LinkExternalLogin
#### Request Body

| Required | Schema |
| -------- | ------ |
|  No | **multipart/form-data**: { **"provider"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string }<br> | **multipart/form-data**: { **"provider"**: string }<br>**application/x-www-form-urlencoded**: { **"provider"**: string }<br> |

#### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

### [POST] /Account/Manage/DownloadPersonalData
#### Responses

| Code | Description |
| ---- | ----------- |
| 200 | OK |

---
### Schemas

#### ActivityDescriptionDto

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | No |
| value | string |  | No |
| favorite | boolean |  | No |

#### ActivityTitleDto

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | No |
| value | string |  | No |
| favorite | boolean |  | No |

#### ClassDto

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | No |
| name | string |  | No |
| shouldTimes | [ [ShouldTimeDto](#shouldtimedto) ] |  | No |

#### DayOfWeek

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| DayOfWeek | integer |  |  |

#### ProblemDetails

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| type | string |  | No |
| title | string |  | No |
| status | integer |  | No |
| detail | string |  | No |
| instance | string |  | No |

#### ShouldTimeDto

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | No |
| classId | string (uuid) |  | No |
| dayOfWeek | [DayOfWeek](#dayofweek) |  | No |
| should | string (date-span) |  | No |

#### TimeEntryDto

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) |  | No |
| start | dateTime |  | No |
| end | dateTime |  | No |
| title | string |  | No |
| description | string |  | No |
| username | string |  | No |
| time | string (date-span) |  | No |
| shouldTime | string (date-span) |  | No |
| ovetime | string (date-span) |  | No |
| sick | boolean |  | No |

#### UserDto

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string |  | No |
| classId | string (uuid) |  | No |
| userName | string |  | No |
| normalizedUserName | string |  | No |
| email | string |  | No |
| normalizedEmail | string |  | No |
| emailConfirmed | boolean |  | No |
| password | string |  | No |
| phoneNumber | string |  | No |
| phoneNumberConfirmed | boolean |  | No |
| twoFactorEnabled | boolean |  | No |
| lockoutEnd | dateTime |  | No |
| lockoutEnabled | boolean |  | No |
| accessFailedCount | integer |  | No |
