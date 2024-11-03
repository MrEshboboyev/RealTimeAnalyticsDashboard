// userActivityUpdates.js

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/analyticsHub")
    .build();

// Listen for real-time activity updates from SignalR
connection.on("ReceiveUserActivityUpdate", function (activity) {
    const activityRow = document.createElement("tr");
    activityRow.id = `activityRow-${activity.id}`;
    activityRow.innerHTML = `
        <td>${activity.activityType}</td>
        <td>${new Date(activity.timestamp).toLocaleString()}</td>
        <td>${activity.sessionId}</td>
    `;

    // Append the new activity row to the table
    document.getElementById("userActivityTableBody").appendChild(activityRow);
});

// Start the SignalR connection
connection.start().catch(function (err) {
    return console.error(err.toString());
});
