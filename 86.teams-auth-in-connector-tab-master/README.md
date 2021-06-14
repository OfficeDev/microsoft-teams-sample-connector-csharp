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
  createdDate: 11/29/2020 10:21:42 PM
---

# Microsoft Teams Sample Connector Configuration Authentication in .NET/C#

This is an ASP.NET Core application generated using the [ASP.NET Core Web Application (.NET Framework)](https://docs.microsoft.com/en-us/visualstudio/ide/quickstart-aspnet-core?view=vs-2019) template.

This application simulates Azure AAD Authentication flow for Connector Configuration.

**For more information on developing apps for Microsoft Teams, please review the Microsoft Teams [developer documentation](https://docs.microsoft.com/en-us/microsoftteams/platform/overview).**
n 
## Prerequisites
The minimum prerequisites to run this sample are:
* The latest update of Visual Studio. You can download the community version [here](http://www.visualstudio.com) for free.
* An Office 365 account with access to Microsoft Teams, with [sideloading enabled](https://msdn.microsoft.com/en-us/microsoft-teams/setup).
* If you want to run this code locally, use a tunnelling service. These instructions assume you are using [ngrok](https://ngrok.com/). 

>**Note**: some features in the sample require that you using [Public Developer Preview mode](https://docs.microsoft.com/en-us/microsoftteams/platform/resources/dev-preview/developer-preview-intro) in Microsoft Teams.


### Configure your own connector
The sample shows a simple implementation of a connector registration implementation. It also sends a connector card to the registered connector via a process triggered "externally."

1. Open the TeamsToDoAppConnectorAuthentication.sln solution with Visual Studio.
1. Begin your tunnelling service to get an https endpoint. 
   1. Open a new command prompt window. 
   1. Change to the directory that contains the ngrok.exe application. 
   1. In the command prompt, run the command `ngrok http [port] --host-header=localhost`.
   1. Ngrok will fill the entire prompt window. Make note of the https:// Forwarding URL. This URL will be your [BASE_URI] referenced below. 
   1. Minimize the ngrok Command Prompt window. It is no longer referenced in these instructions, but it must remain running.
1. Register a new connector in the [Connector Developer Portal](https://outlook.office.com/connectors/home/login/#/new)
   1. Fill in all the basic details such as name, logo, descriptions etc. for the new connector.
   1. For the configuration page, you'll use our sample code's setup endpoint: `https://[BASE_URI]/connector/setup`
   1. For Valid domains, make enter your domain's http or https URL, e.g. XXXXXXXX.ngrok.io.
   1. Click on Save. After the save completes, you will see your connector id.
1. In Visual Studio, click the play button. 
1. Now you can sideload your app package and test your new connector.

## Setup

To be able to use an identity provider, first you have to register your application.

### [Using Azure AD](#using-azure-ad)

1. Go to the [Application Registration Portal](https://aka.ms/appregistrations) and sign in with the your account to create an application.
1. Navigate to **Authentication** under **Manage** and add the following redirect URLs:

    - `https://<your_ngrok_url>/Connector/simpleEnd`

1. Additionally, under the **Implicit grant** subsection select **Access tokens** and **ID tokens**

1. Click on **Expose an API** under **Manage**. Select the Set link to generate the Application ID URI in the form of api://{AppID}. Insert your fully qualified domain name (with a forward slash "/" appended to the end) between the double forward slashes and the GUID. The entire ID should have the form of: api://<your_ngrok_url>/{AppID}

1. Navigate to **API Permissions**, and make sure to add the following delegated permissions:
    - User.Read
    - email
    - offline_access
    - openid
    - profile
1. Scroll to the bottom of the page and click on "Add Permissions".


## Setting up Authentication on Configuration page 


1. Enter your AppId in the `client_id` property in SimpleStart.html page 

```javascript
 let queryParams = {
                    client_id: "***YOUR CLIENT ID HERE***",
                    response_type: "id_token token",
                    response_mode: "fragment",
                    resource: "https://graph.microsoft.com/",
                    redirect_uri: window.location.origin + "/Home/SimpleEnd",
                    nonce: _guid(),
                    state: state,
                    login_hint: context.loginHint,
                };
```


### Update your Microsoft Teams application manifest

1. Add new properties to your Microsoft Teams manifest:

    - **WebApplicationInfo** - The parent of the following elements.
    - **Id** - The client ID of the application. This is an application ID that you obtain as part of registering the application with Azure AD 1.0 endpoint.
    - **Resource** - The domain and subdomain of your application. This is the same URI (including the `api://` protocol) that you used when registering the app in AAD. The domain part of this URI should match the domain, including any subdomains, used in the URLs in the section of your Teams application manifest.

    ```json
    "webApplicationInfo": {
    "id": "<AAD_application_id here>",
    "resource": "<web_API resource here>"
    }
    ```

update validDomains to allow token endpoint used by connector. Teams will only show the sign-in popup if its from a whitelisted domain.

    ```
    "validDomains": [
        "<<BASE_URI_DOMAIN>>",
    ],
    ```

Notes:

-   The resource for an AAD app will usually just be the root of its site URL and the appID (e.g. api://subdomain.example.com/6789/c6c1f32b-5e55-4997-881a-753cc1d563b7). We also use this value to ensure your request is coming from the same domain. Therefore make sure that your contentURL for your tab uses the same domains as your resource property.
-   You need to be using manifest version 1.5 or higher for these fields to be used.
-   Scopes aren’t supported in the manifest and instead should be specified in the API Permissions section in the Azure portal



## More Information
For more information about getting started with Teams, please review the following resources:
- Review [Creating Office 365 Connectors for Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/connectors-creating)
- Review [Getting Started with Authentications for Tabs](https://docs.microsoft.com/en-us/microsoftteams/platform/tabs/how-to/authentication/auth-tab-aad)
- Review [Getting Started with Teams](https://msdn.microsoft.com/en-us/microsoft-teams/setup)


