# XamarinFirebaseNotificationsDemo
 Xamarin application to test firebase push notifications.

There are various formats available to send push notifications to devices. Here I am explaning one among them that is using FCM token.

From the postman, just perform post request using below details.

Http Request Type : POST <br/>
URL : https://fcm.googleapis.com/fcm/send

Request Headers: -
  * Authorization: key=*xxxxxxServer Keyxxxxxx*
  * Content-Type: application/json

You can obtain server key by following below steps.
Go to Firebase console — → Project Settings — → Cloud Messaging.

Request Body: -
 There are multile options available for this. You have to choose it based on your requirement.
 
# 1st Method:
```json
{
 "to" : "YOUR_FCM_TOKEN",
 "data" : {
     "body" : "Notification Body",
     "title": "Notification Title",
     "key_1" : "Value for key_1",
     "key_2" : "Value for key_2"
 }
}
```
# 2nd Method:
```json
{
 "to" : "YOUR_FCM_TOKEN",
 "notification" : {
     "body" : "Body of Your Notification",
     "title": "Title of Your Notification"
 },
 "data" : {
     "body" : "Notification Body",
     "title": "Notification Title",
     "key_1" : "Value for key_1",
     "key_2" : "Value for key_2"
 }
}
```

You will get "YOUR_FCM_TOKEN" once you run application on a device.
