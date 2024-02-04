using UnityEngine;
using Unity.Entities;


public class ObjEntityAuthoring : MonoBehaviour
{
    public float objMoveSpeed;

    public class ObjEntityBaker : Baker<ObjEntityAuthoring>
    {
        public override void Bake(ObjEntityAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ObjEntitySpeed
            {
                objMoveSpeed = authoring.objMoveSpeed
            });
            AddComponent(entity, new InputData
            {

            });
        }
    }
}
