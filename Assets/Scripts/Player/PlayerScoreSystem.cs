using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Burst;
using Unity.Collections;


[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct PlayerScoreSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerScoreData>();
    }


    [BurstCompile]
    public partial struct PlayerScoreTriggerEvents : ITriggerEventsJob
    {
        public NativeArray<int> scoreArray;
        public EntityManager entityManager;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity medalEntity  = (entityManager.HasComponent<MedalData>(triggerEvent.EntityA)) ? triggerEvent.EntityA : triggerEvent.EntityB;

            int scoreToAdd      = entityManager.GetComponentData<MedalData>(medalEntity).scoreValue;
            scoreArray[0]       = scoreToAdd;
        }
    }
    public void OnUpdate(ref SystemState state)
    {
        NativeArray<int> scoreArray = new NativeArray<int>(1, Allocator.TempJob);
        var scoreTriggerEvent       = new PlayerScoreTriggerEvents
        {
            scoreArray      = scoreArray,
            entityManager   = state.EntityManager
        };
        scoreTriggerEvent.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();

        if (scoreArray[0] != 0)
        {
            PlayerScoreJob playerScoreJob = new PlayerScoreJob{ scoreToAdd = scoreArray[0] };
            playerScoreJob.Schedule();
            scoreArray[0] = 0;
        }

        scoreArray.Dispose();
    }
}

public partial struct PlayerScoreJob : IJobEntity
{
    public int scoreToAdd;
    public void Execute(ref PlayerScoreData playerScoreData)
    {
        playerScoreData.playerScore += scoreToAdd;
        Debug.Log("Player Score is now: " + playerScoreData.playerScore);
    }
}

