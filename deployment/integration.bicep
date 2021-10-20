@secure()
param dbPassword string
param dbName string = 'icadmin'

@secure()
param discordBotToken string
param discordServerId string
param discordParticipantRoleId string

var location = resourceGroup().location


resource storage 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name: 'icdiscord'
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Premium_LRS'
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: 'ic-discord-plan'
  location: resourceGroup().location
  sku: {
    tier: 'Free'
    name: 'F1'
  }
}

resource azureFunction 'Microsoft.Web/sites@2021-02-01' = {
  name: 'ic-discord'
  location: location
  kind: 'functionapp'
  dependsOn: [
    appServicePlan
    storage
    sqlServer
    sqlServerDatabase
  ]
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storage.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storage.id, storage.apiVersion).keys[0].value}'
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~2'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'Discord:BotToken'
          value: discordBotToken
        }
        {
          name: 'Discord:DiscordServerId'
          value: discordServerId
        }
        {
          name: 'Discord:RegisteredParticipantRoleId'
          value: discordParticipantRoleId
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultSqlConnection'
          connectionString: 'Server=tcp:${sqlServer.name}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=Integration;Persist Security Info=False;User ID=${dbName};Password=${dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
          type: 'SQLAzure'
        }
      ]
    }
  }
}

resource sqlServer 'Microsoft.Sql/servers@2021-02-01-preview' ={
  name: 'ic-discord'
  location: resourceGroup().location
  properties: {
    administratorLogin: dbName
    administratorLoginPassword: dbPassword
    version: '12.0'
  }
}

resource sqlServerDatabase 'Microsoft.Sql/servers/databases@2021-02-01-preview' = {
  parent: sqlServer
  name: 'Integration'
  location: resourceGroup().location
  dependsOn: [
    sqlServer
  ]
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}
