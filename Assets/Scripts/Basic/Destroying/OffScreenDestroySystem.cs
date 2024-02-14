using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public partial struct OffScreenDestroySystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<OffScreenDestroyTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        DestroyOffScreenEntityJob destroyOffScreenEntityJob = new DestroyOffScreenEntityJob()
        {
            ecb = ecb,
        };
        destroyOffScreenEntityJob.Schedule(state.Dependency).Complete();
        ecb.Playback(state.EntityManager);

        
        
    }



}

public partial struct DestroyOffScreenEntityJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(OffScreenDestroyTag offScreenDestroyTag, Entity entity) => ecb.DestroyEntity(entity);
}