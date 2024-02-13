using UnityEngine;
using Unity.Entities;

public class SpawnNewEntityAuthoring : MonoBehaviour
{

    public GameObject   newObjectToSpawnAsEntity;
    public Vector3      spawnPosition;


    public class SpawnNewEntityBaker : Baker<SpawnNewEntityAuthoring> 
    {
        public override void Bake(SpawnNewEntityAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new SpawnNewEntityData
            {
                newEntityToSpawn = GetEntity(authoring.newObjectToSpawnAsEntity, TransformUsageFlags.Dynamic),
                spawnPosition = authoring.spawnPosition
            });
        }
    }
}