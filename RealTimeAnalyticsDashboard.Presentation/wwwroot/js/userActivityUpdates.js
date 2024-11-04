const connection = new signalR.HubConnectionBuilder()
    .withUrl("/analyticsHub")
    .build();

// Listen for real-time activity updates from SignalR
connection.on("ReceiveUserActivityUpdate", function (activity) {
    // Check if the activity row already exists to prevent duplicates
    if (document.getElementById(`activityRow-${activity.id}`)) {
        console.warn(`Activity ID ${activity.id} already exists in the table.`);
        return;
    }

    // Create a new row for the activity
    const activityRow = document.createElement("tr");
    activityRow.id = `activityRow-${activity.id}`;
    activityRow.innerHTML = `
        <td>${activity.id}</td>
        <td>${activity.activityType}</td>
        <td>${new Date(activity.timestamp).toLocaleString()}</td>
        <td>${activity.sessionId}</td>
        <td>
            <form action="/User/UserActivity/Delete/${activity.id}" method="post" style="display:inline;">
                <button type="submit" class="btn btn-danger btn-sm">Delete</button>
            </form>
        </td>
    `;

    // Append the new activity row to the table body
    document.getElementById("userActivityTableBody").appendChild(activityRow);
});

// Start the SignalR connection
connection.start().catch(function (err) {
    console.error("Error establishing SignalR connection:", err);
});
