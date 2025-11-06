<!-- Generator: Widdershins v4.0.1 -->

<h1 id="zeiterfassungssoftware">Zeiterfassungssoftware v1.0</h1>

> Scroll down for code samples, example requests and responses. Select a language for code samples from the tabs above or the mobile navigation menu.

<h1 id="zeiterfassungssoftware-activities">Activities</h1>

## get__api_v1_Activities_descriptions

> Code samples

```http
GET /api/v1/Activities/descriptions HTTP/1.1

```

`GET /api/v1/Activities/descriptions`

<h3 id="get__api_v1_activities_descriptions-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__api_v1_Activities_descriptions

> Code samples

```http
POST /api/v1/Activities/descriptions HTTP/1.1

Content-Type: application/json

```

`POST /api/v1/Activities/descriptions`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "value": "string",
  "favorite": true
}
```

<h3 id="post__api_v1_activities_descriptions-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ActivityDescriptionDto](#schemaactivitydescriptiondto)|true|none|

<h3 id="post__api_v1_activities_descriptions-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## delete__api_v1_Activities_descriptions_{id}

> Code samples

```http
DELETE /api/v1/Activities/descriptions/{id} HTTP/1.1

```

`DELETE /api/v1/Activities/descriptions/{id}`

<h3 id="delete__api_v1_activities_descriptions_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="delete__api_v1_activities_descriptions_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## put__api_v1_Activities_descriptions_{id}

> Code samples

```http
PUT /api/v1/Activities/descriptions/{id} HTTP/1.1

Content-Type: application/json

```

`PUT /api/v1/Activities/descriptions/{id}`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "value": "string",
  "favorite": true
}
```

<h3 id="put__api_v1_activities_descriptions_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|
|body|body|[ActivityDescriptionDto](#schemaactivitydescriptiondto)|true|none|

<h3 id="put__api_v1_activities_descriptions_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## get__api_v1_Activities_titles

> Code samples

```http
GET /api/v1/Activities/titles HTTP/1.1

```

`GET /api/v1/Activities/titles`

<h3 id="get__api_v1_activities_titles-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__api_v1_Activities_titles

> Code samples

```http
POST /api/v1/Activities/titles HTTP/1.1

Content-Type: application/json

```

`POST /api/v1/Activities/titles`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "value": "string",
  "favorite": true
}
```

<h3 id="post__api_v1_activities_titles-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ActivityTitleDto](#schemaactivitytitledto)|true|none|

<h3 id="post__api_v1_activities_titles-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## delete__api_v1_Activities_titles_{id}

> Code samples

```http
DELETE /api/v1/Activities/titles/{id} HTTP/1.1

```

`DELETE /api/v1/Activities/titles/{id}`

<h3 id="delete__api_v1_activities_titles_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="delete__api_v1_activities_titles_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## put__api_v1_Activities_titles_{id}

> Code samples

```http
PUT /api/v1/Activities/titles/{id} HTTP/1.1

Content-Type: application/json

```

`PUT /api/v1/Activities/titles/{id}`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "value": "string",
  "favorite": true
}
```

<h3 id="put__api_v1_activities_titles_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|
|body|body|[ActivityTitleDto](#schemaactivitytitledto)|true|none|

<h3 id="put__api_v1_activities_titles_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="zeiterfassungssoftware-classes">Classes</h1>

## get__api_v1_Classes

> Code samples

```http
GET /api/v1/Classes HTTP/1.1

```

`GET /api/v1/Classes`

<h3 id="get__api_v1_classes-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__api_v1_Classes

> Code samples

```http
POST /api/v1/Classes HTTP/1.1

Content-Type: application/json

```

`POST /api/v1/Classes`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "shouldTimes": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
      "dayOfWeek": 0,
      "should": "string"
    }
  ]
}
```

<h3 id="post__api_v1_classes-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ClassDto](#schemaclassdto)|true|none|

<h3 id="post__api_v1_classes-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## get__api_v1_Classes_{id}

> Code samples

```http
GET /api/v1/Classes/{id} HTTP/1.1

```

`GET /api/v1/Classes/{id}`

<h3 id="get__api_v1_classes_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="get__api_v1_classes_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## delete__api_v1_Classes_{id}

> Code samples

```http
DELETE /api/v1/Classes/{id} HTTP/1.1

```

`DELETE /api/v1/Classes/{id}`

<h3 id="delete__api_v1_classes_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="delete__api_v1_classes_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## put__api_v1_Classes_{id}

> Code samples

```http
PUT /api/v1/Classes/{id} HTTP/1.1

Content-Type: application/json

```

`PUT /api/v1/Classes/{id}`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "shouldTimes": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
      "dayOfWeek": 0,
      "should": "string"
    }
  ]
}
```

<h3 id="put__api_v1_classes_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|
|body|body|[ClassDto](#schemaclassdto)|true|none|

<h3 id="put__api_v1_classes_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="zeiterfassungssoftware-entries">Entries</h1>

## get__api_v1_Entries

> Code samples

```http
GET /api/v1/Entries HTTP/1.1

```

`GET /api/v1/Entries`

<h3 id="get__api_v1_entries-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|start|query|integer(int32)|false|none|
|limit|query|integer(int32)|false|none|

<h3 id="get__api_v1_entries-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__api_v1_Entries

> Code samples

```http
POST /api/v1/Entries HTTP/1.1

Content-Type: application/json

```

`POST /api/v1/Entries`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "start": "2019-08-24T14:15:22Z",
  "end": "2019-08-24T14:15:22Z",
  "title": "string",
  "description": "string",
  "username": "string",
  "shouldTime": "string"
}
```

<h3 id="post__api_v1_entries-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[TimeEntryDto](#schematimeentrydto)|true|none|

<h3 id="post__api_v1_entries-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## get__api_v1_Entries_{id}

> Code samples

```http
GET /api/v1/Entries/{id} HTTP/1.1

```

`GET /api/v1/Entries/{id}`

<h3 id="get__api_v1_entries_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="get__api_v1_entries_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## delete__api_v1_Entries_{id}

> Code samples

```http
DELETE /api/v1/Entries/{id} HTTP/1.1

```

`DELETE /api/v1/Entries/{id}`

<h3 id="delete__api_v1_entries_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="delete__api_v1_entries_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## put__api_v1_Entries_{id}

> Code samples

```http
PUT /api/v1/Entries/{id} HTTP/1.1

Content-Type: application/json

```

`PUT /api/v1/Entries/{id}`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "start": "2019-08-24T14:15:22Z",
  "end": "2019-08-24T14:15:22Z",
  "title": "string",
  "description": "string",
  "username": "string",
  "shouldTime": "string"
}
```

<h3 id="put__api_v1_entries_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|
|body|body|[TimeEntryDto](#schematimeentrydto)|true|none|

<h3 id="put__api_v1_entries_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="zeiterfassungssoftware-shouldtimes">ShouldTimes</h1>

## get__api_v1_ShouldTimes

> Code samples

```http
GET /api/v1/ShouldTimes HTTP/1.1

```

`GET /api/v1/ShouldTimes`

<h3 id="get__api_v1_shouldtimes-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## get__api_v1_ShouldTimes_{Id}

> Code samples

```http
GET /api/v1/ShouldTimes/{Id} HTTP/1.1

```

`GET /api/v1/ShouldTimes/{Id}`

<h3 id="get__api_v1_shouldtimes_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="get__api_v1_shouldtimes_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## put__api_v1_ShouldTimes_{Id}

> Code samples

```http
PUT /api/v1/ShouldTimes/{Id} HTTP/1.1

Content-Type: application/json

```

`PUT /api/v1/ShouldTimes/{Id}`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
  "dayOfWeek": 0,
  "should": "string"
}
```

<h3 id="put__api_v1_shouldtimes_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|
|body|body|[ShouldTimeDto](#schemashouldtimedto)|true|none|

<h3 id="put__api_v1_shouldtimes_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## delete__api_v1_ShouldTimes_{id}

> Code samples

```http
DELETE /api/v1/ShouldTimes/{id} HTTP/1.1

```

`DELETE /api/v1/ShouldTimes/{id}`

<h3 id="delete__api_v1_shouldtimes_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string(uuid)|true|none|

<h3 id="delete__api_v1_shouldtimes_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="zeiterfassungssoftware-users">Users</h1>

## get__api_v1_Users

> Code samples

```http
GET /api/v1/Users HTTP/1.1

```

`GET /api/v1/Users`

<h3 id="get__api_v1_users-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__api_v1_Users

> Code samples

```http
POST /api/v1/Users HTTP/1.1

Content-Type: application/json

```

`POST /api/v1/Users`

> Body parameter

```json
{
  "id": "string",
  "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
  "userName": "string",
  "normalizedUserName": "string",
  "email": "string",
  "normalizedEmail": "string",
  "emailConfirmed": true,
  "password": "string",
  "phoneNumber": "string",
  "phoneNumberConfirmed": true,
  "twoFactorEnabled": true,
  "lockoutEnd": "2019-08-24T14:15:22Z",
  "lockoutEnabled": true,
  "accessFailedCount": 0
}
```

<h3 id="post__api_v1_users-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[UserDto](#schemauserdto)|true|none|

<h3 id="post__api_v1_users-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## get__api_v1_Users_{id}

> Code samples

```http
GET /api/v1/Users/{id} HTTP/1.1

```

`GET /api/v1/Users/{id}`

<h3 id="get__api_v1_users_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|Id|path|string|true|none|

<h3 id="get__api_v1_users_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## delete__api_v1_Users_{id}

> Code samples

```http
DELETE /api/v1/Users/{id} HTTP/1.1

```

`DELETE /api/v1/Users/{id}`

<h3 id="delete__api_v1_users_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string|true|none|

<h3 id="delete__api_v1_users_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## put__api_v1_Users_{id}

> Code samples

```http
PUT /api/v1/Users/{id} HTTP/1.1

Content-Type: application/json

```

`PUT /api/v1/Users/{id}`

> Body parameter

```json
{
  "id": "string",
  "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
  "userName": "string",
  "normalizedUserName": "string",
  "email": "string",
  "normalizedEmail": "string",
  "emailConfirmed": true,
  "password": "string",
  "phoneNumber": "string",
  "phoneNumberConfirmed": true,
  "twoFactorEnabled": true,
  "lockoutEnd": "2019-08-24T14:15:22Z",
  "lockoutEnabled": true,
  "accessFailedCount": 0
}
```

<h3 id="put__api_v1_users_{id}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|string|true|none|
|body|body|[UserDto](#schemauserdto)|true|none|

<h3 id="put__api_v1_users_{id}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="zeiterfassungssoftware-zeiterfassungssoftware">Zeiterfassungssoftware</h1>

## post__Account_PerformExternalLogin

> Code samples

```http
POST /Account/PerformExternalLogin HTTP/1.1

Content-Type: multipart/form-data

```

`POST /Account/PerformExternalLogin`

> Body parameter

```yaml
provider: string
returnUrl: string

```

<h3 id="post__account_performexternallogin-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|object|false|none|
|» provider|body|string|true|none|
|» returnUrl|body|string|true|none|

<h3 id="post__account_performexternallogin-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__Account_Logout

> Code samples

```http
POST /Account/Logout HTTP/1.1

Content-Type: multipart/form-data

```

`POST /Account/Logout`

> Body parameter

```yaml
returnUrl: string

```

<h3 id="post__account_logout-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|object|false|none|
|» returnUrl|body|string|true|none|

<h3 id="post__account_logout-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__Account_Manage_LinkExternalLogin

> Code samples

```http
POST /Account/Manage/LinkExternalLogin HTTP/1.1

Content-Type: multipart/form-data

```

`POST /Account/Manage/LinkExternalLogin`

> Body parameter

```yaml
provider: string

```

<h3 id="post__account_manage_linkexternallogin-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|object|false|none|
|» provider|body|string|true|none|

<h3 id="post__account_manage_linkexternallogin-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

## post__Account_Manage_DownloadPersonalData

> Code samples

```http
POST /Account/Manage/DownloadPersonalData HTTP/1.1

```

`POST /Account/Manage/DownloadPersonalData`

<h3 id="post__account_manage_downloadpersonaldata-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

# Schemas

<h2 id="tocS_ActivityDescriptionDto">ActivityDescriptionDto</h2>
<!-- backwards compatibility -->
<a id="schemaactivitydescriptiondto"></a>
<a id="schema_ActivityDescriptionDto"></a>
<a id="tocSactivitydescriptiondto"></a>
<a id="tocsactivitydescriptiondto"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "value": "string",
  "favorite": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|false|none|none|
|value|string¦null|false|none|none|
|favorite|boolean|false|none|none|

<h2 id="tocS_ActivityTitleDto">ActivityTitleDto</h2>
<!-- backwards compatibility -->
<a id="schemaactivitytitledto"></a>
<a id="schema_ActivityTitleDto"></a>
<a id="tocSactivitytitledto"></a>
<a id="tocsactivitytitledto"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "value": "string",
  "favorite": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|false|none|none|
|value|string¦null|false|none|none|
|favorite|boolean|false|none|none|

<h2 id="tocS_ClassDto">ClassDto</h2>
<!-- backwards compatibility -->
<a id="schemaclassdto"></a>
<a id="schema_ClassDto"></a>
<a id="tocSclassdto"></a>
<a id="tocsclassdto"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "shouldTimes": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
      "dayOfWeek": 0,
      "should": "string"
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|false|none|none|
|name|string¦null|false|none|none|
|shouldTimes|[[ShouldTimeDto](#schemashouldtimedto)]¦null|false|none|none|

<h2 id="tocS_DayOfWeek">DayOfWeek</h2>
<!-- backwards compatibility -->
<a id="schemadayofweek"></a>
<a id="schema_DayOfWeek"></a>
<a id="tocSdayofweek"></a>
<a id="tocsdayofweek"></a>

```json
0

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none|none|

#### Enumerated Values

|Property|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|
|*anonymous*|4|
|*anonymous*|5|
|*anonymous*|6|

<h2 id="tocS_ShouldTimeDto">ShouldTimeDto</h2>
<!-- backwards compatibility -->
<a id="schemashouldtimedto"></a>
<a id="schema_ShouldTimeDto"></a>
<a id="tocSshouldtimedto"></a>
<a id="tocsshouldtimedto"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
  "dayOfWeek": 0,
  "should": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|false|none|none|
|classId|string(uuid)|false|none|none|
|dayOfWeek|[DayOfWeek](#schemadayofweek)|false|none|none|
|should|string(date-span)|false|none|none|

<h2 id="tocS_TimeEntryDto">TimeEntryDto</h2>
<!-- backwards compatibility -->
<a id="schematimeentrydto"></a>
<a id="schema_TimeEntryDto"></a>
<a id="tocStimeentrydto"></a>
<a id="tocstimeentrydto"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "start": "2019-08-24T14:15:22Z",
  "end": "2019-08-24T14:15:22Z",
  "title": "string",
  "description": "string",
  "username": "string",
  "time": "string",
  "shouldTime": "string",
  "ovetime": "string",
  "sick": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|false|none|none|
|start|string(date-time)|false|none|none|
|end|string(date-time)¦null|false|none|none|
|title|string¦null|false|none|none|
|description|string¦null|false|none|none|
|username|string¦null|false|none|none|
|time|string(date-span)|false|read-only|none|
|shouldTime|string(date-span)|false|none|none|
|ovetime|string(date-span)|false|read-only|none|
|sick|boolean|false|read-only|none|

<h2 id="tocS_UserDto">UserDto</h2>
<!-- backwards compatibility -->
<a id="schemauserdto"></a>
<a id="schema_UserDto"></a>
<a id="tocSuserdto"></a>
<a id="tocsuserdto"></a>

```json
{
  "id": "string",
  "classId": "f0846d40-4884-40d5-8fc5-9f2c5ef371c4",
  "userName": "string",
  "normalizedUserName": "string",
  "email": "string",
  "normalizedEmail": "string",
  "emailConfirmed": true,
  "password": "string",
  "phoneNumber": "string",
  "phoneNumberConfirmed": true,
  "twoFactorEnabled": true,
  "lockoutEnd": "2019-08-24T14:15:22Z",
  "lockoutEnabled": true,
  "accessFailedCount": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string¦null|false|none|none|
|classId|string(uuid)|false|none|none|
|userName|string¦null|false|none|none|
|normalizedUserName|string¦null|false|none|none|
|email|string¦null|false|none|none|
|normalizedEmail|string¦null|false|none|none|
|emailConfirmed|boolean|false|none|none|
|password|string¦null|false|none|none|
|phoneNumber|string¦null|false|none|none|
|phoneNumberConfirmed|boolean|false|none|none|
|twoFactorEnabled|boolean|false|none|none|
|lockoutEnd|string(date-time)|false|none|none|
|lockoutEnabled|boolean|false|none|none|
|accessFailedCount|integer(int32)|false|none|none|

