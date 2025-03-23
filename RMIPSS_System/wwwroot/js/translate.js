async function translatePageToMarshallese() {
    let elements = document.querySelectorAll("p, h1, h2, h3, h4, h5, h6, span, label, td, div"); // Select all text-based elements
    let chunkSize = 500; // MyMemory API limit
    let delayBetweenRequests = 1000; // Delay to prevent API rate limit

    for (let element of elements) {
        let text = element.innerText.trim();
        if (text.length === 0) continue; // Skip empty elements

        // Split long text into chunks
        let chunks = [];
        for (let i = 0; i < text.length; i += chunkSize) {
            chunks.push(text.substring(i, i + chunkSize));
        }

        let translatedChunks = [];

        for (let chunk of chunks) {
            let response = await fetch("/Language/TranslatePage/Marshallese", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Text: chunk })
            });

            let data = await response.json();

            if (data.TranslatedText) {
                translatedChunks.push(data.TranslatedText);
            } else {
                console.error("Translation failed for chunk:", chunk);
                translatedChunks.push(chunk); // Keep original if translation fails
            }

            await new Promise(resolve => setTimeout(resolve, delayBetweenRequests)); // Delay between requests
        }

        element.innerText = translatedChunks.join(" "); // Replace text while keeping UI structure
    }
}
const API_KEY = "AIzaSyB8A-M6MZxLUpJ0y7ByAej_ssauYr2uUQU";
let originalTextMap = new Map(); // Store original text for toggling back to English
let currentLang = "en";
async function toggleTranslation() {
    currentLang = currentLang === "en" ? "mh" : "en"; // Toggle between Marshallese & English
    let elements = document.querySelectorAll(".translate-text"); // Only translate elements with this class

    for (let element of elements) {
        let originalText = element.innerText?.trim() || element.placeholder || element.value || ""; // Ensure text is not undefined

        // Store original text if not already stored (for toggling back to English)
        if (!originalTextMap.has(element)) {
            originalTextMap.set(element, originalText);
        }

        // If toggling back to English, restore original text
        if (currentLang === "en") {
            if (element.tagName === "INPUT" || element.tagName === "TEXTAREA") {
                element.placeholder = originalTextMap.get(element);
            } else if (element.tagName === "OPTION" || element.tagName === "BUTTON") {
                element.innerText = originalTextMap.get(element);
            } else {
                element.innerHTML = originalTextMap.get(element);
            }
            continue;
        }

        let text = element.innerText?.trim() || element.placeholder || element.value || "";
        if (!text || text.length === 0) continue; // Skip empty elements

        try {
            let translatedText = await translateText(text, currentLang);

            // Apply translation while preserving UI
            if (element.tagName === "INPUT" || element.tagName === "TEXTAREA") {
                element.placeholder = translatedText;
            } else if (element.tagName === "OPTION" || element.tagName === "BUTTON") {
                element.innerText = translatedText;
            } else {
                element.innerHTML = originalText.replace(text, translatedText);
            }
        } catch (error) {
            console.error("Translation failed for:", text, error);
        }
    }
}
async function translateText(text, targetLang) {
    let url = `https://translation.googleapis.com/language/translate/v2?key=${API_KEY}`;

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            q: text,
            target: targetLang,
            source: "en",
            format: "text"
        })
    });

    let data = await response.json();
    return data.data?.translations[0]?.translatedText || text;
}



