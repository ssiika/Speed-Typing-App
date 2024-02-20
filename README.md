# Speed Typing App

An app where users can improve their typing speed by typing out randomly generated text. 

**Please note that this app does currently not have a live verion. One was previously available using Azure App Services, but has since been taken down due to server costs**

## Features

Users are given 100 words of randomly generated text to type out. At the end, their score ( number of characters typed divided by time ) is calculated and saved as a record.

The users' top 10 times can be viewed on the records page. 

Unfortunately, I could not find a free API which would provide AI generated random text. Instead, I added the texts which were pre-generated online to the SQL database. 

JSON web tokens are used to secure routes and keep track of the user logged in.


## Preview

### Typing page
[![Image from Gyazo](https://i.gyazo.com/54f7c686842577d73c30753dfd01aa23.gif)](https://gyazo.com/54f7c686842577d73c30753dfd01aa23)

### Records page

![04895ee6092effd4a6ffc40f5c7d0918](https://github.com/ssiika/Speed-Typing-App/assets/102464026/78c9d22a-4499-47ce-ac25-4c0de0bb23ef)

## Built With 

![ANGULAR](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)

![Typescript](https://img.shields.io/badge/TypeScript-6F8FAF.svg?style=for-the-badge&logo=typescript&logoColor=FFF)

![ASP.NET](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

![MicrosoftSqlServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

Angular Material is used for some aspects of styling on the front end.

Entity framework is used on the back end to access the SQL database. 

SQL Server Management Studio was used during development. The database was then migrated to Azure for production.

The website itself is hosted using Azure App Service. 

