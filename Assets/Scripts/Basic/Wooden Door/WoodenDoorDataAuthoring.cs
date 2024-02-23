using UnityEngine;
using Unity.Entities;

public class WoodenDoorDataAuthoring : MonoBehaviour
{
    public int currentSpriteIndex = 0;
    public bool isOpen = false;
    public bool hasOpenDoorBeenSpawned = false;
    public GameObject openDoorEntity;
    public Sprite[] spriteList;
    private class WoodenDoorDataBaker : Baker<WoodenDoorDataAuthoring>
    {

        public override void Bake(WoodenDoorDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new WoodenDoorData()
            {
                currentSpriteIndex = authoring.currentSpriteIndex,
                isOpen = authoring.isOpen,
                hasOpenDoorBeenSpawned = authoring.hasOpenDoorBeenSpawned,
                openDoorEntity = GetEntity(authoring.openDoorEntity, TransformUsageFlags.Dynamic),
            });
            AddComponentObject(entity, new WoodenDoorSpriteData()
            {
                spriteList = authoring.spriteList,
            });
        }


    }



}
