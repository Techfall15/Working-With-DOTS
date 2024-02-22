using Unity.Entities;
using UnityEngine;

public partial struct ExclamationPointData : IComponentData
{

    public int currentSpriteIndex;
    public float timeBetweenFrames;
    public float maxTimeBetweenFrames;
}
