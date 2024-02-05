using UnityEngine;
using Unity.Entities;

public class PlayerShootAuthoring : MonoBehaviour
{

    public GameObject objToSpawn;

    public class PlayerShootBaker : Baker<PlayerShootAuthoring>
    {
        public override void Bake(PlayerShootAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerShootComponent
            {
                objToSpawnEntity = GetEntity(authoring.objToSpawn,TransformUsageFlags.Dynamic)
            });
        }
    }

}

public struct PlayerShootComponent : IComponentData
{
    public Entity objToSpawnEntity;

}