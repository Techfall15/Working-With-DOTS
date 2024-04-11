using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;

public partial struct RotationOverTimeSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotationOverTimeComponentData>();
    }
    public void OnUpdate(ref SystemState state)
    {
        foreach((RefRW<LocalTransform> myLocalTransform, 
                 RefRO<RotationOverTimeComponentData> rotComponentData)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationOverTimeComponentData>>())
        {
            float deltaTime     = SystemAPI.Time.DeltaTime;
            float rotationSpeed = rotComponentData.ValueRO.rotationSpeed;
            bool  clockwise     = rotComponentData.ValueRO.clockwiseRotation;

            myLocalTransform.ValueRW = (clockwise == true) ?
                myLocalTransform.ValueRO.RotateZ(-rotationSpeed * deltaTime) :
                myLocalTransform.ValueRO.RotateZ( rotationSpeed * deltaTime);
        }
    }
}