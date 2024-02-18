using UnityEngine;
using Unity.Entities;
using Unity.Collections;
public partial struct TimeDelayDestroySystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TimeDelayDestroyData>();
    }


    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach(var(timeDelayData, entity) in SystemAPI.Query<RefRW<TimeDelayDestroyData>>().WithEntityAccess())
        {
            if(timeDelayData.ValueRO.timeUntilDestruction > 0)
            {
                timeDelayData.ValueRW.timeUntilDestruction -= SystemAPI.Time.DeltaTime;
            }
            else
            {
                ecb.DestroyEntity(entity);
            }
        }


        ecb.Playback(state.EntityManager);
    }

}