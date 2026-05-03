using System;

public static class GameEvents
{
    // Событие сбора предмета (передаёт текущее количество)
    public static event Action<int> OnPickUpCollected;
    public static void RaisePickUpCollected(int count) => OnPickUpCollected?.Invoke(count);

    // Событие победы
    public static event Action OnWin;
    public static void RaiseWin() => OnWin?.Invoke();

    // Событие поражения
    public static event Action OnLose;
    public static void RaiseLose() => OnLose?.Invoke();

    // Событие перезапуска
    public static event Action OnRestart;
    public static void RaiseRestart() => OnRestart?.Invoke();

    // Состояние музыки (true = включена)
    public static event Action<bool> OnMusicToggled;
    public static void RaiseMusicToggled(bool isOn) => OnMusicToggled?.Invoke(isOn);

    // Состояние звуков (true = включены)
    public static event Action<bool> OnSoundToggled;
    public static void RaiseSoundToggled(bool isOn) => OnSoundToggled?.Invoke(isOn);

    // В класс GameEvents добавьте:
    public static event Action<float> OnSensitivityChanged;
    public static void RaiseSensitivityChanged(float value) => OnSensitivityChanged?.Invoke(value);
}