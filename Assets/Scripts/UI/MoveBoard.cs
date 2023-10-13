using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MoveBoard : MonoBehaviour
{
    [SerializeField] private TMP_Text _move;
    [SerializeField] private CircleRotator _circleRotator;
    [SerializeField] private Circle _circle;
    [SerializeField] private LevelSpawner _levelSpawner;

    private int _currentMoveCount;
    private Coroutine _waitCircleUnlockJob;

    public event Action MovesCompleted;

    public int MoveCount { get; private set; }

    private void OnEnable()
    {
        _circleRotator.MoveDone += OnMoveDone;
        _circle.ColorMismatchFound += OnColorMismatchFound;
        _levelSpawner.LevelSpawned += OnLevelSpawned;
    }

    private void OnDisable()
    {
        _circleRotator.MoveDone -= OnMoveDone;
        _circle.ColorMismatchFound -= OnColorMismatchFound;
        _levelSpawner.LevelSpawned -= OnLevelSpawned;
    }

    public void TrySetCurrentMoveCount()
    {
        const int MinMoveCount = 0;

        if (MoveCount > MinMoveCount)
        {
            if (_circle.IsLocked() == false)
            {
                _circle.MakeForwardMove();
                _currentMoveCount--;
                MoveCount = _currentMoveCount;
                ShowMoveCount();
            }
            else
            {
                StopCoroutine(_waitCircleUnlockJob);
                _waitCircleUnlockJob = StartCoroutine(WaitForCircleUnlock());
            }
        }
    }

    private void OnMoveDone()
    {
        MoveCount--;
        ShowMoveCount();
    }

    private void OnColorMismatchFound()
    {
        const int MinMoveCount = 0;

        if (MoveCount == MinMoveCount)
        {
            MovesCompleted?.Invoke();
        }
    }

    private void OnLevelSpawned(LevelConfig levelConfig)
    {
        _currentMoveCount = levelConfig.MovesHolder.MoveCount;
        MoveCount = _currentMoveCount;
        ShowMoveCount();
    }

    private void ShowMoveCount()
    {
        const int MinMoveCount = 0;

        if (MoveCount < MinMoveCount)
        {
            MoveCount = MinMoveCount;
        }

        _move.text = MoveCount.ToString();
    }

    private IEnumerator WaitForCircleUnlock()
    {
        while (_circle.IsLocked())
        {
            yield return null;
        }

        TrySetCurrentMoveCount();
    }
}
