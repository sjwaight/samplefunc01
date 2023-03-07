# Azure Function to download S3 Object

This C# HTTP-triggered Azure Function is designed to read a single CSV file from an S3 Bucket and display the contents on screen.

Debug locally (or in a Codespace) by renaming the sample.local.settings.json file to local.setting.json and then setting the values for a least-privilege account to access the target S3 bucket (read-only is fine).

Note: you can set AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY as environment variables (or use other AWS credential approaches). If you're using a Codespace you can set them as [Codespace Secrets](https://docs.github.com/en/codespaces/managing-your-codespaces/managing-encrypted-secrets-for-your-codespaces).

While they are included in this repository as references, and should be installed when you start VS Code or a Codespace, this solutions relies on:

- .NET 6 SDK
- Azure Functions Core Tools
- Azurite Azure Storage Emulator
