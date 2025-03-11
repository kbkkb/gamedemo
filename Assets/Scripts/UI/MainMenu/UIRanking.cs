using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRanking : UIWindows
{
    public Text[] Rankings;
    private float[] time  = new float[10];
    // Start is called before the first frame update
    void Start()
    {
        time = DataManager.Instance.LoadArray();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time == null)
        {
            foreach (Text t in Rankings)
            {
                t.text = null;
            }
        }
        else
        {
            for (int i = 0; i < time.Length; i++)
            {
                Rankings[i].text=settime(time[i]);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
            this.gameObject.SetActive(false);
    }
    string settime(float x)
    {
        
        long milliseconds = (long)(x * 1000);

        // 计算分钟和秒
        int minute = (int)(milliseconds / 60000); // 毫秒转分钟
        int seconds = (int)((milliseconds % 60000) / 1000); // 毫秒转秒
        int millisecondsTwoDigits =(int) milliseconds / 10 % 100;
        //对3600取余再对60取余即为秒数
        seconds = seconds % 3600 % 60;
        //返回00:00:00时间格式
        return string.Format("{0:D2}:{1:D2}:{2:D2}", minute,seconds,millisecondsTwoDigits);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}
