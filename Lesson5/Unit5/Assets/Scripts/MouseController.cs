using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool isMouseDown = false;
    public GameObject trailGameObject;

    private float distanceFromCamera = 5.0f;
    private Camera mainCamera;
    private Transform trailTransform;
    private GameManager gameManager;

    void Start()
    {
        mainCamera = Camera.main;
        trailGameObject.SetActive(false);
        gameManager = GetComponent<GameManager>();
    }

    private void Update() {
        if (gameManager.isGameActive)
        {
            MoveTrailToCursor(Input.mousePosition);

            if (Input.GetMouseButtonDown(1))
            {
                isMouseDown = true;
                trailGameObject.SetActive(true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                isMouseDown = false;
                trailGameObject.SetActive(false);
            }
        }
    }

    void MoveTrailToCursor(Vector3 screenPosition)
    {   
        Vector3 point = new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera);
        trailGameObject.transform.position = mainCamera.ScreenToWorldPoint(point);
    }
}
