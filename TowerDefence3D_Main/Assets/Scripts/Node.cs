using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Visual Feedback")]
    public Color hoverColor = Color.gray;
    public Vector3 positionOffset;

    private GameObject turretOnNode;
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

    // Called by BuildManager to show visual feedback (Hovering)
    public void HoverEnter()
    {
        if (rend != null && turretOnNode == null)
        {
            rend.material.color = hoverColor;
        }
    }

    // Called by BuildManager to reset the visual feedback
    public void HoverExit()
    {
        if (rend != null)
        {
            rend.material.color = startColor;
        }
    }

    // Helper method to check if this tile already has a turret
    public bool HasTurret()
    {
        return turretOnNode != null;
    }

    // Assigns the built turret to this tile
    public void SetTurret(GameObject turret)
    {
        turretOnNode = turret;
    }
}