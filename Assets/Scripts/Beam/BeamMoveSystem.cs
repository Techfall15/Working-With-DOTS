using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct BeamMoveSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeamData>();
    }


    public void OnUpdate(ref SystemState state)
    {
        BeamMoveJob beamMoveJob = new BeamMoveJob{ deltaTime = SystemAPI.Time.DeltaTime };
        beamMoveJob.Schedule();
    }

}
public partial struct BeamMoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform localTransform, in BeamData beamData)
    {
        localTransform = localTransform.Translate(new float3 (beamData.moveSpeed * deltaTime, 0f, 0f));
    }

}