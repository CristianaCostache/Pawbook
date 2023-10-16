// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const form = document.querySelector('#contact-form');

form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const formData = new FormData(form);

    const name = formData.get('name');
    const email = formData.get('email');
    const subject = 'Contact form';
    const message = formData.get('message');

    const apiKey = 'AC09A65415411948B4A8B6F52093A3298D3C75E8529B73C90284A5CAD4DC2529DC3AB4F2641842C83811564F293D1E5D';
    const fromEmail = 'costache.gabriela.g4v@student.ucv.ro';
    const toEmail = email;

    const response = await fetch('https://api.elasticemail.com/v2/email/send', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: new URLSearchParams({
            apiKey,
            from: fromEmail,
            to: toEmail,
            subject,
            bodyText: `Name: ${name}\nEmail: ${email}\n\n${message}`,
        }),
    });

    const result = await response.json();

    if (result.success) {
        // show success message to user
        alert('Your message has been sent successfully!');
        window.location.reload();
    } else {
        // show error message to user
        alert(`Error: ${result.error}`);
    }
});