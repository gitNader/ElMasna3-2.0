﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform doorPosition;

    public Transform WayPointTransform;

    public Floor ParentFloor
    {
        get { return GetComponentInParent<Floor>(); }
    }
}
