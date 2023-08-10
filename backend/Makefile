PROJECT_NAME ?= BlogBackend
ORG_NAME ?= BlogBackend
REPO_NAME ?= BlogBackend

.PHONY: migrations db

migrations:
	dotnet ef migrations add $(name) --project ../BlogBackend.Infrastructure.csproj --startup-project ../../BlogBackend.API/BlogBackend.API.csproj

db:
	cd ./BlogBackend.Infrastructure/Data && dotnet ef --startup-project ../../BlogBackend.API/ database update && cd ..