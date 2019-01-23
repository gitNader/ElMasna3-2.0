﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int", menuName = "ElMasna3/Fields/Int Field")]
public class IntField : ScriptableObject {

    [SerializeField]
    int value;

    [Tooltip("You don't have to use it!")]
    public GameEvent onChangeEvent;

    public void SetValue(int v)
    {
        value = v;
        //fire event here
        if (onChangeEvent != null)
            onChangeEvent.Raise();
    }

    public int GetValue()
    {
        return value;
    }
}