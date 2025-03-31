using UnityEngine;
using GameData;

public class Spike : MonoBehaviour
{
    [Header("伤害设置")]
    public int damage = 10;         // 伤害值
    public float damageInterval = 1f; // 伤害间隔（秒）
    public bool isActive = true;     // 是否激活

    private float damageTimer = 0f;
    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerInTrigger = true;
            // 立即造成伤害
            PlayerStats.Instance.TakeDamage(damage);
            AudioManager.Instance.PlayHurtSound();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerInTrigger = false;
            damageTimer = 0f;
        }
    }

    private void Update()
    {
        if (isActive && isPlayerInTrigger)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                PlayerStats.Instance.TakeDamage(damage);
                AudioManager.Instance.PlayHurtSound();
                damageTimer = 0f;
            }
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        isPlayerInTrigger = false;
        damageTimer = 0f;
    }

    // 设置伤害值
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    // 设置伤害间隔
    public void SetDamageInterval(float newInterval)
    {
        damageInterval = newInterval;
    }
} 