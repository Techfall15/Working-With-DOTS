using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct WoodenDoorAspect : IAspect
{
    public readonly RefRW<WoodenDoorData> woodenDoorData;
    public readonly RefRO<LocalTransform> localTransform;


    public void SetIsOpenTo(bool newState) => woodenDoorData.ValueRW.isOpen = newState;
    public void ResetDoorData(EntityCommandBuffer ecb)
    {
        if (woodenDoorData.ValueRO.isOpen == true)
        {
            woodenDoorData.ValueRW.isOpen = false;
            woodenDoorData.ValueRW.hasOpenDoorBeenSpawned = false;
            ecb.Instantiate(woodenDoorData.ValueRO.closeDoorEntityAudioSource);
        }
    }
    public void SpawnAndSetNewOpenDoor(EntityCommandBuffer ecb)
    {
        if (woodenDoorData.ValueRO.isOpen == true && woodenDoorData.ValueRO.hasOpenDoorBeenSpawned == false)
        {
            // Spawn open door entity and set transform next to the door
            Entity newOpenDoorEntity = ecb.Instantiate(woodenDoorData.ValueRO.openDoorEntity);
            float3 doorPos = localTransform.ValueRO.Position;

            LocalTransform openDoorTransform = new LocalTransform()
            {
                Position = new float3(doorPos.x - 0.9f, doorPos.y, doorPos.z),
                Rotation = Quaternion.identity,
                Scale = 1f
            };
            // Change 'hasOpenDoorBeenSpawned' to true, so only 1 gets spawned
            woodenDoorData.ValueRW.hasOpenDoorBeenSpawned = true;
            ecb.Instantiate(woodenDoorData.ValueRO.openDoorEntityAudioSource);
            // Set the spawn position of the open door entity
            ecb.SetComponent<LocalTransform>(newOpenDoorEntity, openDoorTransform);
        }
    }
}