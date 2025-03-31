using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("音效")]
    public AudioClip jumpSound;     // 跳跃音效
    public AudioClip dashSound;     // 冲刺音效
    public AudioClip landSound;     // 落地音效
    public AudioClip hurtSound;     // 受伤音效
    public AudioClip levelUpSound;  // 升级音效

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 播放音效
    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // 播放跳跃音效
    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    // 播放冲刺音效
    public void PlayDashSound()
    {
        PlaySound(dashSound);
    }

    // 播放落地音效
    public void PlayLandSound()
    {
        PlaySound(landSound);
    }

    // 播放受伤音效
    public void PlayHurtSound()
    {
        PlaySound(hurtSound);
    }

    // 播放升级音效
    public void PlayLevelUpSound()
    {
        PlaySound(levelUpSound);
    }
} 