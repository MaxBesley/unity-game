using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timeHandler : MonoBehaviour
{
    [SerializeField] float timeLeft;
    [SerializeField] bool TimerOn = false;
    [SerializeField] TextMeshProUGUI showTime;
    [SerializeField] ToTaskTransition taskTransition;
    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn )
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {   
                timeLeft = 0;
                TimerOn = false;
            }
        }
    }
    void updateTimer(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float centiSeconds = Mathf.FloorToInt(seconds % 100);
        
    }
}
