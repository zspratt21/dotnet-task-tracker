# Task Tracker API
An API for tracking tasks, built using .NET Core and the Entity Framework.
## Table of Contents
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Running the tests](#running-the-tests)
- [Made With](#made-with)
## Getting Started
These instructions will get you a copy of the project up and running on your local machine.
### Prerequisites
- .NET Core 7.0 or higher
- Entity Framework
- ASP.NET Core
- Docker
- An API client such as ARC or Postman
### Installation
1. Using your terminal or IDE of choice, clone the repository:
```bash
git clone https://github.com/zspratt21/TaskTracker.git
```
2. Navigate into the project folder in a terminal or your IDE of choice, create a copy of env.example and rename it to .env:
```bash
cp env.example .env
```
3. After navigating into the project folder on your system or opening the project in your IDE, start the docker containers:
```bash
docker-compose up -d
```
4. Run the migrations:
```bash
dotnet ef database update
```
### Usage
Use an API client such as ARC or Postman, to make requests to the API. By default, the API is hosted at http://localhost:5000. The following endpoints are available:
#### GET /api/Tasks
Returns a list of all tasks.
#### GET /api/Tasks/{id}
Returns a single task with the specified id.
#### POST /api/Tasks
Creates a new task. The request body should contain a JSON object with the following properties:
- name (string)
- isCompleted (bool)
#### PUT /api/Tasks/{id}
Updates an existing task with the specified id. The request body should contain a JSON object with the following properties:
- id (int)
- name (string)
- isCompleted (bool)
#### DELETE /api/Tasks/{id}
Deletes an existing task with the specified id.
### Running the tests
You can run the tests in your IDE of choice or navigate to the tests folder and run:
```bash
dotnet test
```
### Made With
[.NET Core](https://dotnet.microsoft.com/)  
[ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)  
[Entity Framework](https://docs.microsoft.com/en-us/ef/)  
[DotEnv](https://github.com/MrDave1999/dotenv.core)  
[Razor](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/)  
[xUnit](https://xunit.net/)  
[Tailwind CSS](https://tailwindcss.com/)  
[Docker](https://www.docker.com/) via [WSL2](https://learn.microsoft.com/en-us/windows/wsl/install)  
[JetBrains Rider](https://www.jetbrains.com/rider/)
