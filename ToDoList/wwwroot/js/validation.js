
    document.getElementById("CreateForm").addEventListener("submit", function (event) {
        var title = document.getElementById("Title").value.trim();
    var titleWords = title.split(/\s+/);

    var errorMessages = [];
    document.getElementById("titleError").innerHTML = "";

    if (title === "") {
        errorMessages.push("Title is required.");
        }

        // Title Word Count Validation
        if (titleWords.length > 100) {
        errorMessages.push("Title cannot exceed 100 words.");
        }

        if (errorMessages.length > 0) {
        event.preventDefault();
    console.log("fffffffff");
    console.log(titleWords);
    document.getElementById("titleError").innerHTML = errorMessages.join("<br>");
        }
    });
