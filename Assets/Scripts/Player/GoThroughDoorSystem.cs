using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;


public partial struct GoThroughDoorSystem : ISystem
{


    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InputData>();
    }

    public void OnUpdate(ref SystemState state)
    {

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        // For each source of input information
        foreach(var inputData in SystemAPI.Query<RefRO<InputData>>())
        {
            // Check if the player presses the 'goThroughDoor' button
            if (inputData.ValueRO.goThoughDoor == false) continue;

            foreach(var woodenDoorData in SystemAPI.Query<RefRW<WoodenDoorData>>())
            {
                if(woodenDoorData.ValueRO.isOpen == true)
                {
                    foreach(var(localTransform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithEntityAccess().WithAll<PlayerTagComponent>())
                    {
                        float3 newPlayerPosition        = woodenDoorData.ValueRO.playerSpawnLocation;
                        LocalTransform newTransform = new LocalTransform()
                        {
                            Position = newPlayerPosition,
                            Rotation = Quaternion.identity,
                            Scale = 1f
                        };
                        
                        ecb.SetComponent<LocalTransform>(entity, newTransform);
                    }
                    foreach(var(fadeBoxData, localTransform, entity) in SystemAPI.Query<RefRW<FadeBoxData>, RefRO<LocalTransform>>().WithEntityAccess())
                    {
                        if(fadeBoxData.ValueRO.fadeCount < 2) fadeBoxData.ValueRW.isFading = true;
                        fadeBoxData.ValueRW.newPosition = woodenDoorData.ValueRO.cameraSpawnLocation;
                    }
                    
                }

            }
            foreach (var (testNPCData, npcTransform) in SystemAPI.Query<RefRW<TestNPCData>, RefRO<LocalTransform>>())
            {
                if (testNPCData.ValueRO.canSpawnQuestionMark == true && testNPCData.ValueRO.alreadySpawnedQuestionMark == false)
                {
                    Entity questionMark = ecb.Instantiate(testNPCData.ValueRO.questionMarkEntity);
                    LocalTransform questionMarkTransform = new LocalTransform()
                    {
                        Position = new float3(npcTransform.ValueRO.Position.x, npcTransform.ValueRO.Position.y + 1.25f, npcTransform.ValueRO.Position.z),
                        Rotation = Quaternion.identity,
                        Scale = 1f
                    };
                    testNPCData.ValueRW.alreadySpawnedQuestionMark = true;
                    ecb.SetComponent<LocalTransform>(questionMark, questionMarkTransform);
                }
            }
        }


        ecb.Playback(state.EntityManager);


    }

}