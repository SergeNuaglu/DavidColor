using UnityEngine;

public class PlatformSpawner : CircleItemSpawner
{
    [SerializeField] private float _bawlDistance;
    [SerializeField] private float _positionY;

    private MoveColorData _platformColorData;
    private MoveColorData _davidColorData;
    private Arrangement _platformArrangementData;
    private LevelConfig _currentLevelConfig;

    public override void Spawn(LevelConfig currentLevelConfig)
    {
        _currentLevelConfig = currentLevelConfig;
        _platformColorData = currentLevelConfig.PlatformColorData;
        _davidColorData = currentLevelConfig.DavidColorData;
        _platformArrangementData = currentLevelConfig.PlatformArrangement;
        base.Spawn(currentLevelConfig);
    }

    protected override void TryInstantiateItem(GameObject template, int stepNumber)
    {
        Vector3 spawnPosition;
        if (stepNumber == 3)
        {

        }
        if (_platformArrangementData.Data[stepNumber])
        {
            spawnPosition = GetSpawnPosition(stepNumber, _positionY, _bawlDistance);
            GameObject newItem = Instantiate(template, spawnPosition, Quaternion.identity, transform);

            if (newItem.TryGetComponent<Platform>(out Platform platform))
            {
                platform.Init(Circle);

                if (_currentLevelConfig.SecretPlatformArrangement.Data[Counter])
                {
                    platform.BecameSecret();
                }

                platform.SetItemColor(_platformColorData.ItemColors[Counter]);
                platform.David.SetItemColor(_davidColorData.ItemColors[Counter]);
                Circle.AddPlatform(platform);

                if (_bawlDistance < 0)
                {
                    platform.David.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }
            }

            Counter++;
        }
    }
}
