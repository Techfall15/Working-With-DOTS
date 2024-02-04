using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;



public class PlayerMoveAuthoring : MonoBehaviour
{
    public float playerSpeed;
    private class PlayerMoveBaker : Baker<PlayerMoveAuthoring> 
    {
        public override void Bake(PlayerMoveAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerMoveSpeed
            {
                playerSpeed = authoring.playerSpeed
            });
            AddComponent(entity, new InputData
            {

            });



        }
    }
}
