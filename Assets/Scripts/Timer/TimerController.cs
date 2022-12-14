using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float maxTime;
    private float timeLeft;

    [SerializeField] private Slider secondSlider;
    [SerializeField] private TextMeshProUGUI secondsText;

    private bool countingDown;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = maxTime;
        //countingDown = true;
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (countingDown == false)
            return;

        timeLeft -= Time.deltaTime;

        UpdateTimer();

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            countingDown = false; 
            UpdateTimer();
            secondsText.text = "RIP";

            GetComponent<TriggerEvent>().DoEvent();

            timeLeft = maxTime;
        }

    }

    public void UpdateTimer()
    {
        secondsText.text = Mathf.FloorToInt(timeLeft).ToString();
        secondSlider.value = (timeLeft % 1) + Mathf.FloorToInt(timeLeft/maxTime);
    }

    public void ToggleCounting()
    {
        countingDown = !countingDown;
    }
}
