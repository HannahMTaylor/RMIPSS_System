const dbName = "RMIPSS-Database";
const storeName = "RMIPSS-DataStore";

// Open or create IndexedDB
function openDatabase() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName, 1);
        request.onupgradeneeded = event => {
            const db = event.target.result;
            if (!db.objectStoreNames.contains(storeName)) {
                db.createObjectStore(storeName, { keyPath: "formId" });
            }
        };
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject("Error opening IndexedDB");
    });
}

// Save form data (as object)
async function saveFormData(formId, data) {
    const db = await openDatabase();
    const transaction = db.transaction(storeName, "readwrite");
    transaction.objectStore(storeName).put({ formId, data });
}

// Get form data
async function getFormData(formId) {
    const db = await openDatabase();
    const transaction = db.transaction(storeName, "readonly");
    const request = transaction.objectStore(storeName).get(formId);

    return new Promise(resolve => {
        request.onsuccess = () => resolve(request.result?.data || null);
    });
}

// Clear form data
async function clearFormData(formId) {
    const db = await openDatabase();
    const transaction = db.transaction(storeName, "readwrite");
    transaction.objectStore(storeName).delete(formId);
}

// Convert FormData to plain object
function formDataToObject(formData) {
    const obj = {};
    for (const [key, value] of formData.entries()) {
        obj[key] = value;
    }
    return obj;
}

// Restore saved form data on page load
document.addEventListener("DOMContentLoaded", async function () {
    const form = document.querySelector("form");
    if (!form) return;

    const formId = form.id;
    const savedData = await getFormData(formId);

    if (savedData) {
        for (const [key, value] of Object.entries(savedData)) {
            const field = form.querySelector(`[name="${key}"]`);
            if (field) field.value = value;
        }
    }
});

// Auto-save form input using FormData
document.addEventListener("input", async function (event) {
    const form = event.target.form;
    if (!form) return;

    const formId = form.id;
    const formData = new FormData(form);
    const formObject = formDataToObject(formData);

    saveFormData(formId, formObject);
});

// Submit form automatically when online
window.addEventListener("online", async () => {
    const visibleForm = Array.from(document.querySelectorAll('form'))
        .find(form => form.id !== '');

    const formId = visibleForm?.id;
    let offlineData = await getFormData(formId);
    let StudentId;
    
    if (offlineData) {
        const formData = new URLSearchParams();
        console.log(formData);
        for (const key in offlineData) {
            if (key === "StudentId") {
                StudentId = offlineData[key];
            }
            formData.append(key, offlineData[key]);

        }
        console.log(formData);
        if (formId === "ReferralForm") {
            fetch("/Referral/SaveReferralForm", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: formData.toString()
            }).then(response => {
                if (response.ok) {
                    clearFormData(formId);
                }
            });
        }
        if (formId === "ConsentForm") {
            fetch("/ConsentForm/Create", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: formData.toString()
            }).then(async response => {
                console.log(response.text);
                if (response.ok) {
                    clearFormData(formId);
                    alert("Form submitted successfully!");
                    window.location.href = "/Student/StudentViewDetails/" + StudentId;
                } else if (response.status === 409) {
                    // Conflict detected (version mismatch)
                    alert("Conflict: Someone else has updated this form. Please refresh and try again.");
                }
                else {
                    alert("Error: Undefined Error");
                }
            })
                .catch(error => {
                    console.error("Network or unexpected error:", error);
                    alert("Network error occurred. Please try again.");
                });
        }
    }
       
});