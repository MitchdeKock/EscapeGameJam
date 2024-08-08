using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashCooldown = 1;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveInput;
    private float activeMoveSpeed;
    private float dashCooldownCounter;
    private float dashCounter;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (dashCounter > 0)
        {
            // ToDo Uncomment for dash to mouse
            //Vector2 mousePos = Input.mousePosition;
            //moveInput = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - transform.position;

            //moveInput.Normalize();
        }
        else
        {
            GetInput();
        }

        rb.velocity = moveInput * activeMoveSpeed;

        if (Input.GetButtonDown("Dash"))
        {
            if (dashCooldownCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashDuration;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }

    private void GetInput()
    {
        moveInput.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
    }
}
