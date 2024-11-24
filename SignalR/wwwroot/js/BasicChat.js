var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hub/basic-chat").build();

document.getElementById("sendMessage").disabled = true;
 
connectionChat.on("MessageReceived", function (user, message) {
    var li = document.createElement("li");
    li.textContent = user + " says " + message;
    document.getElementById("messagesList").appendChild(li);
    
});

document.getElementById("sendMessage").addEventListener("click", function (event){
    var sender = document.getElementById("senderEmail").value;
    var message = document.getElementById("chatMessage").value; 
    var receiver = document.getElementById("receiverEmail").value;
    if (receiver.length > 0) {
        connectionChat.invoke("SendMessageToReceiver", sender, receiver, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connectionChat.invoke("SendMessageToAll", sender, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    
    event.preventDefault(); 
});
connectionChat.start().then(function () {

    document.getElementById("sendMessage").disabled = false;

});


