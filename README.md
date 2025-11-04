[![.NET Build & Test](https://github.com/marlenhalvorsen/KAW/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/marlenhalvorsen/KAW/actions/workflows/dotnet.yml)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)
# KAW – North Jutlandic Dictionary of *kaw’e* Expressions

**KAW** is a word from the North Jutland dialect. It describes something difficult, heavy, or a bit troublesome – e.g., *a kaw case*.

This project is a **practice playground** where I explore software architecture and coding patterns, while also having fun with North Jutlandic expressions.

## Tech Stack
- .NET 8
- Entity Framework Core
- xUnit + Moq
- GitHub Actions (CI)

## Continuous Integration & Branch Policy

All code changes go through Pull Requests against the `master` branch.  
Each PR triggers the .NET Build & Test GitHub Actions workflow, which must pass before merging.  
Branch protection rules ensure that:
- The PR branch is up to date with `master`
- All checks (build & tests) pass successfully
- No direct pushes are allowed to `master`

This setup keeps the master branch stable and production-ready at all times.

## Project Structure
This solution follows a layered (hexagonal) architecture:

- **Domain** – Core entities and models  
- **Application** – Business logic, services, interfaces  
- **Infrastructure** – EF Core, repositories, and persistence  
- **Web** – (Coming soon) API layer  

🛈 The previous console app used for manual testing has been removed to keep the solution clean and focused.

## Purpose
I use the project as a learning ground to:
- Revisit C# fundamentals
- Work with Hexagonal architecture (Domain, Application, Infrastructure, Web)
- Apply SOLID principles in practice
- Develop iteratively: starting as a console app, later evolving into a Web API (and possibly a frontend)
- Ensure quality with testing

## Status
- [x] Basic structure (Domain, Application, Infrastructure)
- [x] Repository implementation
- [x] In-memory list with methods
- [x] EF Core integration
- [x] Refactor repo and service for DbContext
- [ ] Integration of message broker for event-based architecture
- [ ] API endpoints

## Getting Started

Quick setup guide for running KAW locally.

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Optional: [Docker](https://www.docker.com/) if you plan to containerize the service later

### Clone & Build
```bash
git clone https://github.com/MarlenHalvorsen/kaw.git
cd kaw
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release
```
