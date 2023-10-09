using UnityEngine;

public abstract class CircleItemSpawner : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GameObject _itemTemplate;
    [SerializeField] private Circle _circle;

    private float _stepSize;

    protected int Counter;

    public Circle Circle => _circle;
    public LevelConfig LevelConfig => _levelConfig;

    protected virtual void Awake()
    {
        _stepSize = (_circle.ArcAngle * Mathf.Deg2Rad) / _levelConfig.CircleStepCount;
        Circle.SetStepSize(_stepSize);
        Spawn();
    }

    protected Vector3 GetSpawnPosition(int circleStepNumber, float positionY = 0, float radiusOffset = 0)
    {
        float positionX = (_circle.Radius + radiusOffset) * Mathf.Sin(_stepSize * circleStepNumber);
        float positionZ = (_circle.Radius + radiusOffset) * Mathf.Cos(_stepSize * circleStepNumber);
        return new Vector3(positionX + _circle.transform.position.x, positionY, positionZ + _circle.transform.position.z);
    }

    protected void Spawn()
    {
        for (int i = 0; i < _levelConfig.CircleStepCount; i++)
        {
            TryInstantiateItem(_itemTemplate, i);
        }
    }

    protected abstract void TryInstantiateItem(GameObject template, int stepNumber);
}
