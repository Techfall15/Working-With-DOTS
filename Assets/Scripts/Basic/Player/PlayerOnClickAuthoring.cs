using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerOnClickAuthoring : MonoBehaviour
{


    [Header("-States-")]
    public bool hasBeenClicked = false;
    [Header("-Sizes-")]
    public float xSize = 0.5f;
    public float ySize = 0.5f;
    [Header("-Spawning-")]
    public float2 spawnPos;
    public GameObject entityToSpawn;

    private class PlayerOnClickBaker : Baker<PlayerOnClickAuthoring>
    {

        public override void Bake(PlayerOnClickAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerOnClickData()
            {
                hasBeenClickedOn    = authoring.hasBeenClicked,
                xSize               = authoring.xSize,
                ySize               = authoring.ySize,
                spawnPos            = authoring.spawnPos,
                entityToSpawn       = GetEntity(authoring.entityToSpawn, TransformUsageFlags.Dynamic)
            });
        }


    }


}
