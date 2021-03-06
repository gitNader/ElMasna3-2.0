﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkerCustomization : MonoBehaviour
{
    public GameObject SpineBone;
    public GameObject HeadBone;

    public Transform HeadPlace;
    public Transform BodyPlace;
    private bool dirty;

    public GameEvent SaveGameCustomization;

    [Header("Customization Scheme")]
    [Tooltip("Use Gender Specific List.\n0) Head\n1) Face.\n2) Body.")]
    public ScriptableObjectsList CustomizaitonSchemes;

    public CustomizationPanelScheme HeadItems
    {
        get { return (CustomizationPanelScheme)CustomizaitonSchemes.ListElements[0]; }
    }

    public CustomizationPanelScheme FaceItems
    {
        get { return (CustomizationPanelScheme)CustomizaitonSchemes.ListElements[1]; }
    }

    public CustomizationPanelScheme BodyItems
    {
        get { return (CustomizationPanelScheme)CustomizaitonSchemes.ListElements[2]; }
    }

    [Header("Basic Hair")]
    public GameObject basicHair;

    [Header("Customization Items")]
    public CustomizationObject HeadItem;
    public CustomizationObject FaceItem;
    public CustomizationObject BodyItem;

    private CustomizationItem previewHeadInfo;
    private CustomizationItem previewFaceInfo;
    private CustomizationItem previewBodyInfo;

    [Header("Cost")]
    public FloatField costOfPreviewed;

    private void Start()
    {
        HeadPlace.SetParent(HeadBone.transform);
        BodyPlace.SetParent(SpineBone.transform);

        previewHeadInfo = new CustomizationItem();
        previewFaceInfo = new CustomizationItem();
        previewBodyInfo = new CustomizationItem();
    }

    private void OnEnable()
    {
        costOfPreviewed.SetValue(0);
    }

    public void PreviewItem(CustomizationItem item)
    {
        switch (item.type)
        {
            case CustomizationType.HEAD:
                if (HeadItem.myself.id > -1)
                {
                    HeadItem.gameObject.SetActive(false);
                }

                //if (previewHead != null)
                if (previewHeadInfo.item != null)
                {
                    Destroy(previewHeadInfo.item);
                    costOfPreviewed.AddValue(-previewHeadInfo.price);
                }

                previewHeadInfo.item = Instantiate(item.item, HeadPlace);
                previewHeadInfo.item.layer = 11;
                previewHeadInfo.FillData(item);
                break;
            case CustomizationType.FACE:
                if (FaceItem.myself.id > -1)
                {
                    FaceItem.gameObject.SetActive(false);
                }

                //if (previewFace != null)
                if (previewFaceInfo.item != null)
                {
                    Destroy(previewFaceInfo.item);
                    costOfPreviewed.AddValue(-previewFaceInfo.price);

                }

                previewFaceInfo.item = Instantiate(item.item, HeadPlace);
                previewFaceInfo.item.layer = 11;
                previewFaceInfo.FillData(item);
                break;
            case CustomizationType.BODY:
                if (BodyItem.myself.id > -1)
                {
                    BodyItem.gameObject.SetActive(false);
                }

                //if (previewBody != null)
                if (previewBodyInfo.item != null)
                {
                    Destroy(previewBodyInfo.item);
                    costOfPreviewed.AddValue(-previewBodyInfo.price);
                }

                previewBodyInfo.item = Instantiate(item.item, BodyPlace);
                previewBodyInfo.item.layer = 11;
                //previewBodyInfo = item;
                //previewBodyInfo.item = previewBody;
                previewBodyInfo.FillData(item);
                break;
            default:
                break;
        }

        costOfPreviewed.AddValue(item.price);

        dirty = true;
    }

    public void EndPreview()
    {
        if (dirty)
        {
            if (previewHeadInfo.item != null)
                Destroy(previewHeadInfo.item);
            if (previewFaceInfo.item != null)
                Destroy(previewFaceInfo.item);
            if (previewBodyInfo.item != null)
                Destroy(previewBodyInfo.item);

            HeadItem.gameObject.SetActive(true);
            BodyItem.gameObject.SetActive(true);
            FaceItem.gameObject.SetActive(true);
        }
        costOfPreviewed.SetValue(0);
    }

    public void ConfirmPreview()
    {
        Worker myWorker = GetComponent<Worker>();

        if (previewHeadInfo.item != null)
        {
            previewHeadInfo.item.transform.SetParent(HeadItem.gameObject.transform);
            HeadItem.myself = previewHeadInfo;
            HeadItem.myself.item = Instantiate(previewHeadInfo.item, HeadPlace);
            //give happiness to myWorker
            GetComponent<Worker>().AddHappiness(HeadItem.myself.happyAdd);
        }
        if (previewFaceInfo.item != null)
        {
            previewFaceInfo.item.transform.SetParent(FaceItem.gameObject.transform);
            FaceItem.myself = previewFaceInfo;
            FaceItem.myself.item = Instantiate(previewFaceInfo.item, HeadPlace);
            GetComponent<Worker>().AddHappiness(FaceItem.myself.happyAdd);

        }
        if (previewBodyInfo.item != null)
        {
            previewBodyInfo.item.transform.SetParent(BodyItem.gameObject.transform);
            BodyItem.myself = previewBodyInfo;
            BodyItem.myself.item = Instantiate(previewBodyInfo.item, BodyPlace);
            GetComponent<Worker>().AddHappiness(BodyItem.myself.happyAdd);
        }

        SaveGameCustomization.Raise();
        dirty = false;
    }

    public int[] GetCustomizationDataArray()
    {
        int[] arr = new int[3];

        arr[0] = (HeadItem.myself.item != null) ? HeadItem.myself.id : -1;
        arr[1] = (FaceItem.myself.item != null) ? FaceItem.myself.id : -1;
        arr[2] = (BodyItem.myself.item != null) ? BodyItem.myself.id : -1;

        return arr;
    }

    public void LoadCustomizationData(int[] cData)
    {
        if (cData[0] >= 0)
        {
            CustomizationItem headItem = HeadItems.Items.FirstOrDefault(x => x.id == cData[0]); ;
            if (headItem.item != null)
            {
                HeadItem.loadObject(headItem, HeadPlace);
            }
            else
            {
                Debug.LogWarning("No Head Items found with ID (" + cData[0] + ").");
            }
        }

        if (cData[1] >= 0)
        {
            var faceItem = FaceItems.Items.FirstOrDefault(x => x.id == cData[1]);
            if (faceItem != null)
            {
                FaceItem.loadObject(faceItem, HeadPlace);
            }
            else
            {
                Debug.LogWarning("No Face Items found with ID (" + cData[1] + ").");
            }
        }

        if (cData[2] >= 0)
        {
            var BodyObj = BodyItems.Items.FirstOrDefault(x => x.id == cData[2]);
            if (BodyObj != null)
            {
                BodyItem.loadObject(BodyObj, BodyPlace);
            }
            else
            {
                Debug.LogWarning("No Body Items found with ID (" + cData[2] + ").");
            }
        }

    }

}
