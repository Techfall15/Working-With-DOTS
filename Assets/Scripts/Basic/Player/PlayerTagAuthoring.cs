using UnityEngine;
using Unity.Entities;


public class PlayerTagAuthoring : MonoBehaviour
{


    private class PlayerTagBaker : Baker<PlayerTagAuthoring> 
    {

        public override void Bake(PlayerTagAuthoring authoring)
        {
            
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerTagComponent()
            {

            });



        }

    }

}
public partial struct PlayerTagComponent : IComponentData
{

}