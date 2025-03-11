using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Image[] Hp;
    public Text[] time;
    public GameObject EscPanel;
    public Text killnum;
    
    public static MainUI Instance { get; private set; }

    private void Awake()
    {
        // 确保只有一个实例存在
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 如果已经有一个实例存在，销毁当前对象
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 如果需要在场景切换时保留单例
    }
    
    void Start()
    {
        EscPanel.SetActive(false);
        updateHp();
        killnum.text = GameManager.Instance.killnums.ToString();
    }
    

    // Update is called once per frame
    void Update()
    {
        updateHp();
        updateTime();
        stopGame();
    }

    public void updateHp()
    {
        // for (int i = 0; i < Hp.Length; i++)
        // {
        //     if (player.HP > i)
        //     {
        //         Hp[i].gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         Hp[i].gameObject.SetActive(false);
        //     }
        // }
    }

    public void updateTime()
    {

        time[0].text=GameManager.Instance.time[0];
        time[1].text=GameManager.Instance.time[1];
        time[2].text=GameManager.Instance.time[2];
    }
    
    void stopGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            EscPanel.SetActive(true);
        }
    }
    public void OnClickBag()
    {
        
        UIManager.Instance.Show<UIBag>();
    }
    public void OnClickAdd()
    {
        BagManager.Instance.AddItem(1,1);
        BagManager.Instance.AddItem(2,1);
        BagManager.Instance.AddItem(3,1);
    }
    public void OnClickDelete()
    {
        BagManager.Instance.RemoveItem(1,1);
    }

    public void ShowBag()
    {
        BagManager.Instance.outbagItems();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
