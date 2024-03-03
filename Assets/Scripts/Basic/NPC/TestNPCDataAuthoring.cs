using UnityEngine;
using Unity.Entities;
public class TestNPCDataAuthoring : MonoBehaviour
{
    public bool canSpawnQuestionMark = false;
    public bool alreadySpawnedQuestionMark = false;
    public GameObject questionMarkEntity;

    private class TestNPCDataBaker : Baker<TestNPCDataAuthoring>
    {

        public override void Bake(TestNPCDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new TestNPCData()
            {
                canSpawnQuestionMark = authoring.canSpawnQuestionMark,
                alreadySpawnedQuestionMark = authoring.alreadySpawnedQuestionMark,
                questionMarkEntity = GetEntity(authoring.questionMarkEntity, TransformUsageFlags.Dynamic),
            });
        }



    }






}
