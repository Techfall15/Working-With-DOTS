using Unity.Entities;
using UnityEngine;



public class CircleRotationAuthoring : MonoBehaviour
{


    public float rotationSpeed = 2.0f;
    public bool rotateClockwise = false;

    public class CircleRoationBaker : Baker<CircleRotationAuthoring>
    {



        public override void Bake(CircleRotationAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CircleRotationData
            {
                rotationSpeed   = authoring.rotationSpeed,
                rotateClockwise = authoring.rotateClockwise
            });
        }
    }






}