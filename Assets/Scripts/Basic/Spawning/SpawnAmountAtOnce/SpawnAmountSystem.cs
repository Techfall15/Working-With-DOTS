using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.VisualScripting;
public partial struct SpawnAmountSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnAmountComponentData>();
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        foreach(RefRW<SpawnAmountComponentData> spawnComponentData 
            in SystemAPI.Query<RefRW<SpawnAmountComponentData>>())
        {

            if (spawnComponentData.ValueRO.doneSpawning == true) continue;
            for(int i = 0; i < spawnComponentData.ValueRO.amountToSpawn; i++)
            {
                var newSpawn = entityManager.Instantiate(spawnComponentData.ValueRO.prefabToSpawn);

                
                /*float3 newPos   = new float3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-7f, 7f), 0f);
                LocalTransform newTransform = new LocalTransform()
                {
                    Position = newPos,
                    Rotation = Quaternion.identity,
                    Scale    = 1f
                };
                entityManager.SetComponentData<LocalTransform>(newSpawn, newTransform);
                if (entityManager.HasComponent<RotationOverTimeComponentData>(newSpawn) == true)
                {
                    RotationOverTimeComponentData componentData = entityManager.GetComponentData<RotationOverTimeComponentData>(newSpawn);
                    SetNewRotationData(newSpawn, componentData, entityManager);
                }
                */
                
                
            }
            spawnComponentData.ValueRW.doneSpawning = true;
        }
    }
    public void SetNewRotationData(Entity newSpawn, RotationOverTimeComponentData componentData, EntityManager entityManager)
    {
        entityManager.SetComponentData<RotationOverTimeComponentData>(newSpawn, new RotationOverTimeComponentData()
        {
            rotationSpeed = (componentData.randomizeSpeed == true) ?
                            UnityEngine.Random.Range(componentData.minMax.x, componentData.minMax.y) :
                            componentData.rotationSpeed,
            clockwiseRotation = (componentData.randomizeDirection == true) ?
                            (UnityEngine.Random.Range(0, 1f) > 0.5f) ? true : false :
                            componentData.clockwiseRotation,
        });
    }
    public float GetRandom(float min, float max) => UnityEngine.Random.Range(min, max);

}
