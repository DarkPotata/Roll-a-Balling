using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Кнопки уровней")]
    [SerializeField] private Button _level1Button;
    [SerializeField] private Button _level2Button;

    [Header("Кнопки звука")]
    [SerializeField] private Button _musicToggleButton;
    [SerializeField] private Button _soundToggleButton;

    [Header("Спрайты для музыки")]
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;

    [Header("Спрайты для звуков")]
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    // Текущие состояния
    private bool _isMusicOn = true;
    private bool _isSoundOn = true;

    void Start()
    {
        // Привязываем кнопки уровней
        _level1Button.onClick.AddListener(() => LoadLevel("Level1"));
        _level2Button.onClick.AddListener(() => LoadLevel("Level2"));

        // Привязываем кнопки звука
        _musicToggleButton.onClick.AddListener(ToggleMusic);
        _soundToggleButton.onClick.AddListener(ToggleSound);

        // Устанавливаем начальные спрайты
        UpdateMusicSprite();
        UpdateSoundSprite();
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // === ЗАГЛУШКИ ДЛЯ ЗВУКА ===

    void ToggleMusic()
    {
        _isMusicOn = !_isMusicOn;
        UpdateMusicSprite();
        GameEvents.RaiseMusicToggled(_isMusicOn);

        // ЗАГЛУШКА: потом здесь будет вызов MusicManager.ToggleMusic()
        Debug.Log("Музыка: " + (_isMusicOn ? "включена" : "выключена"));
    }

    void ToggleSound()
    {
        _isSoundOn = !_isSoundOn;
        UpdateSoundSprite();
        GameEvents.RaiseSoundToggled(_isSoundOn);

        // ЗАГЛУШКА: потом здесь будет вызов SoundManager.ToggleSound()
        Debug.Log("Звуки: " + (_isSoundOn ? "включены" : "выключены"));
    }

    // === СМЕНА СПРАЙТОВ ===

    void UpdateMusicSprite()
    {
        _musicToggleButton.image.sprite = _isMusicOn ? _musicOnSprite : _musicOffSprite;
    }

    void UpdateSoundSprite()
    {
        _soundToggleButton.image.sprite = _isSoundOn ? _soundOnSprite : _soundOffSprite;
    }
}