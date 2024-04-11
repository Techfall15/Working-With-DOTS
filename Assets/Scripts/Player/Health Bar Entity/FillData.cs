using UnityEngine;
using Unity.Entities;

public struct FillData : IComponentData
{
    public Vector3 fillPosition;
    public Vector3 fillScale;
}
