using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float time = 600f;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;

            int minutes = (int)time / 60;
            int seconds = (int)time % 60;

            if (seconds >= 10)
            {
                text.text = minutes + ":" + seconds;
            }
            else
            {
                text.text = minutes + ":0" + seconds;
            }
        }
        
    }
}
