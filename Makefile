.PHONY: migrations db run run-tests

migrations:
	cd ./backend/Backend.Infrastructure/Data && dotnet ef migrations add $(name) --project ../Backend.Infrastructure.csproj --startup-project ../../Backend.API/Backend.API.csproj && cd ../..

db:
	cd ./backend/Backend.Infrastructure/Data && dotnet ef database update --project ../Backend.Infrastructure.csproj --startup-project ../../Backend.API/Backend.API.csproj && cd ../..

run:
	docker compose up --build

run-tests:
	cd ./backend && docker build -t dotnet-tests -f Dockerfile.tests . && docker run --rm dotnet-tests