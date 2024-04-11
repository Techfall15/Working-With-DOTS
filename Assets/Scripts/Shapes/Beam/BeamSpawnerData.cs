using UnityEngine;
using Unity.Entities;
public partial struct BeamSpawnerData : IComponentData
{

    public float        timeUntilSpawn;
    public float        timeBetweenSpawns;
    public Entity       entityToSpawn;
    public Vector3      spawnPosition;

}
