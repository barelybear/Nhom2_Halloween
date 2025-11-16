using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text displayTextField;
    public int maxHealth = 3; 
    private int currentHealth;

    public float invincibilityDuration = 1f; 
    private bool isInvincible = false;
    private Animator animator;
    private GameManager gameManager;
    public Image[] hearts;
    public GameObject gameover;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); 
        gameManager = GameManager.instance;
        gameover.SetActive(false);
    }

    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible) return;

        currentHealth -= damageAmount;
        Debug.Log("Player took " + damageAmount + " damage. Current Health: " + currentHealth);


        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            UpdateHeartDisplay();
            Die();
            
        }
        else
        {
            UpdateHeartDisplay();
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }

        if (gameManager != null)
        {
            gameManager.GameOver();
            gameover.SetActive(true);
            int score = gameManager.score;
            displayTextField.text = "Your score is: " + score;

        }
        Destroy(gameObject, 2f);
    }
    
    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth); 
        }
    }
}
