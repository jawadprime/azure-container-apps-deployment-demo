@description('Workspace name')
param workspaceName string

@description('Deployment location')
param location string

resource workspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: workspaceName
  location: location

  properties: {
    sku: {
      name: 'PerGB2018'
    }

    retentionInDays: 30

    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}