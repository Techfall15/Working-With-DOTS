using UnityEngine;
using Unity.Entities;

public class WoodenDoorDataAuthoring : MonoBehaviour
{
    public int currentSpriteIndex = 0;
    public bool isOpen = false;
    public bool hasOpenDoorBeenSpawned = false;
    public GameObject openDoorEntity;
    public GameObject openDoorEntityAudioSource;
    public GameObject closeDoorEntityAudioSource;
    public Sprite[] spriteList;
    private class WoodenDoorDataBaker : Baker<WoodenDoorDataAuthoring>
    {

        public override void Bake(WoodenDoorDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new WoodenDoorData()
            {
                isOpen                      = authoring.isOpen,
                currentSpriteIndex          = authoring.currentSpriteIndex,
                hasOpenDoorBeenSpawned      = authoring.hasOpenDoorBeenSpawned,

                openDoorEntity              = GetEntity(authoring.openDoorEntity, TransformUsageFlags.Dynamic),
                openDoorEntityAudioSource   = GetEntity(authoring.openDoorEntityAudioSource, TransformUsageFlags.Dynamic),
                closeDoorEntityAudioSource  = GetEntity(authoring.closeDoorEntityAudioSource, TransformUsageFlags.Dynamic)
            }) ;
            AddComponentObject(entity, new WoodenDoorSpriteData()
            {
                spriteList = authoring.spriteList,
            });
        }


    }



}
