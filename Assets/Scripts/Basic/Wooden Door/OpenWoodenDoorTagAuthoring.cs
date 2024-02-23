using UnityEngine;
using Unity.Entities;

public class OpenWoodenDoorTagAuthoring : MonoBehaviour
{

    private class OpenWoodenDoorTagBaker : Baker<OpenWoodenDoorTagAuthoring>
    {

        public override void Bake(OpenWoodenDoorTagAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new OpenWoodenDoorTagComponent()
            {

            });
        }


    }






}