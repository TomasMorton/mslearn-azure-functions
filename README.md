# Overview
Code samples developed for the ["Create serverless applications"](https://docs.microsoft.com/en-nz/learn/paths/create-serverless-applications/) path from Microsoft Learn.

# Tools and Setup
| Tool | CLI | Installation |
|-|-|-|
| Azure CLI | `az` | https://docs.microsoft.com/en-us/cli/azure/install-azure-cli |
| Azure Functions Core Tools | `func` | https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local |
| Azure Toolkit for Rider | - | https://plugins.jetbrains.com/plugin/11220-azure-toolkit-for-rider |

## Completing Installation
Once `az` is installed, run `az login`

# Useful Commands
### Create a new function app project
`func init`

### Add a function to a function app project
`func new`

### Run a function app project locally
`func start`

### Create a new Function App in Azure
```
export RESOURCEGROUP=TODO
export STORAGEACCT=TODO
export FUNCTIONAPP=TODO

az storage account create \
  --resource-group "$RESOURCEGROUP" \
  --name "$STORAGEACCT" \
  --kind StorageV2 \
  --location centralus

az functionapp create \
  --resource-group "$RESOURCEGROUP" \
  --name "$FUNCTIONAPP" \
  --storage-account "$STORAGEACCT" \
  --runtime node \
  --consumption-plan-location centralus \
  --functions-version 2
  ```

### Deploy a function app project to an existing Function App in Azure
`func azure functionapp publish <Function App Name>`

### Connect to log stream
`func azure functionapp logstream <Function App Name>`
