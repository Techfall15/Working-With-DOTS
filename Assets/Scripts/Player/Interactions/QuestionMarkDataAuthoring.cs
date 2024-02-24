using UnityEngine;
using Unity.Entities;

public class QuestionMarkDataAuthoring : MonoBehaviour
{
    public int currentSpriteIndex = 0;
    public float timeBetweenFrames = 0.1f;
    public float maxTimeBetweenFrames = 0.1f;
    public Sprite[] spriteList;

    private class QuestionMarkDataBaking : Baker<QuestionMarkDataAuthoring>
    {
        public override void Bake(QuestionMarkDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new QuestionMarkData()
            {
                currentSpriteIndex = authoring.currentSpriteIndex,
                timeBetweenFrames = authoring.timeBetweenFrames,
                maxTimeBetweenFrames = authoring.maxTimeBetweenFrames
            });
            AddComponentObject(entity, new QuestionMarkSpriteData()
            {
                spriteList = authoring.spriteList
            });
        }
    }



}