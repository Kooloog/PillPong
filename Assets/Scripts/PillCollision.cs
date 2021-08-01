using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PillCollision : MonoBehaviour
{
    Vector3 startCoordinates;
    Quaternion startRotation;
    float startMass;
    GameObject[] pills;
    bool effectWasZeroG;

    public int pillsCollected;
    public static bool isPlaying;
    public TextMeshProUGUI pillWarning;
    public TextMeshProUGUI flagWarning;
    public ParticleSystem pillGet;

    public GameObject levelCompleteUI;
    public GameObject everythingElse;
    public GameObject editModeMenu;
    public GameObject playtimeCircle;
    public GameObject tutorialButton;

    public AudioSource levelCompleteSound;
    public AudioSource stopPlaytest;
    public AudioSource startPlaytest;
    public AudioSource cantDoThat;
    public AudioSource pillGetSound;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        pillsCollected = 0;
        effectWasZeroG = false;

        startCoordinates = transform.position;
        startRotation = transform.rotation;
        startMass = this.GetComponent<Rigidbody2D>().mass;

        pills = GameObject.FindGameObjectsWithTag("Pill");

        //Changing volume of Audio Sources
        levelCompleteSound.volume = PlayerPrefs.GetFloat("Sounds");
        stopPlaytest.volume = PlayerPrefs.GetFloat("Sounds");
        startPlaytest.volume = PlayerPrefs.GetFloat("Sounds");
        cantDoThat.volume = PlayerPrefs.GetFloat("Sounds");
        pillGetSound.volume = PlayerPrefs.GetFloat("Sounds");
    }

    // Update is called once per frame
    void Update()
    {
        pillGet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pill")
        {
            collision.gameObject.SetActive(false);
            pillGet.Play();
            pillsCollected++;

            switch(collision.gameObject.GetComponent<DropEffect>().currentEffect)
            {
                case "Flag":
                    updateMasses();
                    if(pillsCollected == pills.Length)
                    {
                        Debug.Log("Level Complete!");
                        levelCompleteSound.Play();
                        levelCompleteUI.SetActive(true);
                        everythingElse.SetActive(false);

                        if (PlayerPrefs.GetInt("Levels") == SceneManager.GetActiveScene().buildIndex)
                        {
                            PlayerPrefs.SetInt("Levels", SceneManager.GetActiveScene().buildIndex + 1);
                        }
                    }
                    else
                    {
                        flagWarning.gameObject.SetActive(true);
                        cantDoThat.Play();
                    }
                    break;

                case "Speed":
                    pillGetSound.Play();
                    if (effectWasZeroG)
                    {
                        updateMasses();
                        if (this.transform.rotation.x >= 0)
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(18f, -1f);
                        }
                        else
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-18f, -1f);
                        }
                    }
                    else
                    {
                        updateMasses();
                        if (this.GetComponent<Rigidbody2D>().velocity.x >= 0)
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(18f, -1f);
                        }
                        else
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-18f, -1f);
                        }
                    }
                    break;

                case "Reverse":
                    pillGetSound.Play();
                    updateMasses();
                    Vector2 velocityChange = this.GetComponent<Rigidbody2D>().velocity;
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocityChange.x, velocityChange.y);
                    break;

                case "Gravity":
                    pillGetSound.Play();
                    updateMasses();
                    this.GetComponent<Rigidbody2D>().gravityScale *= -1;
                    break;

                case "Still":
                    pillGetSound.Play();
                    updateMasses();
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                    this.GetComponent<Rigidbody2D>().freezeRotation = true;
                    this.GetComponent<Rigidbody2D>().freezeRotation = false;
                    break;

                case "ZeroG":
                    pillGetSound.Play();
                    this.GetComponent<Rigidbody2D>().gravityScale = 0;
                    this.GetComponent<Rigidbody2D>().mass = 0;
                    effectWasZeroG = true;
                    break;
            }
        }
    }

    public void activateBall()
    {
        int pillsWithEffect = 0;

        foreach (GameObject pill in pills)
        {
            if (pill.GetComponent<DropEffect>().currentEffect != null) pillsWithEffect++;
        }

        if (pillsWithEffect == pills.Length)
        {
            //Sets gravity to 1 so it starts moving.
            this.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            this.GetComponent<Rigidbody2D>().freezeRotation = false;

            editModeMenu.SetActive(false);
            playtimeCircle.SetActive(true);
            tutorialButton.SetActive(false);
            startPlaytest.Play();
            isPlaying = true;
        }
        else
        {
            cantDoThat.Play();
            pillWarning.gameObject.SetActive(true);
            StartCoroutine(WaitAndRemove());
        }
    }

    public void returnToStart()
    {
        //Returns ball to starting position and stops rotation.
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().freezeRotation = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        transform.position = startCoordinates;
        transform.rotation = startRotation;

        editModeMenu.SetActive(true);
        playtimeCircle.SetActive(false);
        flagWarning.gameObject.SetActive(false);

        if (Tutorials.hasTutorials == true)
        {
            tutorialButton.SetActive(true);
        }

        if (isPlaying)
        {
            stopPlaytest.Play();
            isPlaying = false;
            effectWasZeroG = false;
        }

        pillsCollected = 0;

        //Sets all pills to their active state.
        foreach(GameObject pill in pills)
        {
            Debug.Log("found");
            pill.SetActive(true);
        }
    }

    IEnumerator WaitAndRemove()
    {
        yield return new WaitForSeconds(1);
        pillWarning.gameObject.SetActive(false);
    }

    void updateMasses()
    {
        if (effectWasZeroG)
        {
            if (this.GetComponent<Rigidbody2D>().velocity.y >= 0)
            {
                this.GetComponent<Rigidbody2D>().gravityScale = -1.0f;
            }
            else
            {
                this.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            }
            effectWasZeroG = false;
        }

        this.GetComponent<Rigidbody2D>().mass = startMass;
    }
}
