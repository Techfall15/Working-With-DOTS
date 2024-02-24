using UnityEngine;
using Unity.Entities;
public class InteractEntityTagAuthoring : MonoBehaviour
{
    


    private class InteractEntityTagBaker : Baker<InteractEntityTagAuthoring>
    {

        public override void Bake(InteractEntityTagAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new InteractEntityTagComponent()
            {

            });

        }


    }

}
public partial struct InteractEntityTagComponent : IComponentData
{

}