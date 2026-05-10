using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("Turret Prefabs")]
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    public LayerMask nodeLayerMask;

    private Node currentlyHighlightedNode;
    private Camera mainCamera;

    private List<GameObject> activeTurrets = new List<GameObject>();

    public GameObject SelectedTurret
    {
        get { return turretToBuild; }
        set { turretToBuild = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 80;
    }

    private void Start()
    {
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
            if (EventSystem.current.IsPointerOverGameObject()) return;
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
        if (ShopManager.manager.currentSelected_turnet.Turnet_stock > 0)
        {
            ShopManager.manager.currentSelected_turnet.Turnet_stock--;
            ShopManager.manager.currentSelected_turnet.txt_turnet_stock_value.text = ShopManager.manager.currentSelected_turnet.Turnet_stock.ToString();
            Vector3 spawnPosition = node.transform.position + node.positionOffset;
            GameObject turret = Instantiate(GetTurretToBuild(), spawnPosition, Quaternion.identity);
            node.SetTurret(turret);
            activeTurrets.Add(turret);
        }
        else
        {
            
        }
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

    public void ClearAllTowerss()
    {
        foreach (GameObject turret in activeTurrets)
        {
            if (turret != null)
            {
                // Find the node this turret was placed on and clear its reference
                // (Assumes your turret has a reference to its Node or you fetch it from parent)
                Node nodeOfTurret = turret.GetComponentInParent<Node>();
                if (nodeOfTurret != null)
                {
                    // This tells the Node that it is empty again so we can build there in the next level
                    nodeOfTurret.SetTurret(null);
                }
                Destroy(turret);
            }
        }
        // Empty the list after destroying everything
        activeTurrets.Clear();
        ShopManager.manager.Update_shop();
    }
}