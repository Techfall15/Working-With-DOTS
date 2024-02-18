using UnityEngine;
using Unity.Entities;

public class TimeDelayDestroyAuthoring : MonoBehaviour
{
    public float timeUntilDestruction = 2f;


    private class TimeDelayDestroyBaker : Baker<TimeDelayDestroyAuthoring>
    {

        public override void Bake(TimeDelayDestroyAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new TimeDelayDestroyData()
            {
                timeUntilDestruction = authoring.timeUntilDestruction,
            });

        }


    }

}