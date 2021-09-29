using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Singleton
    private static PlayerHealth _instance = null;
    public static PlayerHealth Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayerHealth>();
                if(_instance == null)
                {
                    Debug.Log("Fatal Error: Player Health");
                }
            } return _instance;
        }
    }
    #endregion
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    InputHandler inputHandler;
    bool isDead;                                                
    bool damaged;                                               


    void Awake()
    {
        // Get the component's refrence
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren<PlayerShooting>();
        inputHandler = GetComponent<InputHandler>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        // Got damage
        if (damaged)
        {
            // Flash color
            damageImage.color = flashColour;
        }
        else
        {   
            // Fade out effect
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        // Set damage to false state
        damaged = false;
    }

    // function to get damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        // decrease health
        currentHealth -= amount;

        // Change the view of the health slider
        healthSlider.value = currentHealth;

        // Play audio
        playerAudio.Play();

        // calling the death method
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        // Play audio 
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // turn off the player movement
        playerMovement.enabled = false;

        playerShooting.enabled = false;

        inputHandler.enabled = false;
    }

    public void RestartLevel ()
    {
        //Restart Scene
        SceneManager.LoadScene(0);
    }

    public void AddHealth()
    {
        currentHealth += 25;
        healthSlider.value = currentHealth;
    }
}