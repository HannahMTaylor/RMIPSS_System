document.addEventListener("DOMContentLoaded", function () {
    alert("entered event listener for DOMContentLoaded")
    function updateSelectedValues(sectionClass, hiddenFieldId) {
        let selectedValues = [];
        /*alert("hello") */
        document.querySelectorAll("." + sectionClass + ":checked").forEach(function (checkbox) {
            selectedValues.push(checkbox.value);
        });

        document.getElementById(hiddenFieldId).value = selectedValues.join(",");
    }

    function initializeSection(sectionClass, hiddenFieldId) {
        //testing checkboxes registering
        let checkboxes = document.querySelectorAll("." + sectionClass);

        // Load saved values from hidden input (from the database)
        let savedValues = document.getElementById(hiddenFieldId).value;
        /*alert(savedValues)*/ 
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
            /*alert("hello from SelectorAll")*/
            checkbox.addEventListener("change", function () {
                updateSelectedValues(sectionClass, hiddenFieldId);
            });
        });
    }

    // Initialize each section separately
    initializeSection("reasons-checkbox", "reasons");
    
});

$(document).ready(function () {
    $("#ReferralForm").submit(function (event) {
        event.preventDefault();
        let isValid = true;
        let firstInvalidField = null;

        const fields = [
            //{ id: "completedByName", errorId: "completedByNameError", message: "Full Name is required." },
            { id: "studentLastName", errorId: "studentLastNameError", message: "Last Name Required" },
            { id: "studentFirstName", errorId: "studentFirstNameError", message: "First Name Required" },
            { id: "studentVillage", errorId: "studentVillageError", message: "Village Required" },
            { id: "studentAtoll", errorId: "studentAtollError", message: "Attol Required" },
            { id: "studentPOBox", errorId: "studentPOBoxError", message: "Po Box Required" },
            { id: "studentFatherName", errorId: "studentFatherNameError", message: "Father Name Required" },
            { id: "studentMotherName", errorId: "studentMotherNameError", message: "Mother Name Required" },
            { id: "studentPhone", errorId: "studentPhoneError", message: "Phone Number Required" },
            { id: "studentEmail", errorId: "studentEmailError", message: "Email Required" },
            { id: "studentSex", errorId: "studentSexError", message: "Sex Required" },
            { id: "studentAge", errorId: "studentAgeError", message: "Age Required" },
            { id: "studentDOB", errorId: "studentDOBError", message: "DOB Required" },
            { id: "studentHospitalNo", errorId: "studentHospitalNoError", message: "Hospital Number Required" },
            { id: "studentSSN", errorId: "studentSSNError", message: "SSN Required" },
            { id: "studentGrade", errorId: "studentGradeError", message: "Grade Required" },
            { id: "studentLang", errorId: "studentLangError", message: "Primary Language Required" },
            { id: "studentGuardLang", errorId: "studentGuardLangError", message: "Guardian Primary Language Required" },
            { id: "referralName", errorId: "referrerPersonNameError", message: "Referrer Name Required" },
            { id: "phone", errorId: "referrerPersonPhoneError", message: "Referrer Phone Number Required" },
            { id: "referrerPersonEmail", errorId: "referrerPersonEmailError", message: "Referrer Email Required" },
            { id: "referralDate", errorId: "referrerPersonDateFilledError", message: "Referral Filled Date Required" },
            { id: "dateReferralReceived", errorId: "dateReceivedError", message: "Date Received Required" }, //school only here down
            { id: "dateChildStudyTeam", errorId: "toChildStudyError", message: "Child Study Team Date Required" },
            { id: "dateTeamRecommendation", errorId: "teamRecommendataionError", message: "Recommendation Date Required" },
            { id: "disposition", errorId: "dispositionError", message: "Disposition Date Required" },
            { id: "dateDispositionNoticeReferring", errorId: "dispositionNoticeToReferrerError", message: "Disposition Notice Date Required" },
            { id: "dateDispositionNoticeParent", errorId: "dispositionNoticeToParentError", message: "Disposition Notice to Parent Date Required" },
            { id: "dateParentConsentEvaluation", errorId: "parentConsentForEvalError", message: "Parent Consent Date Required" },
            { id: "dateReferralEvaluationTeam", errorId: "referEvalTeamError", message: "Referral to Eval Team Date Required" },
            { id: "dateEvaluationTeamRecommendation", errorId: "evalTeamError", message: "Evaluation Team Date Required" },
            { id: "recommendation", errorId: "recommendationError", message: "Recommendation Date Required" },
            { id: "dateParentNoticeMeeting", errorId: "parentNoticeError", message: "Parent Notice of Meeting Date Required" },
            { id: "dateIEPMeeting", errorId: "iepMeetingError", message: "IEP Meeting Date Required" },
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

        let email = $("#studentEmail").val().trim();
        if (email !== "" && !/^\S+@\S+\.\S+$/.test(email)) {
            $("#studentEmailError").text("Enter a valid email.");
            $("#studentEmail").addClass("error-border");

            if (!firstInvalidField) {
                firstInvalidField = $("#completedByEmail")[0];
            }

            isValid = false;
        }
        let emailRP = $("#referrerPersonEmail").val().trim();
        if (emailRP !== "" && !/^\S+@\S+\.\S+$/.test(emailRP)) {
            $("#referrerPersonEmailError").text("Enter a valid email.");
            $("#referrerPersonEmail").addClass("error-border");

            if (!firstInvalidField) {
                firstInvalidField = $("#completedByEmail")[0];
            }

            isValid = false;
        }

        let checkBoxValid = false;
        $(".ReferralReasonCheckbox").each(function () {
            if ($(this).val().trim() !== "") {
                checkBoxValid = true;
                return false;
            }
        });

        if (!checkBoxValid) {
            $("#checkboxError").text("At least one reason for referral must be selected.").show();
            isValid = false;

            if (!firstInvalidField) {
                firstInvalidField = $("#checkboxError")[0];
            }
        } else {
            $("#checkboxError").hide();
        }

        if (!isValid && firstInvalidField) {
            firstInvalidField.scrollIntoView({ behavior: "instant", block: "center" });
            firstInvalidField.focus();
        }

        if (isValid) {
            if (navigator.onLine) {
                // Submit directly if online
                this.submit();
            } else {
                // Save form data if offline
                alert("You're offline. Data will be submitted when back online.");
                window.location.href = "/";
            }
        }
    });
});
