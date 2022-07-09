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
    private void OnApplicationQuit()
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

        _notification.Title = "Your piggy bank is full!";
        _notification.Style = NotificationStyle.BigTextStyle;
        _notification.Text = "Go into the game and smash it for the reward.";
        _notification.FireTime = (PigMoneybox.NextClaim - DateTime.Now).TotalMinutes > 60
            ? DateTime.Now.AddMinutes(280)
            : DateTime.Now.AddMinutes(60);
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


        const int notificationID1 = 11000;

        _notification = new AndroidNotification();
        _notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID1);

        _channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Competition Chanel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(_channel);

        _notification.Title = "It's time to compete with other players!";
        _notification.Style = NotificationStyle.BigTextStyle;
        _notification.Text = "Win them all and get your reward.";
        _notification.FireTime = DateTime.Now.AddDays(1);
        switch (_notificationStatus)
        {
            case NotificationStatus.Scheduled:
                // Replace the scheduled notification with a new notification.
                AndroidNotificationCenter.UpdateScheduledNotification(notificationID1, _notification, "channel_id");
                break;
            case NotificationStatus.Delivered:
                // Remove the previously shown notification from the status bar.
                AndroidNotificationCenter.CancelNotification(notificationID1);
                // _rewardPanel.SetActive(true);
                AndroidNotificationCenter.SendNotificationWithExplicitID(_notification, "channel_id", notificationID1);
                break;
            case NotificationStatus.Unavailable:
            case NotificationStatus.Unknown:
            default:
                AndroidNotificationCenter.SendNotificationWithExplicitID(_notification, "channel_id", notificationID1);
                break;
        }

        Debug.Log(_notificationStatus);
        const int notificationID2 = 11001;

        _notification = new AndroidNotification();
        _notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID1);

        _channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Reward Chanel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(_channel);

        _notification.Title = "The waiting time in the store is over!";
        _notification.Style = NotificationStyle.BigTextStyle;
        _notification.Text = "You can pick up your reward again at the store.";
        _notification.FireTime = RewardDiamonds.LastReceiveDiamond.AddHours(12);
        switch (_notificationStatus)
        {
            case NotificationStatus.Scheduled:
                // Replace the scheduled notification with a new notification.
                AndroidNotificationCenter.UpdateScheduledNotification(notificationID1, _notification, "channel_id");
                break;
            case NotificationStatus.Delivered:
                // Remove the previously shown notification from the status bar.
                AndroidNotificationCenter.CancelNotification(notificationID1);
                // _rewardPanel.SetActive(true);
                if (RewardDiamonds.LastReceiveDiamond.AddHours(12) > DateTime.Now)
                    AndroidNotificationCenter.SendNotificationWithExplicitID(_notification, "channel_id",
                        notificationID1);
                break;
            case NotificationStatus.Unavailable:
            case NotificationStatus.Unknown:
            default:
                if (RewardDiamonds.LastReceiveDiamond.AddHours(12) > DateTime.Now)
                    AndroidNotificationCenter.SendNotificationWithExplicitID(_notification, "channel_id",
                        notificationID1);
                break;
        }

        Debug.Log(_notificationStatus);
    }
}