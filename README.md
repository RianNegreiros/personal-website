# Migrated to a [app using Sanity.io as CMS](https://github.com/RianNegreiros/riannegreiros.dev) instead of creating my own backend for it.

[![Build and deploy container app to Azure Web App - Portfolio API](https://github.com/RianNegreiros/portfolio/actions/workflows/portfolio-api.yml/badge.svg)](https://github.com/RianNegreiros/portfolio/actions/workflows/portfolio-api.yml)

# Portfolio

## Table of Contents

- [Usage](#usage)
- [How to Run](#how-to-run)
- [Built With](#built-with)
- [Principles](#principles)
- [Methodologies](#methodologies)
- [Tests Features](#tests-features)
- [Author](#author)

## Usage

You can access the application, hosted at `riannegreiros.dev`, using the following URL:

[https://riannegreiros.dev](https://riannegreiros.dev)

Here are some key endpoints you may want to explore:

- [Home Page](https://riannegreiros.dev)
- [Blog Page](https://riannegreiros.dev/posts)
- [Projects Page](https://riannegreiros.dev/projects)
- [API Endpoints](https://rnds-portfolio-api.azurewebsites.net/swagger/index.html)
- [Health Checks](https://rnds-portfolio-api.azurewebsites.net/api/health)
- [RSS Feed](https://rnds-portfolio-api.azurewebsites.net/api/rss)

## How to run

### Prerequisites

- [Docker Compose](https://docs.docker.com/compose/gettingstarted/)

1- Clone the repository

```bash
git clone https://github.com/RianNegreiros/portfolio.git
```

2- Create and set up the environment variables in the `appsettings.json`/`appsettings.Development.json` using [appsettings.Development.json.example](https://github.com/RianNegreiros/portfolio/blob/main/backend/Backend.API/appsettings.Development.json.example) as reference

3- Run the application

```bash
docker compose up
```

Now you can access the client on `localhost:3000` and the API on `localhost:80/swagger`

## Built With

- [ASP.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install)
- [xUnit](https://xunit.net/#documentation)
- [Next.js](https://nextjs.org/docs)
- [Typescript](https://www.typescriptlang.org/docs)
- [Tailwind CSS](https://tailwindcss.com/docs/installation)
- [PostgreSQL](https://www.postgresql.org/about)
- [MongoDB](https://www.mongodb.com/atlas/database)
- [Redis](https://redis.io/docs/getting-started)
- [Swagger](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0)
- [Syndication](https://www.nuget.org/packages/System.ServiceModel.Syndication/)
- [FluentValidation](https://www.nuget.org/packages/FluentValidation/)
- [Hangfire](https://www.nuget.org/packages/Hangfire/)
- [BCrypt](https://www.nuget.org/packages/BCrypt.Net-Next/)
- [Cloudinary](https://www.nuget.org/packages/CloudinaryDotNet/)
- [MailKit](https://www.nuget.org/packages/MailKit/)
- [Moq](https://www.nuget.org/packages/Moq/)

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
