using UnityEngine;
using Unity.Entities;

public class GoldCoinDataAuthoring : MonoBehaviour
{

    public int currentSpriteIndex = 0;
    public bool isAnimating = false;
    public float timeBetweenFrames = 0.1f;
    public float maxTimeBetweenFrames = 0.1f;
    public Sprite[] spriteList;


    private class GoldCoinDataBaker : Baker<GoldCoinDataAuthoring>
    {

        public override void Bake(GoldCoinDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new GoldCoinData()
            {
                currentSpriteIndex = authoring.currentSpriteIndex,
                isAnimating = authoring.isAnimating,
                timeBetweenFrames = authoring.timeBetweenFrames,
                maxTimeBetweenFrames = authoring.maxTimeBetweenFrames,
            });

            AddComponentObject(entity, new GoldCoinSpriteData()
            {
                spriteList = authoring.spriteList,
            });

            AddComponent(entity, new TriggerComponent()
            {

            });
        }


    }


}