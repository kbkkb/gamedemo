using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gametime = 0f;
    private float startTime;
    private float stopTime;
    public static GameManager Instance { get; private set; }
    public string[] time = new string[3];
    public Transform playertransform;
    public GameObject playerprb;
    private GameObject player;
    public CinemachineVirtualCamera  cinemachine;
    public int diff;
    public GameObject overPanel;
    public int killnums;
    public int fools;
    private void Awake()
    {
        diff = 0;
        // 确保只有一个实例存在
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 如果已经有一个实例存在，销毁当前对象
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 如果需要在场景切换时保留单例
        player = Instantiate(this.playerprb,playertransform.position,Quaternion.identity);
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachine.Follow = player.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        stopTime = 0;
        killnums = 0;
        fools = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale==1)
            settime();
    }
    
    void settime()
    {
        gametime = Time.realtimeSinceStartup - startTime;
        
        long milliseconds = (long)(gametime * 1000);

        // 计算分钟和秒
        int minute = (int)(milliseconds / 60000); // 毫秒转分钟
        int seconds = (int)((milliseconds % 60000) / 1000); // 毫秒转秒
        int millisecondsTwoDigits =(int) milliseconds / 10 % 100;
        //对3600取余再对60取余即为秒数
        seconds = seconds % 3600 % 60;
        //返回00:00:00时间格式
        time[0] = string.Format("{0:D2}", minute);
        time[1] = string.Format("{0:D2}", seconds);
        time[2] = string.Format("{0:D2}", millisecondsTwoDigits);
    }

    public void ReStart()
    {
        fools++;
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneName);
        player.transform.position = playertransform.position;
    }

    public void End()
    {
        // PlayerMovement.Instance.Destroy();
        // UIManager.Instance.Destroy();
        // MainUI.Instance.Destroy();
        
    }
    public void Gameover()
    {
        GameObject overPanelInstance = Instantiate(this.overPanel, playertransform.position, Quaternion.identity);
        GameObject canvas = GameObject.Find("Canvas");
        // 设置父物体为 UI 层级（或者任何你想设置的父物体）
        overPanelInstance.transform.SetParent(canvas.transform, false);  // UIManager.Instance 是父物体
        Time.timeScale = 0;
        
    }
}
