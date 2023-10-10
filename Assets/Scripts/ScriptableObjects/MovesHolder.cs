using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovesHolder", menuName = "LevelData/MovesHolder", order = 51)]

public class MovesHolder : ScriptableObject
{
    [SerializeField] private int _moveCount;
    [SerializeField] private MoveColorData[] _bowlMoveColors;
    [SerializeField] private MoveColorData[] _davidMoveColors;
    [SerializeField] private Arrangement[] _bowlMoveArrangements;
    [SerializeField] private Arrangement[] _davidIsFreezedConditions;

    public int MoveCount => _moveCount;

    public IReadOnlyList<MoveColorData> BowlMoveColors => _bowlMoveColors;
    public IReadOnlyList<MoveColorData> DavidMoveColors => _davidMoveColors;
    public IReadOnlyList<Arrangement> BowlMoveArrangements => _bowlMoveArrangements;
    public IReadOnlyList<Arrangement> DavidIsFreezedConditions => _davidIsFreezedConditions;

    private void OnValidate()
    {
        CheckArrayLength(_bowlMoveColors);
        CheckArrayLength(_davidMoveColors);
        CheckArrayLength(_bowlMoveArrangements);
    }

    private void CheckArrayLength(Array checkingArray)
    {
        int nullMoveCount = 1;

        if (checkingArray.Length != _moveCount + nullMoveCount)
        {
            checkingArray = new MoveColorData[_moveCount + nullMoveCount];
        }
    }
}


