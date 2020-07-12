using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    public float countdownNumberInSeconds = 600;
    public float timer;

    void Start()
    {
        timer = countdownNumberInSeconds;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            print("you lose");

            
            GM.LosePanel.SetActive(true);
            Time.timeScale = 0 ;
        }
    }

    


}
