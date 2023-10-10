public interface IColoredItem
{
    ItemColor CurrentColor { get; }

    void SetItemColor(ItemColor newColor);
}

