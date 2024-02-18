using UnityEngine;
using Unity.Entities;

public partial struct TimeDelayDestroyData : IComponentData
{
    public float timeUntilDestruction;
}
