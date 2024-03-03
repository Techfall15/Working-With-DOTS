using Unity.Entities;

public partial struct BonsaiTreeData : IComponentData
{
    public int      currentSpriteIndex;
    public bool     isAnimating;
    public float    timeBetweenFrames;
    public float    maxTimeBetweenFrames;
}