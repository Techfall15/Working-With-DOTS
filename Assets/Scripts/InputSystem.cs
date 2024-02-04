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
            myInputData.ValueRW.move = myInputMap.KeyboardMap.MoveAction.ReadValue<Vector2>();
        }
    }

    protected override void OnDestroy()
    {
        myInputMap.KeyboardMap.Disable();
        myInputMap.Dispose();
    }
}
