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
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator headAnimator;
    [SerializeField] private Animator staffAnimator;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private Transform staff;

    private Vector2 moveInput;
    private Vector2 lastMoveInput;
    private float activeMoveSpeed;
    private float dashCooldownCounter;
    private float dashDurationCounter;
    private float slashCounter;
    private float blinkCounter;

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
                staffAnimator.SetTrigger("attack");
                staff.gameObject.SetActive(true);
                slashCounter = 0.2f;
            }

            if (Input.GetButton("Fire2") && secondaryAttack.canAttack)
            {
                secondaryAttack.Attack(this.gameObject);
                staffAnimator.SetTrigger("attack");
            }

            GetMoveInput();

            if (Input.GetButtonDown("Dash"))
            {
                if (dashCooldownCounter <= 0 && dashDurationCounter <= 0)
                {
                    SFXManager.instance.PlaySoundFXClip(dashSound, transform, 1f);
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
        if (blinkCounter > 0)
        {
            blinkCounter -= Time.deltaTime;
        }
        else
        {
            blinkCounter = UnityEngine.Random.Range(3, 7);
            headAnimator.SetTrigger("Blink");
        }

        if (slashCounter > 0)
        {
            slashCounter -= Time.deltaTime;
        }
        else
        {
            staff.gameObject.SetActive(false);
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

        if (moveInput.magnitude > 0.05f)
        {
            lastMoveInput = moveInput;
        }

        if (rb.velocity.x > 0.1)
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        else if (rb.velocity.x < -0.1)
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        if (rb.velocity.y > 0.1)
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if (rb.velocity.y < -0.1)
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 3;
        }

        // Animate
        bodyAnimator.SetFloat("MoveX", lastMoveInput.x);
        bodyAnimator.SetFloat("MoveY", lastMoveInput.y);
        bodyAnimator.SetFloat("MoveMagnitude", moveInput.magnitude);

        headAnimator.SetFloat("MoveX", lastMoveInput.x);
        headAnimator.SetFloat("MoveY", lastMoveInput.y);

        staffAnimator.SetFloat("MoveX", lastMoveInput.x);
        staffAnimator.SetFloat("MoveY", lastMoveInput.y);
    }

    private void FaceMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 relativeMouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - transform.position;

        staff.rotation = Quaternion.LookRotation(Vector3.forward, relativeMouseWorldPosition);
    }
}
