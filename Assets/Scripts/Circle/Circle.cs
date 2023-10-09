using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _arcAngle;

    private List<Bowl> _bowls = new List<Bowl>();

    public float Radius => _radius;
    public float ArcAngle => _arcAngle;
    public float StepSize { get; private set; }

    public void SetStepSize(float stepSize)
    {
        StepSize = stepSize;
    }

    public void AddBall(Bowl newBawl)
    {
        _bowls.Add(newBawl);
    }
}
