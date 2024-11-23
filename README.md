```markdown
# SignalR Project

This project demonstrates the use of SignalR in an ASP.NET Core application. It includes various hubs for real-time communication and interaction.

## Table of Contents

- [Technologies](#technologies)
- [Setup](#setup)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Technologies

- ASP\.NET Core
- SignalR
- JavaScript
- TypeScript
- npm
- Bootstrap
- Toastr

## Setup

1. **Clone the repository:**
   ```sh
   git clone https://github.com/Mo7ammedd/SignalR.git
   cd signalr-project
   ```

2. **Install dependencies:**
   ```sh
   npm install
   ```

3. **Build the project:**
   ```sh
   dotnet build
   ```

4. **Run the project:**
   ```sh
   dotnet run
   ```

## Usage

- Navigate to `http://localhost:5000` in your browser.
- Interact with the various SignalR hubs through the UI.

### SignalR Hubs

- **DeathlyHallowsHub:** Tracks and updates the count of Deathly Hallows.
- **HouseGroupsHub:** Manages user subscriptions to different house groups.
- **UserHub:** Tracks the total number of users and viewers.

### JavaScript Integration

- The `HouseGroup.js` file handles client-side interactions with the `HouseGroupsHub`.

## Project Structure

```plaintext
SignalR/
├── Controllers/
│   └── HomeController.cs
├── Hubs/
│   ├── DeathlyHallowsHub.cs
│   ├── HouseGroupsHub.cs
│   └── UserHub.cs
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   ├── js/
│   │   └── HouseGroup.js
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── Models/
│   └── ErrorViewModel.cs
├── Helpers/
│   └── SD.cs
└── Program.cs
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License.
```