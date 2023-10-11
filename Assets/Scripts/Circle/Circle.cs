using System;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _arcAngle;
    [SerializeField] private int _reward = 70;
    [SerializeField] private MovesHandler _moveHandler;

    private const float TotalAngle = 360f;

    private List<Bowl> _bowls = new List<Bowl>();
    private List<Platform> _platforms = new List<Platform>();

    public float StepSize { get; private set; }
    public float Radius => _radius;
    public float ArcAngle => _arcAngle;

    public event Action AllColorsMatched;
    public event Action ColorMismatchFound;
    public event Action ForwardMoveMade;

    public IReadOnlyList<Platform> Platforms => _platforms;
    public IReadOnlyList<Bowl> Bowls => _bowls;

    public void AddBall(Bowl newBawl)
    {
        _bowls.Add(newBawl);
        _moveHandler.AddColoredItem(newBawl);
        _moveHandler.AddMovableItem(newBawl);
    }

    public void AddPlatform(Platform newPlatform)
    {
        _platforms.Add(newPlatform);
        newPlatform.SetAngleOnCircle();
        _moveHandler.AddColoredItem(newPlatform.David);
        _moveHandler.AddFreezableItem(newPlatform.David);
    }

    public void SetStepSize(float stepSize)
    {
        StepSize = stepSize;
    }

    public void MakeForwardMove()
    {
        _moveHandler.MakeMove(StepSize, Radius, transform.position.y);
        TryAllowHitBowls();
        ForwardMoveMade?.Invoke();
    }

    public bool IsLocked()
    {
        if (_platforms.Count == 0)
        {
            return false;
        }

        foreach (var platform in _platforms)
        {
            if (platform.David.IsAnimationPlaying(AnimatorDavidController.States.Hit))
                return true;
        }

        if (_moveHandler.CheckColorsMatch(Platforms))
        {
            AllColorsMatched?.Invoke();
            return true;
        }
        else
        {
            ColorMismatchFound?.Invoke();
        }

        return false;
    }

    public float GetAngleToFixedPosition()
    {
        float smallerAngle = 360;

        foreach (var platform in _platforms)
        {
            foreach (var bowl in _bowls)
            {
                var angle = GetAngleBetweenBowlAndPlatform(bowl, platform);

                if (Mathf.Abs(angle) < Mathf.Abs(smallerAngle))
                {
                    smallerAngle = angle;
                }
            }
        }

        return smallerAngle;
    }

    public float GetAngleBetweenBowlAndPlatform(Bowl bowl, Platform platform)
    {
        Vector3 bowlPositionOnCircle = bowl.GetPositionOnCircle();
        Vector3 platformPositionOnCircle = platform.GetPositionOnCircle();

        return Vector3.SignedAngle(bowlPositionOnCircle.normalized, platformPositionOnCircle.normalized, Vector3.up);
    }

    public void TryAllowHitBowls()
    {
        const float Offset = 10;
        const float MinValue = 0f;

        foreach (var platform in _platforms)
        {
            foreach (var bowl in _bowls)
            {
                if (platform.David.IsFreezed)
                {
                    platform.David.Unfreeze();
                    break;
                }

                float bowlAngle = bowl.GetAngleOnCircle();

                if (bowlAngle >= TotalAngle - Offset && bowlAngle <= TotalAngle + Offset)
                {
                    bowlAngle = MinValue;
                }

                if (bowlAngle >= platform.AngleOnCircle - Offset && bowlAngle <= platform.AngleOnCircle + Offset)
                {
                    if (bowl.CurrentColor.CanPaint)
                    {
                        platform.David.HitBowl();
                        break;
                    }
                }
            }
        }
    }
}

