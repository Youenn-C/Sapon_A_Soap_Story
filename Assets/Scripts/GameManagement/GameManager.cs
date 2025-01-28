using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("DontDestroyOnLoad")]
    [SerializeField] private GameObject[] _dontDestroyOnLoad;

    [SerializeField] private GameObject _cavasMainMenu;
    
    [Header("Fade System"), Space(5)]
    [SerializeField] private Image _fadeImage;
    [SerializeField] private Image _saponImage;
    [Space(5)]
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private Animator _saponFadeAnimator;
    
    [Header("Player"), Space(5)]
    public int _deathCount;
    public TMP_Text _deathCountText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Start()
    {
        foreach (var element in _dontDestroyOnLoad)
        {
            DontDestroyOnLoad(element); // Permet la conservation de tout les gameobjects lors des changemnt de niveau
        }
    }

    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            default:
                Debug.Log("Scene not found");
                break;
            case "Hub":
                SceneManager.LoadScene("S_Hub");
                break;
            case "Level_Kitchen":
                SceneManager.LoadScene("S_Level_Kitchen");
                break;
        }
    }

    public void StartGame()
    {
        _cavasMainMenu.SetActive(false);
        StartCoroutine(SwitchSceneFade());
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void FadeIn()
    {
        _fadeAnimator.SetTrigger("FadeIn");
        _saponFadeAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _fadeAnimator.SetTrigger("FadeOut");
        _saponFadeAnimator.SetTrigger("FadeOut");
    }

    public void ToggleEnable(bool value)
    {
        _fadeImage.enabled = value;
        _saponImage.enabled = value;
    }

    public IEnumerator FadeCooldown()
    {
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator SwitchSceneFade()
    {
        ToggleEnable(true);
        FadeIn();
        yield return new WaitForSeconds(1f);
        LoadScene("Hub");
        FadeOut();
        yield return new WaitForSeconds(1f);
        ToggleEnable(false);
    }
}
