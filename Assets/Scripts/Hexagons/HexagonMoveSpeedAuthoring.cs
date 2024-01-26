using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class HexagonMoveSpeedAuthoring : MonoBehaviour
{

    public float moveSpeed;

    private class HexagonMoveSpeedBaker : Baker<HexagonMoveSpeedAuthoring> 
    {

        public override void Bake(HexagonMoveSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HexagonMoveSpeed
            {
                moveSpeed = authoring.moveSpeed
            });

        }

    }



}
