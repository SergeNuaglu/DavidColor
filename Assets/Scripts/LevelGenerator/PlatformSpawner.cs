using UnityEngine;

public class PlatformSpawner : CircleItemSpawner
{
    [SerializeField] private float _bawlDistance;
    [SerializeField] private float _positionY;

    private MoveColorData _platformColorData;
    private MoveColorData _davidColorData;
    private Arrangement _platformArrangementData;

    public override void Spawn()
    {
        _platformColorData = LevelConfig.PlatformColorData;
        _davidColorData = LevelConfig.DavidColorData;
        _platformArrangementData = LevelConfig.PlatformArrangement;
        base.Spawn();
    }

    protected override void TryInstantiateItem(GameObject template, int stepNumber)
    {
        Vector3 spawnPosition;

        if (_platformArrangementData.Data[stepNumber])
        {
            spawnPosition = GetSpawnPosition(stepNumber, _positionY, _bawlDistance);
            GameObject newItem = Instantiate(template, spawnPosition, Quaternion.identity, transform);

            if (newItem.TryGetComponent<Platform>(out Platform platform))
            {
                platform.Init(Circle);

                if (LevelConfig.SecretPlatformArrangement.Data[Counter])
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
