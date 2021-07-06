using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraManager : MonoBehaviour
{
    // Main AR Components
    [Header("AR Components")]
    public ARCameraManager arCameraManager;
    public ARPlaneManager arPlaneManager;
    public ARFaceManager arFaceManager;
    public ARSession arSession;
    private bool isBackCamera = true;

    // Game objects
    [Header("Cube Game Object")]
    public GameObject cubeObject;

    [Header("Switch Filter")]
    public GameObject switchFilterButton;

    // Face material index
    [Header("List of face materials")]
    public List<Material> faceMaterialList;
    private int faceMaterialIndex = 0;

    // Public function referenced from the Inspector
    public void SwitchCameras()
    {
        isBackCamera = !isBackCamera;

        arSession.enabled = false;

        if (isBackCamera)
        {
            arPlaneManager.enabled = true;
            arFaceManager.enabled = false;

            arCameraManager.requestedFacingDirection = CameraFacingDirection.World;

            SetBackCameraActive(true);
            SetFrontCameraActive(false);
            switchFilterButton.SetActive(false);
        }
        else
        {
            arPlaneManager.enabled = false;
            arFaceManager.enabled = true;

            arCameraManager.requestedFacingDirection = CameraFacingDirection.User;

            SetBackCameraActive(false);
            SetFrontCameraActive(true);
            switchFilterButton.SetActive(true);
        }

        arSession.enabled = true;
    }


    // Set Back Camera State
    private void SetBackCameraActive(bool backCameraValue)
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(backCameraValue);
        }

        cubeObject.SetActive(backCameraValue);
    }

    // Set Front Camera State
    private void SetFrontCameraActive( bool frontCameraValue)
    {
        foreach (var face in arFaceManager.trackables)
        {
            face.gameObject.SetActive(frontCameraValue);
        }
    }


    // Change material for face manager
    public void SwitchFilter()
    {
        faceMaterialIndex++;

        if (faceMaterialIndex == faceMaterialList.Count)
        {
            faceMaterialIndex = 0;
        }

        foreach (var face in arFaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = faceMaterialList[faceMaterialIndex];
        }
    }
}
