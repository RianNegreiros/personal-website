# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY . .
RUN dotnet restore

# Copy everything else and build the API
COPY . ./
RUN dotnet publish "./BlogBackend.API/BlogBackend.API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

COPY ./BlogBackend.API/appsettings.Development.json ./

# Start the API
ENTRYPOINT ["dotnet", "BlogBackend.API.dll"]
CMD ["dotnet", "ef", "database", "update"]