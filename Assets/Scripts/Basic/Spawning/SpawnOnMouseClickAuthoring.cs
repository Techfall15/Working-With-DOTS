using UnityEngine;
using Unity.Entities;


public class SpawnOnMouseClickAuthoring : MonoBehaviour
{


    private class SpawnOnMouseClickBaker : Baker<SpawnOnMouseClickAuthoring>
    {

        public override void Bake(SpawnOnMouseClickAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new InputData
            {

            });
        }


    }





}
