using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public GameObject notificationPrefab;

    private GameObject notification;

    public void DisplayNotificationCanvas(int index)
    {
        notification = Instantiate(notificationPrefab.gameObject, new Vector3(0, 0, 0), transform.rotation) as GameObject;
        notification.transform.SetParent(gameObject.transform, false);
        notification.transform.localScale = new Vector3(1, 1, 1);

        switch (index)
        {
            case 2: // Error: Asset is null
                SetNotificationMessage("Error! Json file is null!");
                break;
            case 1: // Finished exporting scouting data
                SetNotificationMessage("Finished exporting scouting data!");
                break;
            default:
                SetNotificationMessage("Error! Invalid notification code!");
                break;
        }
        index = 0;
    }

    public void ErrorNullAsset()
    {
        DisplayNotificationCanvas(2);
    }

    public void FinishedExportingData()
    {
        DisplayNotificationCanvas(1);
    }

    private void SetNotificationMessage(string message)
    {
        notification.GetComponent<Notification>().notificationMessage.text = message;
    }
}