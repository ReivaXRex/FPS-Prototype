using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] RigidbodyFirstPersonController rbFPSController;

    [SerializeField] private float zoomedIn = 20f;
    [SerializeField] private float zoomedOut = 60f;
    [SerializeField] private float zoomedInSensitivity = 0.5f;
    [SerializeField] private float zoomedOutSensitivity = 2f;

    private bool zoomedInToggle;

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();

            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        cam.fieldOfView = zoomedOut;
        rbFPSController.mouseLook.XSensitivity = zoomedOutSensitivity;
        rbFPSController.mouseLook.YSensitivity = zoomedOutSensitivity;
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        cam.fieldOfView = zoomedIn;
        rbFPSController.mouseLook.XSensitivity = zoomedInSensitivity;
        rbFPSController.mouseLook.YSensitivity = zoomedInSensitivity;
    }
}