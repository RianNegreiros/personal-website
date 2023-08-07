PROJECT_NAME ?= BlogBackend
ORG_NAME ?= BlogBackend
REPO_NAME ?= BlogBackend

.PHONY: migrations db

migrations:
	cd ./BlogBackend.Infrastructure/Data && dotnet ef --startup-project ../../BlogBackend.API/ migrations add $(name) && cd ..

db:
	cd ./BlogBackend.Infrastructure/Data && dotnet ef --startup-project ../../BlogBackend.API/ database update && cd ..