using Unity.Entities;
using UnityEngine;

public class MoveWithkeyboardAuthoring : MonoBehaviour
{

    public float moveSpeed;

    public class MoveWithKeyboardBaker : Baker<MoveWithkeyboardAuthoring>
    {

        public override void Bake(MoveWithkeyboardAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new MoveWithKeyboardData
            {
                moveSpeed = authoring.moveSpeed
            });
            AddComponent(entity, new InputData
            {

            });

        }


    }



}