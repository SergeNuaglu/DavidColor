using UnityEngine;

public class Bowl : CircleItem, IMovable
{
    private void Start()
    {
        TurnToCenter(GetAngleOnCircle());
    }

    public override void SetItemColor(ItemColor newColor)
    {
        base.SetItemColor(newColor);
    }

    public void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}