using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class StarAuthoring : MonoBehaviour
{
    public bool isRotating      = false;
    public bool randomizeSpawn  = false;
    public bool randomizeSpeed  = false;
    public bool randomizeScale  = false;
    public bool positionSetupComplete   = false;
    public bool rotationSetupComplete   = false;
    public bool scaleSetupComplete      = false;
    public bool colorSetupComplete      = false;
    public Color spriteColor    = Color.white;
    public float rotationSpeed  = 5f;
    public float customScale    = 1f;
    public float2 spawnPos      = new float2(0,5f);
    public float2 xPosMinMax    = new float2(-12f, 12f);
    public float2 yPosMinMax    = new float2(-7f, 7f);

    private class StarBaker : Baker<StarAuthoring>
    {
        public override void Bake(StarAuthoring authoring)
        {
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            AddComponent(entity, new StarComponentData()
            {
                isRotating              = authoring.isRotating,
                randomizeScale          = authoring.randomizeScale,
                randomizeSpawn          = authoring.randomizeSpawn,
                randomizeSpeed          = authoring.randomizeSpeed,
                positionSetupComplete   = authoring.positionSetupComplete,
                rotationSetupComplete   = authoring.rotationSetupComplete,
                scaleSetupComplete      = authoring.scaleSetupComplete,
                colorSetupComplete      = authoring.colorSetupComplete,
                spriteColor             = authoring.spriteColor,
                rotationSpeed           = authoring.rotationSpeed,
                customScale             = authoring.customScale,
                spawnPos                = authoring.spawnPos,
                xPosMinMax              = authoring.xPosMinMax,
                yPosMinMax              = authoring.yPosMinMax,
            });
        }
    }

    public float2 GetRandomSpawnPos() => new float2(
        GetRandomFloatBetween(-7f,7f),
        GetRandomFloatBetween(-7f,7f));
    public float GetRandomFloatBetween(float x, float y) => UnityEngine.Random.Range(x, y);

}