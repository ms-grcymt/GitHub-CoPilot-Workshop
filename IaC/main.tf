// Create a terraform block with azurerm as the provider and create a resource group named by a parameter named rg-name with an azure webb app associated with application insights. (Hint: You can use the azurerm_app_service resource type and look at the docs for the application_insights block)
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = var.rg-name
  location = var.location
}

resource "azurerm_application_insights" "app_insights" {
  name                = "${var.rg-name}-app-insights"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  application_type = "web"
}

resource "azurerm_app_service_plan" "app_service_plan" {
  name                = "${var.rg-name}-app-service-plan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "app_service" {
  name                = "${var.rg-name}-app-service"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.app_service_plan.id
  
  site_config {
    dotnet_framework_version = "v6.0"
  }

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = azurerm_application_insights.app_insights.instrumentation_key
  }
}

# Path: variables.tf
variable "rg-name" {
  description = "Name of the resource group"
  default     = "rg-terraform"
}

variable "location" {
  description = "Location of the resource group"
  default     = "northeurope"
}

# Path: terraform.tfvars
rg-name = "rg-terraform"
location = "northeurope"
```

## 7.2. Azure DevOps

### 7.2.1. Create a new project

- Create a new project in Azure DevOps named **terraform-azure-devops**
- Create a new repository in Azure DevOps named **terraform-azure-devops**
- Clone the repository to your local machine

### 7.2.2. Create a service connection

- In Azure DevOps go to **Project Settings > Service Connections > New Service Connection > Azure Resource Manager**
- Select **Service Principal (Automatic)**