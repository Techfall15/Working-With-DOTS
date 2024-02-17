using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using System.Collections;

public class OpenCloseAction : MonoBehaviour
{

    [SerializeField] private List<Sprite> spriteList = new List<Sprite>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    public NativeArray<Entity> entityArray;
    [SerializeField]public Entity entity;
    public EntityManager entityManager;
    public bool needsToChange = false;

    protected IEnumerator Start()
    {
        WaitForSeconds ecsDelay = new WaitForSeconds(0.1f);
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        yield return ecsDelay;
        entityArray = entityManager.CreateEntityQuery(typeof(TreasureChestData)).ToEntityArray(Allocator.Temp);
        entity = entityArray[0];
    }


    protected void Update()
    {

        if (needsToChange == false) return;

        TreasureChestData chestData = entityManager.GetComponentData<TreasureChestData>(entity);
        if(chestData.isOpen == true)
        {
            Sprite newSprite;
            TreasureChestData newData = new TreasureChestData()
            {
                isOpen = false,
                currentSpriteIndex = 0
            };
            entityManager.SetComponentData<TreasureChestData>(entity, newData);
            newSprite = spriteList[entityManager.GetComponentData<TreasureChestData>(entity).currentSpriteIndex];
            SetNewSpriteTo(newSprite);
        }
        else
        {
            Sprite newSprite;
            TreasureChestData newData = new TreasureChestData()
            {
                isOpen = true,
                currentSpriteIndex = 1
            };
            entityManager.SetComponentData<TreasureChestData>(entity, newData);
            newSprite = spriteList[entityManager.GetComponentData<TreasureChestData>(entity).currentSpriteIndex];
            SetNewSpriteTo(newSprite);
        }
        needsToChange = false;
    }



    public void SetNewSpriteTo(Sprite newSprite) => spriteRenderer.sprite = newSprite;


}
