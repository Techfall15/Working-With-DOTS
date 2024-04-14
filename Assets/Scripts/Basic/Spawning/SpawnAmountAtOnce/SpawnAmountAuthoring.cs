using UnityEngine;
using Unity.Entities;


public class SpawnAmountAuthoring : MonoBehaviour
{

    public int amountToSpawn = 50;
    public GameObject prefabToSpawn;
    public bool doneSpawning = false;
    private class SpawnAmountBaker : Baker<SpawnAmountAuthoring>
    {
        public override void Bake(SpawnAmountAuthoring authoring)
        {
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);
            AddComponent(entity, new SpawnAmountComponentData()
            {
                amountToSpawn = authoring.amountToSpawn,
                prefabToSpawn = GetEntity(authoring.prefabToSpawn, TransformUsageFlags.None),
                doneSpawning = authoring.doneSpawning
            });
        }
    }
}
