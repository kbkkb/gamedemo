using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : UIWindows
{
    public GameObject RankingPanel;
    public GameObject h2p;
    void Start()
    {
        RankingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RankingGame()
    {
        RankingPanel.SetActive(true);
    }

    public void H2P()
    {
        h2p.gameObject.SetActive(true);
    }
}
