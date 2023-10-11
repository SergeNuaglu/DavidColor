using System.Collections.Generic;
using UnityEngine;

public class MovesHandler : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;

    private MovesHolder _movesHolder;
    private List<IColoredItem> _bowls = new List<IColoredItem>();
    private List<IColoredItem> _davids = new List<IColoredItem>();
    private List<IMovable> _movableItems = new List<IMovable>();
    private List<IFreezable> _freezableItems = new List<IFreezable>();
    private MoveColorData _colorData;

    private int _nextMove = 1;

    private void OnEnable()
    {
        _levelSpawner.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _levelSpawner.LevelChanged -= OnLevelChanged;
    }

    public void AddColoredItem(IColoredItem item)
    {
        if (item is Bowl)
        {
            _bowls.Add(item);
        }
        else if (item is David)
        {
            _davids.Add(item);
        }
    }

    public void AddMovableItem(IMovable item)
    {
        _movableItems.Add(item);
    }

    public void AddFreezableItem(IFreezable item)
    {
        _freezableItems.Add(item);
    }

    public void MakeMove(float step, float radius, float positionY)
    {
        IReadOnlyList<bool> nextIsFreezedCondition = _movesHolder.DavidIsFreezedConditions[_nextMove].Data;

        SetIsFreezedConditions(nextIsFreezedCondition);
        ChangeColors(_nextMove, _bowls, _movesHolder.BowlMoveColors);
        ChangeColors(_nextMove, _davids, _movesHolder.DavidMoveColors);

        IReadOnlyList<bool> nextBowlsArrangement = _movesHolder.BowlMoveArrangements[_nextMove].Data;
        int counter = 0;

        for (int i = 0; i < nextBowlsArrangement.Count; i++)
        {
            if (nextBowlsArrangement[i])
            {
                float positionX = (radius) * Mathf.Sin(step * i);
                float positionZ = (radius) * Mathf.Cos(step * i);
                Vector3 newPosition = new Vector3(positionX + transform.position.x, positionY, positionZ + transform.position.z);
                _movableItems[counter].Move(newPosition);
                counter++;
            }
        }

        _nextMove++;
    }

    public bool CheckColorsMatch(IReadOnlyList<Platform> platforms)
    {
        int matchCounter = 0;

        foreach (var platform in platforms)
        {
            if (platform.CheckColorMatch())
            {
                matchCounter++;
            }
        }

        if (matchCounter == platforms.Count)
        {
            return true;
        }

        return false;
    }

    private void ChangeColors(int nextMoveNumber, List<IColoredItem> coloredItems, IReadOnlyList<MoveColorData> moveColorData)
    {
        _colorData = moveColorData[nextMoveNumber];

        for (int i = 0; i < coloredItems.Count; i++)
        {
            coloredItems[i].SetItemColor(_colorData.ItemColors[i]);
        }
    }

    private void SetIsFreezedConditions(IReadOnlyList<bool> nextIsFreezedCondition)
    {
        for (int i = 0; i < _freezableItems.Count; i++)
        {
            if (nextIsFreezedCondition[i])
            {
                if (_freezableItems[i].IsFreezed == false)
                {
                    _freezableItems[i].Freeze();
                }
            }
            else
            {
                _freezableItems[i].Unfreeze(true);
            }
        }
    }

    private void OnLevelChanged(MovesHolder movesHolder)
    {
        _movesHolder = movesHolder;
    }
}
