using Unity.Entities;
public partial struct TestNPCData : IComponentData
{


    public bool canSpawnQuestionMark;
    public bool alreadySpawnedQuestionMark;
    public Entity questionMarkEntity;


}
