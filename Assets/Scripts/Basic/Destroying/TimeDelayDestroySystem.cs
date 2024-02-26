using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Burst;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct TimeDelayDestroySystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TimeDelayDestroyData>();
    }


    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        CountDownToDestroyJob countDown = new CountDownToDestroyJob()
        {
            ecb = ecb,
            deltaTime = SystemAPI.Time.DeltaTime
        };

        countDown.Schedule(state.Dependency).Complete();
        ecb.Playback(state.EntityManager);
    }

}
[BurstCompile]
public partial struct CountDownToDestroyJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public float deltaTime;
    public void Execute(ref TimeDelayDestroyData timeDestroyData, Entity entity)
    {
        if (timeDestroyData.timeUntilDestruction > 0) 
            timeDestroyData.timeUntilDestruction -= deltaTime;
        else 
            ecb.DestroyEntity(entity);
    }
}