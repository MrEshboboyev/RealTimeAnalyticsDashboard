// sessionUpdates.js

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/analyticsHub")
    .build();

connection.on("ReceiveSessionUpdate", function (session) {
    const sessionRow = document.createElement("tr");
    sessionRow.id = `sessionRow-${session.id}`;
    sessionRow.innerHTML = `
        <td>${session.id}</td>
        <td>${session.userDTO.fullName} (${session.appUserId})</td>
        <td>${new Date(session.startTime).toLocaleString()}</td>
        <td>${session.endTime ? new Date(session.endTime).toLocaleString() : "Ongoing"}</td>
        <td>
            <ul class="list-unstyled mb-0">
                ${session.userActivityDTOs.map(activity => `
                    <li>${activity.activityType} - ${new Date(activity.timestamp).toLocaleString()}</li>
                `).join("")}
            </ul>
        </td>
    `;

    // Append the new session row to the table body
    document.getElementById("sessionTableBody").appendChild(sessionRow);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
