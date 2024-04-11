using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class DiamondMoveSpeedAuthoring : MonoBehaviour
{

    public float moveSpeed;


    private class DiamondMoveSpeedBaker: Baker<DiamondMoveSpeedAuthoring> 
    {


        public override void Bake(DiamondMoveSpeedAuthoring authoring)
        {

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new DiamondMoveSpeed
            {
                moveSpeed = authoring.moveSpeed
            });



        }




    }



}
