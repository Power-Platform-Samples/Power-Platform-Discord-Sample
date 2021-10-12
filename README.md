# Power Platform Discord Sample
[![Build using .NET Core](https://github.com/discord-net/Discord.Net/actions/workflows/build.yml/badge.svg)](https://github.com/discord-net/Discord.Net/actions/workflows/build.yml)

This sample showcases how **Azure Functions** can be used in conjunction with **Power Virtual Agents** (PVA) and **Power Automate Flows** (PA).

In practice, the PVA delegates an action to a PA Flow. The PA Flow is responsible for invoking API methods using Azure Functions.

![High-level Overview](docs/assets/flow.png)

This repository is a sample and real-world use case for integrating PVA with your custom API methods. This Function is being used to verify Microsoft [Imagine Cup](https://imaginecup.microsoft.com/en-us/Events) participants on the official IC Discord server and add neccessary roles upon verification.