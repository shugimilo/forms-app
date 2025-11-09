# Forms App

**Forms App** is a university project that mimics the functionality of Google Forms. It allows users to register and log in, create forms with multiple questions, and even include questions with multiple options and optional images.  

**Status:** In development

---

## Table of Contents

- [Tech Stack](#tech-stack)  
- [Features](#features)  
- [Installation](#installation)  
- [Usage](#usage)  
- [Docker](#docker)  
- [Contributing](#contributing)  
- [Author](#author)  
- [License](#license)  

---

## Tech Stack

- **Backend:** C#, ASP.NET Core, Entity Framework Core  
- **Frontend:** Razor Pages (CSHTML)  
- **Database:** SQLite (created via EF Core)  

---

## Features

- User registration and login  
- Create, edit, and delete forms  
- Add multiple questions to forms  
- Support for different question types:  
  - Short Text  
  - Long Text  
  - Single Choice  
  - Multiple Choice  
  - Numeric  
  - Date  
  - Time  
- Add multiple options for choice-based questions  
- Optional placeholder images for questions and options  
- Forms can be accessed by anonymous users or restricted to logged-in users  
- Planned features (not yet implemented):  
  - Clone existing forms  

---

## Installation

1. **Prerequisites:**  
   - [.NET 8.0 SDK](https://dotnet.microsoft.com/download)  
   - SQLite Studio (optional, for inspecting the database)  

2. **Clone the repository:**  
   ```bash
   git clone https://github.com/your-username/forms-app.git
   cd forms-app
   ```
3. Restore dependencies and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```
4. Run the project:
   ```bash
   dotnet run
   ```
   By default, the application runs at http://localhost:5000 (or a random port displayed in the terminal).

## Usage

1. Open the application in your browser (e.g., `http://localhost:5000`).  
2. Register a new user account or log in.  
3. Create a new form, add questions, and optionally add multiple choice options.  
4. Access forms anonymously or based on user permissions.  

*Screenshots and more detailed instructions will be added soon.*

---

## Docker

You can run the application inside a Docker container with a persistent database:

1. **Prerequisites:**  
   - [Docker](https://www.docker.com/) and [Docker Compose](https://docs.docker.com/compose/) installed  

2. **Run with Docker Compose:**  
   ```bash
   cd FormsApp
   docker-compose up --build
   ```
3. Notes on database persistence:
  - The **formsapp.db** file is mounted as a volume:
    ```yaml
    volumes:
      - ./formsapp.dbL/app/formsapp.db
    ```
  - This ensures that the same database is used across:
    - Local runs (dotnet run)
    - Debug sessions in Visual Studio
    - Docker containers
  - Environment variable FORMSAPP_DB=/app/formsapp.db is used inside the container for consistent paths.
4. Access the app:
  - Open [localhost port 8080](http://localhost:8080) in your browser (mapped in **docker-compose.yml**).
5. Resetting the database:
  - If needed, you can remove formsapp.db in the project root to start fresh.

---

## Contributing

If you want to contribute:

1. Fork the repository.  
2. Create a new branch for your feature.  
3. Submit a pull request.  

---

## Author

Petar Milojević - [shugimilo](https://github.com/shugimilo)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
