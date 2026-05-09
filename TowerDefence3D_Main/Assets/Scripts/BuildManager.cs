using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("Turret Prefabs")]
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;
    
    public LayerMask nodeLayerMask;

    private Node currentlyHighlightedNode;
    private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, nodeLayerMask))
            {
                Node touchedNode = hit.collider.GetComponent<Node>();

                if (touchedNode != null)
                {
                    if (currentlyHighlightedNode != touchedNode)
                    {
                        ClearCurrentHighlight();

                        if (!touchedNode.HasTurret())
                        {
                            currentlyHighlightedNode = touchedNode;
                            currentlyHighlightedNode.HoverEnter();
                        }
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (currentlyHighlightedNode != null && !currentlyHighlightedNode.HasTurret())
                        {
                            BuildTurretOn(currentlyHighlightedNode);
                        }
                        ClearCurrentHighlight();
                    }
                }  
            }
            else
            {
                ClearCurrentHighlight();
            }
        }
    }

    private void BuildTurretOn(Node node)
    {
        Vector3 spawnPosition = node.transform.position + node.positionOffset;
        GameObject turret = Instantiate(GetTurretToBuild(), spawnPosition, Quaternion.identity);
        node.SetTurret(turret);
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