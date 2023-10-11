using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public event Action<IColoredItem> BowlHit;
    public event Action BowlIsFreezing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bowl>(out Bowl bowl))
        {
            if (bowl.CurrentColor.CanFreeze)
            {
                BowlIsFreezing?.Invoke();
            }
            else if (bowl.CurrentColor.CanPaint)
            {
                BowlHit?.Invoke(bowl);
            }
        }
    }
}
