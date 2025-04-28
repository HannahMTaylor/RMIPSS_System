// Restore saved form data on page load
document.addEventListener("DOMContentLoaded", async function () {
    const forms = document.querySelectorAll("form");

    for (const form of forms) {
        const formId = form.id;
        if (!formId) continue; // skip if form doesn't have an ID

        const savedData = await getFormData(formId);

        if (savedData) {
            for (const [key, value] of Object.entries(savedData)) {
                const field = form.querySelector(`[name="${key}"]`);
                if (field) field.value = value;
            }
        }
    }
});

// Auto-save form input using FormData
document.addEventListener("input", async function (event) {
    const input = event.target;
    const form = input.closest("form");

    if (!form || !form.id) return;

    const formId = form.id;
    const formData = new FormData(form);
    const formObject = formDataToObject(formData);

    await saveFormData(formId, formObject);
});
