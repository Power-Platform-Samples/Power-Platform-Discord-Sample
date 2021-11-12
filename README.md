# Power Platform Discord Sample
[![Build and Deploy to Function App](https://github.com/Power-Platform-Samples/Power-Platform-Discord-Sample/actions/workflows/ic-discord.yml/badge.svg)](https://github.com/Power-Platform-Samples/Power-Platform-Discord-Sample/actions/workflows/ic-discord.yml)

This sample showcases how **Azure Functions** can be used in conjunction with **Power Virtual Agents** (PVA) and **Power Automate Flows** (PA).

In practice, the PVA delegates an action to a PA Flow. The PA Flow is responsible for invoking API methods using Azure Functions.

![High-level Overview](docs/assets/flow.png)

This repository is a sample and real-world use case for integrating PVA with your custom API methods. This Function is being used to verify Microsoft [Imagine Cup](https://imaginecup.microsoft.com/en-us/Events) participants on the official IC Discord server and add neccessary roles upon verification.

### Functions
This sample includes two Functions:
- `ValidateParticipant`: Validates whether the participants is enrolled into Imagine Cup using their email address.
- `ApproveParticipant`: Approves an Imagine Cup participant and grants the designated Discord server role to them. This uses their email address as well. 

## Get started
To get started, it's recommended to use Visual Studio 2022 with the Azure Workload installed. The Azure Workload includes all the tooling required for C# and Azure Functions.

The project makes use of Test-Driven Development using Live Unit Test. If you decide to contribute to this repository, please make sure to follow the TDD principles.

### Configuration
- `Discord:BotToken`: Discord bot token obtained from Discord Developer Portal
- `Discord:DiscordServerId`: The server ID (in this case Imagine Cup)
- `Discord:RegisteredParticipantRoleId`: The role ID that grants broader access to the user
- `ConnectionStrings:DefaultSqlConnection` or `DefaultSqlConnection`: Connection string for the SQL database.
 
## Deployment
This sample contains an Azure Bicep deployment file that instructs what resources are required on Azure. In a nutshell, this project requires an Azure Functions resource plus its dependencies and a SQL database.

## References
- [What is Azure Functions? | Microsoft Docs](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)
- [Quickstart: Create a C# function in Azure using Visual Studio Code | Microsoft Docs](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process)
- [Deploy Azure resources by using Bicep and GitHub Actions | Microsoft Docs](https://docs.microsoft.com/en-us/learn/paths/bicep-github-actions/)
- [Entity Framework | Microsoft Docs](https://docs.microsoft.com/en-us/ef/)
