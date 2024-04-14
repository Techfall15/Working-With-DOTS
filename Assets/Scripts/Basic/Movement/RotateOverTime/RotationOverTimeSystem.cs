using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
[BurstCompile]
public partial struct RotationOverTimeSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotationOverTimeComponentData>();
    }
    public void OnUpdate(ref SystemState state)
    {
        RotateTransformJob rotateJob = new RotateTransformJob()
        {
            entityManager = state.EntityManager,
            deltaTime = SystemAPI.Time.DeltaTime,
        };

        rotateJob.Schedule(state.Dependency).Complete();
    }
}
[BurstCompile]
public partial struct RotateTransformJob : IJobEntity
{
    public EntityManager entityManager;
    public float deltaTime;
    public void Execute(in RotationOverTimeComponentData rotComponentData, ref LocalTransform myLocalTransform)
    {
        bool clockwise      = rotComponentData.clockwiseRotation;
        float rotationSpeed = rotComponentData.rotationSpeed;


        myLocalTransform = (clockwise == true) ?
            myLocalTransform.RotateZ(-rotationSpeed * deltaTime) :
            myLocalTransform.RotateZ( rotationSpeed * deltaTime);
    }
}