using UnityEngine;
using Unity.Entities;

public class PlayerScoreAuthoring : MonoBehaviour
{
    public int playerScore = 0;
    
    public class PlayerScoreBaker : Baker<PlayerScoreAuthoring> 
    {

        public override void Bake(PlayerScoreAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerScoreData
            {
                playerScore = authoring.playerScore,
            });

        }

    }

}
