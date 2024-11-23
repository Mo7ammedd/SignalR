// Create a connection to the hub
var connectionUserCount = new signalR.HubConnectionBuilder()
    .withUrl("/hub/userCount",signalR.HttpTransportType.WebSockets).build();

// Connect  to method in the hub invokes aka receives the message

connectionUserCount.on("UpdateTotalView",(value)=>{
    var newCountSpan = document.getElementById("TotalViewsCounter");
    newCountSpan.innerHTML = value.toString();
})
connectionUserCount.on("UpdateTotalUsers",(value)=>{
    var newCountSpan = document.getElementById("TotalUsersCounter");
    newCountSpan.innerHTML = value.toString();
})
// invoke hub methods

function newWindowLoadedOnClientSide(){
    connectionUserCount.invoke("NewWindowLoaded");
}

// Start the connection
connectionUserCount.start().then(function(){
    console.log("Connected to UserCount Hub");
    newWindowLoadedOnClientSide();
}).catch(function(err){
    return console.error(err.toString());
});