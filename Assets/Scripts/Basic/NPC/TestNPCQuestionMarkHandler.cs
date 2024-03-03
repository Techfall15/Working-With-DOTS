using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct TestNPCQuestionMarkHandler : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TestNPCData>();
    }



    public void OnUpdate(ref SystemState state)
    {

        bool isFadeBoxOverMe = false;


        foreach(var fadeBoxData in SystemAPI.Query<RefRO<FadeBoxData>>())
        {
            if(fadeBoxData.ValueRO.newPosition.y > 0)
            {
                isFadeBoxOverMe = true;
            }
        }
        if(isFadeBoxOverMe == true) 
        { 
            foreach(var TestNPCData in SystemAPI.Query<RefRW<TestNPCData>>())
            {
                TestNPCData.ValueRW.canSpawnQuestionMark = true;
                
            }
        }
        
    }




}
