using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public struct RotationOverTimeComponentData : IComponentData
{
    public float    rotationSpeed;
    public bool     clockwiseRotation;
    public bool     randomizeDirection;
    public bool     randomizeSpeed;
    public bool     canChangeDirection;
    public float2   minMax;

}