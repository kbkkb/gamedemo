using UnityEngine;
using System;
using GameData;

[Serializable]
public class PlayerStatsData
{
    public int level;
    public int experience;
    public int expToNextLevel;
    public int gold;
    public float maxHealth;
    public float health;
    public int attack;
    public int defense;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;


    public PlayerStatsData()
    {
        level = 1;
        experience = 0;
        expToNextLevel = 100;
        gold = 0;
        maxHealth = 100f;
        health = 100f;
        attack = 10;
        defense = 5;
        moveSpeed = 5f;
        dashSpeed = 15f;
        dashDuration = 0.2f;
        dashCooldown = 1f;
    }

    public void CopyFrom(PlayerStats stats)
    {
        level = stats.level;
        experience = stats.experience;
        expToNextLevel = stats.expToNextLevel;
        gold = stats.gold;
        maxHealth = stats.maxHealth;
        health = stats.health;
        attack = stats.attack;
        defense = stats.defense;
        moveSpeed = stats.moveSpeed;
        dashSpeed = stats.dashSpeed;
        dashDuration = stats.dashDuration;
        dashCooldown = stats.dashCooldown;
    }

    public void ApplyTo(PlayerStats stats)
    {
        stats.level = level;
        stats.experience = experience;
        stats.expToNextLevel = expToNextLevel;
        stats.gold = gold;
        stats.maxHealth = maxHealth;
        stats.health = health;
        stats.attack = attack;
        stats.defense = defense;
        stats.moveSpeed = moveSpeed;
        stats.dashSpeed = dashSpeed;
        stats.dashDuration = dashDuration;
        stats.dashCooldown = dashCooldown;
    }
} 