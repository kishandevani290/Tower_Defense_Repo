using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            camTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (camTransform == null) return;
        transform.rotation = Quaternion.LookRotation(transform.position - camTransform.position);
    }
}