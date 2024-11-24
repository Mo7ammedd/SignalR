// create connection
const connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hub/deathlyHallows")
    .withAutomaticReconnect()  // Add automatic reconnection
    .build();

// connect to methods that hub invokes aka receive notifications from hub
connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak, stone, wand) => {
    updateCounters(cloak, stone, wand);
});

function updateCounters(cloak, stone, wand) {
    const cloakSpan = document.getElementById("cloakCounter");
    const stoneSpan = document.getElementById("stoneCounter");
    const wandSpan = document.getElementById("wandCounter");

    if (cloakSpan) cloakSpan.innerText = cloak.toString();
    if (stoneSpan) stoneSpan.innerText = stone.toString();
    if (wandSpan) wandSpan.innerText = wand.toString();
}

// Function to handle voting
async function vote(type) {
    if (connectionDeathlyHallows.state !== signalR.HubConnectionState.Connected) {
        console.error('SignalR connection is not active');
        return;
    }

    try {
        const response = await fetch(`/Home/DeathlyHallows?type=${type}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        console.log('Vote registered:', data);

        // Optionally invoke a hub method to refresh counts
        await connectionDeathlyHallows.invoke("GetRaceStatus");
    } catch (error) {
        console.error('Error voting:', error);
    }
}

// Start connection and setup event handlers
async function startConnection() {
    try {
        await connectionDeathlyHallows.start();
        console.log("Connected to Deathly Hallows Hub");

        // Get initial race status
        const raceCounter = await connectionDeathlyHallows.invoke("GetRaceStatus");
        updateCounters(raceCounter.cloak, raceCounter.stone, raceCounter.wand);

        // Setup vote button handlers
        const voteButtons = document.querySelectorAll('.vote-btn');
        voteButtons.forEach(button => {
            button.addEventListener('click', () => {
                const type = button.getAttribute('data-type');
                vote(type);
            });
        });
    } catch (err) {
        console.error("Error starting connection:", err);
        // Retry connection after 5 seconds
        setTimeout(startConnection, 5000);
    }
}

// Connection error handling
connectionDeathlyHallows.onclose(() => {
    console.log("Connection closed, attempting to reconnect...");
});

// Initialize when DOM is ready
document.addEventListener('DOMContentLoaded', startConnection);