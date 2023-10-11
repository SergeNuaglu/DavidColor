using UnityEngine;

public abstract class CircleItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _itemTemplate;
    [SerializeField] private Circle _circle;

    private float _stepSize;

    protected int Counter;

    public Circle Circle => _circle;

    public virtual void Spawn(LevelConfig currentLevelConfig)
    {
        _stepSize = (_circle.ArcAngle * Mathf.Deg2Rad) / currentLevelConfig.CircleStepCount;
        Circle.SetStepSize(_stepSize);

        for (int i = 0; i < currentLevelConfig.CircleStepCount; i++)
        {
            TryInstantiateItem(_itemTemplate, i);
        }
    }

    protected Vector3 GetSpawnPosition(int circleStepNumber, float positionY = 0, float radiusOffset = 0)
    {
        float positionX = (_circle.Radius + radiusOffset) * Mathf.Sin(_stepSize * circleStepNumber);
        float positionZ = (_circle.Radius + radiusOffset) * Mathf.Cos(_stepSize * circleStepNumber);
        return new Vector3(positionX + _circle.transform.position.x, positionY, positionZ + _circle.transform.position.z);
    }

    protected abstract void TryInstantiateItem(GameObject template, int stepNumber);
}
