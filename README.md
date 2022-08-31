---
page_type: sample
products:
- office-teams
- office-365
languages:
- csharp
extensions:
  contentType: samples
  technologies:
  - Connectors
  createdDate: 10/10/2020 10:21:42 PM
---

# Microsoft Teams Sample Connector in .NET/C#

This is an MVC sample task management application generated using the [ASP.NET Web Application (.NET Framework)](https://docs.microsoft.com/aspnet/mvc/overview/getting-started/introduction/getting-started#creating-your-first-application) template. The majority of the code is related to either basic MVC configuration or Task management.

The main connector code is found here:
* ConnectorController.cs - `Setup` & `Save` actions
* TaskController.cs - `Create` & `Update` actions

This application simulates a real task management system and allows users to create and view tasks. The content is randomly generated to simulate how notification can be sent into Microsoft Teams channel using connector.

**For more information on developing apps for Microsoft Teams, please review the Microsoft Teams [developer documentation](https://docs.microsoft.com/microsoftteams/platform/overview).**

## Prerequisites
The minimum prerequisites to run this sample are:
* The latest update of Visual Studio. You can download the community version [here](http://www.visualstudio.com) for free.
* An Office 365 account with access to Microsoft Teams, with [sideloading enabled](https://docs.microsoft.com/microsoftteams/platform/concepts/deploy-and-publish/apps-upload).
* If you want to run this code locally, use a tunnelling service. These instructions assume you are using [ngrok](https://ngrok.com/). 

### How to see the connector working in Microsoft Teams
1) [Upload your custom app in Microsoft Teams](https://docs.microsoft.com/microsoftteams/platform/concepts/apps/apps-upload) using [this manifest file](TeamsToDoAppConnector/TeamsAppPackages/manifest.json).
2) Configure the [TeamsToDoAppConnector](https://docs.microsoft.com/microsoftteams/platform/concepts/connectors#accessing-office-365-connectors-from-microsoft-teams) connector.
3) Select either Create or Update on the registration page and click Save. 
4) Once the connector is configured, you will get a notification in channel with link to the Task Manager application.
5) Go to Task Manager portal and click on Create New and enter the task details and Save.
6) You will see the MessageCard in the registered Teams channel.
7) You can try the actionable buttons available on the message card.

>**Note**: With the above instructions, you can use sample connector which is deployed on Azure. Please follow the instructions below to create your own connector.

### [Configure your own connector](https://docs.microsoft.com/microsoftteams/platform/webhooks-and-connectors/how-to/connectors-creating)
The sample shows a simple implementation of a connector registration implementation. It also sends a connector card to the registered connector via a process triggered "externally."

1. Open the TeamsToDoAppConnectorAuthentication.sln solution with Visual Studio.
1. Begin your tunnelling service to get an https endpoint. 
   1. Open a new command prompt window. 
   1. Change to the directory that contains the ngrok.exe application. 
   1. In the command prompt, run the command `ngrok http 3978 --host-header=localhost`.
   1. Ngrok will fill the entire prompt window. Make note of the https:// Forwarding URL. This URL will be your [BASE_URI] referenced below. 
   1. Minimize the ngrok Command Prompt window. It is no longer referenced in these instructions, but it must remain running.
1. Register a new connector in the [Connector Developer Portal](https://outlook.office.com/connectors/home/login/#/new)
   1. Fill in all the basic details such as name, logo, descriptions etc. for the new connector.
   1. For the configuration page, you'll use our sample code's setup endpoint: `https://[BASE_URI]/connector/MainSetup`
   1. For Valid domains, make entery of your domain's https URL, e.g. XXXXXXXX.ngrok.io.
   1. Enable the action on connector card by selecting the Yes radio button and enter the update endpoint: `https://[BASE_URI]/Task/Update`
   1. Click on Save. After the save completes, you will see your connector id.
1. In the Web.config file, set the `configuration.appSettings.Base_Uri` variable to the ngrok https forwarding url from the above.
1. In Visual Studio, press the green arrow (Start button) on the main Visual Studio toolbar, or press F5 or Ctrl+F5 to run the program. 
1. Now you can sideload your app package and test your new connector.

## Setting up Authentication on Configuration page 

Above steps will let you configure connector without authentication. Please follow these steps to enable authentication in your connector config page.
To be able to use an identity provider, first you have to register your application.

1. Using Azure AD
   1. Go to the [Application Registration Portal](https://aka.ms/appregistrations) and sign in with the your account to create an application.
   1. Navigate to **Authentication** under **Manage** and add the following redirect URLs:

      `https://<your_ngrok_url>/Authentication/SimpleEnd`

   1. Additionally, under the **Implicit grant** subsection select **Access tokens** and **ID tokens**

1. Setting up Authentication on Configuration page 

1. Set the ClientAppId in web.config, for example : https://contoso.ngrok.io

    ```
    <add key="ClientAppId" value="***Your Client App Id***"/>
    ```

1.  Update your Microsoft Teams application manifest

    1. Add your ngrok URL to validDomains. Teams will only show the sign-in popup if its from a whitelisted domain.

        ```json
        "validDomains": [
            "<<BASE_URI_DOMAIN>>",
        ],
        ```

App structure
=============

### Routes
1. `/Connector/MainSetup` renders the connector config UI.
   - This is the landing page for connector configuration. The purpose of this view is primarily to allow user to navigate to either Simple setup / Setup with auth page.
1. `/Connector/Setup` renders the connector config UI.
   - This is simple connector configuration page which allows configuration without login. 
1. `/Connector/SetupAuth` renders the connector config UI with auth.
   - This is connector configuration page which shows how to restrict configuration to authenticate users.
1. `/Authenication/SimpleStart` and `/Authenication/SimpleEnd` routes are used to grant the permissions. This experience happens in a separate window.
    - The Authenication/SimpleStart view merely creates a valid AAD authorization endpoint and redirects to that AAD consent page.
    - Once the user has consented to the permissions, AAD redirects the user back to `Authenication/SimpleEnd`. This view is responsible for returning the results back to the start view by calling the notifySuccess API.
    - This workflow is only necessary if you want authorization to use additional Graph APIs. Most apps will find this flow unnecessary if all they want to do is authenticate the user.
    - This workflow is the same as our standard [web-based authentication flow](https://docs.microsoft.com/microsoftteams/platform/tabs/how-to/authentication/auth-tab-aad#navigate-to-the-authorization-page-from-your-popup-page) that we've always had in Teams before we had single sign-on support. It just so happens that it's a great way to request additional permissions from the user, so it's left in this sample as an illustration of what that flow looks like.
1. `/Tasks` controller.
     1. `/Tasks/Index` renders the tasks list page.
     1. `/Tasks/Create` allows users to create new task, which triggers notification to all the webhooks registered for create event.
     1. `/Tasks/Update` allows users to update new task, which triggers notification to all the webhooks registered for update event.

## More Information
For more information about getting started with Teams, please review the following resources:
- Review [Getting Started with Authentications for Tabs](https://docs.microsoft.com/microsoftteams/platform/tabs/how-to/authentication/auth-tab-aad)
- Review [Get started with Microsoft Teams](https://docs.microsoft.com/microsoftteams/platform/get-started/get-started-overview)


