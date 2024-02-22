using UnityEngine;
using Unity.Entities;

public class PlayerInteractAuthoring : MonoBehaviour
{
    public GameObject interactEntity;


    private class PlayerInteractBaker : Baker<PlayerInteractAuthoring>
    {


        public override void Bake(PlayerInteractAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerInteractData()
            {
                interactEntity          = GetEntity(authoring.interactEntity, TransformUsageFlags.Dynamic),
                isInteractEntitySpawned = false,
            });
        }

    }

}