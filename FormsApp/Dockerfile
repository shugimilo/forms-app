# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY FormsApp.csproj .
RUN dotnet restore FormsApp.csproj

# Copy the rest of the app and build it
COPY . .
RUN dotnet publish FormsApp.csproj -c Release -o /app/publish

# Use a smaller runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Copy your SQLite database file into the container
COPY formsapp.db .

# Expose the default ASP.NET port
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "FormsApp.dll"]
