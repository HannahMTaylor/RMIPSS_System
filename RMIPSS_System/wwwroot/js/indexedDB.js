const dbName = "RMIPSS-Database";
const stores = {
    submitted: "SubmittedFormData",
    unsubmitted: "UnsubmittedFormData"
};

// Open or create IndexedDB with multiple object stores
function openDatabase() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName, 1);
        request.onupgradeneeded = event => {
            const db = event.target.result;
            if (!db.objectStoreNames.contains(stores.submitted)) {
                db.createObjectStore(stores.submitted, { keyPath: "formId" });
            }
            if (!db.objectStoreNames.contains(stores.unsubmitted)) {
                db.createObjectStore(stores.unsubmitted, { keyPath: "formId" });
            }
        };
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject("Error opening IndexedDB");
    });
}

// Save form data (as object)
async function saveFormData(formId, data) {
    const db = await openDatabase();
    const transaction = db.transaction(stores.unsubmitted, "readwrite");
    transaction.objectStore(stores.unsubmitted).put({ formId, data });
}

// Get form data
async function getFormData(formId) {
    const db = await openDatabase();
    const transaction = db.transaction(stores.unsubmitted, "readonly");
    const request = transaction.objectStore(stores.unsubmitted).get(formId);

    return new Promise(resolve => {
        request.onsuccess = () => resolve(request.result?.data || null);
    });
}

// Clear form data
async function clearFormData(formId) {
    const db = await openDatabase();
    const transaction = db.transaction(stores.submitted, "readwrite");
    transaction.objectStore(stores.submitted).delete(formId);
}

async function moveToSubmitted(formId) {
    const db = await openDatabase();
    const unsubmittedTx = db.transaction(stores.unsubmitted, "readwrite");
    const unsubmittedStore = unsubmittedTx.objectStore(stores.unsubmitted);
    const unsubmittedData = await new Promise(resolve => {
        const req = unsubmittedStore.get(formId);
        req.onsuccess = () => resolve(req.result?.data || null);
    });

    if (unsubmittedData) {
        const submittedTx = db.transaction(stores.submitted, "readwrite");
        submittedTx.objectStore(stores.submitted).put({ formId, data: unsubmittedData });
        unsubmittedStore.delete(formId);
    }
}

// Convert FormData to plain object
function formDataToObject(formData) {
    const obj = {};
    for (const [key, value] of formData.entries()) {
        obj[key] = value;
    }
    return obj;
}

const formSyncRoutes = {
    ReferralForm: "/Referral/SaveReferralForm",
    SE2Form: "/Se2/SaveScreeningInformationForm",
    consentForm: "/ConsentForm/Create"
};

// Submit form automatically when online
window.addEventListener("online", async () => {
    const db = await openDatabase();
    const tx = db.transaction(stores.submitted, "readonly");
    const store = tx.objectStore(stores.submitted);

    const itemsToSync = [];

    store.openCursor().onsuccess = event => {
        const cursor = event.target.result;
        if (cursor) {
            itemsToSync.push(cursor.value);
            cursor.continue();
        } else {
            // Once cursor is done, sync all items
            syncItems(itemsToSync, db);
        }
    };
});

async function syncItems(items, db) {
    for (const { formId, data } of items) {
        const formType = formId.split("_")[0];
        const url = formSyncRoutes[formType];

        if (!url) {
            console.warn(`No sync URL defined for form type: ${formType}`);
            continue;
        }

        const formData = new URLSearchParams();
        for (const key in data) {
            formData.append(key, data[key]);
        }

        try {
            const response = await fetch(url, {
                method: "POST",
                headers: { "Content-Type": "application/x-www-form-urlencoded" },
                body: formData.toString()
            });

            if (response.ok) {
                const delTx = db.transaction(stores.submitted, "readwrite");
                delTx.objectStore(stores.submitted).delete(formId);
                console.log(`Synced and deleted ${formId}`);
            } else {
                console.error(`Failed to sync ${formId}: ${response.status}`);
            }
        } catch (err) {
            console.error(`Network error syncing ${formId}`, err);
        }
    }
}