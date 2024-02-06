# Portfolio website

## Deployment on Azure with Azure Docker Registry

[![Build and deploy to Azure Web App](https://github.com/RianNegreiros/portfolio/actions/workflows/main_personalwebsitebackend.yml/badge.svg)](https://github.com/RianNegreiros/portfolio/actions/workflows/main_personalwebsitebackend.yml)

## Usage

You can access the application, hosted at `riannegreiros.dev`, using the following URL:

[https://riannegreiros.dev](https://riannegreiros.dev)

Here are some key endpoints you may want to explore:

- [Home Page](https://riannegreiros.dev)
- [Blog Page](https://riannegreiros.dev/posts)
- [Projects Page](https://riannegreiros.dev/projects)
- [API Endpoints](https://personalwebsitebackend.azurewebsites.net/swagger/index.html)
- [Health Checks](https://personalwebsitebackend.azurewebsites.net/health)
- [RSS Feed](https://personalwebsitebackend.azurewebsites.net/api/posts/rss)

## How to run

### Prerequisites

- [Docker Engine](https://docs.docker.com/engine/install)
- Make
  - [Linux](https://www.gnu.org/software/make/)
  - [Windows](https://gnuwin32.sourceforge.net/packages/make.htm)
  - [macOS](https://formulae.brew.sh/formula/make)

1- Clone the repository

```bash
git clone https://github.com/RianNegreiros/portfolio.git
```

2- Create and set up the environment variables in the `appsettings.json`/`appsettings.Development.json` using [appsettings.Development.json.example](https://github.com/RianNegreiros/portfolio/blob/main/backend/Backend.API/appsettings.Development.json.example) as reference

3- Run the tests

```bash
make run-tests
```

4- Run the backend on `localhost:5000`

```bash
make run-backend
```

5- Run the client on `localhost:3000`

```bash
make run-client
```

## Libraries and Tools

- [ASP.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install)
- [xUnit](https://xunit.net/#documentation)
- [Next.js](https://nextjs.org/docs)
- [Typescript](https://www.typescriptlang.org/docs)
- [Tailwind CSS](https://tailwindcss.com/docs/installation)
- [Docker](https://docs.docker.com)
- [Docker compose](https://docs.docker.com/compose/gettingstarted)
- [PostgreSQL](https://www.postgresql.org/about)
- [MongoDB](https://www.mongodb.com/atlas/database)
- [Redis](https://redis.io/docs/getting-started)
- [Swagger](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0)

## Principles

- SOLID
- Don't Repeat Yourself
- You Aren't Gonna Need It
- Keep It Simple
- Small Commits

## Methodologies

- Clean Architecture
- GitFlow
- Conventional Commits
- Modular Design
- Refactoring
- Continuous Integration
- Continuous Delivery
- Continuous Deployment

## Tests Features

- Unit Tests
- Mocks

## Author

Rian Negreiros Dos Santos

[![Linkedin Badge](https://img.shields.io/badge/-RianNegreiros-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/tgmarinho/)](https://www.linkedin.com/in/riannegreiros/)

[![Gmail Badge](https://img.shields.io/badge/-riannegreiros@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:tgmarinho@gmail.com)](mailto:riannegreiros@gmail.com)
