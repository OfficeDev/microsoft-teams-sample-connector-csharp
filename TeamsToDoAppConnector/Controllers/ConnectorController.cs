using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeamsToDoAppConnector.Models;
using TeamsToDoAppConnector.Repository;
using TeamsToDoAppConnector.Utils;

namespace TeamsToDoAppConnector.Controllers
{
    /// <summary>
    /// Represents the controller responsible for setting up the connector.
    /// </summary>
    public class ConnectorController : Controller
    {

        /// <summary>
        /// This is main landing page which allows user to choose between simple config vs auth config page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult MainSetup()
        {
            return View();
        }

        /// <summary>
        /// This is the landing page which shows simple configuration without auth. Check SetupAuth() for login implementation.
        /// </summary>
        public ViewResult Setup()
        {
            return View();
        }

        /// <summary>
        /// This is the landing page shows implementation of login.
        /// </summary>
        [HttpGet]
        public ViewResult SetupAuth()
        {
            return View();
        }

        /// <summary>
        /// This enpoint is called when we need to save the webhook details.
        /// This contains Webhook Url and event type which can be used to push change notifications to the channel.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Save(WebhookDetails webhookInfo)
        {
            if (webhookInfo == null || webhookInfo.WebhookUrl == null)
            {
                return RedirectToAction("Error"); // You could pass error message to Error Action. 
            }
            else
            {
                var subscription = SubscriptionRepository.Subscriptions.Where(sub => sub.WebHookUri == webhookInfo.WebhookUrl).FirstOrDefault();
                if (subscription == null)
                {
                    Subscription newSubscription = new Subscription
                    {
                        WebHookUri = webhookInfo.WebhookUrl,
                        EventType = webhookInfo.EventType
                    };

                    // Save the subscription so that it can be used to push data to the registered channels.
                    SubscriptionRepository.Subscriptions.Add(newSubscription);
                }
                else
                {
                    // Update existing
                    subscription.EventType = webhookInfo.EventType;
                }

                await TaskHelper.PostWelcomeMessage(webhookInfo.WebhookUrl);

                return View();
            }
        }

        // Error page
        public ActionResult Error()
        {
            return View();
        }
    }
}

