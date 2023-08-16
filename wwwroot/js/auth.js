$(document).ready(function () {
    // Handling login submission

    $("#loginForm").submit(function (e) {
        e.preventDefault();

        let formData = $(this).serializeArray();

        $.post("http://localhost:5159/auth/login", formData, function (data) {
            if (data.token) {
                localStorage.setItem('authToken', data.token);
                fetchUserDetails(data.token);
            } else {
                alert("Login failed. Check your email and password.");
            }
        });
    });

    // Handling registration submission
    $("#registerForm").submit(function (e) {
        e.preventDefault();

        let formData = $(this).serializeArray();

        $.post("http://localhost:5159/auth/register", formData, function (data, status, xhr) {
            if (xhr.status === 409) {
                alert("Email already in use.");
            } else if (data) {
                alert("Registered successfully! You can now log in.");
            } else {
                alert("Registration failed.");
            }
        });
    });
});

function fetchUserDetails(token) {
    $.ajax({
        url: "http://localhost:5159/api/users", // Replace with your GetUserDetails endpoint
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (userDetails) {
            // Do something with userDetails
            // For example, update some UI element to welcome the user
            $('#welcomeUser').text(`Welcome, ${userDetails.firstName}`);
        }
    });
}
