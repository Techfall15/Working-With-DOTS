using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


public struct StarComponentData : IComponentData
{
    public bool isRotating;
    public bool randomizeSpawn;
    public bool randomizeSpeed;
    public bool randomizeScale;
    public bool positionSetupComplete;
    public bool rotationSetupComplete;
    public bool scaleSetupComplete;
    public bool colorSetupComplete;
    public Color spriteColor;
    public float rotationSpeed;
    public float customScale;
    public float2 spawnPos;
    public float2 xPosMinMax;
    public float2 yPosMinMax;
}