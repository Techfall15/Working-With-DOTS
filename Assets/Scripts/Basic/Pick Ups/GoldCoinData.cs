using Unity.Entities;
using UnityEngine;

public partial struct GoldCoinData : IComponentData
{

    public int currentSpriteIndex;
    public bool isAnimating;
    public float timeBetweenFrames;
    public float maxTimeBetweenFrames;


}

public class GoldCoinSpriteData : IComponentData
{
    public Sprite[] spriteList;
}
