# Azure Function to download S3 Object

This C# HTTP-triggered Azure Function is designed to read a single CSV file from an S3 Bucket and display the contents on screen.

Debug locally (or in a Codespace) by renaming the sample.local.settings.json file to local.setting.json and then setting the values for a least-privilege account to access the target S3 bucket (read-only is fine).

While they are included in this repository as references, and should be installed when you start VS Code or a Codespace, this solutions relies on:

- .NET 6 SDK
- Azure Functions Core Tools
- Azurite Azure Storage Emulator
