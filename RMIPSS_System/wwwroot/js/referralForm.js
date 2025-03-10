document.addEventListener("DOMContentLoaded", function () {
    function updateSelectedValues(sectionClass, hiddenFieldId) {
        let selectedValues = [];
        alert("hello") 
        document.querySelectorAll("." + sectionClass + ":checked").forEach(function (checkbox) {
            selectedValues.push(checkbox.value);
        });

        document.getElementById(hiddenFieldId).value = selectedValues.join(",");
    }

    function initializeSection(sectionClass, hiddenFieldId) {
        // Load saved values from hidden input (from the database)
        let savedValues = document.getElementById(hiddenFieldId).value;
        alert(savedValues) 
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
            alert("hello from SelectorAll")
            checkbox.addEventListener("change", function () {
                updateSelectedValues(sectionClass, hiddenFieldId);
            });
        });
    }

    // Initialize each section separately
    initializeSection("reasons-checkbox", "reasons");
    
});

$(document).ready(function () {
    $("#SE2Form").submit(function (event) {
        event.preventDefault();
        let isValid = true;
        let firstInvalidField = null;

        const fields = [
            //{ id: "completedByName", errorId: "completedByNameError", message: "Full Name is required." },
            { id: "studentLastName", errorId: "studentLastNameError", message: "Required Field" },
            { id: "studentFirstName", errorId: "studentFirstNameError", message: "Required Field" },
            { id: "studentVillage", errorId: "studentVillageError", message: "Required Field" },
            { id: "studentAtoll", errorId: "studentAtollError", message: "Required Field" },
            { id: "studentPOBox", errorId: "studentPOBoxError", message: "Required Field" },
            { id: "studentFatherName", errorId: "studentFatherNameError", message: "Required Field" },
            { id: "studentMotherName", errorId: "studentMotherNameError", message: "Required Field" },
            { id: "studentPhone", errorId: "studentPhoneError", message: "Required Field" },
            { id: "studentEmail", errorId: "studentEmailError", message: "Required Field" },
            { id: "studentSex", errorId: "studentSexError", message: "Required Field" },
            { id: "studentAge", errorId: "studentAgeError", message: "Required Field" },
            { id: "studentDOB", errorId: "studentDOBError", message: "Required Field" },
            { id: "studentHospitalNo", errorId: "studentHospitalNoError", message: "Required Field" },
            { id: "studentSSN", errorId: "studentSSNError", message: "Required Field" },
            { id: "studentGrade", errorId: "studentGradeError", message: "Required Field" },
            { id: "studentLang", errorId: "studentLangError", message: "Required Field" },
            { id: "studentGuardLang", errorId: "studentGuardLangError", message: "Required Field" },
            { id: "referralName", errorId: "referrerPersonNameError", message: "Required Field" },
            { id: "phone", errorId: "referrerPersonPhoneError", message: "Required Field" },
            { id: "email", errorId: "referrerPersonEmailError", message: "Required Field" },
            { id: "referralDate", errorId: "referrerPersonDateFilledError", message: "Required Field" },
            { id: "dateReferralReceived", errorId: "dateReceivedError", message: "Required Field" },
            { id: "dateChildStudyTeam", errorId: "toChildStudyError", message: "Required Field" },
            { id: "dateTeamRecommendation", errorId: "teamRecommendataionError", message: "Required Field" },
            { id: "disposition", errorId: "dispositionError", message: "Required Field" },
            { id: "dateDispositionNoticeReferring", errorId: "dispositionNoticeToReferrerError", message: "Required Field" },
            { id: "dateDispositionNoticeParent", errorId: "dispositionNoticeToParentError", message: "Required Field" },
            { id: "dateParentConsentEvaluation", errorId: "parentConsentForEvalError", message: "Required Field" },
            { id: "dateReferralEvaluationTeam", errorId: "referEvalTeamError", message: "Required Field" },
            { id: "dateEvaluationTeamRecommendation", errorId: "evalTeamError", message: "Required Field" },
            { id: "recommendation", errorId: "recommendationError", message: "Required Field" },
            { id: "dateParentNoticeMeeting", errorId: "parentNoticeError", message: "Required Field" },
            { id: "dateIEPMeeting", errorId: "iepMeetingError", message: "Required Field" },
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
        let email = $("#email").val().trim();
        if (email !== "" && !/^\S+@\S+\.\S+$/.test(email)) {
            $("#referrerPersonEmailError").text("Enter a valid email.");
            $("#email").addClass("error-border");

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
            this.submit();
        }
    });
});
