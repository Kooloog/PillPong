using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject MainMenuCanvas;
    [SerializeField] public GameObject LevelSelectCanvas;
    [SerializeField] public GameObject OptionsCanvas;
    [SerializeField] public GameObject CreditsCanvas;
    [SerializeField] public GameObject ball;

    [SerializeField] public AudioSource soundTest;
    [SerializeField] public AudioSource warning;
    [SerializeField] public AudioSource UltraSecretCheat;

    [SerializeField] public GameObject resetDataCheck;
    [SerializeField] public GameObject cheatCode;
    [SerializeField] public GameObject cheatCodeBackButton;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider soundsSlider;

    //Private
    AudioSource mainMusic;

    //Could this be... the set-up for a secret cheat??
    private KeyCode[] sequence = new KeyCode[]{
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A,
    };
    private int sequenceIndex;

    public void changeToLevelSelect()
    {
        MainMenuCanvas.SetActive(false);
        ball.SetActive(false);
        LevelSelectCanvas.SetActive(true);
    }

    public void changetoOptionsScreen()
    {
        MainMenuCanvas.SetActive(false);
        ball.SetActive(false);
        OptionsCanvas.SetActive(true);
    }

    public void changeToCreditsCanvas()
    {
        MainMenuCanvas.SetActive(false);
        ball.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void backToMainMenu()
    {
        LevelSelectCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        ball.SetActive(true);
    }

    public void setMusicVolume(float vol)
    {
        mainMusic.volume = vol;
    }

    public void setSoundsVolume(float vol)
    {
        PlayerPrefs.SetFloat("Sounds", vol);
        soundTest.volume = vol;
        warning.volume = vol;
        UltraSecretCheat.volume = vol;
    }

    public void PlayAudioTest()
    {
        soundTest.volume = PlayerPrefs.GetFloat("Sounds");
        soundTest.Play();
    }

    public void AreYouSure()
    {
        warning.Play();
        resetDataCheck.SetActive(true);
    }

    public void CheckNo()
    {
        resetDataCheck.SetActive(false);
    }

    public void CheckYes()
    {
        PlayerPrefs.SetInt("Levels", 1);
        warning.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void activateCheat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        backToMainMenu();

        if(!PlayerPrefs.HasKey("Levels"))
        {
            PlayerPrefs.SetInt("Levels", 1);
            PlayerPrefs.SetFloat("Sounds", 0.5f);
        }

        mainMusic = GameObject.Find("Singleton").GetComponent<AudioSource>();

        musicSlider.value = mainMusic.volume;
        soundsSlider.value = PlayerPrefs.GetFloat("Sounds");
    }

    private void Update()
    {
        if (CreditsCanvas.activeInHierarchy)
        {
            if (Input.GetKeyDown(sequence[sequenceIndex]))
            {
                if (++sequenceIndex == sequence.Length)
                {
                    UltraSecretCheat.Play();
                    PlayerPrefs.SetInt("Levels", 15);
                    cheatCode.SetActive(true);
                    cheatCodeBackButton.SetActive(false);
                }
            }
            else if (Input.anyKeyDown) sequenceIndex = 0;
        }
    }
}
