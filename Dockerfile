FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY ./backend .
RUN dotnet restore

# Copy everything else and build the API
COPY ./backend ./
RUN dotnet publish "./Backend.API/Backend.API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Backend.API.dll"]