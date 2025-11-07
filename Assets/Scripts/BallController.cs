using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float bounceForce = 5f;
    public float waterFloatForce = 7f;
    
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    
    [Header("References")]
    public HealthUI healthUI;
    
    private Rigidbody rb;
    private bool isGrounded;
    private bool inWater;
    private AudioSource audioSource;
    
    [Header("Sound Effects")]
    public AudioClip bounceSound;
    public AudioClip damageSound;
    public AudioClip jumpSound;
    public AudioClip waterSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        
        if (healthUI != null)
            healthUI.SetMaxHealth(maxHealth);
    }
    
    void Update()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Apply movement force
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;
        rb.AddForce(movement, ForceMode.Force);
        
        // Auto-bounce when grounded
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            PlaySound(bounceSound);
        }
        
        // Super jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlaySound(jumpSound);
        }
        
        // Water floating
        if (inWater)
        {
            rb.AddForce(Vector3.up * waterFloatForce, ForceMode.Acceleration);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if grounded
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
        
        // Handle spike collision
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(1);
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = false;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Heart pickup
        if (other.CompareTag("Heart"))
        {
            Heal(1);
            Destroy(other.gameObject);
        }
        
        // Water zone
        if (other.CompareTag("Water"))
        {
            inWater = true;
            PlaySound(waterSound);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        if (healthUI != null)
            healthUI.SetHealth(currentHealth);
            
        PlaySound(damageSound);
        
        // Visual feedback - flash red
        StartCoroutine(FlashRed());
        
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        
        if (healthUI != null)
            healthUI.SetHealth(currentHealth);
    }
    
    IEnumerator FlashRed()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        renderer.material.color = originalColor;
    }
    
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    
    void GameOver()
    {
        Debug.Log("Game Over!");
        // Implement game over logic here
        // For example: restart level, show game over screen, etc.
    }
}
