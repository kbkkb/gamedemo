using UnityEngine;
using UnityEngine.UI;
using GameData;

public class PlayerUIManager : MonoBehaviour
{
    [Header("状态UI")]
    public Slider healthSlider;
    public Text healthText;
    public Slider expSlider;
    public Text expText;
    public Text levelText;
    public Text goldText;
    public Text dashCooldownText;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (healthSlider == null) Debug.LogError("Health Slider 未设置！");
        if (expSlider == null) Debug.LogError("Exp Slider 未设置！");
        if (levelText == null) Debug.LogError("Level Text 未设置！");
        if (goldText == null) Debug.LogError("Gold Text 未设置！");
        if (dashCooldownText == null) Debug.LogError("Dash Cooldown Text 未设置！");

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (PlayerStats.Instance == null) return;

        // 更新生命值
        healthSlider.value = PlayerStats.Instance.health / PlayerStats.Instance.maxHealth;
        healthText.text = $"{PlayerStats.Instance.health}/{PlayerStats.Instance.maxHealth}";

        // 更新经验值
        expSlider.value = (float)PlayerStats.Instance.experience / PlayerStats.Instance.expToNextLevel;
        expText.text = $"{PlayerStats.Instance.experience}/{PlayerStats.Instance.expToNextLevel}";

        // 更新等级
        levelText.text = $"等级: {PlayerStats.Instance.level}";

        // 更新金币
        goldText.text = $"金币: {PlayerStats.Instance.gold}";

        // 更新冲刺冷却
        if (playerController != null)
        {
            if (playerController.CanDash)
            {
                dashCooldownText.text = "冲刺就绪";
            }
            else
            {
                dashCooldownText.text = $"冲刺冷却中: {playerController.DashCooldownTimer:F1}秒";
            }
        }
    }
}