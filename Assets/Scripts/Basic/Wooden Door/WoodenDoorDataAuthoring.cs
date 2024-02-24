using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class WoodenDoorDataAuthoring : MonoBehaviour
{
    public int currentSpriteIndex = 0;
    public bool isOpen = false;
    public bool hasOpenDoorBeenSpawned = false;
    public bool hasAudioBeenSpawned = false;
    public bool wasEnteredLast = false;
    public float3 cameraSpawnLocation;
    public float3 playerSpawnLocation;
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
                hasAudioBeenSpawned         = authoring.hasAudioBeenSpawned,
                wasEnteredLast              = authoring.wasEnteredLast,
                cameraSpawnLocation         = authoring.cameraSpawnLocation,
                playerSpawnLocation         = authoring.playerSpawnLocation,

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
