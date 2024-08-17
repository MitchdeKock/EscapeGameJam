using Assets.Escape__.Code.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private CoreHealthHandler coreScriptComponent;
    private PlayerController playerScript;
    public event Action<bool> OnToggle;
    public bool isActive = false;
    private float timer = 0f;
    private PowerUpModel _powerUpModel;
    void Start()
    {
        coreScriptComponent = GameObject.FindGameObjectWithTag("Core").GetComponent<CoreHealthHandler>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _powerUpModel = new PowerUpModel()
        {
            DashSpeedUpgrade = 2,
            MoveSpeedUpgrade = 2,
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && Input.GetKeyDown(KeyCode.LeftShift) && coreScriptComponent.Health > 5)
        {
            coreScriptComponent.Health -= 5;
            StartPowerUp();
        }
        else if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || coreScriptComponent.Health < 3)
            {
                timer = 0f; // Reset timer when not active
                StopPowerUp();
            }
            timer += Time.deltaTime;
            if (timer >= 1f) //Number of seconds we want the cost to be
            {
                coreScriptComponent.Health -= 1;
                timer = 0f;
            }
        }

    }

    private void StartPowerUp()
    {
        playerScript.MoveSpeed += _powerUpModel.MoveSpeedUpgrade;
        playerScript.DashSpeed += _powerUpModel.DashSpeedUpgrade;
        isActive = true;
        OnToggle?.Invoke(true);
    }
    private void StopPowerUp()
    {
        isActive = false;
        playerScript.MoveSpeed -= _powerUpModel.MoveSpeedUpgrade;
        playerScript.DashSpeed -= _powerUpModel.DashSpeedUpgrade;
        OnToggle?.Invoke(false);
    }

}
