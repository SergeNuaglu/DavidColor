using UnityEngine;

public class BowlSpawner : CircleItemSpawner
{
    private MoveColorData _colorData;
    private Arrangement _arrangementData;

    public override void Spawn(LevelConfig currentLevelConfig)
    {
        _colorData = currentLevelConfig.BowlColorData;
        _arrangementData = currentLevelConfig.BowlArrangement;
        base.Spawn(currentLevelConfig);
    }

    protected override void TryInstantiateItem(GameObject template, int stepNumber)
    {
        if (_arrangementData.Data[stepNumber])
        {
            GameObject newItem = Instantiate(template, GetSpawnPosition(stepNumber, Circle.transform.position.y), Quaternion.identity, transform);

            if (newItem.TryGetComponent<Bowl>(out Bowl bowl))
            {
                bowl.Init(Circle);
                bowl.SetItemColor(_colorData.ItemColors[Counter]);
                Circle.AddBall(bowl);
            }

            Counter++;
        }
    }
}

