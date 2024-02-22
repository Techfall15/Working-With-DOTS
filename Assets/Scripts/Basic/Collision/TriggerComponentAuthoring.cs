using UnityEngine;
using Unity.Entities;


public class TriggerComponentAuthoring : MonoBehaviour
{


    public class TriggerComponentBaker : Baker<TriggerComponentAuthoring>
    {


        public override void Bake(TriggerComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new TriggerComponent()
            {

            });
            AddComponent(entity, new InteractableTagComponent()
            {

            });
        }



    }

}
public partial struct InteractableTagComponent : IComponentData
{

}