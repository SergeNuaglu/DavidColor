using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;

    public event Action LevelButtonClicked;
    public event Action ShopButtonClicked;
    public event Action SoundButtonClicked;
    public event Action LeaderboardButtonClicked;
    public event Action CloseButtonClicked;

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(() => OnButtonClicked(LevelButtonClicked));
        _shopButton.onClick.AddListener(() => OnButtonClicked(ShopButtonClicked));
        _soundButton.onClick.AddListener(() => OnButtonClicked(SoundButtonClicked));
        _leaderboardButton.onClick.AddListener(() => OnButtonClicked(LeaderboardButtonClicked));
        _closeButton.onClick.AddListener(() => OnButtonClicked(CloseButtonClicked));
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveAllListeners();
        _shopButton.onClick.RemoveAllListeners();
        _soundButton.onClick.RemoveAllListeners();
        _leaderboardButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }

}
