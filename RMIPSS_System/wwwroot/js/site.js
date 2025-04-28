function toggleSidebar() {
    let body = document.body;
    body.classList.toggle("collapsed");
}

async function updateOnlineOfflineStatus() {
    const statusDiv = document.getElementById("connection-status");
    if (!statusDiv) return;

    const online = await isActuallyOnline();
    console.log("Detected status:", online); // should be true or false

    if (online) {
        statusDiv.textContent = "🟢 You are online.";
        statusDiv.style.backgroundColor = "#d4edda";
        statusDiv.style.color = "#155724";
    } else {
        statusDiv.textContent = "🔴 You are offline.";
        statusDiv.style.backgroundColor = "#f8d7da";
        statusDiv.style.color = "#721c24";
    }
}

async function isActuallyOnline() {
    try {
        const response = await fetch('/api/ping?ver=' + Date.now(), {
            method: 'HEAD',
            cache: 'no-store',
        });
        
        return response.ok;
    } catch (error) {
        return false;
    }
}

// Trigger on page load
window.addEventListener("DOMContentLoaded", () => updateOnlineOfflineStatus());

// Trigger on connection status change
window.addEventListener("online", () => updateOnlineOfflineStatus());
window.addEventListener("offline", () => updateOnlineOfflineStatus());