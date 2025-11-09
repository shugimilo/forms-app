# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FormsApp.csproj .
RUN dotnet restore FormsApp.csproj

COPY . .
RUN dotnet publish FormsApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Do NOT copy the DB at build time — we want to mount it from host for persistence

EXPOSE 8080
ENTRYPOINT ["dotnet", "FormsApp.dll"]
