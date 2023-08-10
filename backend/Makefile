PROJECT_NAME ?= Backend
ORG_NAME ?= Backend
REPO_NAME ?= Backend

.PHONY: migrations db

migrations:
	cd ./Backend.Infrastructure/Data && dotnet ef migrations add $(name) --project ../Backend.Infrastructure.csproj --startup-project ../../Backend.API/Backend.API.csproj && cd ../..

db:
	cd ./Backend.Infrastructure/Data && dotnet ef --startup-project ../../Backend.API/ database update && cd ..