let sessionId = null;

function trackUserActivity(activityType) {
    if (!sessionId) {
        console.warn("Session ID is missing. Activity tracking is disabled.");
        return;
    }

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
