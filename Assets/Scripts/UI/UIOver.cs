using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOver : MonoBehaviour
{
    public Text[] time;
    public Text fool;
    public Text killnums;
    public float times;
    public float[] rank;
    void Start()
    {
        update(GameManager.Instance.fools,GameManager.Instance.killnums);
    }

    private void Update()
    {
        setover(GameManager.Instance.time);
        
    }

    public void setover(string[] Time)
    {
        time[0].text=Time[0];
        time[1].text=Time[1];
        time[2].text=Time[2];
    }
    
    public void update(int Fool,int Killnums)
    {
        fool.text=Fool.ToString();
        killnums.text=Killnums.ToString();
        times=GameManager.Instance.gametime;
        rank=DataManager.Instance.Rank;
        Array.Reverse(rank);
        if(times > rank[9])
        {
            rank[9] = times;
            Array.Reverse(rank);
            DataManager.Instance.SaveArray(rank);
        }
        
    }
    public void OnclickBcak()
    {
        Time.timeScale = 1;
        //PlayerMovement.Instance.Destroy();
        UIManager.Instance.Destroy();
        MainUI.Instance.Destroy();
        SceneManager.LoadScene(0);
    }

    public void Close()
    {
        Destroy(gameObject);
    }

}
