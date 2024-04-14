using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct SpriteColorEditSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpriteColorEditComponentData>();
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityManager   eManager   = state.EntityManager;
        
        foreach((RefRO<SpriteColorEditComponentData> colorEditData,
                 RefRO<LocalTransform> myLocalTransform, Entity entity)
            in SystemAPI.Query<RefRO<SpriteColorEditComponentData>, 
                               RefRO<LocalTransform>>().WithEntityAccess())
        {
            Color   spriteColor     = colorEditData.ValueRO.spriteColor;
            var     childBuffer     = eManager.GetBuffer<Child>(entity);
            
            if (childBuffer.Length == 0) continue;

            

            
            foreach(var child in childBuffer)
            {
                SpriteRenderer renderer = eManager.GetComponentObject<SpriteRenderer>(child.Value);
                if ( renderer.color == spriteColor) continue;
                else renderer.color =  spriteColor;
            }
        }
    }
}