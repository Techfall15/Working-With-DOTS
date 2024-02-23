using Unity.Entities;

public partial struct WoodenDoorData : IComponentData
{

    public int currentSpriteIndex;
    public bool isOpen;
    public bool hasOpenDoorBeenSpawned;
    public Entity openDoorEntity;
    public Entity openDoorEntityAudioSource;
    public Entity closeDoorEntityAudioSource;
}
