using UnityEngine;
using Unity.Entities;


public partial struct PlayerInteractData : IComponentData
{

    public Entity interactEntity;
    public bool isInteractEntitySpawned;

}
