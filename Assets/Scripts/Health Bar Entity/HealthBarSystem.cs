using UnityEngine;
using Unity.Entities;
using Unity.Burst;


public partial struct HealthBarSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        ReduceHealthJob reduceHealthJob = new ReduceHealthJob { damageAmount = 2 };

        reduceHealthJob.Schedule();
    }



}

[BurstCompile]
public partial struct ReduceHealthJob : IJobEntity
{
    public float damageAmount;
    public void Execute(ref HealthBarData healthBarData, ref InputData inputData)
    {
        if (inputData.damage == false) return;
        healthBarData.healthLeft = (healthBarData.healthLeft - damageAmount > 0f) ? healthBarData.healthLeft - damageAmount : 0f;
        inputData.damage = false;
    }
}