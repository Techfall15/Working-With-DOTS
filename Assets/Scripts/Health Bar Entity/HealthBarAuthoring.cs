using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using System;

public class HealthBarAuthoring : MonoBehaviour
{

    public float minHealth = 0f;
    public float maxHealth = 10f;
    [Range(0f, 10f)] public float healthLeft = 10f;

    public class HealthBarBaker : Baker<HealthBarAuthoring>
    {
        public override void Bake(HealthBarAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new HealthBarData
            {
                healthLeft = authoring.healthLeft,
                minHealth = authoring.minHealth,
                maxHealth = authoring.maxHealth
            });

            AddComponent(entity, new InputData
            {

            });
        }
    }




}
