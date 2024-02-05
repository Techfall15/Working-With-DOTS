using Unity.Entities;
using Unity.Mathematics;

public struct InputData : IComponentData
{
    public float2 move;
    public bool damage;
}
