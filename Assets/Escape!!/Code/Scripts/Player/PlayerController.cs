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

    private float attackCooldown;
    private IAttack mainAttack;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        mainAttack = new AttackMeleeStaff(1, 5, 3, 70); // ToDo Add weapon in meaningful way
    }

    void Update()
    {
        RotateToMouse();

        if (Input.GetButton("Fire1") && attackCooldown <= 0)
        {
            mainAttack.Attack(this.gameObject);
            attackCooldown = mainAttack.Cooldown;
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

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

    private void RotateToMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 relativeMouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane)) - transform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, relativeMouseWorldPosition);
    }

    // Temp For Visualizing Attack
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 centerPosition = transform.position;
        Vector2 direction = (Vector2)transform.up;

        float arcRadius = 3f;
        float arcAngle = 70f;
        int segments = 7;
        float angleStep = arcAngle / segments;

        for (float angle = -arcAngle / 2f; angle <= arcAngle / 2f; angle += angleStep)
        {
            Vector2 arcDirection = Quaternion.Euler(0, 0, angle) * direction;
            Vector2 arcPoint = centerPosition + arcDirection * arcRadius;
            Gizmos.DrawLine(centerPosition, arcPoint);
        }
    }

}
