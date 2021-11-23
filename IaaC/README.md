# Azure DevOps ToolKit
  The purpose of this project is to help accelerate your Azure Devops onboarding process. The below details will help you understand the automation process and toolkit usage.
  - Published Date : 24 March 2021 
  - Organisation : Microsoft Corporation
  - Contributors : 
  <br>` Saumilkumar Shah( saumilkumar.shah@microsoft.com ) `
  <br>` Prashant Akhouri ( prakhour@microsoft.com )` 
  <br>` Avinash Mitta ( avmitta@microsoft.com ) `

# Introduction 
This  is a DevOps project created for YAML Devops CICD pipeline demonstration . 
It can act as base code for setting up Azure YAML DevOps pipeline across projects . 
This DevOps project does mainly following :
- It Builds 3 Visual Studio Projects in /Apps folder as mentioned below - <br>
        a) DevopsDatabase     -- Azure SQL database project <br>
        b) DevOpsFunctionApp  -- Azure Function App project with queues <br>
        c) DevOpsWebApp       -- ASP.Net MVC Web Application (Demo Website project with Create, Edit , Update , Delete operations SQL database Employee table )
 - Creates Azure Infrastructure for above Application Deployments.
 - Deploys the 3 Application Solution Artifacts  (successfully build as .zip files into "DropAppArtifacts") to Azure Infrastucture.

# Getting Started
  - Familiarize yourself with ARM Templates,Azure YAML code,Devops CI-CD concepts and Powershell.
  - Please find few installations steps to get you started with. [Tools and configuaration](https://dev.azure.com/ms-cdd-eas-internal/EAS%20AAM%20Artifacts/_wiki/wikis/EAS-AAM-Artifacts.wiki/157/Tool-and-Configuration)


# Startup Steps
For Creating Demo & Test Run , Create 2 Azure CI/CD Pipelines in Azure ADO as below

1. **QRSDEVOPS-CICD**  
  <br>This pipeline is to create and deploy all infra and application resources to Azure
   Steps : 
    - Set up this pipeline with startup file as /IaaC/Pipelines/main.yml.
    
    - In the pipeline , Set a Variable named "sequence" with value Ex:001 to create all Azure resources with suffix as 001. (for example)

    - In the ADO environment Library , create 2 Variable Groups -  "prashantDev" and "prashantProd" (Variable Groups Name can be modifed in main.yml file)
      In each of the VARIABLE GROUPS  create below variables with values as :

       |  **Name** | **Value** |  |  
       |-----------|:-----------:|-----------:|  
       | kvsecVariableName1 | (anyvalue) | Any dummy kevault variable name , value will be overrriden by system generated value. |  
       | sqlAdminLoginId | qrsadmin | (set variabletype to secret) Azure SQL database Admin user id used for application database login. |
       | sqlAdminLoginPassword | (set some password) | (set variabletype to secret) Azure SQL database password used for application database login. |

    - Set the Service Connection Name in /Variables/dev.yaml and /Variables/prod.yaml files for Development and Production environments in below section - 
         name: serviceConnection
         value: 'prashant-qrs-subscription-dev'  ( replace with your environment Service Connection name )
     
2. **QRSDEVOPS-CICD-Cleanup**
    - This pipeline is to cleanor remove all resources in a given resource group from Azure

    - Set up this pipeline with startup file as /IaaC/Pipelines/cleanup.yml. 

    - Set the Service Connection Name in /Variables/dev.yaml and /Variables/prod.yaml files for Development and Production environments in below section - 
         name: serviceConnection
         value: 'prashant-qrs-subscription-dev'  ( replace with your environment Service Connection name )

    - Set the resourceGroup Name to be Cleanedup in /Variables/dev.yaml and /Variables/prod.yaml files for Development and Production environments in below section -
        - name : resourceGroupNamesForCleanup
          value: 'rg-qrs-prod-devops-001'  ( replace with your environment existing Resource Group name to be removed )

# Note
This reusable code act as base CICD setup for YAML DevOps pipeline . It can be extended/modified by individual contributors as per their project requirements to include multiple additional features.   


