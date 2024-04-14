using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Random = Unity.Mathematics.Random;

public partial struct StarSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StarComponentData>();
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityManager eManager = state.EntityManager;
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        
        foreach((RefRW<StarComponentData> starData, RefRW<LocalTransform> myLocalTransform, Entity entity) in SystemAPI.Query<RefRW<StarComponentData>, RefRW<LocalTransform>>().WithEntityAccess())
        {
            
            if (starData.ValueRO.randomizeSpawn == true && starData.ValueRO.positionSetupComplete == false)
            {
                var xPos = starData.ValueRO.xPosMinMax;
                var yPos = starData.ValueRO.yPosMinMax;
                LocalTransform newTransform = myLocalTransform.ValueRO.WithPosition(UnityEngine.Random.Range(xPos.x, xPos.y), UnityEngine.Random.Range(yPos.x, yPos.y), myLocalTransform.ValueRO.Position.z);
                if(starData.ValueRO.randomizeScale == true && starData.ValueRO.scaleSetupComplete == false)
                {
                    eManager.SetComponentData<LocalTransform>(entity, new LocalTransform()
                    {
                        Position = newTransform.Position,
                        Rotation = newTransform.Rotation,
                        Scale = UnityEngine.Random.Range(0.05f, 1f)
                    });
                    starData.ValueRW.scaleSetupComplete = true;
                }
                else
                {
                    eManager.SetComponentData<LocalTransform>(entity, newTransform);
                    starData.ValueRW.scaleSetupComplete = true;
                }
                starData.ValueRW.positionSetupComplete = true;
            }
            if (starData.ValueRO.randomizeSpeed == true && starData.ValueRO.rotationSetupComplete == false)
            {
                RotationOverTimeComponentData rotationData = eManager.GetComponentData<RotationOverTimeComponentData>(entity);
                ecb.SetComponent<RotationOverTimeComponentData>(entity, new RotationOverTimeComponentData()
                {
                    minMax = rotationData.minMax,
                    randomizeSpeed = starData.ValueRO.randomizeSpeed,
                    rotationSpeed = UnityEngine.Random.Range(1f, 5f),
                    clockwiseRotation = (UnityEngine.Random.Range(1f,5f) > 2.5f) ? true : false,
                    randomizeDirection = true,
                    canChangeDirection = true,
                });
                starData.ValueRW.rotationSetupComplete = true;
            }
            if (eManager.HasComponent<SpriteColorEditComponentData>(entity) == true && starData.ValueRO.colorSetupComplete == false)
            {

                SpriteColorEditComponentData colorData = eManager.GetComponentData<SpriteColorEditComponentData>(entity);
                
                eManager.SetComponentData<SpriteColorEditComponentData>(entity, new SpriteColorEditComponentData()
                {
                    spriteColor = (colorData.randomizeColor == false) ?
                        colorData.spriteColor :
                        new Color(
                            GetRandomFloat(0, 1f),
                            GetRandomFloat(0, 1f),
                            GetRandomFloat(0, 1f),
                            GetRandomFloat(.15f, 1f)),
                    isColorSet = true,
                    randomizeColor = colorData.randomizeColor,
                });
                starData.ValueRW.spriteColor = colorData.spriteColor;
                starData.ValueRW.colorSetupComplete = true;    
            }
            
        }
        ecb.Playback(eManager);
        ecb.Dispose();
    }
    public float GetRandomFloat(float min, float max) => UnityEngine.Random.Range(min, max);
}
