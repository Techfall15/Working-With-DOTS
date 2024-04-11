using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;



public partial struct FillSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FillData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        NativeArray<float> newScale = new NativeArray<float>(1, Allocator.TempJob);
        GetNewScaleJob newScaleJob  = new GetNewScaleJob{ newScale = newScale };
        newScaleJob.Schedule(state.Dependency).Complete();

        NativeArray<float> newXPos  = new NativeArray<float>(1, Allocator.TempJob);
        GetNewXPosJob newXPosJob    = new GetNewXPosJob
        {
            scale   = newScaleJob.newScale[0],
            newXPos = newXPos 
        };
        newXPosJob.Schedule(state.Dependency).Complete();


        ModifyFillDataJob modifyFillDataJob = new ModifyFillDataJob
        {
            newScale    = newScaleJob.newScale[0],
            newXPos     = newXPosJob.newXPos[0],
        };
        modifyFillDataJob.Schedule(state.Dependency).Complete();


        SetNewFillDataJob setFillJob = new SetNewFillDataJob();
        setFillJob.Schedule(state.Dependency).Complete();


        newScale.Dispose();
        newXPos.Dispose();
    }
    public partial struct GetNewScaleJob : IJobEntity
    {
        public NativeArray<float> newScale;
        public void Execute(in HealthBarData healthBarData) => newScale[0] = healthBarData.healthLeft / healthBarData.maxHealth;
    }
    public partial struct GetNewXPosJob : IJob
    {
        public float scale;
        public NativeArray<float> newXPos;
        public void Execute() => newXPos[0] = (1f - scale) / 4f * -1;
    }
    public partial struct ModifyFillDataJob : IJobEntity
    {
        public float newScale;
        public float newXPos;
        public void Execute(ref FillData fillData)
        {
            fillData.fillScale = new Vector3(newScale, 0.1f, 0);
            fillData.fillPosition = new Vector3(newXPos, 0, 0);
        }
    }
    public partial struct SetNewFillDataJob : IJobEntity
    {
        public void Execute(ref LocalTransform localTransform, in FillData fillData, ref PostTransformMatrix pTM)
        {
            var position = localTransform.Position;
            position.x = fillData.fillPosition.x;

            float3 newScale = new float3(fillData.fillScale.x, fillData.fillScale.y, fillData.fillScale.z);
            PostTransformMatrix newPTM = new PostTransformMatrix { Value = float4x4.TRS(position, localTransform.Rotation, newScale) };


            pTM = newPTM;
            localTransform = localTransform.WithPosition(position);
        }
    }
}
