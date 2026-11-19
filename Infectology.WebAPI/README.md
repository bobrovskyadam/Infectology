# Infectology API Documentation

This API is used for communication between the frontend and backend of the Infectology system.

* Data format: **JSON**
* Date format: **ISO 8601**
* Authentication: **JWT (JSON Web Token)**

Protected endpoints require a valid token in the `Authorization` header:

```
Authorization: Bearer {token}
```

Error responses always have the following structure:

```json
{
  "message": "Error description"
}
```

---

## Authentication

### POST `/api/Login`

Logs user into the system and returns a JWT token.

**Request**

```json
{
  "email": "admin@test.sk",
  "password": "heslo123"
}
```

**Response (200 OK)**

```json
{
  "token": "jwt-token",
  "email": "admin@test.sk",
  "role": "Admin",
  "firstName": "Admin",
  "lastName": "Admin"
}
```

**Response (401 Unauthorized)**

```json
{
  "message": "Invalid credentials."
}
```

---

### POST `/api/Register`

Creates a new user account.

**Request**

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1985-03-20",
  "titleBefore": "MD",
  "titleAfter": null,
  "email": "john.doe@hospital.sk",
  "phoneNumber": "+421 900 000 000",
  "mobileNumber": null,
  "personalAddress": "Main Street 1",
  "personalPostalCode": "040 01",
  "workplaceAddress": "Hospital Road 5",
  "workplacePostalCode": "040 01",
  "workplacePhone": "+421 55 123 4567",
  "workplaceEmail": "john.doe@hospital.sk",
  "workplaceFunction": "Infectologist",
  "focus": "HIV, Hepatitis",
  "graduationYear": 2010,
  "graduationPlace": "Faculty of Medicine, UPJŠ Košice",
  "chamber": "Slovak Medical Chamber",
  "chamberId": "SMC-12345",
  "password": "MyPassword123!"
}
```

**Response (201 Created)**

```json
{
  "id": 42,
  "firstName": "John",
  "lastName": "Doe",
  "role": "Doctor"
}
```

**Response (409 Conflict)**

```json
{
  "message": "Email already exists."
}
```

---

## Users (Admin only)

### GET `/api/User`

Returns a list of all users.

**Response (200 OK)**

```json
[
  {
    "id": 1,
    "firstName": "Admin",
    "lastName": "Admin",
    "role": "Admin",
    "email": "admin@test.sk",
    "phoneNumber": null,
    "isVerified": true,
    "createdDateTime": "2026-04-30T12:00:00Z"
  }
]
```

---

### GET `/api/User/{id}`

Returns a specific user by ID.

**Response (404 Not Found)**

```json
{
  "message": "User not found."
}
```

---

### PUT `/api/User/{id}`

Used by Admin to change user role or verify user account.

**Request**

```json
{
  "id": 42,
  "role": "Doctor",
  "isVerified": true
}
```

**Response (204 No Content)**

---

### DELETE `/api/User/{id}`

Deletes a user.

**Response (204 No Content)**

---

## Articles

Public read access. Creating and editing requires **Editor** or **Admin** role.
The `authorId` is automatically assigned based on the logged-in user.

### GET `/api/Article`

Returns all articles.

```json
[
  {
    "id": 1,
    "heading": "New COVID-19 Guidelines",
    "headingImage": "https://cdn.example.com/img/covid.jpg",
    "dateTimeCreated": "2026-04-20T09:00:00Z",
    "dateTimeEdited": null,
    "markdown": "# Article Content\n\nText...",
    "authorId": 1
  }
]
```

---

### GET `/api/Article/{id}`

Returns a specific article.

**Response (404 Not Found)**

```json
{
  "message": "Article not found."
}
```

---

### POST `/api/Article`

Creates a new article.

```json
{
  "heading": "Article title",
  "headingImage": null,
  "markdown": "Content..."
}
```

**Response (201 Created)**

---

### PUT `/api/Article/{id}`

Updates an existing article.

```json
{
  "id": 1,
  "heading": "Updated title",
  "headingImage": "https://cdn.example.com/img/new.jpg",
  "markdown": "Updated content"
}
```

**Response (204 No Content)**

---

### DELETE `/api/Article/{id}`

Deletes an article.

**Response (204 No Content)**

---

## Events

Public read access. Editing requires **Editor** or **Admin** role.
The `authorId` is automatically assigned based on the logged-in user.

### GET `/api/Event`

Returns all events.

```json
[
  {
    "id": 1,
    "name": "Infectology Conference 2026",
    "start": "2026-06-15",
    "end": "2026-06-17",
    "place": "Košice",
    "link": "https://conference.sk",
    "authorId": 1
  }
]
```

---

### GET `/api/Event/{id}`

Returns a specific event.

**Response (404 Not Found)**

```json
{
  "message": "Event not found."
}
```

---

### POST `/api/Event`

Creates a new event.

```json
{
  "name": "Workshop",
  "start": "2026-09-10",
  "end": null,
  "place": "Online",
  "link": "https://zoom.us/j/123456"
}
```

**Response (201 Created)**

---

### PUT `/api/Event/{id}`

Updates an event.

```json
{
  "id": 1,
  "name": "Updated event",
  "start": "2026-06-16",
  "end": "2026-06-18",
  "place": "Košice",
  "link": "https://conference.sk"
}
```

**Response (204 No Content)**

---

### DELETE `/api/Event/{id}`

Deletes an event.

**Response (204 No Content)**

---

## Roles

| Role   | Description                    |
| ------ | ------------------------------ |
| User   | Basic registered user          |
| Doctor | Verified doctor                |
| Editor | Can manage articles and events |
| Admin  | Full system access             |

---

## HTTP Status Codes

| Code | Meaning      |
| ---- | ------------ |
| 200  | OK           |
| 201  | Created      |
| 204  | No Content   |
| 400  | Bad Request  |
| 401  | Unauthorized |
| 403  | Forbidden    |
| 404  | Not Found    |
| 409  | Conflict     |