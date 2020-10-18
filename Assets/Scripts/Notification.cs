using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public TMP_Text notificationMessage;
    public Slider slider;
    public float maxTime = 5f;
    float timeLeft;

    void Start()
    {
        timeLeft = maxTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft / maxTime;
        }
        else
        {
            //Close notification
            Destroy(gameObject);
        }
    }
}