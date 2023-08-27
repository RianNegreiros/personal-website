# Portfolio website

This is a protfolio website that i built to post my articles and projects

<br />

Can see it live on [riannegreiros.dev](https://www.riannegreiros.dev)

![Homepage screenshot](./_docs/images/homepage.png)

## Features

- Users can
  - Log in
  - Register
  - Post comments

- Admins can
  - Create posts
  - Create projects

You can check the API endpoints [here](https://personalwebsitebackend.azurewebsites.net/swagger/index.html)

## Tecnologies

- [ASP.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio)
- [xUnit](https://xunit.net/#documentation)
- [Next.js](https://nextjs.org/docs)
- [Typescript](https://www.typescriptlang.org/docs)
- [Tailwind CSS](https://tailwindcss.com/docs/installation)
- [Docker](https://docs.docker.com)
- [Docker compose](https://docs.docker.com/compose/gettingstarted)
- [PostgreSQL](https://www.postgresql.org/about)
- [MongoDB](https://www.mongodb.com/atlas/database)

## Prerequisites

- [Docker Engine](https://docs.docker.com/engine/install)
- Make
  - [Linux](https://www.gnu.org/software/make/)
  - [Windows](https://gnuwin32.sourceforge.net/packages/make.htm)
  - [macOS](https://formulae.brew.sh/formula/make)

## Setup

1. Clone the repository
2. Set up the project environment variables in [appsettings.Development.json](https://github.com/RianNegreiros/website/blob/main/backend/Backend.API/appsettings.Development.json) or create appsettings.json for production environment
3. Run the backend with the command: `make run-backend`
4. The API can be accessed at [localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)
5. Run the client with the command: `make run-client`
6. The client can be accessed at [localhost:3000](http://localhost:3000)
