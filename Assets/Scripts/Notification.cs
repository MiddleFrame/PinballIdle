using UnityEngine;
//using NotificationSamples;
using System;
using Shop;
using Unity.Notifications.Android;

public class Notification : MonoBehaviour
{
    //private GameNotificationsManager notificationManager = new GameNotificationsManager();
    // Start is called before the first frame update
    // [SerializeField] GameObject _rewardPanel; 

    private void Start()
    {
        const int notificationID = 10000;

        var _notification = new AndroidNotification();
        var _notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID);

        var _channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(_channel);

        _notification.Title = "Your reward is ready!";
        _notification.Text = "Log into the game NOW and get your coins!";
        _notification.FireTime = (PigMoneybox.NextClaim - DateTime.Now).TotalMinutes > 15 ? PigMoneybox.NextClaim : DateTime.Now.AddMinutes(15);
        switch (_notificationStatus)
        {
            case NotificationStatus.Scheduled:
                // Replace the scheduled notification with a new notification.
                AndroidNotificationCenter.UpdateScheduledNotification(notificationID, _notification, "channel_id");
                break;
            case NotificationStatus.Delivered:
                // Remove the previously shown notification from the status bar.
                AndroidNotificationCenter.CancelNotification(notificationID);
                // _rewardPanel.SetActive(true);
                AndroidNotificationCenter.SendNotificationWithExplicitID(_notification, "channel_id", notificationID);
                break;
            case NotificationStatus.Unavailable:
            case NotificationStatus.Unknown:
            default:
                AndroidNotificationCenter.SendNotificationWithExplicitID(_notification, "channel_id", notificationID);
                break;
        }
        Debug.Log(_notificationStatus);
    }
}
