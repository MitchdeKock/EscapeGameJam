using System;
using System.Collections;
using UnityEngine;

public class CoreHealthHandler : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private AudioClip healthSound;
    [SerializeField] private AudioClip hurtSound;
    private SpriteRenderer Headsprite;
    private SpriteRenderer BodySprite;
    public IEnumerator Flash()
    {
        Headsprite.color = Color.red;
        BodySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Headsprite.color = Color.white;
        BodySprite.color = Color.white;
    }
    private void Start()
    {
        Transform spriteHeadTransform = transform.Find("Sprite_head");
        Transform spriteBodyTransform = transform.Find("Sprite_body");

        Headsprite = spriteHeadTransform.GetComponent<SpriteRenderer>();
        BodySprite = spriteBodyTransform.GetComponent<SpriteRenderer>();
    }
    public int Health 
    { 
        get => health;
        set
        {
            int oldHealth = health;
            health = Math.Min(value, maxHealth);
            if (oldHealth>health && !PauseManager.IsPaused) {
                StartCoroutine(Flash());
                SFXManager.instance.PlaySoundFXClip(hurtSound, Headsprite.transform, 1f);
            }
            else
            {
                SFXManager.instance.PlaySoundFXClip(healthSound, Headsprite.transform, 1f);
            }
            OnHealthValueChanged?.Invoke(health);
        }
    }

    public int MaxHealth 
    { 
        get => maxHealth;
        set
        {
            maxHealth = value;
            OnMaxHealthValueChanged?.Invoke(maxHealth);
        }
    }

    public event Action<int> OnHealthValueChanged;
    public event Action<int> OnMaxHealthValueChanged;

}
