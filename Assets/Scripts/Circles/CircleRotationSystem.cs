using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

public partial struct CircleRotationSystem : ISystem
{
    public void OnCreate(ref SystemState state) 
    {
        state.RequireForUpdate<CircleRotationData>();
    }


    public void OnUpdate(ref SystemState state)
    {
        RotateCircleJob rotateCircleJob = new RotateCircleJob { deltaTime = SystemAPI.Time.DeltaTime };




        NativeArray<float> rowSpeeds = new NativeArray<float>(1, Allocator.TempJob);
        GetRowSpeedJob getRowSpeedJob = new GetRowSpeedJob()
        {
            rowSpeeds = rowSpeeds,
        };
        getRowSpeedJob.Schedule(state.Dependency).Complete();





        SetNewRotationSpeedJob setNewRotationSpeedJob = new SetNewRotationSpeedJob()
        {
            newSpeed = rowSpeeds[0],
        };
        setNewRotationSpeedJob.Schedule(state.Dependency).Complete();



        rotateCircleJob.Schedule();
    }

}
public partial struct GetRowSpeedJob : IJobEntity
{
    public NativeArray<float> rowSpeeds;
    public void Execute(ref CircleRotationRowData rowData)
    {
        rowSpeeds[0] = rowData.rotationSpeed;
    }
}
public partial struct SetNewRotationSpeedJob : IJobEntity
{
    public float newSpeed;

    public void Execute(ref CircleRotationData circleRotationData)
    {
        circleRotationData.rotationSpeed = newSpeed;
    }
}
public partial struct RotateCircleJob : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform localTransform, in CircleRotationData circleRotationData)
    {

        localTransform = (circleRotationData.rotateClockwise == false) ? localTransform.RotateZ(circleRotationData.rotationSpeed * deltaTime) : localTransform.RotateZ(-circleRotationData.rotationSpeed * deltaTime);
    }
}