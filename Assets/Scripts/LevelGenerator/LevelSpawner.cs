using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private List<LevelConfig> _levels;
    [SerializeField] private EnvironmentSetter _environmentSetter;
    [SerializeField] private BowlSpawner _bowlSpawner;
    [SerializeField] private PlatformSpawner _platformSpawner;

    private void Awake()
    {
        foreach (var level in _levels)
        {
            if (level.LevelNumber == _levelNumber)
            {
                _environmentSetter.Set(level);
            }
        }

        _bowlSpawner.Spawn();
        _platformSpawner.Spawn();
    }
}
