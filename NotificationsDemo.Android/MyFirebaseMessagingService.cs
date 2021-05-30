using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Firebase.Messaging;

namespace NotificationsDemo.Droid
{
    [Service(Name = "com.companyname.notificationsdemo.MyFirebaseMessagingService")]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        private string TAG = "MyFirebaseMsgService";

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Console.WriteLine("NEW_TOKEN", token);
            SendRegistrationToServer(token);
        }

        private void SendRegistrationToServer(string token)
        {
            /*
            NotificationHub hub = new NotificationHub(AppConstants.NotificationHubName, AppConstants.ListenConnectionString, this);
            // register device with Azure Notification Hub using the token from FCM
            Registration reg = hub.Register(token, AppConstants.SubscriptionTags);
            // subscribe to the SubscriptionTags list with a simple template.
            string pnsHandle = reg.PNSHandle;
            hub.RegisterTemplate(pnsHandle, "defaultTemplate", AppConstants.FCMTemplateBody, AppConstants.SubscriptionTags);
            */
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            //string messageBody = string.Empty;
            //if (message.GetNotification() != null)
            //{
            //    messageBody = message.GetNotification().Body;
            //}
            //else
            //{
            //    messageBody = message.Data.Values.First();
            //}
            //try
            //{
            //    MessagingCenter.Send(messageBody, "Update");
            //}
            //catch (Exception e)
            //{

            //}
            //SendLocalNotification(messageBody);
            var body = message.GetNotification().Body;
            var data = message.Data;
            SendNotification(body, data);
        }

        private void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity)); intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }
            var pendingIntent = PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);
            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                .SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)
                .SetContentTitle("FCM Message")
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                notificationBuilder.SetChannelId("my_notification_channel");
            }

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}
