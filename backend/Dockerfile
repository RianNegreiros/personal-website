# Use the official ASP.NET Core SDK image as a build environment.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory to /app.
WORKDIR /app

# Copy the project file and restore dependencies.
COPY *.sln .
COPY Backend.API/Backend.API.csproj ./Backend.API/
COPY Backend.Application/Backend.Application.csproj ./Backend.Application/
COPY Backend.Core/Backend.Core.csproj ./Backend.Core/
COPY Backend.Infrastructure/Backend.Infrastructure.csproj ./Backend.Infrastructure/
COPY Backend.Tests/Backend.Tests.csproj ./Backend.Tests/
# Repeat the above line for any other project references.

# Restore NuGet packages.
RUN dotnet restore

# Copy the rest of the application code.
COPY . .

# Build the application with Release configuration.
RUN dotnet build -c Release --no-restore

# Publish the application for deployment.
RUN dotnet publish ./Backend.API/Backend.API.csproj -c Release -o /app/publish

# Build the final runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory to /app.
WORKDIR /app

# Copy the published application from the build environment.
COPY --from=build /app/publish .

# Expose the port that the application will run on.
EXPOSE 80

ENTRYPOINT ["dotnet", "Backend.API.dll"]
CMD ["dotnet", "ef", "database", "update"]