// create connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hub/deathlyHallows").build();

// connect to methods that hub invokes aka receive notifications from hub
connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak, stone, wand) => {
    var cloakSpan = document.getElementById("cloakCounter");
    var stoneSpan = document.getElementById("stoneCounter");
    var wandSpan = document.getElementById("wandCounter");
    console.log("Updated counts received:", { cloak, stone, wand });
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
});

// Add click handlers for vote buttons
document.addEventListener('DOMContentLoaded', function() {
    const voteButtons = document.querySelectorAll('.vote-btn');
    voteButtons.forEach(button => {
        button.addEventListener('click', function() {
            const type = this.getAttribute('data-type');
            vote(type);
        });
    });
});

// Function to handle voting
function vote(type) {
    fetch(`/Home/DeathlyHallows?type=${type}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Vote registered:', data);
        })
        .catch(error => {
            console.error('Error voting:', error);
        });
}

// start connection
function fulfilled() {
    //do something on start
    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter => {
        var cloakSpan = document.getElementById("cloakCounter");
        var stoneSpan = document.getElementById("stoneCounter");
        var wandSpan = document.getElementById("wandCounter");
        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    }))
    console.log("Connection to Deathly Hub Successful");
}

function rejected() {
    console.error("Connection to Deathly Hub Failed");
}

connectionDeathlyHallows.start().then(fulfilled, rejected);