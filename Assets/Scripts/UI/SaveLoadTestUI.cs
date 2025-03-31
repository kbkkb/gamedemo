using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameData;

public class SaveLoadTestUI : MonoBehaviour
{
    [Header("按钮")]
    public Button saveButton;
    public Button loadButton;
    public Button deleteButton;

    [Header("状态显示")]
    public Text statusText;
    public Text lastSaveTimeText;

    private void Start()
    {
        // 设置按钮点击事件
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(OnSaveButtonClick);
        }
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(OnLoadButtonClick);
        }
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(OnDeleteButtonClick);
        }

        // 更新UI状态
        UpdateUI();
    }

    private void Update()
    {
        // 更新最后保存时间
        if (lastSaveTimeText != null)
        {
            var lastSaveTime = GameDataManager.Instance.GetLastSaveTime();
            if (lastSaveTime.HasValue)
            {
                lastSaveTimeText.text = $"上次保存: {lastSaveTime.Value:yyyy-MM-dd HH:mm:ss}";
            }
            else
            {
                lastSaveTimeText.text = "没有存档";
            }
        }
    }

    private void OnSaveButtonClick()
    {
        GameDataManager.Instance.SaveGame();
        UpdateUI();
    }

    private void OnLoadButtonClick()
    {
        // 确保GameDataManager单例存在
        if (GameDataManager.Instance == null)
        {
            Debug.LogError("GameDataManager单例未初始化！");
            return;
        }

        // 确保PlayerStats单例存在
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats单例未初始化！");
            return;
        }

        // 检查是否有存档数据
        if (!GameDataManager.Instance.HasSaveData())
        {
            Debug.LogWarning("没有找到存档数据！");
            return;
        }

        GameDataManager.Instance.LoadGame();
        UpdateUI();
    }

    private void OnDeleteButtonClick()
    {
        if (GameDataManager.Instance.HasSaveData())
        {
            PlayerPrefs.DeleteKey("GameSaveData");
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (statusText != null)
        {
            statusText.text = GameDataManager.Instance.HasSaveData() ? "有存档" : "无存档";
        }

        if (loadButton != null)
        {
            loadButton.interactable = GameDataManager.Instance.HasSaveData();
        }

        if (deleteButton != null)
        {
            deleteButton.interactable = GameDataManager.Instance.HasSaveData();
        }
    }
} 