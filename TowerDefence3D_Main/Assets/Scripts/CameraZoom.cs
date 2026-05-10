using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;

    public float scrollSpeed = 15f;
    public float pinchSpeed = 0.1f;
    public float minFOV = 20f;
    public float maxFOV = 70f;



    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 t0Prev = t0.position - t0.deltaPosition;
            Vector2 t1Prev = t1.position - t1.deltaPosition;

            float prevMag = (t0Prev - t1Prev).magnitude;
            float currentMag = (t0.position - t1.position).magnitude;

            float diff = prevMag - currentMag;
            Zoom(diff * pinchSpeed);
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                Zoom(-scroll * scrollSpeed);
            }
        }
    }

    private void Zoom(float increment)
    {
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + increment, minFOV, maxFOV);
    }
}