using UnityEngine;
using Unity.Entities;



public class BeamAuthoring : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float xLimit = 5f;

    public class BeamBaker : Baker<BeamAuthoring>
    {
        public override void Bake(BeamAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BeamData
            {
                moveSpeed = authoring.moveSpeed,
                xLimit = authoring.xLimit
            });

        }

    }

}
