using UnityEngine;
using TMPro;

public class Timer_Script : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float milsec;
    private int sec;
    private int min;
    private int hour;

    void Start()
    {
        timerText.text = "0:00:00";
    }

    void Update()
    {
        TimerUpdater();
        SetText();
    }

    public void Stop()
    {
        this.enabled = false;
    }

    public void Play()
    {
        milsec = 0;
        sec = 0;
        min = 0;
        hour = 0;

        this.enabled = true;
    }

    private void TimerUpdater()
    {
        if (milsec >= 99)
        {
            milsec = 0;

            if (sec == 59)
            {
                sec = 0;

                if (min == 59)
                {
                    min = 0;
                    ++hour;
                }
                else
                {
                    ++min;
                }
            }
            else
            {
                ++sec;
            }
        }
        else
        {
            milsec += Time.deltaTime * 100;
        }
    }

    private void SetText()
    {
        string secText;
        if (sec < 10)
        {
            secText = "0" + sec;
        }
        else
        {
            secText = sec.ToString();
        }

        string milsecText;
        if (milsec < 10)
        {
            milsecText = "0" + Mathf.Round(milsec);
        }
        else
        {
            milsecText = Mathf.Round(milsec).ToString();
        }

        string minText;
        if (min < 10)
        {
            minText = "0" + min;
        }
        else
        {
            minText = min.ToString();
        }

        if (hour <= 0)
        {
            timerText.text = minText + ":" + secText + ":" + milsecText;
        }
        else
        {
            timerText.text = hour + ":" + minText + ":" + secText + ":" + milsecText;
        }
    }
}
