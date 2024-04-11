using Unity.Entities;

public struct RotationOverTimeComponentData : IComponentData
{
    public float rotationSpeed;
    public bool clockwiseRotation;
}