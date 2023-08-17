.PHONY: migrations db build-client run-client

migrations:
	cd ./backend/Backend.Infrastructure/Data && dotnet ef migrations add $(name) --project ../Backend.Infrastructure.csproj --startup-project ../../Backend.API/Backend.API.csproj && cd ../..

db:
	cd ./backend/Backend.Infrastructure/Data && dotnet ef database update --project ../Backend.Infrastructure.csproj --startup-project ../../Backend.API/Backend.API.csproj && cd ../..

run-backend:
	cd ./backend && docker-compose up --build

run-client:
	cd ./client && docker build -t nextjs-client-image . --no-cache && docker run -p 3000:3000 nextjs-client-image
