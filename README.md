# AzureResume
This is my Azure online Resume project I did as part of Cloud Resume Challenge to deepen my Azure knowledge. 
Project code is from: https://github.com/madebygps/azure-resume 

Code is not mine but there were some challenges with the it, ex. .net packages versions which needed updates, customizing HTML, adding new font... This part took me to longest as I do not know much about the code.
Code is in Github, content is automatically synced with azure using Github Actions. 

The website is running as Azure Storage static website, with one Azure Function to count visitors. 
CosmosDB is used to store the visitor count.

I created CDN endpoint for the website becouse Azure storage doesnt suppor HTTPS connection for custom domains. 
CDN endpoint is mapped to my static website with added custom domain which has HTTPS enabled and TLS certificate provisioned. 
Domain is from Azure managed by Azure DNS zone. 
CDN endpoint is caching the content of the website so it should be delivered faster, but if you change the code the CDN needs to be purged to show updates. 

CNAME record was added to DNS zone pointing from my custom domain to CDN endpoint.

To expand the project and learn more about containerization, I added a simple API that allows the website to send a welcome email when a visitor submits their email address.

The backend is built in Node.js and runs inside a Docker container. It uses SendGrid to send emails and loads the email content from an external HTML template, this mean that the email can be updated without rebuilding the container.

The Docker image is stored in GitHub Container Registry(still private as in need to hande some security concern first) and deployed to Azure using Azure Container Apps. It scales automatically and exposes a secure HTTPS endpoint that the static site can call.

Secrets such as the SendGrid API key, sender email address, and subject are stored in Azure Key Vault. Azure container authenticates using a managed identity. During local testing, secrets are loaded through Azure CLI. 

The static website was updated with a new section and a JavaScript script (contact-form.js) to handle form submissions. The script sends the visitor’s email address to the container API, which then sends the welcome email.

To make this secure, I configured CORS in the backend to only accept requests from my static site.

