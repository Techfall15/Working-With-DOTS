using UnityEngine;
using Unity.Entities;


public class TreasureChestAuthoring : MonoBehaviour
{

    public bool     canOpen             = false;
    public bool     isOpen              = false;
    public int      currentSpriteIndex  = 0;
    public Sprite[] spriteList;

    public class TreasureChestBaker : Baker<TreasureChestAuthoring>
    {

        public override void Bake(TreasureChestAuthoring authoring)
        {
            Entity entity           = GetEntity(TransformUsageFlags.Renderable);


            AddComponent(entity, new TreasureChestData()
            {
                canOpen             = authoring.canOpen,
                isOpen              = authoring.isOpen,
                currentSpriteIndex  = authoring.currentSpriteIndex
            });
            AddComponentObject(entity, new TreasureChestSpriteData()
            {
                spriteList          = authoring.spriteList
            });
            AddComponent(entity, new InputData()
            {

            });
            AddComponent(entity, new TriggerComponent()
            {

            });

        }

    }
}