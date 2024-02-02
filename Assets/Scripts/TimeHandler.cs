using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeHandler : MonoBehaviour
{
    [SerializeField]
    private float timeDuration;
    [SerializeField]
    public float timeRemain;
    [SerializeField]
    private bool timerOn = false;
    [SerializeField]
    public TextMeshProUGUI timeString;
    [SerializeField] Spawner spawner;
    //private float flashTimer;
    //private float flashTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn && !spawner.inTask)
        {
            if (timeRemain - Time.deltaTime > 0)
            {
                
                timeRemain -= Time.deltaTime;
                timePrint(timeRemain);
            }
            else
            {
                timeRemain = 0;
                timePrint(timeRemain);
                timerOn = false;
                SceneManager.LoadScene("VictoryScene");
            }
        }
    }
    private void timePrint(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float centiseconds = Mathf.FloorToInt(currentTime * 100 % 100);
        timeString.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
    private void ResetTimer()
    {
        timeRemain = timeDuration;
    }
    private void Flash()
    {
        if (timeRemain != 0)
        {
            timeRemain = 0;
            timePrint(timeRemain);
        }
    }
}
