using Unity.Entities;
using Unity.Mathematics;

public struct InputData : IComponentData
{
    public float2 move;
    public bool damage;
    public bool shoot;
    public bool spawnMedal;
    public bool openChest;
    public bool goThoughDoor;
    public float2 mousePos;
}
