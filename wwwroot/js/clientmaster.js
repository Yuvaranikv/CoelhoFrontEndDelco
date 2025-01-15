const apiUrl = "/api/Client";

$(document).ready(function () {
    console.log("Document ready - initializing event handlers");

    // Attach event listener to Save button
    $("#saveButton").on("click", async function () {
        await saveClient();
    });

    // Attach event listener to Update button
    $("#updateButton").on("click", async function () {
        await updateClient();
    });

    // Attach event listener to Delete button
    $("#deleteButton").on("click", async function () {
        await deleteClient();
    });

    // Check for query parameters and populate the form if data exists
    const urlParams = new URLSearchParams(window.location.search);
    if (urlParams.has("ClientID")) {
        const client = {
            ClientID: urlParams.get("ClientID"),
            FirstName: urlParams.get("FirstName"),
            MiddleName: urlParams.get("MiddleName"),
            LastName: urlParams.get("LastName"),
            Suffix: urlParams.get("Suffix"),
            DateOfBirth: urlParams.get("DateOfBirth"),
            Gender: urlParams.get("Gender"),
            ContactNumber: urlParams.get("ContactNumber"),
            Email: urlParams.get("Email"),
        };

        // Populate the form and toggle buttons
        populateClientForm(client);
        $("#saveButton").addClass("d-none");
        $("#updateButton, #deleteButton").removeClass("d-none");
    }
});

async function saveClient() {
    const client = getClientDataFromForm();
    if (!client) return;

    try {
        const response = await fetch(`${apiUrl}/Save`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(client),
        });

        if (response.ok) {
            alert("Client saved successfully!");
            resetForm();
        } else {
            alert("Failed to save client. Please try again.");
        }
    } catch (error) {
        console.error("Error saving client:", error);
    }
}

async function updateClient() {
    const client = getClientDataFromForm();
    if (!client || !client.ClientID) {
        alert("Client ID is required for update.");
        return;
    }
    console.log(client);
    try {
        const response = await fetch(`${apiUrl}/Update`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(client),
        });
        console.log(client);
        if (response.ok) {
            alert("Client updated successfully!");
        } else {
            alert("Failed to update client. Please try again.");
        }
    } catch (error) {
        console.error("Error updating client:", error);
    }
}

async function deleteClient() {
    const clientID = $("#clientID").val();
    if (!clientID) {
        alert("Client ID is required for deletion.");
        return;
    }

    if (!confirm("Are you sure you want to delete this client?")) {
        return;
    }

    try {
        const response = await fetch(`${apiUrl}/Delete/${clientID}`, {
            method: "DELETE",
        });

        if (response.ok) {
            alert("Client deleted successfully!");
            resetForm();
        } else {
            alert("Failed to delete client. Please try again.");
        }
    } catch (error) {
        console.error("Error deleting client:", error);
    }
}

// Utility function to populate form with client data
function populateClientForm(client) {
    $("#clientID").val(client.ClientID || "");
    $("#firstName").val(client.FirstName || "");
    $("#middleName").val(client.MiddleName || "");
    $("#lastName").val(client.LastName || "");
    $("#suffix").val(client.Suffix || "");
    $("#dateOfBirth").val(client.DateOfBirth ? new Date(client.DateOfBirth).toISOString().split("T")[0] : "");
    $("#gender").val(client.Gender || "");
    $("#contactNumber").val(client.ContactNumber || "");
    $("#email").val(client.Email || "");
}

// Utility function to get client data from the form
function getClientDataFromForm() {
    const firstName = $("#FirstName").val();
    const lastName = $("#LastName").val();


    if (!firstName || !lastName) {
        alert("First Name and Last Name are required.");
        return null;
    }

    return {
        ClientID: $("#ClientID").val() || null,
        FirstName: firstName,
        MiddleName: $("#MiddleName").val(),
        LastName: lastName,
        Suffix: $("#Suffix").val(),
        DateOfBirth: $("#DateOfBirth").val(),
        Gender: $("#Gender").val(),
        ContactNumber: $("#ContactNumber").val(),
        Email: $("#Email").val(),
    };
}

// Utility function to reset the form
function resetForm() {
    $("#clientForm")[0].reset();
    $("[asp-for='ClientID']").val("");
    $("#saveButton").removeClass("d-none");
    $("#updateButton, #deleteButton").addClass("d-none");
}
