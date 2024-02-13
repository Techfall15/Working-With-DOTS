using UnityEngine;
using Unity.Entities;


public partial struct SpawnNewEntityData : IComponentData
{

    public Entity newEntityToSpawn;
    public Vector3 spawnPosition;

}
