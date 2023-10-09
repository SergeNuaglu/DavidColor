using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveData", menuName = "LevelData/MoveColorData", order = 51)]

public class MoveColorData : ScriptableObject
{
    [SerializeField] private ItemColor[] _itemColors;

    public IReadOnlyList<ItemColor> ItemColors => _itemColors;

    private void OnValidate()
    {
        foreach (var color in ItemColors)
        {
            if (color == null)
            {
                throw new UnassignedReferenceException("ItemColor");
            }
        }
    }

    public void SetLenght(int lenght)
    {
        _itemColors = new ItemColor[lenght];
    }
}
