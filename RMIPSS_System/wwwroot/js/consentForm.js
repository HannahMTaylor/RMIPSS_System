$(document).ready(function () {
    $("#ConsentForm").submit(async function (event) {
        event.preventDefault();
        if (await isActuallyOnline()) {
            // Submit directly if online
            this.submit();
        } else {
            // Save form data if offline
            const formId = this.id;
            await moveToSubmitted(formId); // moves from unsubmitted to submitted
            alert("You're offline. Data will be submitted when back online.");
            window.location.href = "/";
        }
    });
});
