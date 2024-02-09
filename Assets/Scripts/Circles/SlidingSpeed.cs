using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;
public struct SlidingSpeed : IComponentData
{

    public float slideSpeed;
    public bool isMovingRight;
}
