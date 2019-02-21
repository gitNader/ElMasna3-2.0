﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledRotator : MonoBehaviour
{
    public bool currentActive;
    Rigidbody myBody;
    public RotatingObjectsList listOfMes;

    [Header("Input")]
    public FloatField swipeMagnitude;
    public float torqueMultiplyer;

    private void OnEnable()
    {
        listOfMes.Add(this);
    }

    private void OnDisable()
    {
        listOfMes.Remove(this);
    }

    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    public void RotateRight()
    {
        if (!currentActive)
            return;

        if (myBody.angularVelocity.y > 0)
            myBody.angularVelocity = Vector3.zero;

        myBody.AddTorque(Vector3.down * torqueMultiplyer * swipeMagnitude.GetValue());
    }

    public void RotateLeft()
    {
        if (!currentActive)
            return;

        if (myBody.angularVelocity.y < 0)
            myBody.angularVelocity = Vector3.zero;

        myBody.AddTorque(Vector3.up * torqueMultiplyer * swipeMagnitude.GetValue());
    }

    public void StopRotation()
    {
        if (!currentActive)
            return;

        myBody.angularVelocity = Vector3.zero;
    }

}
