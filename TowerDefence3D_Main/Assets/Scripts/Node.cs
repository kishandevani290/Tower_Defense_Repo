using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Visual Feedback")]
    public Color hoverColor = Color.gray;
    public Vector3 positionOffset;

    private GameObject turretOnNode;
    [SerializeField] private GameObject SelectionOutline;
    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            startColor = rend.material.color;
        }
    }
    
    public void HoverEnter()
    {
        if (rend != null && turretOnNode == null)
        {
            SelectionOutline.SetActive(true);
        }
    }
    
    public void HoverExit()
    {
        if (rend != null)
        {
            SelectionOutline.SetActive(false);
        }
    }
    
    public bool HasTurret()
    {
        return turretOnNode != null;
    }
    
    public void SetTurret(GameObject turret)
    {
        turretOnNode = turret;
    }
}