using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;
public partial struct MoveWithKeyboardSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MoveWithKeyboardData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        NativeArray<int>    isMovingResults     = new NativeArray<int>(1, Allocator.TempJob);
        CheckIfMovingJob    moveCheckJob        = new CheckIfMovingJob { isMovingResults = isMovingResults };
        MoveWithKeyboardJob moveJob             = new MoveWithKeyboardJob{ deltaTime = SystemAPI.Time.DeltaTime };

        moveCheckJob.Schedule(state.Dependency).Complete();

        if (isMovingResults[0] == 0)
        {
            isMovingResults.Dispose();
            return;
        }
        moveJob.Schedule(state.Dependency).Complete();
        isMovingResults.Dispose();
    }

}
[BurstCompile]
public partial struct MoveWithKeyboardJob : IJobEntity
{
    public float deltaTime;
    public void Execute(MoveWithKeyboardAspect keyboardAspect) => keyboardAspect.MoveEntity(deltaTime);

}
[BurstCompile]
public partial struct CheckIfMovingJob : IJobEntity
{
    public NativeArray<int> isMovingResults;
    public void Execute(MoveWithKeyboardAspect keyboardAspect)
    {
        bool movingResult =  keyboardAspect.IsMovingCheck();
        if (movingResult == true) isMovingResults[0] = 0;
        else isMovingResults[0] = 1;
    }
}