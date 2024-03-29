using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour
{
    MiniMapEntity linkedMiniMapEntity;
    MiniMapController mmc;
    GameObject owner;
    Camera mapCamera;
    Image spr;
    GameObject panelGO;

    Vector3 viewPortPos;
    RectTransform rt;
    Vector3[] cornerss;

    RectTransform sprRect;
    Vector2 screenPos;
    Transform miniMapTarget;

    void FixedUpdate()
    {
        if (owner == null)
            Destroy(this.gameObject);
        else
            SetPositionAndRotation();
    }

    public void SetMiniMapEntityValues(MiniMapController controller, MiniMapEntity mme, GameObject attachedGO, Camera renderCamera, GameObject parentPanelGO)
    {
        linkedMiniMapEntity = mme;
        owner = attachedGO;
        mapCamera = renderCamera;
        panelGO = parentPanelGO;
        spr = gameObject.GetComponent<Image>();
        spr.sprite = mme.icon;
        sprRect = spr.gameObject.GetComponent<RectTransform>();
        sprRect.sizeDelta = mme.size;
        rt = panelGO.GetComponent<RectTransform>();
        mmc = controller;
        miniMapTarget = mmc.target;
        SetPositionAndRotation();
    }

    void SetPositionAndRotation()
    {
        transform.SetParent(panelGO.transform, false);
        SetPosition();
        SetRotation();
    }

    void SetPosition()
    {
        screenPos = mapCamera.WorldToScreenPoint(owner.transform.position); // Changed from RectTransformUtility.WorldToScreenPoint
        if (linkedMiniMapEntity.clampInBorder && Mathf.Abs(Vector3.Distance(owner.transform.position, mmc.target.transform.position)) < linkedMiniMapEntity.clampDist)
        {
            ClampIconColliderWise();
        }
        else
        {
            sprRect.anchoredPosition = screenPos; // Removed unnecessary adjustment
        }
    }

    void ClampIconColliderWise()
    {
        sprRect.anchoredPosition = screenPos; // Removed unnecessary adjustment
        Vector2 diff = (rt.position - sprRect.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(sprRect.position, diff);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.name == mmc.shapeColliderGO.name)
                {
                    sprRect.position = hits[i].point;
                    break;
                }
            }
        }
    }

    void SetRotation()
    {
        if (linkedMiniMapEntity.rotateWithObject || linkedMiniMapEntity.rotateWithObject)
        { // Consider both cases
            Vector3 euler = Vector3.zero;
            if (Mathf.Abs(linkedMiniMapEntity.upAxis.y) == 1)
            {
                euler.z = linkedMiniMapEntity.upAxis.y * (miniMapTarget.transform.localEulerAngles.y - mmc.rotationOfCam.z - owner.transform.localEulerAngles.y) + linkedMiniMapEntity.rotation;
            }
            else if (Mathf.Abs(linkedMiniMapEntity.upAxis.z) == 1)
            {
                euler.z = linkedMiniMapEntity.upAxis.z * (miniMapTarget.transform.localEulerAngles.z - mmc.rotationOfCam.z - owner.transform.localEulerAngles.z) + linkedMiniMapEntity.rotation;
            }
            else if (Mathf.Abs(linkedMiniMapEntity.upAxis.x) == 1)
            {
                euler.z = linkedMiniMapEntity.upAxis.x * (miniMapTarget.transform.localEulerAngles.x - mmc.rotationOfCam.z - owner.transform.localEulerAngles.x) + linkedMiniMapEntity.rotation;
            }
            sprRect.localEulerAngles = euler;
        }
        else
        {
            Vector3 euler = sprRect.localEulerAngles;
            if (Mathf.Abs(linkedMiniMapEntity.upAxis.y) == 1)
            {
                euler.z += linkedMiniMapEntity.rotation;
            }
            else if (Mathf.Abs(linkedMiniMapEntity.upAxis.z) == 1)
            {
                euler.z += linkedMiniMapEntity.rotation;
            }
            else if (Mathf.Abs(linkedMiniMapEntity.upAxis.x) == 1)
            {
                euler.z += linkedMiniMapEntity.rotation;
            }
            sprRect.localEulerAngles = euler;
        }
    }
}
