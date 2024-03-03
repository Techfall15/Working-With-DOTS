using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Xml.Linq;

public readonly partial struct TestNPCAspect : IAspect
{

    public readonly RefRW<TestNPCData> testNPCData;
    public readonly RefRO<LocalTransform> localTransform;



    public void SpawnAndSetNewQuestionMark(EntityCommandBuffer ecb)
    {
        if (testNPCData.ValueRO.canSpawnQuestionMark == true && testNPCData.ValueRO.alreadySpawnedQuestionMark == false)
        {
            Entity questionMark = ecb.Instantiate(testNPCData.ValueRO.questionMarkEntity);
            LocalTransform questionMarkTransform = new LocalTransform()
            {
                Position = new float3(localTransform.ValueRO.Position.x, localTransform.ValueRO.Position.y + 1.25f, localTransform.ValueRO.Position.z),
                Rotation = Quaternion.identity,
                Scale = 1f
            };
            testNPCData.ValueRW.alreadySpawnedQuestionMark = true;
            ecb.SetComponent<LocalTransform>(questionMark, questionMarkTransform);
        }
    }
}