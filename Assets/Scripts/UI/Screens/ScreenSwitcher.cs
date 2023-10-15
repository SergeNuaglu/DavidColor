using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
    [SerializeField] private HomeScreen _homeScreen;
    [SerializeField] private PlayScreen _playScreen;
    [SerializeField] private WinScreen _winScreen;

    private Screen _currentScreen;

    private void OnEnable()
    {
        _homeScreen.CloseButtonClicked += OnCloseButtonClicked;
        _playScreen.HomeButtonClicked += OnHomeButtonClicked;
    }

    private void Start()
    {
        _currentScreen = _playScreen;
    }

    private void OnDisable()
    {
        _homeScreen.CloseButtonClicked -= OnCloseButtonClicked;
        _playScreen.HomeButtonClicked -= OnHomeButtonClicked;
    }

    private void OnGameEnded(GameResult gameResult)
    {
        if (gameResult == GameResult.Win)
        {
            SetWinScreen();
        }

        _playScreen.Close();
    }

    private void OnHomeButtonClicked()
    {
        SetHomeScreen();
    }

    private void OnCloseButtonClicked()
    {
        SetPlayScreen();
    }

    private void SetPlayScreen()
    {
        SetScreen(_playScreen);
    }

    private void SetHomeScreen()
    {
        SetScreen(_homeScreen);
    }

    private void SetWinScreen()
    {
        SetScreen(_winScreen);
    }

    private void SetScreen(Screen newScreen)
    {
        if (_currentScreen != newScreen)
        {
            SwitchCurrentScreen(newScreen);
            _currentScreen.Open();
        }
    }

    private void SwitchCurrentScreen(Screen newScreen)
    {
        if (_currentScreen != null)
        {
            _currentScreen.Close();
        }

        _currentScreen = newScreen;
    }
}