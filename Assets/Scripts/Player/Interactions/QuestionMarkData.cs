using Unity.Entities;


public partial struct QuestionMarkData : IComponentData
{
    public int currentSpriteIndex;
    public float timeBetweenFrames;
    public float maxTimeBetweenFrames;
}
