DOTNET_RUN_PROJECT = dotnet run --project
DOTNET_TEST = dotnet test
AUTOREST_GENERATE = autorest generate

run_webapi:
	$(DOTNET_RUN_PROJECT) passwordChecker.WebAPI/passwordChecker.WebAPI.csproj

test_webapi: 
	$(DOTNET_TEST)

run_console_application:
	$(DOTNET_RUN_PROJECT) passwordChecker.ConsoleApp/passwordChecker.ConsoleApp.csproj
	
generate_client:
	$(AUTOREST_GENERATE) --csharp --input-file="swagger.json" \
	--output-folder=passwordChecker.WebAPI.Client/ --namespace=passwordChecker.WebAPI.Client

run_solution: 
	$(DOTNET_RUN_PROJECT) passwordChecker.WebAPI/passwordChecker.WebAPI.csproj & 
	$(DOTNET_RUN_PROJECT) passwordChecker.ConsoleApp/passwordChecker.ConsoleApp.csproj && fg

kill_ports:
	for port in $(WS_PORT) $(CLIENT_PORT) $(SERVER_PORT) $(MOCK_PORT) ; do \
		lsof -i tcp:$${port} | awk 'NR!=1 {print $$2}' | xargs kill -TERM ; \
	done