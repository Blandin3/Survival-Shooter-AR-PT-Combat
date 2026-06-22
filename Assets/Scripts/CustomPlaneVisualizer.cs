using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Attach to the AR Plane Prefab assigned in ARPlaneManager.
/// Shows a scanning grid while detecting planes, switches to
/// occlusion-only material once the game is placed so the
/// game world appears grounded on the real surface.
/// </summary>
[RequireComponent(typeof(ARPlaneMeshVisualizer))]
[RequireComponent(typeof(MeshRenderer))]
public class CustomPlaneVisualizer : MonoBehaviour
{
    [Tooltip("Scanning grid material shown while detecting planes")]
    public Material scanningMaterial;

    [Tooltip("Occlusion material used after game is placed — use ARPlaneOcclusion shader")]
    public Material occlusionMaterial;

    MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (scanningMaterial != null)
            meshRenderer.material = scanningMaterial;
    }

    /// <summary>
    /// Called by ARPlacementManager after the game is placed.
    /// Switches to occlusion so the plane becomes invisible but still
    /// blocks virtual objects from clipping through the real surface.
    /// </summary>
    public void SwitchToOcclusion()
    {
        if (occlusionMaterial != null)
            meshRenderer.material = occlusionMaterial;
    }
}
