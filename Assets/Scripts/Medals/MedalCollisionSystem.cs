using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Burst;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Jobs;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial class GetNumCollisionEventsSystem : SystemBase
{
    [BurstCompile]
    public partial struct CountNumCollisionEvents : ICollisionEventsJob
    {
        public NativeReference<int> NumCollisionEvents;

        public void Execute(CollisionEvent collisionEvent)
        {
            NumCollisionEvents.Value++;
        }
    }

    protected override void OnUpdate()
    {
        NativeReference<int> numCollisionEvents = new NativeReference<int>(0, Allocator.TempJob);

        var job = new CountNumCollisionEvents
        {
            NumCollisionEvents = numCollisionEvents
        };

        job.Schedule<CountNumCollisionEvents>(
            SystemAPI.GetSingleton<SimulationSingleton>(),
            this.Dependency
            ).Complete();

        UnityEngine.Debug.Log("CollisionP: " + numCollisionEvents.Value);

        numCollisionEvents.Dispose();
    }
}
