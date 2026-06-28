@description('Container Apps Environment Name')
param environmentName string

@description('Container Registry Name')
param acrName string

@description('Log Analytics Workspace Name')
param workspaceName string

@description('Deployment location')
param location string

resource workspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' existing = {
  name: workspaceName
}

resource acr 'Microsoft.ContainerRegistry/registries@2023-07-01' existing = {
  name: acrName
}

var workspaceKeys = listKeys(
  workspace.id,
  '2023-09-01'
)

resource managedEnvironment 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: environmentName
  location: location

  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'

      logAnalyticsConfiguration: {
        customerId: workspace.properties.customerId
        sharedKey: workspaceKeys.primarySharedKey
      }
    }
  }
}