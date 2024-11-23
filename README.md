# Real-Time Magic with SignalR

A modern real-time communication platform built with SignalR, featuring everything from instant messaging to interactive multiplayer experiences.

## Core Features

### Live User Tracking
Real-time user presence monitoring with instant connection state updates.

### Smart Chat System
Advanced communication suite featuring:
- Global broadcast messaging
- Direct messaging via email
- Instant delivery and read receipts

### House Groups
Dynamic group management system:
- Fluid group transitions between houses
- Real-time member presence updates
- Exclusive group communications

### Interactive Racing
Multiplayer racing experience:
- Character selection system
- Live competitive gameplay
- Real-time position tracking

### Smart Notifications
Modern notification system:
- Instant push notifications
- Streamlined dropdown interface
- Live notification counter

## Tech Stack

Built with industry-standard technologies:
- ASP.NET Core
- SignalR
- JavaScript & HTML
- Bootstrap
- Entity Framework Core

## Quick Start

### 1. Setup
```sh
git clone https://github.com/Mo7ammedd/SignalR.git
cd signalr-projects
```

### 2. Prerequisites
Required:
- .NET SDK (latest)
- SQL Server/SQLite

### 3. Configuration
Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SignalR_Projects;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Database Setup
```sh
dotnet ef database update
```

### 5. Launch
```sh
dotnet run
```

## Architecture

### User Tracking Service
`UserCountHub` manages real-time user presence and connection states.

### Chat Service
`ChatHub` handles message routing and delivery with support for public and private communications.

### Group Management
`HouseGroupHub` provides real-time group membership and messaging capabilities.

### Racing System
`HallowsRaceHub` coordinates multiplayer racing events with live position updates.

### Notification Service
`NotificationHub` manages real-time alerts and message delivery tracking.

## Contributing

Join our development:
1. Fork repository
2. Branch (`git checkout -b feature/YourFeature`)
3. Commit (`git commit -m 'Add: feature description'`)
4. Push (`git push origin feature/YourFeature`)
5. Create Pull Request

## License

MIT Licensed. See LICENSE for full details.