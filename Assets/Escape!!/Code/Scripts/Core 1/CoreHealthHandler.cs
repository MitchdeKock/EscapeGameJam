using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoreHealthHandler : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int maxHealth = 20;
    private SpriteRenderer Headsprite;
    private SpriteRenderer BodySprite;
    public IEnumerator Flash()
    {
        Color oldColourHead = Headsprite.color;
        Color oldColourBody = BodySprite.color;

        Headsprite.color = Color.red;
        BodySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Headsprite.color = oldColourHead;
        BodySprite.color = oldColourBody;
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
            if (oldHealth - health>0 && oldHealth-health<10) {
                StartCoroutine(Flash());
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
