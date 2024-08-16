using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashCooldown = 1;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Projectile rangedProjectile;

    private Vector2 moveInput;
    private float activeMoveSpeed;
    private float dashCooldownCounter;
    private float dashDurationCounter;

    [Header("Attacks")]
    [SerializeField] private BaseWeapon mainAttack;
    [SerializeField] private BaseWeapon secondaryAttack;

    public float MoveSpeed
    {
        get => moveSpeed;
        set 
        { 
            moveSpeed = activeMoveSpeed = value;
        }
    }
    public float DashSpeed { get => dashSpeed;  set => dashSpeed = value; }

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseManager.TogglePause();
        }

        // Input
        if (!PauseManager.IsPaused)
        {
            mainAttack.Tick();
            secondaryAttack.Tick();
            if (Input.GetButton("Fire1") && mainAttack.canAttack)
            {
                mainAttack.Attack(this.gameObject);
            }

            if (Input.GetButton("Fire2") && secondaryAttack.canAttack)
            {
                secondaryAttack.Attack(this.gameObject);
            }

            GetMoveInput();

            if (Input.GetButtonDown("Dash"))
            {
                if (dashCooldownCounter <= 0 && dashDurationCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashDurationCounter = dashDuration;
                }
            }
            GetMoveInput();
            FaceMousePosition();
        }

        Dash();
        Move();

        // Cooldowns
        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }

    private void Move()
    {
        rb.velocity = moveInput * activeMoveSpeed;
    }

    private void Dash()
    {
        if (dashDurationCounter > 0)
        {
            dashDurationCounter -= Time.deltaTime;

            if (dashDurationCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }
    }

    private void GetMoveInput()
    {
        moveInput.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
    }

    private void FaceMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 relativeMouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - transform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, relativeMouseWorldPosition);
    }
}
