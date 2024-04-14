using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;
[ExecuteAlways]

public class SpriteColorEditAuthoring : MonoBehaviour
{
    public Color spriteColor = Color.white;
    public bool randomizeColor = true;
    public bool isColorSet = false;
    public class SpriteColorEditBaker : Baker<SpriteColorEditAuthoring>
    {
        public override void Bake(SpriteColorEditAuthoring authoring)
        {
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);
            AddComponent(entity, new SpriteColorEditComponentData()
            {
                spriteColor = authoring.spriteColor,
                randomizeColor = authoring.randomizeColor,
                isColorSet = authoring.isColorSet,
            });
        }

    }
    public void UpdateSpriteColor(Color newColor)
    {
        if (isColorSet == true) return;
        if (transform.childCount == 0) transform.GetComponent<SpriteRenderer>().color = newColor;
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.GetComponent<SpriteRenderer>().color = newColor;
                isColorSet = true;
            }
        }
    }
}
