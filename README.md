# 🏎️ F1 Fantasy League

> A comprehensive full-stack Fantasy Formula 1 application built with .NET 8 and React

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-18.2-61DAFB?style=flat-square&logo=react)](https://reactjs.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.0-3178C6?style=flat-square&logo=typescript)](https://www.typescriptlang.org/)
[![License](https://img.shields.io/badge/License-MIT-green.svg?style=flat-square)](LICENSE)

## 📖 Overview

F1 Fantasy League is a modern web application that allows Formula 1 fans to create fantasy teams, compete with friends, and track their performance throughout the F1 season. Built with Clean Architecture principles and modern web technologies.

### ✨ Key Features

- 🏆 **Fantasy Team Management** - Create and manage your F1 fantasy team
- 👥 **Private Leagues** - Compete with friends in custom leagues
- 📊 **Real-time Scoring** - Automatic points calculation based on real F1 results
- 📈 **Advanced Statistics** - Detailed performance analytics and insights
- 🔄 **Transfer System** - Strategic player trading between races
- 📱 **Responsive Design** - Perfect experience on all devices
- 🔔 **Live Updates** - Real-time race tracking and notifications

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────┐
│   Presentation  │  ← React Frontend + .NET API Controllers
├─────────────────┤
│   Application   │  ← CQRS with MediatR, Business Logic
├─────────────────┤
│     Domain      │  ← Entities, Value Objects, Domain Services
├─────────────────┤
│ Infrastructure  │  ← Data Access, External APIs, Services
└─────────────────┘
```

### 🛠️ Tech Stack

**Backend:**
- .NET 8 Web API
- Entity Framework Core
- MediatR (CQRS Pattern)
- AutoMapper
- FluentValidation
- JWT Authentication
- SignalR (Real-time)

**Frontend:**
- React 18 with TypeScript
- Tailwind CSS
- React Query/TanStack Query
- React Router v6
- Zustand (State Management)

**Database:**
- SQL Server / PostgreSQL
- Redis (Caching)

**External APIs:**
- Ergast F1 API (Historical Data)
- OpenF1 API (Live Data)

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or [PostgreSQL](https://www.postgresql.org/)
- [Redis](https://redis.io/) (optional, for caching)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/f1-fantasy-league.git
   cd f1-fantasy-league
   ```

2. **Backend Setup**
   ```bash
   # Navigate to the API project
   cd src/F1Fantasy.API
   
   # Install dependencies
   dotnet restore
   
   # Update database connection string in appsettings.json
   # Run database migrations
   dotnet ef database update
   
   # Start the API
   dotnet run
   ```

3. **Frontend Setup**
   ```bash
   # Navigate to the client project
   cd client
   
   # Install dependencies
   npm install
   
   # Start the development server
   npm start
   ```

4. **Access the application**
   - API: `https://localhost:7001`
   - Frontend: `http://localhost:3000`
   - Swagger UI: `https://localhost:7001/swagger`

### 🐳 Docker Setup

```bash
# Run the entire application with Docker Compose
docker-compose up -d

# Or build and run individual containers
docker build -t f1fantasy-api ./src/F1Fantasy.API
docker build -t f1fantasy-client ./client
```

## 📁 Project Structure

```
F1Fantasy/
├── src/
│   ├── F1Fantasy.API/           # Web API Controllers
│   ├── F1Fantasy.Application/   # Business Logic (CQRS)
│   ├── F1Fantasy.Domain/        # Domain Entities
│   ├── F1Fantasy.Infrastructure/# Data Access & External Services
│   └── F1Fantasy.Shared/        # Common DTOs and Utilities
├── client/                      # React Frontend
├── tests/
│   ├── F1Fantasy.UnitTests/
│   └── F1Fantasy.IntegrationTests/
├── docs/                        # Documentation
└── docker-compose.yml
```

## 🎮 How to Play

1. **Create Account** - Register with your email and create a profile
2. **Build Your Team** - Select 2 drivers and 1 constructor within budget
3. **Join Leagues** - Create private leagues or join existing ones
4. **Set Your Captain** - Choose a driver to receive double points
5. **Make Transfers** - Trade drivers between races (limited transfers)
6. **Track Performance** - Monitor your team's performance and league standings

### 💰 Budget System

- Starting budget: **$100.0 million**
- Drivers cost between $4.0M - $15.0M based on performance
- Constructors cost between $8.0M - $25.0M
- Prices fluctuate based on popularity and performance

### 🏆 Scoring System

**Driver Points:**
- Race Position: 25pts (1st), 18pts (2nd), 15pts (3rd)... 1pt (10th)
- Pole Position: +3pts
- Fastest Lap: +2pts
- Captain Bonus: 2x all points

**Constructor Points:**
- Based on combined driver performance
- Bonus for race wins and podium finishes

## 🔧 Development

### Code Style

This project follows established .NET and React conventions:
- **C#**: Microsoft coding standards
- **TypeScript/React**: ESLint + Prettier configuration
- **Commit Messages**: Conventional Commits

### Testing

```bash
# Run backend tests
dotnet test

# Run frontend tests
cd client && npm test
```

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update
```

## 📊 API Documentation

The API is fully documented with Swagger/OpenAPI. When running the application, visit:
- **Swagger UI**: `https://localhost:7001/swagger`
- **OpenAPI Spec**: `https://localhost:7001/swagger/v1/swagger.json`

### Key Endpoints

```
Authentication:
POST /api/auth/register
POST /api/auth/login

Fantasy Teams:
GET    /api/fantasy-teams/my-teams
POST   /api/fantasy-teams
PUT    /api/fantasy-teams/{id}

Leagues:
GET    /api/leagues/my-leagues
POST   /api/leagues
POST   /api/leagues/join

Transfers:
POST   /api/transfers
GET    /api/transfers/validate

Leaderboards:
GET    /api/leaderboards/global
GET    /api/leaderboards/league/{id}
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Ergast Developer API](http://ergast.com/mrd/) for Formula 1 data
- [OpenF1 API](https://openf1.org/) for live timing data
- Formula 1 for the amazing sport that inspired this project

## 📞 Support

If you have any questions or need help with setup, please:
- 📧 Email: [perjansdavid@gmail.com]
- 💬 Create an issue on GitHub
- 📖 Check the [Wiki](https://github.com/davidperjans/f1-fantasy-league/wiki)

---

<p align="center">
  <strong>Made with ❤️ for F1 fans around the world</strong>
</p>

<p align="center">
  <sub>Built as a portfolio project to demonstrate full-stack development skills</sub>
</p>
