using UnityEngine;
using Unity.Entities;
public struct SpawnAmountComponentData : IComponentData
{
    public int amountToSpawn;
    public Entity prefabToSpawn;
    public bool doneSpawning;
}
