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

    private float mainAttackCooldown;
    private float secondaryAttackCooldown;
    [Header("Attacks")]
    [SerializeField] private BaseWeapon mainAttack;
    [SerializeField] private BaseWeapon secondaryAttack;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        //mainAttack = new AttackMeleeStaff(1, 5, 3, 70); // ToDo Add weapon in meaningful way
        //secondaryAttack = new AttackRangedStaff(0.3f, 3, 8, rangedProjectile); // ToDo Add weapon in meaningful way
    }

    public void upgradeMovementSpeed()
    {
       // moveSpeed= moveSpeed+ 40;
        Debug.Log(moveSpeed.ToString());
    }

    void Update()
    {
        mainAttack.Tick();
        secondaryAttack.Tick();

        // Input
        if (Input.GetButton("Fire1") && mainAttack.canAttack)
        {
            mainAttack.Attack(this.gameObject);
        }

        if (Input.GetButton("Fire2") && secondaryAttack.canAttack)
        {
            secondaryAttack.Attack(this.gameObject);
        }

        if (dashDurationCounter > 0)
        {
            // ToDo Uncomment for dash to mouse
            //Vector2 mousePos = Input.mousePosition;
            //moveInput = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - transform.position;

            //moveInput.Normalize();
        }
        else
        {
            GetMoveInput();
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (dashCooldownCounter <= 0 && dashDurationCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashDurationCounter = dashDuration;
            }
        }

        // Cooldowns
        if (mainAttackCooldown > 0)
        {
            mainAttackCooldown -= Time.deltaTime;
        }

        if (secondaryAttackCooldown > 0)
        {
            secondaryAttackCooldown -= Time.deltaTime;
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }

        FaceMousePosition();
        Move();
        Dash();
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
