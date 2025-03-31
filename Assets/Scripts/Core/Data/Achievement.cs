using UnityEngine;
using System;

[Serializable]
public class Achievement
{
    public string id;                 // 成就唯一标识符
    public string title;              // 成就标题
    public string description;        // 成就描述
    public bool isUnlocked;           // 是否已解锁
    public float progress;            // 成就进度
    public float targetProgress;      // 目标进度
    public DateTime unlockTime;       // 解锁时间

    public Achievement()
    {
        id = "";
        title = "";
        description = "";
        isUnlocked = false;
        progress = 0f;
        targetProgress = 1f;
        unlockTime = DateTime.MinValue;
    }

    public Achievement(string id, string title, string description, float targetProgress = 1f)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.isUnlocked = false;
        this.progress = 0f;
        this.targetProgress = targetProgress;
        this.unlockTime = DateTime.MinValue;
    }

    public void UpdateProgress(float newProgress)
    {
        progress = Mathf.Clamp(newProgress, 0f, targetProgress);
        
        if (progress >= targetProgress && !isUnlocked)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            unlockTime = DateTime.Now;
            Debug.Log($"成就解锁: {title}");
        }
    }

    public float GetProgressPercentage()
    {
        return (progress / targetProgress) * 100f;
    }
} 