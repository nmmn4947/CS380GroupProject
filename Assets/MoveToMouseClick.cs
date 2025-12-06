using UnityEngine;

public class MoveToMouseClick : MonoBehaviour
{
    Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                transform.position = hitInfo.point + hitInfo.normal * (transform.localScale.x / 2);
            }
        }
    }
}
