using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Entities.UI;
public class FillAuthoring : MonoBehaviour
{
    
    public Vector3 fillPosition;
    public Vector3 fillScale;

    public class FillBaker : Baker<FillAuthoring> 
    {
        public override void Bake(FillAuthoring authoring)
        {

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new FillData
            {
                fillPosition = authoring.fillPosition,
                fillScale = authoring.fillScale,
            });
            

        }
    }
}
