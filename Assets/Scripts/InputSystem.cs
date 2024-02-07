using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public partial class InputSystem : SystemBase
{

    public InputMap myInputMap;
    protected override void OnCreate()
    {
        myInputMap = new InputMap();
        myInputMap.KeyboardMap.Enable();
    }

    protected override void OnUpdate()
    {
        foreach(RefRW<InputData> myInputData in SystemAPI.Query<RefRW<InputData>>())
        {
            myInputData.ValueRW.move        = myInputMap.KeyboardMap.MoveAction.ReadValue<Vector2>();
            myInputData.ValueRW.shoot       = myInputMap.KeyboardMap.ShootAction.WasPressedThisFrame();
            myInputData.ValueRW.damage      = myInputMap.KeyboardMap.DamageAction.WasPressedThisFrame();
            myInputData.ValueRW.spawnMedal  = myInputMap.KeyboardMap.SpawnMedalAction.WasPressedThisFrame();
        }
    }

    protected override void OnDestroy()
    {
        myInputMap.KeyboardMap.Disable();
        myInputMap.Dispose();
    }
}
