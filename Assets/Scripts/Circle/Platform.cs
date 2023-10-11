using UnityEngine;

public class Platform : CircleItem
{
    [SerializeField] private David _david;
    [SerializeField] private Texture _secretTexture;
    [SerializeField] private ItemColor _defaultColor;

    private ItemColor _secretColor;

    public David David => _david;
    public float AngleOnCircle { get; private set; }
    public bool IsSecret { get; private set; }

    private void OnEnable()
    {
        David.ColorСhanged += OnColorChanged;
    }

    private void OnDisable()
    {
        David.ColorСhanged -= OnColorChanged;
    }

    private void Start()
    {
        if (IsSecret)
        {
            SetTexture(_secretTexture);
            _secretColor = CurrentColor;
            SetItemColor(_defaultColor);
        }
        else
        {
            _secretColor = new ItemColor();
        }
    }

    public void BecameSecret()
    {
        IsSecret = true;
    }

    public void SetAngleOnCircle()
    {
        AngleOnCircle = GetAngleOnCircle();
        TurnToCenter(AngleOnCircle);
    }

    public bool CheckColorMatch()
    {
        if (David.CurrentColor == CurrentColor)
        {
            return true;
        }

        return false;
    }

    private void OnColorChanged(ItemColor color)
    {
        if (IsSecret && color == _secretColor)
        {
            SetTexture();
            SetItemColor(_secretColor);
            IsSecret = false;
        }
    }
}
