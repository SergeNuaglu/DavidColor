using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]
public class David : MonoBehaviour, IColoredItem, IFreezable
{
    [SerializeField] private Hammer _hammer;

    private Renderer _renderer;
    private Animator _animator;

    public event Action<ItemColor> ColorСhanged;

    public ItemColor CurrentColor { get; private set; }
    public bool IsFreezed { get; private set; }

    private void OnEnable()
    {
        _hammer.BowlHit += OnBowlHit;
        _hammer.BowlIsFreezing += OnBowlFreezing;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _hammer.BowlHit -= OnBowlHit;
        _hammer.BowlIsFreezing -= OnBowlFreezing;
    }

    public bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName(animationName);
    }

    public void SetItemColor(ItemColor newColor)
    {
        CurrentColor = newColor;
        _renderer.material.color = CurrentColor.MainColor;
        ColorСhanged?.Invoke(newColor);
    }

    public void HitBowl()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.Hit);
    }

    public void Unfreeze(bool isMadeMoveForAd = false)
    {
        if (isMadeMoveForAd)
        {
            _animator.Play(AnimatorDavidController.States.Idle);
        }
        else
        {
            _animator.SetTrigger(AnimatorDavidController.Params.FreezeOff);
        }

        IsFreezed = false;
    }

    public void Freeze()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.FreezeOn);
        IsFreezed = true;
    }

    public void CelebrateVictory()
    {
        _animator.Play(AnimatorDavidController.States.Victory);
    }

    private void OnBowlHit(IColoredItem colorItem)
    {
        ExchangeColors(colorItem);
    }

    private void OnBowlFreezing()
    {
        Freeze();
    }

    private void ExchangeColors(IColoredItem colorItem)
    {
        ItemColor tempColor = CurrentColor;
        SetItemColor(colorItem.CurrentColor);
        colorItem.SetItemColor(tempColor);
    }
}
