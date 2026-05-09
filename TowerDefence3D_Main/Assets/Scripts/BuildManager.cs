using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("Turret Prefabs")]
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    [Header("Mobile Settings")]
    [Tooltip("The layer mask assigned to your Node objects so the raycast only hits tiles.")]
    public LayerMask nodeLayerMask;

    private Node currentlyHighlightedNode;
    private Camera mainCamera;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMobileInput();
    }

    private void HandleMobileInput()
    {
        // Check if there is at least one touch on the screen
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch");
            Touch touch = Input.GetTouch(0);

            // 1. Raycast from the touch position into the 3D world
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, nodeLayerMask))
            {
                Node touchedNode = hit.collider.GetComponent<Node>();

                if (touchedNode != null)
                {
                    Debug.Log("Node name" + touchedNode.name);
                    // If we touch a new node, unhighlight the old one and highlight the new one
                    if (currentlyHighlightedNode != touchedNode)
                    {
                        ClearCurrentHighlight();

                        // Only highlight if the node is empty
                        if (!touchedNode.HasTurret())
                        {
                            currentlyHighlightedNode = touchedNode;
                            currentlyHighlightedNode.HoverEnter();
                        }
                    }

                    // 2. Build the turret when the user lifts their finger off the screen (TouchPhase.Ended)
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (currentlyHighlightedNode != null && !currentlyHighlightedNode.HasTurret())
                        {
                            BuildTurretOn(currentlyHighlightedNode);
                        }
                        ClearCurrentHighlight();
                    }
                }
                else
                {
                    Debug.Log("Touch node is null i think");
                }
            }
            else
            {
                // If the touch moved off any node, clear the highlight
                ClearCurrentHighlight();
            }
        }
    }

    private void BuildTurretOn(Node node)
    {
        if (turretToBuild == null)
        {
            Debug.LogWarning("No turret selected to build!");
            return;
        }

        Vector3 spawnPosition = node.transform.position + node.positionOffset;
        GameObject turret = Instantiate(turretToBuild, spawnPosition, Quaternion.identity);
        node.SetTurret(turret);

        Debug.Log($"Turret successfully built on {node.gameObject.name} via mobile touch!");
    }

    private void ClearCurrentHighlight()
    {
        if (currentlyHighlightedNode != null)
        {
            currentlyHighlightedNode.HoverExit();
            currentlyHighlightedNode = null;
        }
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectTurretToBuild(GameObject turretPrefab)
    {
        turretToBuild = turretPrefab;
    }
}