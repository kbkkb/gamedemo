using UnityEngine;

namespace GameData
{
    public class PlayerStats : Singleton<PlayerStats>
    {

        [Header("基础属性")]
        public float maxHealth = 100f;
        public float health;
        public int level = 1;
        public int experience = 0;
        public int expToNextLevel = 100;
        public int gold = 0;

        [Header("战斗属性")]
        public int defense = 5;  // 默认防御值
        public float dashSpeed = 10f;  // 默认冲刺速度



        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public float dashForce = 15f;
        public float dashDuration = 0.2f;
        public float dashCooldown = 1f;

        public int attack = 10;

        private void Awake()
        {
        }

        private void Start()
        {
            health = maxHealth; // 初始化血量
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health < 0) health = 0;
            Debug.Log($"受到伤害: {damage}，当前血量: {health}");
        }

        public void Heal(float amount)
        {
            health += amount;
            if (health > maxHealth) health = maxHealth;
            Debug.Log($"恢复生命: {amount}，当前血量: {health}");
        }

        public void GainExp(int amount)
        {
            experience += amount;
            if (experience >= expToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level++;
            experience = 0;
            expToNextLevel += 50; // 经验需求增加
            Debug.Log($"升级到等级 {level}，下一级需要 {expToNextLevel} 经验");
        }

        public void AddGold(int amount)
        {
            gold += amount;
            Debug.Log($"获得金币: {amount}，当前金币: {gold}");
        }
    }
}