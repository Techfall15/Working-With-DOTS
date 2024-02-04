using UnityEngine;
using Unity.Entities;

public struct HealthBarData : IComponentData
{
    public float healthLeft;
    public float minHealth;
    public float maxHealth;
}
