
# Microsoft Teams Sample Connector in .NET/C#

This is a MVC sample task management system generated using [ASP.NET Web Application (.NET Framework)](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started#creating-your-first-application) template. Majority of the code is related either basic MVC configuration or Task management.

The main connector part includes following code:
1) ConnectorController.cs - `Setup` & `Save` actions
2) TaskController.cs - `Create` & `Update` actions

This app simulates task management system and allows users to create and view tasks. The content is randomly generated to simulate how notification can be sent into Microsoft Teams channel using connector.

**For more information on developing apps for Microsoft Teams, please review the Microsoft Teams [developer documentation](https://msdn.microsoft.com/en-us/microsoft-teams/index).**

## Prerequisites
The minimum prerequisites to run this sample are:
* The latest update of Visual Studio. You can download the community version [here](http://www.visualstudio.com) for free.
* An Office 365 account with access to Microsoft Teams, with [sideloading enabled](https://msdn.microsoft.com/en-us/microsoft-teams/setup).
* Install any of the tunnelling service. These instructions assume you are using ngrok: https://ngrok.com/
>**Note**: some features in the sample require that you [enable Public Developer Preview mode](https://msdn.microsoft.com/en-us/microsoft-teams/publicpreview) in Microsoft Teams.

### How to see connector working in Microsoft Teams
1) [Upload your custom app in Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/apps/apps-upload) (Manifest file: ~\TeamsToDoAppConnector\TeamsAppPackages\manifest.json).
2) Configure [Teams ToDo Notification](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/connectors#accessing-office-365-connectors-from-microsoft-teams) connector.
3) Select either create or update radio button on registration page and click on save. 
4) Once connector is configured, you get a notification in channle with link to Task Manager portal.
5) Go to Task Manager portal and click on Create New and enter the task details and Save.
6) You will see the MessageCard in the registered Teams channel.
7) You can try actionable buttons available on message card.

>**Note**: With above instructions you could try out sample connector which is already deployed on azure. Please follow the instruction given below to create your own connector.

### Configure Connector code
The sample shows a simple implementation of a Connector registration implementation. It also sends a Connector Card to the registered Connector via a process triggered "externally."

1. Open the TeamsToDoAppConnector.sln solution with Visual Studio.
2. Begin your tunnelling service to get an https endpoint. 
	1. Open a new **Command Prompt** window. 
	2. Change to the directory that contains the ngrok.exe application. 
	3. Run the command `ngrok http [port] --host-header=localhost` (you'll need the https endpoint for the connector registration) e.g.<br>
		```
		ngrok http 5555 --host-header=localhost
		```
	4. The ngrok application will fill the entire prompt window. Make note of the Forwarding address using https. This address is required in the next steps(BASE_URI). 
	5. Minimize the ngrok Command Prompt window. It is no longer referenced in this lab, but it must remain running.
3. You'll need to register a new connector in the [Connector Developer Portal](https://outlook.office.com/connectors/home/login/#/new)
	1. Fill in all the basic details such as name, logo, descriptions etc for the new connector.
	2. For the configuration page, you'll use our sample code's setup endpoint: `https://[BASE_URI]/connector/setup`
	3. For Valid domains make sure you enter your domain's http or https URL.
	4. Enable the action on connector card by selecting Yes radio button and enter update endpoint : `https://[BASE_URI]/Task/Update`
	5. Click on Save. After successful save you could see your connector id in the brower window.
4. In the Web.config file, set the `configuration.appSettings.Base_Uri` variable to ngrok https endpoint url which we got from step #2.4.
5. In Visual Studio click the play button (should be defaulted to running the Microsoft Edge configuration) 
6. Now you can sideload and test your new connector.

## More Information
For more information about getting started with Teams, please review the following resources:
- Review [Getting Started with Teams](https://msdn.microsoft.com/en-us/microsoft-teams/setup)
- Review [Getting Started with Bot Framework](https://docs.microsoft.com/en-us/bot-framework/bot-builder-overview-getstarted)
- Review [Testing your bot with Teams](https://msdn.microsoft.com/en-us/microsoft-teams/botsadd)

