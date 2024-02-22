using UnityEngine;
using Unity.Entities;


public class ExclamationPointDataAuthoring : MonoBehaviour
{

    public int      currentSpriteIndex = 0;
    public Sprite[] spriteList;
    public float    timeBetweenFrames = 0.1f;
    public float maxTimeBetweenFrames = 0.1f;

    private class ExclamationPointDataBaker : Baker<ExclamationPointDataAuthoring>
    {

        public override void Bake(ExclamationPointDataAuthoring authoring)
        {
            Entity entity           = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ExclamationPointData()
            {
                currentSpriteIndex      = authoring.currentSpriteIndex,
                timeBetweenFrames       = authoring.timeBetweenFrames,
                maxTimeBetweenFrames    = authoring.maxTimeBetweenFrames
            });
            AddComponentObject(entity, new ExclamationPointSpriteData()
            {
                spriteList              = authoring.spriteList
            });
        }

    }




}