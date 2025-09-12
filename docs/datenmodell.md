# Inhaltsverzeichnis

- [1 ERD](#1-erd)
- [2 Tabellen](#2-tabellen)
  - [2.1 Framework](#21-framework)
    - [2.1.1 AspNetRoleClaims](#211-aspnetroleclaims)
    - [2.1.2 AspNetRoles](#212-aspnetroles)
    - [2.1.3 AspNetUserClaims](#213-aspnetuserclaims)
    - [2.1.4 AspNetUserLogins](#214-aspnetuserlogins)
    - [2.1.5 AspNetUserRoles](#215-aspnetuserroles)
    - [2.1.6 AspNetUsetTokens](#216-aspnetusettokens)
  - [2.2 Applikation](#22-applikation)
    - [2.2.1 ActivityTitles](#221-activitytitles)
    - [2.2.2 ActivityDescriptions](#222-activitydescriptions)
    - [2.2.3 Classes](#223-classes)
    - [2.2.4 Entries](#224-entries)
    - [2.2.5 ShouldTimes](#225-shouldtimes)


# 1 ERD
![ERD](../assets/ERD_Minimal.jpg)

# 2 Tabellen

## 2.1 Framework

### 2.1.1 AspNetRoleClaims
Speichert Claims, die einer bestimmten Rolle zugeordnet sind.

| Name       | Typ           | Schlüssel   | Beschreibung |
|------------|---------------|-------------|--------------|
| Id         | int           | Primär      | -            |
| RoleId     | nvarchar(450) | AspNetRoles | -            |
| ClaimType  | nvarchar(MAX) | -           | -            |
| ClaimValue | nvarchar(MAX) | -           | -            |

### 2.1.2 AspNetRoles
Speichert Rollen.

| Name             | Typ           | Schlüssel | Beschreibung |
|------------------|---------------|-----------|--------------|
| Id               | nvarchar(450) | Primär    | -            |
| Name             | nvarchar(256) | -         | -            |
| NormalizedName   | nvarchar(256) | -         | -            |
| ConcurrencyStamp | nvarchar(MAX) | -         | -            |


### 2.1.3 AspNetUserClaims
Speichert Claims, die einzelnen Benutzern zugewisen sind.

| Name       | Typ           | Schlüssel   | Beschreibung |
|------------|---------------|-------------|--------------|
| Id         | int           | Primär      | -            |
| UserId     | nvarchar(450) | AspNetUsers | -            |
| ClaimType  | nvarchar(MAX) | -           | -            |
| ClaimValue | nvarchar(MAX) | -           | -            |

### 2.1.4 AspNetUserLogins
**Primärschlüssel:** Kombination aus **LoginProvider** und **ProviderKey**

| Name                | Typ           | Schlüssel   | Beschreibung             |
|---------------------|---------------|-------------|--------------------------|
| LoginProvider       | nvarchar(450) | Primär      | Login Anbieter           |
| ProviderKey         | nvarchar(450) | Primär      | Wert des externen Logins |
| ProviderDisplayName | nvarchar(MAX) | -           | Anbietername             |
| UserId              | nvarchar(450) | AspNetUsers | -                        |

### 2.1.5 AspNetUserRoles
Speichert die Rollen, die einzelnen Benutzern zugewiesen sind.

**Primärschlüssel:** Kombination aus **UserId** und **RoleId**

| Name   | Typ           | Schlüssel   | Beschreibung |
|--------|---------------|-------------|--------------|
| UserId | nvarchar(450) | AspNetUsers | -            |
| RoleId | nvarchar(450) | AspNetRoles | -            |

### 2.1.6 AspNetUsetTokens
Speichert Tokens für Benutzer.

**Primärschlüssel:** Kombination aus **UserId**, **LoginProvider** und **Name**

| Name          | Typ           | Schlüssel   | Beschreibung |
|---------------|---------------|-------------|--------------|
| UserId        | nvarchar(450) | AspNetUsers | -            |
| LoginProvider | nvarchar(450) | -           | Anbieter     |
| Name          | nvarchar(450) | -           | Tokentyp     |
| Value         | nvarchar(MAX) | -           | Token        |

## 2.2 Applikation 
### 2.2.1 ActivityTitles
Speichert die Aktivitäten der Nutzer.

|  Name    | Typ              | Schlüssel   | Beschreibung |
|----------|------------------|-------------|--------------|
| Id       | uniqueidentifier | Primär      | -            |
| Titel    | nvarchar(MAX)    | -           | -            |
| UserId   | nvarchar(450)    | AspNetUsers | -            |
| Favorite | bit              | -           | -            |

### 2.2.2 ActivityDescriptions
Speichert die Aktivitäten beschreibungen der Nutzer.

|  Name    | Typ              | Schlüssel   | Beschreibung |
|----------|------------------|-------------|--------------|
| Id       | uniqueidentifier | Primär      | -            |
| Value    | nvarchar(MAX)    | -           | -            |
| UserId   | nvarchar(450)    | AspNetUsers | -            |
| Favorite | bit              | -           | -            |

### 2.2.3 Classes
Speichert alle Klassen

| Name     | Typ              | Schlüssel   | Beschreibung |
|----------|------------------|-------------|--------------|
| Id       | uniqueidentifier | Primär      | -            |
| Name     | nvarchar(250)    | -           | -            |


### 2.2.4 Entries
Speichert die Einträge der Nutzer.

| Name        | Typ              | Schlüssel | Beschreibung                    |
|-------------|------------------|-----------|---------------------------------|
| Id          | uniqueidentifier | Primär    | -                               |
| Start       | datetime         | -         | -                               |
| End         | datetime         | -         | -                               |
| Title       | nvarchar(MAX)    | -         | -                               |
| Description | nvarchar(MAX)    | -         | -                               |
| UserId      | nvarchar(450)    | -         | -                               |
| ShouldTime  | time(7)          | -         | Soll-Arbeitszeit für diesen Tag |

### 2.2.5 ShouldTimes
Speichert die Soll-Arbeitszeit pro Tag für eine Klasse.

| Name      | Typ              | Schlüssel | Beschreibung                             |
|-----------|------------------|-----------|------------------------------------------|
| Id        | uniqueidentifier | Primär    | -                                        |
| DayOfWeek | int              | -         | Wochentag (0 = Sonntag, 1 = Montag, ...) |
| Should    | time(7)          | -         | Erwartete Arbeitszeit an diesem Tag      |
| ClassId   | uniqueidentifier | Class     | -                                        |