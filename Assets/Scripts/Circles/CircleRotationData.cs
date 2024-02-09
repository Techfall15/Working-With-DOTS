using UnityEngine;
using Unity.Entities;

public partial struct CircleRotationData : IComponentData
{

    public float rotationSpeed;

    public bool rotateClockwise;

}
