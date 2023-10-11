using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelData/LevelConfig", order = 51)]

public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private Material _environmentMaterial;
    [SerializeField] private GameObject _building;
    [SerializeField] private int _circleStepCount;
    [SerializeField] private int _platformCount;
    [SerializeField] private int _bowlCount;
    [SerializeField] private Arrangement _platformArrangement;
    [SerializeField] private Arrangement _secretPlatformArrangement;
    [SerializeField] private MoveColorData _platformColorData;
    [SerializeField] private MovesHolder _movesHolder;

    public int LevelNumber => _levelNumber;
    public Material EnvironmentMaterial => _environmentMaterial;
    public GameObject Building => _building;
    public MovesHolder MovesHolder => _movesHolder;
    public Arrangement PlatformArrangement => _platformArrangement;
    public Arrangement BowlArrangement => _movesHolder.BowlMoveArrangements[0];
    public Arrangement SecretPlatformArrangement => _secretPlatformArrangement;
    public MoveColorData PlatformColorData => _platformColorData;
    public MoveColorData BowlColorData => _movesHolder.BowlMoveColors[0];
    public MoveColorData DavidColorData => _movesHolder.DavidMoveColors[0];
    public int CircleStepCount => _circleStepCount;
    public int PlatformCount => _platformCount;
    public int BowlCount => _bowlCount;

    private void OnValidate()
    {
        TryCorrectLength(ref _platformCount, _circleStepCount);
        TryCorrectLength(ref _bowlCount, _circleStepCount);
        TryCorrectLength(_platformColorData, _platformCount);
        TryCorrectLength(_secretPlatformArrangement, _platformCount);
        TryCorrectLength(_platformArrangement, _circleStepCount);

        foreach (var oneMoveColor in _movesHolder.BowlMoveColors)
        {
            TryCorrectLength(oneMoveColor, _bowlCount);
        }

        foreach (var oneMoveColor in _movesHolder.DavidMoveColors)
        {
            TryCorrectLength(oneMoveColor, _platformCount);
        }

        foreach (var oneMoveArrangement in _movesHolder.BowlMoveArrangements)
        {
            TryCorrectLength(oneMoveArrangement, _circleStepCount);
        }

        foreach (var isFreezedCondition in _movesHolder.DavidIsFreezedConditions)
        {
            TryCorrectLength(isFreezedCondition, _platformCount);
        }
    }

    private void TryCorrectLength(MoveColorData colorData, int length)
    {
        if (colorData.ItemColors.Count != length)
        {
            colorData.SetLenght(length);
        }
    }

    private void TryCorrectLength(Arrangement arrangement, int length)
    {
        if (arrangement.Data.Count != length)
        {
            arrangement.SetLenght(length);
        }
    }

    private void TryCorrectLength(ref int value, int length)
    {
        if (value > length)
        {
            value = length;
        }
    }
}
