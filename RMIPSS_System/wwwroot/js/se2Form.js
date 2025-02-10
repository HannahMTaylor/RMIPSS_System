document.addEventListener("DOMContentLoaded", function () {
    function updateSelectedValues(sectionClass, hiddenFieldId) {
        let selectedValues = [];
        document.querySelectorAll("." + sectionClass + ":checked").forEach(function (checkbox) {
            selectedValues.push(checkbox.value);
        });

        document.getElementById(hiddenFieldId).value = selectedValues.join(",");
    }

    function initializeSection(sectionClass, hiddenFieldId) {
        // Load saved values from hidden input (from the database)
        let savedValues = document.getElementById(hiddenFieldId).value;
        if (savedValues) {
            let checkedValues = savedValues.split(",");
            document.querySelectorAll("." + sectionClass).forEach(function (checkbox) {
                if (checkedValues.includes(checkbox.value)) {
                    checkbox.checked = true;
                }
            });
        }

        // Update hidden input on change
        document.querySelectorAll("." + sectionClass).forEach(function (checkbox) {
            checkbox.addEventListener("change", function () {
                updateSelectedValues(sectionClass, hiddenFieldId);
            });
        });
    }

    // Initialize each section separately
    initializeSection("physical-checkbox", "physicalValues");
    initializeSection("vision-checkbox", "visionValues");
    initializeSection("hearing-checkbox", "hearingValues");
    initializeSection("languageSpeech-checkbox", "languageSpeechValues");
    initializeSection("behavior-checkbox", "behaviorValues");
    initializeSection("academic-checkbox", "academicValues");
    initializeSection("other-checkbox", "otherValues");
});

$(document).ready(function () {
    $("#SE2Form").submit(function (event) {
        event.preventDefault();
        let isValid = true;
        let firstInvalidField = null;

        const fields = [
            { id: "completedByName", errorId: "completedByNameError", message: "Full Name is required." },
            { id: "completedByPhone", errorId: "completedByPhoneError", message: "Phone is required." },
            { id: "completedByEmail", errorId: "completedByEmailError", message: "Email is required." },
            { id: "completedDate", errorId: "completedDateError", message: "Date is required." }
        ];

        fields.forEach(field => {
            let value = $("#" + field.id).val().trim();
            if (value === "") {
                $("#" + field.errorId).text(field.message);
                $("#" + field.id).addClass("error-border");

                if (!firstInvalidField) {
                    firstInvalidField = $("#" + field.id)[0];
                }

                isValid = false;
            } else {
                $("#" + field.errorId).text("");
                $("#" + field.id).removeClass("error-border");
            }
        });

        let email = $("#completedByEmail").val().trim();
        if (email !== "" && !/^\S+@\S+\.\S+$/.test(email)) {
            $("#completedByEmailError").text("Enter a valid email.");
            $("#completedByEmail").addClass("error-border");

            if (!firstInvalidField) {
                firstInvalidField = $("#completedByEmail")[0];
            }

            isValid = false;
        }

        let checkBoxValid = false;
        $(".SE2CheckBoxValues").each(function () {
            if ($(this).val().trim() !== "") {
                checkBoxValid = true;
                return false;
            }
        });

        if (!checkBoxValid) {
            $("#checkBoxError").text("At least one concern must be selected.").show();
            isValid = false;

            if (!firstInvalidField) {
                firstInvalidField = $("#checkBoxError")[0];
            }
        } else {
            $("#checkBoxError").hide();
        }

        if (!isValid && firstInvalidField) {
            firstInvalidField.scrollIntoView({ behavior: "instant", block: "center" });
            firstInvalidField.focus();
        }

        if (isValid) {
            this.submit();
        }
    });
});
