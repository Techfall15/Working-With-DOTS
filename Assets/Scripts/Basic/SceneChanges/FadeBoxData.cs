using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public partial struct FadeBoxData : IComponentData
{

    public float currentAlpha;
    public float fadeTime;
    public Color newColor;
    public bool isFading;
    public bool isFadingOut;
    public int fadeCount;
    public float3 newPosition;
}
