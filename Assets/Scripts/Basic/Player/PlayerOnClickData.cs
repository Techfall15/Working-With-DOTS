using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
public partial struct PlayerOnClickData : IComponentData
{

    public bool hasBeenClickedOn;

    public float xSize;
    public float ySize;

    public float2 spawnPos;
    public Entity entityToSpawn;

}
