using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("Панель паузы")]
    [SerializeField] private GameObject _pausePanel;

    [Header("Слайдер чувствительности")]
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private TextMeshProUGUI _sensitivityValueText;

    [Header("Кнопки")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resumeButton;

    private bool _isPaused = false;
    private Keyboard _keyboard;

    void Start()
    {
        _keyboard = Keyboard.current;

        _pausePanel.SetActive(false);

        _resumeButton.onClick.AddListener(ResumeGame);
        _restartButton.onClick.AddListener(RestartLevel);
        _mainMenuButton.onClick.AddListener(GoToMainMenu);

        _sensitivitySlider.minValue = 0.01f;
        _sensitivitySlider.maxValue = 4f;
        _sensitivitySlider.value = 1f;
        _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        UpdateSensitivityText(3f);
    }

    void Update()
    {
        // Прямое чтение клавиши Escape через новую систему ввода
        if (_keyboard != null && _keyboard.escapeKey.wasPressedThisFrame)
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        _isPaused = true;
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        _isPaused = false;
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void OnSensitivityChanged(float value)
    {
        UpdateSensitivityText(value);
        GameEvents.RaiseSensitivityChanged(value);
    }

    void UpdateSensitivityText(float value)
    {
        _sensitivityValueText.text = value.ToString("F1");
    }
}