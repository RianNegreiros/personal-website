# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY . .
RUN dotnet restore

# Copy everything else and build the API
RUN dotnet publish "./Backend.API/Backend.API.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy published output and appsettings
COPY --from=build-env /app/out .
COPY ./Backend.API/appsettings.Development.json ./

# Start the API
ENTRYPOINT ["dotnet", "Backend.API.dll"]
CMD ["dotnet", "ef", "database", "update"]
