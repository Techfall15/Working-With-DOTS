using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

public partial struct CircleSlidingSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        CircleSlideJob circleSlideJob = new CircleSlideJob { deltaTime = SystemAPI.Time.DeltaTime };
        circleSlideJob.Schedule();
    }



}
[BurstCompile]
public partial struct CircleSlideJob : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform localTransform, in SlidingSpeed slideSpeed)
    {
        localTransform = localTransform.Translate(new float3(
                slideSpeed.slideSpeed * deltaTime,
                0,
                0));
    }
}