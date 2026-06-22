using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
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
    bool isDead;
    bool damaged;


    void Awake()
    {
        //Mendapatkan refernce komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren <PlayerShooting> ();
        // Ensure initial health is set from startingHealth so scene-serialized values
        // (e.g. 0) don't leave the player immediately dead at start.
        currentHealth = startingHealth;

        // If the health slider or damage image weren't assigned in the inspector,
        // try to find common UI elements by name so the Game view shows damage.
        if (healthSlider == null)
        {
            var sliderObj = GameObject.Find("HealthSlider");
            if (sliderObj) healthSlider = sliderObj.GetComponent<UnityEngine.UI.Slider>();
        }
        if (healthSlider == null)
        {
            healthSlider = FindObjectOfType<UnityEngine.UI.Slider>();
        }

        if (healthSlider == null)
        {
            CreateHealthSlider();
        }

        if (damageImage == null)
        {
            var dmgObj = GameObject.Find("DamageImage");
            if (dmgObj) damageImage = dmgObj.GetComponent<UnityEngine.UI.Image>();
        }

        if (healthSlider)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = startingHealth;
            healthSlider.value = currentHealth;
        }
    }


    void Update()
    {
        //Jika terkena damaage
        if (damaged)
        {
            //Merubah warna gambar menjadi value dari flashColour
            damageImage.color = flashColour;
        }
        else
        {
            //Fade out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //Set damage to false
        damaged = false;
    }


    //fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        //mengurangi health
        currentHealth -= amount;

        //Merubah tampilan dari health slider
        if (healthSlider) healthSlider.value = currentHealth;

        //Memainkan suara ketika terkena damage
        if (playerAudio) playerAudio.Play();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    // fungsi untuk menambah nyawa
    public void Healing()
    {
        //mengurangi health
        int newHealth = currentHealth + 40;
        if (newHealth >= 100)
        {
            currentHealth = 100;
        }
        else
        {
            currentHealth = newHealth;
        }

        //Merubah tampilan dari health slider
        if (healthSlider)
        {
            healthSlider.maxValue = startingHealth;
            healthSlider.value = currentHealth;
        }
    }


    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;

        playerShooting.enabled = false;

        // Notify GameOverManager so the end-game UI appears immediately
        var gom = FindObjectOfType<GameOverManager>();
        if (gom != null)
        {
            gom.OnPlayerDeath();
        }
    }

    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }

    void CreateHealthSlider()
    {
        GameObject canvasObject = GameObject.Find("HUDCanvas");
        if (canvasObject == null)
            canvasObject = GameObject.FindObjectOfType<Canvas>() ? GameObject.FindObjectOfType<Canvas>().gameObject : null;

        if (canvasObject == null)
            return;

        GameObject sliderObject = new GameObject("HealthSlider", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Slider));
        sliderObject.transform.SetParent(canvasObject.transform, false);

        RectTransform sliderTransform = sliderObject.GetComponent<RectTransform>();
        sliderTransform.anchorMin = new Vector2(0f, 1f);
        sliderTransform.anchorMax = new Vector2(0f, 1f);
        sliderTransform.pivot = new Vector2(0f, 1f);
        sliderTransform.anchoredPosition = new Vector2(20f, -20f);
        sliderTransform.sizeDelta = new Vector2(220f, 22f);

        Image backgroundImage = sliderObject.GetComponent<Image>();
        backgroundImage.color = new Color(0.18f, 0.18f, 0.18f, 0.85f);

        Slider slider = sliderObject.GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = startingHealth;
        slider.value = currentHealth;
        slider.interactable = false;
        slider.direction = Slider.Direction.LeftToRight;

        GameObject fillAreaObject = new GameObject("Fill Area", typeof(RectTransform));
        fillAreaObject.transform.SetParent(sliderObject.transform, false);

        RectTransform fillAreaTransform = fillAreaObject.GetComponent<RectTransform>();
        fillAreaTransform.anchorMin = new Vector2(0f, 0f);
        fillAreaTransform.anchorMax = new Vector2(1f, 1f);
        fillAreaTransform.offsetMin = new Vector2(4f, 4f);
        fillAreaTransform.offsetMax = new Vector2(-4f, -4f);

        GameObject fillObject = new GameObject("Fill", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        fillObject.transform.SetParent(fillAreaObject.transform, false);

        RectTransform fillTransform = fillObject.GetComponent<RectTransform>();
        fillTransform.anchorMin = new Vector2(0f, 0f);
        fillTransform.anchorMax = new Vector2(1f, 1f);
        fillTransform.offsetMin = Vector2.zero;
        fillTransform.offsetMax = Vector2.zero;

        Image fillImage = fillObject.GetComponent<Image>();
        fillImage.color = new Color(0.85f, 0.15f, 0.15f, 1f);

        slider.fillRect = fillTransform;
        slider.targetGraphic = backgroundImage;
        healthSlider = slider;
    }
}
