using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Death event
    public delegate void DeathHandler();

    public event DeathHandler OnDeath;

    [SerializeField] public FloatReference maxHealth;

    [Header("HealthBar animation settings")]
    [SerializeField] private float ghostDelayConstant = 1;
    [SerializeField] private float ghostHealthChangeSpeed = 10;

    [Header("UI")]
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Vector3 healthBarOffset;

    // Cached ui elements
    private GameObject healthBarInstance = null;
    private Image healthBarFill;
    private Image ghostHealthBarFill;

    // Health Values
    [HideInInspector] public float Health { get => health; }
    private float health;
    private float ghostHealth;
    private float ghostDelay = 0;

    [Header("Testing")]
    [SerializeField] private float changeAmount = 0;

    [SerializeField] private bool canDie = true;

    void Start()
    {
        // Initialise health values
        health = ghostHealth = maxHealth.Value;

        // Instantiate healthBar
        GameObject worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");
        if (worldCanvas != null)
            healthBarInstance = Instantiate(healthBarPrefab, gameObject.transform.position + healthBarOffset, Quaternion.identity, worldCanvas.transform);
        else
            Debug.LogWarning("No world canvas found. The HealthBar script requires a world canvas tagged \"WorldCanvas\" to be active in the scene it is used in.");

        // Get refrence to healthBar fills
        if (healthBarInstance != null)
        {
            ghostHealthBarFill = healthBarInstance.transform.GetChild(0).GetComponent<Image>();
            healthBarFill = healthBarInstance.transform.GetChild(1).GetComponent<Image>();
        }
    }

    void Update()
    {
        if (healthBarInstance != null)
        {
            HandleDelayedHealthChange();

            // Lock health bar to unit
            healthBarInstance.transform.position = transform.position + healthBarOffset;
        }
    }

    public void ChangeHealth(float amount)
    {
        // Change health
        health += amount;

        // Check if still alive
        if (health <= 0)
        {
            // Clamp health to never be negative
            health = 0;

            if (canDie)
            { // Call on death events
                OnDeath?.Invoke();
            }
        }

        // Clamp health to never excede maxHealth
        if (health > maxHealth.Value)
            health = maxHealth.Value;

        // Update UI
        if (healthBarInstance != null)
        {
            if (amount > 0)
                ghostHealthBarFill.fillAmount = health / maxHealth.Value;
            else
                healthBarFill.fillAmount = health / maxHealth.Value;
        }

        ghostDelay = ghostDelayConstant;
    }

    public void TestHealth()
    {
        ChangeHealth(changeAmount);
    }

    private void HandleDelayedHealthChange()
    {
        if (ghostDelay <= 0 && ghostHealth != health)
        {
            if (health < ghostHealth)
            {
                // Decrease ghostHealth
                ghostHealth -= ghostHealthChangeSpeed * 10 * Time.deltaTime;

                // Clamp ghostHealth to never go below health
                if (ghostHealth < health)
                    ghostHealth = health;

                // Change UI fill amount
                ghostHealthBarFill.fillAmount = ghostHealth / maxHealth.Value;
            }
            else
            {
                // Increase health
                ghostHealth += ghostHealthChangeSpeed * 10 * Time.deltaTime;

                // Clamp ghostHealth to never go above health
                if (ghostHealth > health)
                    ghostHealth = health;

                // Change UI fill amount
                healthBarFill.fillAmount = ghostHealth / maxHealth.Value;
            }
        }
        else if (ghostDelay > -0.5)
        {
            ghostDelay -= Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.SetActive(false);
        }
    }
}
