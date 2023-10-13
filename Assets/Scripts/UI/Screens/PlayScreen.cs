using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreen : Screen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _hintButton;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private MoveBoard _moveBoard;
    [SerializeField] private LevelSpawner _levelSpawner;

    public event Action HomeButtonClicked;
    public event Action RestartButtonClicked;
    public event Action StepForwardButtonClicked;
    public event Action Opened;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(() => OnButtonClicked(HomeButtonClicked));
        _restartButton.onClick.AddListener(() => OnButtonClicked(RestartButtonClicked));
        _hintButton.onClick.AddListener(OnHintButtonClicked);
        _levelSpawner.LevelSpawned += OnLevelSpawned;
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _hintButton.onClick.RemoveListener(OnHintButtonClicked);
        _levelSpawner.LevelSpawned -= OnLevelSpawned;
    }

    public override void Open()
    {
        base.Open();
        Opened?.Invoke();
        Time.timeScale = 1.0f;
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 0f;
    }

    protected void OnHintButtonClicked()
    {
        _moveBoard.TrySetCurrentMoveCount();
    }

    private void OnLevelSpawned(LevelConfig levelConfig)
    {
        _levelNumber.text += levelConfig.LevelNumber;
    }
}
