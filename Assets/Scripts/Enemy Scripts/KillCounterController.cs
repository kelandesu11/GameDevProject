using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounterController : MonoBehaviour
{
    public int maxKills;

    int currentKills;

    Text youWinText;
    Text killCountText;

    // Start is called before the first frame update
    void Start()
    {
        youWinText = GameObject.Find("YouWin").GetComponent<Text>();
        killCountText = GameObject.Find("KillCount").GetComponent<Text>();
        currentKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addKill()
    {
        currentKills++;
        killCountText.text = "Kill Count: " + currentKills;
        print(currentKills + " out of " + maxKills + " kills");
        if(currentKills >= maxKills)
        {
            youWinText.text = "You Win!" + "\n" + "Left-click to play again";
            Invoke("winPause", 1);
        }
    }

    void winPause()
    {
        Time.timeScale = 0;
    }
}
