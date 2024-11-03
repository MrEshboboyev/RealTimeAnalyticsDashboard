function trackUserActivity(activityType, sessionId) {
    fetch('/User/UserActivity/Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            activityType: activityType,
            sessionId: sessionId
        })
    })
        .then(response => response.json())
        .then(data => console.log('Activity tracked:', data))
        .catch(error => console.error('Error tracking activity:', error));
}

// Example usage for different activity types
document.addEventListener('DOMContentLoaded', () => {
    // Track a page view
    trackUserActivity('PageView', sessionId);  // Replace `sessionId` with the actual ID

    // Track a click on a specific button
    document.getElementById('someButton').addEventListener('click', () => {
        trackUserActivity('Click', sessionId);  // Replace `sessionId` with the actual ID
    });
});
