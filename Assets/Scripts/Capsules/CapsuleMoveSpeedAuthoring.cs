using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class CapsuleMoveSpeedAuthoring : MonoBehaviour
{
    public float moveSpeed;


    private class CapsuleMoveSpeedBaker: Baker<CapsuleMoveSpeedAuthoring> 
    {

        public override void Bake(CapsuleMoveSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CapsuleMoveSpeed
            {
                moveSpeed = authoring.moveSpeed,
            }); 
        }




    }


}
