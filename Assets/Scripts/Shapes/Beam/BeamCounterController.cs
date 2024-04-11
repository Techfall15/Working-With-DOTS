using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Entities;

public class BeamCounterController : MonoBehaviour
{
    public Entity beamCounterEntity;
    public EntityManager entityManager;
    public TextMeshProUGUI beamCounterText;
    public int currentCount;
    protected IEnumerator Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        WaitForSeconds ecsDelay = new WaitForSeconds(0.5f);

        yield return ecsDelay;
        beamCounterEntity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(BeamCounterData)).GetSingletonEntity();

        Debug.Log(beamCounterEntity);
        UpdateCurrentCountFrom(beamCounterEntity);
        UpdateBeamDataText(currentCount);
    }

    private void Update()
    {
        if (entityManager.Exists(beamCounterEntity) == false) return;
        if (currentCount == GetCurrentCountFrom(beamCounterEntity)) return;
        UpdateCurrentCountFrom(beamCounterEntity);
        UpdateBeamDataText(currentCount);
    }

    private void UpdateBeamDataText(int beamCount)      => beamCounterText.text = $"<color=green>Beams Destroyed: {beamCount.ToString()}</color>";
    private void UpdateCurrentCountFrom(Entity entity)  => currentCount = entityManager.GetComponentData<BeamCounterData>(entity).beamCount;
    private int GetCurrentCountFrom(Entity entity)      => entityManager.GetComponentData<BeamCounterData>(entity).beamCount;

}
