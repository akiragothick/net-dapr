# Levantar mediante comandos

dapr run --app-id myfrontend --app-port 7001 --app-protocol http --dapr-http-port 3501 -- dotnet run

dapr run --app-id mybackend --app-port 7002 --app-protocol http --dapr-http-port 3500 -- dotnet run

dapr run --app-id myfrontend --app-port 7001 -- dotnet run

dapr run --app-id mybackend --app-port 7002 -- dotnet run

# Tambien disponible para levantar con docker compose



# Llamada mediante http

[GET] http://localhost:3500/v1.0/invoke/mybackend/method/weatherforecast

dapr invoke -a mybackend -m weatherforecast -v GET
