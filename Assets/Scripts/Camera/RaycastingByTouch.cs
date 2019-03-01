﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingByTouch : MonoBehaviour
{
    public float rayLength = 100;

    [Header("Layer Masks")]
    public LayerMask WorkerLayerMask;
    public LayerMask MachineLayerMask;
    public LayerMask VFXEmptyMachineLayerMask;

    public void RaycastingFromScreenPoint()
    {
        if (Input.touchCount == 1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit, rayLength, WorkerLayerMask))
                {
                    Debug.Log("Worker " + hit.collider.name);
                }
                else if (Physics.Raycast(ray, out hit, rayLength, MachineLayerMask))
                {
                    Debug.Log("Machine " + hit.collider.name);
                }
                else if (Physics.Raycast(ray, out hit, rayLength, VFXEmptyMachineLayerMask))
                {
                    Debug.Log("Empty Machine " + hit.collider.name);
                }

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             
            if (Physics.Raycast(ray, out hit, rayLength, WorkerLayerMask))
            {
                Debug.Log("Worker " + hit.collider.name);
            }
            else if (Physics.Raycast(ray, out hit, rayLength, MachineLayerMask))
            {
                Debug.Log("Machine " + hit.collider.name);
            }
            else if (Physics.Raycast(ray, out hit, rayLength, VFXEmptyMachineLayerMask))
            {
                Debug.Log("Empty Machine " + hit.collider.name);
            }

        }
    }
}
