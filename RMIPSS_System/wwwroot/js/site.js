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
});