using UnityEngine;
using Unity.Entities;

public class BeamCounterAuthoring : MonoBehaviour
{

    public int beamCount = 0;

    public class BeamCounterBaker : Baker<BeamCounterAuthoring>
    {


        public override void Bake(BeamCounterAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new BeamCounterData
            {
                beamCount = authoring.beamCount,
            });


        }


    }


}
