using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionMod : MonoBehaviour
{
    private OVRPassthroughLayer passthroughLayer;
    public MeshFilter projectionObject;
    MeshRenderer quadOutline;

    private bool addedToSurface;

    void Start()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
        }
        
        passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject, true);
        addedToSurface = true;

        // The MeshRenderer component renders the quad as a blue outline
        // we only use this when Passthrough isn't visible
        quadOutline = projectionObject.GetComponent<MeshRenderer>();
        quadOutline.enabled = false;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && addedToSurface)
        {
            passthroughLayer.RemoveSurfaceGeometry(projectionObject.gameObject);
            addedToSurface = false;
            quadOutline.enabled = true;
        }
        if (OVRInput.Get(OVRInput.Button.One))
        {
            OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
            transform.position = OVRInput.GetLocalControllerPosition(controllingHand);
            transform.rotation = OVRInput.GetLocalControllerRotation(controllingHand);
        }
        if (OVRInput.GetUp(OVRInput.Button.One) && !addedToSurface)
        {
            passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject);
            addedToSurface = true;
            quadOutline.enabled = false;
        }
        
        // Hide object when A button is held, show it again when button is released, move it while held.
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (addedToSurface)
            {
                addedToSurface = false;
                passthroughLayer.RemoveSurfaceGeometry(projectionObject.gameObject);
                quadOutline.enabled = true;
            }
            else
            {
                addedToSurface = true;
                passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject);
                quadOutline.enabled = false;
            }
        }
        if (OVRInput.Get(OVRInput.Button.One))
        {
            OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
            transform.position = OVRInput.GetLocalControllerPosition(controllingHand);
            transform.rotation = OVRInput.GetLocalControllerRotation(controllingHand);
        }


    }
}
