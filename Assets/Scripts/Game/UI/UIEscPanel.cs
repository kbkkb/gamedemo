using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEscPanel : UIWindows
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            close();
    }

    // Update is called once per frame
    public new void close()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        
    }
    public new void back1()
    {
        Time.timeScale = 1;
        GameManager.Instance.End();
        SceneManager.LoadScene(0);
    }
    public new void back2()
    {
        Application.Quit();
    }
}
