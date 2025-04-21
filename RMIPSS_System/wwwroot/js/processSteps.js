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

    await saveFormData(formId, formObject);
});
