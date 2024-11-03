const connection = new signalR.HubConnectionBuilder()
    .withUrl("/analyticsHub")
    .build();

connection.on("ReceiveSessionUpdate", function (session) {
    // Find the existing row for the session using its ID
    const existingRow = document.getElementById(`sessionRow-${session.id}`);

    if (existingRow) {
        // Update the EndTime cell only
        const endTimeCell = existingRow.cells[3]; // EndTime is in the 4th column (index 3)
        endTimeCell.textContent = session.endTime ? new Date(session.endTime).toLocaleString() : "Ongoing";

        // If you want to highlight the row to indicate that it was updated, you can add a class or animation
        existingRow.classList.add("highlight");
        setTimeout(() => existingRow.classList.remove("highlight"), 3000); // Remove highlight after 2 seconds
    } else {
        // If the session row does not exist, create and append a new one
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
        document.getElementById("sessionTableBody").appendChild(sessionRow);
    }
});

// Start the SignalR connection
connection.start().catch(function (err) {
    return console.error(err.toString());
});
