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
        int nullMoveCount = 1;

        if (_bowlMoveColors.Length != _moveCount + nullMoveCount)
        {
            _bowlMoveColors = new MoveColorData[_moveCount + nullMoveCount];
        }

        if (_bowlMoveArrangements.Length != _moveCount + nullMoveCount)
        {
            _bowlMoveArrangements = new Arrangement[_moveCount + nullMoveCount];
        }
    }
}


