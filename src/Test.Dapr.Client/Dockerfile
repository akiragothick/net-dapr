#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Test.Dapr.Client/Test.Dapr.Client.csproj", "Test.Dapr.Client/"]
RUN dotnet restore "Test.Dapr.Client/Test.Dapr.Client.csproj"
COPY . .
WORKDIR "/src/Test.Dapr.Client"
RUN dotnet build "Test.Dapr.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.Dapr.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.Dapr.Client.dll"]