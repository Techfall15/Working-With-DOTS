using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using System;

public class HealthBarAuthoring : MonoBehaviour
{

    public float healthLeft;
    public float minHealth;
    public float maxHealth;

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

        }
    }




}
