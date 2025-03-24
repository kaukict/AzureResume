document.getElementById('email-form').addEventListener('submit', async function (e) {
    e.preventDefault(); // Prevent the form from submitting the traditional way

    // Get the email value from the input field
    const email = document.getElementById('email').value;

    // Ensure the email is valid (you can add more validation here if needed)
    if (!email) {
        alert("Email is required!");
        return;
    }

    // Create the formData object
    const formData = {
        email: email
    };

    try {
        // Send the data to the backend using a POST request
        //const response = await fetch('http://192.168.0.132:3000/send', { ---Local container app
            const response = await fetch('https://dockercontainerapi.calmsmoke-f3f18966.westeurope.azurecontainerapps.io', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData) // Convert the object to a JSON string
        });

        // Handle the response from the backend
        const result = await response.json();
        document.getElementById('response-message').innerText = result.success || result.error;
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('response-message').innerText = 'Error sending email';
    }
});
