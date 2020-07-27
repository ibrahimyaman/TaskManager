# TaskManager
TaskManager is an applications tat you can plan your daily,weekly and monthly events. This repository has been developed by using .Net Core 3.1 WebApi. 

## Used Frameworks and Libraries
- Fluentvalidation for models/entities validation.
- Autofac for DI (Dependency Injection)
- EntityFrameworkCore
- Json Web Token (JWT) for Authorization
## User Operations
### Registration 
Membership registration is required to operate within the system. You should use the following URL to register
```
[POST] domain.com/api/auth/register
```
> Content Body Form Data(JSON)
```json
{
    "email":"example@mail.com",
    "password":"123456789aB+",
    "name":"First Name",
    "surname":"Last Name"
}
```
When user registiration request is sent if it is successfull operation, you will take a JWT authorization to access other operations.
> Return Value
In this object you will see a token for authorization, token expiration time, refresh token to refresh token before expire, token created time.
```json
{
    "token": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJlbWFpbCI6ImJ1cmdhejM5QG1haWwuY29tLnRyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImlicmFoaW0geWFtYW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJMb2dnZWRJbiIsIm5iZiI6MTU5NTgyNzAwMywiZXhwIjoxNTk1ODI3OTAzLCJpc3MiOiJCYWNrZW5kIERldmVsb3BlciDvv71icmFoaW0gWWFtYW4iLCJhdWQiOiJCYWNrZW5kIERldmVsb3BlciDvv71icmFoaW0gWWFtYW4ifQ.4TbE901oIi_qYqFuIYYvqpkXYh_MrRNNx0eO8zeK_C0",
    "expiration": "2020-07-27T08:31:43.5290735+03:00",
    "refreshToken": "gtUKqgPOFL0VZk_3cYWRtXeVDdUz20zh",
    "createdTime": "2020-07-27T08:16:43.7052469+03:00"
}
```
### Login 
After user registration, following part is for user log in.
```
[POST] domain.com/api/auth/login
```
> Content Body Form Data(JSON)
```json
{
    "email":"example@mail.com",
    "password":"123456789aB+",   
}
```
> Return Value
```json
{
    "token": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJlbWFpbCI6ImJ1cmdhejM5QG1haWwuY29tLnRyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImlicmFoaW0geWFtYW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJMb2dnZWRJbiIsIm5iZiI6MTU5NTgyNzE4MywiZXhwIjoxNTk1ODI4MDgzLCJpc3MiOiJCYWNrZW5kIERldmVsb3BlciDvv71icmFoaW0gWWFtYW4iLCJhdWQiOiJCYWNrZW5kIERldmVsb3BlciDvv71icmFoaW0gWWFtYW4ifQ.JmVwFko5onqTrggLc-NR0tv6dRCFifm-CRaKWSv55YQ",
    "expiration": "2020-07-27T08:34:43.3149194+03:00",
    "refreshToken": "Oi8-cRg_dGrekXaGhvmAR_XXRBHLdrKD",
    "createdTime": "2020-07-27T08:19:43.3156069+03:00"
}
```
### Refresh Token
The token given after login has time to expire. So a new token should be taken before this expiration time. You can use following part.
```
[POST] domain.com/api/auth/refresh-token/{refreshtoken}
```
This action needs authorization to refresh user token. We add "Authorization" property in request header. This will be applied for all actions that needs authorization.
> Request Header
```
Authorization = Bearer {token}
```
> Return Value
```json
{
    "token": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJlbWFpbCI6ImJ1cmdhejM5QG1haWwuY29tLnRyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImlicmFoaW0geWFtYW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJMb2dnZWRJbiIsIm5iZiI6MTU5NTgyNzE4MywiZXhwIjoxNTk1ODI4MDgzLCJpc3MiOiJCYWNrZW5kIERldmVsb3BlciDvv71icmFoaW0gWWFtYW4iLCJhdWQiOiJCYWNrZW5kIERldmVsb3BlciDvv71icmFoaW0gWWFtYW4ifQ.JmVwFko5onqTrggLc-NR0tv6dRCFifm-CRaKWSv55YQ",
    "expiration": "2020-07-27T08:34:43.3149194+03:00",
    "refreshToken": "tPl8-cRg_dOFL0VZk_3cYWRtR_XXLdr86",
    "createdTime": "2020-07-27T08:19:43.3156069+03:00"
}
```
## Plan Operations
We will need user authorization to access user informations(Id, Name etc.), while plan operations are being done. After we get JWT, new plan can be added, added plan can be updated and deleted with this JWT. 

### Importance Types
When we plan events or activities or works, we describe it by using importance/urgent priority. And there is an [Importance Matrix](https://www.groupmap.com/map-templates/urgent-important-matrix/) terminology. So we will add ImportanceTypeId in all types of plans. To do that:
```
[GET] domain.com/api/parameter/getimportancetypes
```
> Return Value
```json
[
    {
        "id": 1,
        "description": "Important - Urgent",        
    },
    {
        "id": 2,
        "description": "Important - Not Urgent",        
    },
    {
        "id": 3,
        "description": "Not Important - Urgent",        
    },
    {
        "id": 4,
        "description": "Not Important - Not Urgent",       
    }
]
```
### DailyPlan
```
[POST] domain.com/api/dailyplan/add
```
> Request Header
```
Authorization = Bearer {token}
```
> Content Body Form Data(JSON)
```json
{
    "name" :"Daily plan 1",
    "description" :"Descriptionk Daily plan 1",
    "date" : "2021-01-01",
    "ImportanceTypeId" :4

}
```
