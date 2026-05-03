using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;   // <-- мнбне онке
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private TextMeshProUGUI _countText;

    private int _currentCount = 0;

    void OnEnable()
    {
        GameEvents.OnPickUpCollected += HandlePickUpCollected;
        GameEvents.OnWin += HandleWin;
        GameEvents.OnLose += HandleLose;

        _restartButton.onClick.AddListener(HandleRestartClick);

        if (_backToMenuButton != null)
            _backToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void OnDisable()
    {
        GameEvents.OnPickUpCollected -= HandlePickUpCollected;
        GameEvents.OnWin -= HandleWin;
        GameEvents.OnLose -= HandleLose;

        _restartButton.onClick.RemoveListener(HandleRestartClick);

        if (_backToMenuButton != null)
            _backToMenuButton.onClick.RemoveListener(GoToMainMenu);
    }

    void Start()
    {
        _winText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);

        if (_backToMenuButton != null)
            _backToMenuButton.gameObject.SetActive(false);

        UpdateCountDisplay(0);
    }

    void HandlePickUpCollected(int count)
    {
        _currentCount = count;
        UpdateCountDisplay(count);

        if (count >= 8)
        {
            GameEvents.RaiseWin();
        }
    }

    void HandleWin()
    {
        _winText.gameObject.SetActive(true);
        _winText.text = "You Win!";
        _restartButton.gameObject.SetActive(true);

        if (_backToMenuButton != null)
            _backToMenuButton.gameObject.SetActive(true);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null) Destroy(enemy);
    }

    void HandleLose()
    {
        _winText.gameObject.SetActive(true);
        _winText.text = "You Lose!";
        _restartButton.gameObject.SetActive(true);

        if (_backToMenuButton != null)
            _backToMenuButton.gameObject.SetActive(true);
    }

    void HandleRestartClick()
    {
        GameEvents.RaiseRestart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateCountDisplay(int count)
    {
        _countText.text = "Count: " + count.ToString();
    }
}