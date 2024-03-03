using UnityEngine;
using Unity.Entities;

public class BonsaiTreeDataAuthoring : MonoBehaviour
{
    public int      currentSpriteIndex;
    public bool     isAnimating;
    public float    timeBetweenFrames;
    public float    maxTimeBetweenFrames;
    public Sprite[] spriteList;


    private class BonsaiTreeDataBaker : Baker<BonsaiTreeDataAuthoring>
    {
        public override void Bake(BonsaiTreeDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new BonsaiTreeData()
            {
                currentSpriteIndex      = authoring.currentSpriteIndex,
                isAnimating             = authoring.isAnimating,
                timeBetweenFrames       = authoring.timeBetweenFrames,
                maxTimeBetweenFrames    = authoring.maxTimeBetweenFrames,
            });
            AddComponentObject(entity, new BonsaiTreeSpriteData()
            {
                spriteList = authoring.spriteList
            });
        }
    }
}