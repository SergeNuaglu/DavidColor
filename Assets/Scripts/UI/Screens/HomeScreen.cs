using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;

    public event Action SoundButtonClicked;
    public event Action LeaderboardButtonClicked;

    private void OnEnable()
    {
        _soundButton.onClick.AddListener(OnSoundButtonClick);
        _leaderboardButton.onClick.AddListener(() => OnButtonClicked(LeaderboardButtonClicked));
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveListener(OnSoundButtonClick);
        _leaderboardButton.onClick.RemoveAllListeners();
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }

    private void OnSoundButtonClick()
    {

    }
}
