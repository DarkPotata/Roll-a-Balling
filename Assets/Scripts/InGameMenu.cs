using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button _backToMenuButton;

    void Start()
    {
        _backToMenuButton.onClick.AddListener(GoToMainMenu);

        //  нопка спр€тана до конца игры
        _backToMenuButton.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        GameEvents.OnWin += ShowButton;
        GameEvents.OnLose += ShowButton;
    }

    void OnDisable()
    {
        GameEvents.OnWin -= ShowButton;
        GameEvents.OnLose -= ShowButton;
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowButton()
    {
        _backToMenuButton.gameObject.SetActive(true);
    }
}