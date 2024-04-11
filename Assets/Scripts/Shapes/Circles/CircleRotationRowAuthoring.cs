using Unity.Entities;
using UnityEngine;

public class CircleRotationRowAuthoring : MonoBehaviour
{
    public float rotationSpeed;


    public class CircleRotationRowBaker : Baker<CircleRotationAuthoring>
    {


        public override void Bake(CircleRotationAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CircleRotationRowData
            {
                rotationSpeed = authoring.rotationSpeed,
            });
        }


    }



}