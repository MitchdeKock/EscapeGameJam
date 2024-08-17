using Assets.Escape__.Code.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public CoreHealthHandler coreScriptComponent;
    public PlayerController playerScript;
    public event Action<bool> OnToggle;
    public bool isActive = false;
    public int Cost;
    private float timer = 0f;
    private float initialTimeBetweenCost = 1f;
    private float timeBetweenCost = 0f;
    private float rampUpFactor = 0.1f; // Adjust this value to control the ramp-up speed

    private PowerUpModel _powerUpModel;
    

    public AttackMeleeStaff attackMeleeStaff;
    public AttackRangedStaff rangedStaff;
    void Start()
    {
        timer = initialTimeBetweenCost;
        timeBetweenCost = 1f;
        Cost = 5;
        _powerUpModel = new PowerUpModel()
        {
            DashSpeedUpgrade = 5,
            MoveSpeedUpgrade = 5,
            DashCoolDownUpgrade = -1,
            DashDurationUpgrade = 1,
            RangeAttackDamageUpgrade = 1,
            RangeAttackDistanceUpgrade = 10,
            MeleeAttackDamageUpgrade = 5
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && Input.GetKeyDown(KeyCode.LeftShift) && coreScriptComponent.Health > Cost)
        {
            coreScriptComponent.Health -= Cost;
            TogglePowerUp(true);
        }
        else if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                TogglePowerUp(false);
                ResetTimer();
            }
            timer -= Time.deltaTime;
            if (timer <= 0f) //Number of seconds we want the cost to be
            {
                if (coreScriptComponent.Health < 3) TogglePowerUp(false);
                else
                coreScriptComponent.Health -= 1;
                ResetTimer();
            }
        }

    }
    private void ResetTimer()
    {
        if(isActive)
            timeBetweenCost = initialTimeBetweenCost * Mathf.Exp(rampUpFactor * (initialTimeBetweenCost - timeBetweenCost));
        else
            timeBetweenCost = initialTimeBetweenCost;
        timer = timeBetweenCost;
    }

    private void TogglePowerUp(bool isOn)
    {
        int powerUp = (isOn ? 1 : -1);
        playerScript.MoveSpeed +=powerUp * _powerUpModel.MoveSpeedUpgrade;
        playerScript.DashSpeed += powerUp * _powerUpModel.DashSpeedUpgrade;
        rangedStaff.Range += powerUp * _powerUpModel.RangeAttackDistanceUpgrade;
        rangedStaff.Damage += powerUp * _powerUpModel.RangeAttackDamageUpgrade;
        attackMeleeStaff.Damage += powerUp * _powerUpModel.MeleeAttackDamageUpgrade;

        isActive = isOn;
        OnToggle?.Invoke(isOn);
    }

}
