using Unity.Entities;
using Unity.Mathematics;

public partial struct WoodenDoorData : IComponentData
{

    public int currentSpriteIndex;
    public bool isOpen;
    public bool hasOpenDoorBeenSpawned;
    public bool wasEnteredLast;
    public Entity openDoorEntity;
    public Entity openDoorEntityAudioSource;
    public Entity closeDoorEntityAudioSource;
    public float3 cameraSpawnLocation;
    public float3 playerSpawnLocation;
}
