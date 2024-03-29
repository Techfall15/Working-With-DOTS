using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

public partial struct BeamDespawnSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeamData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        IncreaseBeamCountJob increaseBeamCountJob = new IncreaseBeamCountJob();
        foreach(var(localTransform, beamData, entity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<BeamData>>().WithEntityAccess())
        {
            if(localTransform.ValueRO.Position.x > beamData.ValueRO.xLimit)
            {
                ecb.AddComponent<OffScreenDestroyTag>(entity);
                increaseBeamCountJob.Schedule(state.Dependency).Complete();
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }


}

public partial struct IncreaseBeamCountJob : IJobEntity
{
    public void Execute(ref BeamCounterData counterData)
    {
        counterData.beamCount++;
        Debug.Log("Number of beams destroyed: " + counterData.beamCount);
    }
}