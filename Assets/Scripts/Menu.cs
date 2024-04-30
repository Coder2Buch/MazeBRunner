using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject selectedButtonSettings;
    public GameObject selectedButtonMenu;
    public GameObject cupPanel;
    public GameObject selectedButtonCup;
    public GameObject selectedButtonCredits;
    public GameObject creditsPanel;
    public GameObject dataObj;
    public GameObject controls;
    public GameObject selectedButtonControls;
    public Sprite[] numbers;
    public Image roundCountImage;
    public Dropdown roundDropDown;
    public Animator sceneAnimator;
    public Slider audioSlider;
    public AudioMixer audioMixer;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    Data data;
    int roundCount = 2;

    EventSystem m_EventSystem;

    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;
        data = dataObj.GetComponent<Data>();
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        audioSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        sceneAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        m_EventSystem.SetSelectedGameObject(selectedButtonCredits);
    }
    public void Settings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        m_EventSystem.SetSelectedGameObject(selectedButtonSettings);
    }

    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        cupPanel.SetActive(false);
        creditsPanel.SetActive(false);
        m_EventSystem.SetSelectedGameObject(selectedButtonMenu);
        controls.SetActive(false);

    }
    public void BackToOptions()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        cupPanel.SetActive(false);
        m_EventSystem.SetSelectedGameObject(selectedButtonSettings);
        controls.SetActive(false);
    }

    void OnCancel()
    {
        BackToMenu();
    }
    public void CupMode()
    {
        menuPanel.SetActive(false);
        cupPanel.SetActive(true);
        m_EventSystem.SetSelectedGameObject(selectedButtonCup);
    }
    public void StartCup()
    {
        data.cupMode = true;
        data.rounds = roundCount;
        StartCoroutine(LoadScene());

    }
    public void OnControls()
    {
        controls.SetActive(true);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        m_EventSystem.SetSelectedGameObject(selectedButtonControls);
    }

    public void ValueChangeCheck()
    {
        audioMixer.SetFloat("volume", audioSlider.value);
    }

    public void OnFullscreenToggle()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }


    public void OnClickRoundsUp()
    {
        roundCount++;
        if ( roundCount > 9 )
            roundCount = 9;
        int spriteNumber = roundCount - 2;
       
        switch ( spriteNumber )
        {
            case 1:
                roundCountImage.sprite = numbers [ 1 ];
                break;
            case 2:
                roundCountImage.sprite = numbers [ 2 ];
                break;
            case 3:
                roundCountImage.sprite = numbers [ 3 ];
                break;
            case 4:
                roundCountImage.sprite = numbers [ 4 ];
                break;
            case 5:
                roundCountImage.sprite = numbers [ 5 ];
                break;
            case 6:
                roundCountImage.sprite = numbers [ 6 ];
                break;
            case 7:
                roundCountImage.sprite = numbers [ 7 ];
                break;

            default:
                roundCountImage.sprite = numbers [ 0 ];
                break;
        }
    }
    
    public void OnClickRoundsDown ()
    {
        roundCount--;
        if ( roundCount < 2 )
            roundCount = 2;

        int spriteNumber = roundCount - 2;
        switch ( spriteNumber )
        {
            case 1:
                roundCountImage.sprite = numbers [ 1 ];
                break;
            case 2:
                roundCountImage.sprite = numbers [ 2 ];
                break;
            case 3:
                roundCountImage.sprite = numbers [ 3 ];
                break;
            case 4:
                roundCountImage.sprite = numbers [ 4 ];
                break;
            case 5:
                roundCountImage.sprite = numbers [ 5 ];
                break;
            case 6:
                roundCountImage.sprite = numbers [ 6 ];
                break;
            case 7:
                roundCountImage.sprite = numbers [ 7 ];
                break;

            default:
                roundCountImage.sprite = numbers [ 0 ];
                break;
        }
    }

}
    


