using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private List<LevelConfig> _levelsConfigs;
    [SerializeField] private EnvironmentSetter _environmentSetter;
    [SerializeField] private BowlSpawner _bowlSpawner;
    [SerializeField] private PlatformSpawner _platformSpawner;

    public event Action<MovesHolder> LevelChanged;

    private void Awake()
    {
        foreach (var levelConfig in _levelsConfigs)
        {
            if (levelConfig.LevelNumber == _levelNumber)
            {
                _environmentSetter.Set(levelConfig);
                _bowlSpawner.Spawn(levelConfig);
                _platformSpawner.Spawn(levelConfig);
                LevelChanged?.Invoke(levelConfig.MovesHolder);
                return;
            }
        }
    }
}
