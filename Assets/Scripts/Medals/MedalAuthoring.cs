using UnityEngine;
using Unity.Entities;

public class MedalAuthoring : MonoBehaviour
{
    public int scoreValue = 10;

    public class MedalBaker : Baker<MedalAuthoring>
    {
        public override void Bake(MedalAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new MedalData
            {
                scoreValue = authoring.scoreValue,
            });
        }
    }
}
