using UnityEngine;
using Unity.Entities;

public class BeamSpawnerAuthoring : MonoBehaviour
{

    public float        timeUntilSpawn;
    public float        timeBetweenSpawns;
    public GameObject   entityToSpawn;
    public Vector3      spawnPosition;
    

    public class BeamSpawnerBaker : Baker<BeamSpawnerAuthoring>
    {

        public override void Bake(BeamSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BeamSpawnerData
            {
                timeUntilSpawn = authoring.timeUntilSpawn,
                timeBetweenSpawns = authoring.timeBetweenSpawns,
                entityToSpawn = GetEntity(authoring.entityToSpawn, TransformUsageFlags.Dynamic),
                spawnPosition = authoring.spawnPosition,
            });
            
        }

    }

}
