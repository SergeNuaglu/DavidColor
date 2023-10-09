using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]

public class David : MonoBehaviour, IColoredItem
{
    private Renderer _renderer;

    public event Action <ItemColor> ColorChanged;

    public ItemColor CurrentColor { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetItemColor(ItemColor newColor)
    {
        CurrentColor = newColor;
        _renderer.material.color = CurrentColor.MainColor;

        ColorChanged?.Invoke(newColor);
    }
}
