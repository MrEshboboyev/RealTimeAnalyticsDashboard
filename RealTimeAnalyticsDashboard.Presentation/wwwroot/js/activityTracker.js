let sessionId = null;

function trackUserActivity(activityType) {
    if (!sessionId) {
        console.warn("Session ID is missing. Activity tracking is disabled.");
        return;
    }

    // Logging
    const requestBody = {
        activityType: activityType
    };
    console.log("Sending activity data:", JSON.stringify(requestBody)); // Log the exact payload

    fetch(`/User/UserActivity/Create/${sessionId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            activityType: activityType
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => console.log('Activity tracked:', data))
        .catch(error => console.error('Error tracking activity:', error));
}

// Fetch the session ID when the page loads
document.addEventListener('DOMContentLoaded', () => {
    fetch('/api/user/session')
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("No active session found or user not authenticated.");
            }
        })
        .then(data => {
            sessionId = data.sessionId;
            console.log("Session ID retrieved:", sessionId);

            // Example: Track an initial page view
            trackUserActivity('PageView');
        })
        .catch(error => console.warn(error.message));
});
