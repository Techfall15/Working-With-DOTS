using UnityEngine;
using Unity.Entities;

public class PlayerShootAuthoring : MonoBehaviour
{

    public GameObject objToSpawn;
    public GameObject medalToSpawn;

    public class PlayerShootBaker : Baker<PlayerShootAuthoring>
    {
        public override void Bake(PlayerShootAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerShootComponent
            {
                objToSpawnEntity = GetEntity(authoring.objToSpawn,TransformUsageFlags.Dynamic),
            });
            AddComponent(entity, new PlayerSpawnComponent
            {
                medalToSpawnEntity = GetEntity(authoring.medalToSpawn, TransformUsageFlags.None)
            });
        }
    }

}

public struct PlayerShootComponent : IComponentData
{
    public Entity objToSpawnEntity;

}

public struct PlayerSpawnComponent : IComponentData
{
    public Entity medalToSpawnEntity;
}