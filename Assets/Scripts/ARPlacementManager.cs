using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Detects a horizontal AR plane via ARFoundation, lets the player tap to place
/// the game root (arena + all spawners) on that plane, then disables AR scanning UI.
/// The existing game scripts are completely untouched.
/// </summary>
public class ARPlacementManager : MonoBehaviour
{
    [Header("AR References")]
    public ARRaycastManager arRaycastManager;
    public ARPlaneManager arPlaneManager;

    [Header("Game Root")]
    [Tooltip("The parent GameObject that holds the entire game world (Environment, SpawnPoints, etc.)")]
    public GameObject gameRoot;

    [Header("Placement UI")]
    [Tooltip("Panel shown while scanning for a plane")]
    public GameObject scanningUI;
    [Tooltip("Text shown while scanning")]
    public Text instructionText;

    bool placed = false;
    static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [Header("Placement Settings")]
    [Tooltip("Vertical offset applied after placement to keep the game root slightly above the detected plane")]
    public float placementYOffset = 0.02f;
    [Tooltip("Vertical offset applied to the player above the plane (meters)")]
    public float playerYOffset = 0.05f;
    [Tooltip("Vertical offset applied to enemy spawn points above the plane (meters)")]
    public float spawnPointYOffset = 0.02f;

    void Start()
    {
#if UNITY_EDITOR
        // Disable AR components to suppress subsystem warnings in Editor
        if (arPlaneManager) arPlaneManager.enabled = false;
        if (arRaycastManager) arRaycastManager.enabled = false;
        if (scanningUI) scanningUI.SetActive(false);
        placed = true;
        return;
#endif
        // Hide the game world until a plane is tapped
        gameRoot.SetActive(false);
        if (scanningUI) scanningUI.SetActive(true);
        if (instructionText) instructionText.text = "Point your camera at a flat surface and tap to place the game.";
    }

    void Update()
    {
        if (placed) return;

        // Wait until at least one plane is detected before accepting taps
        if (arPlaneManager.trackables.count == 0) return;

        if (instructionText)
            instructionText.text = "Plane detected! Tap to place the game.";

        // Detect touch input
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        // Raycast against detected planes
        if (!arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon)) return;

        Pose hitPose = hits[0].pose;

        PlaceGame(hitPose);
    }

    void PlaceGame(Pose pose)
    {
        // Place and activate the game world slightly above the plane to avoid clipping
        Vector3 placedPosition = pose.position + pose.up * placementYOffset;
        gameRoot.transform.SetPositionAndRotation(placedPosition, pose.rotation);
        gameRoot.SetActive(true);

        // Align player and spawn points to the detected plane height so they sit on the floor
        AlignGameObjectsToPlane(pose.position.y);


    void AlignGameObjectsToPlane(float planeY)
    {
        // Align player (find by tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 p = player.transform.position;
            player.transform.position = new Vector3(p.x, planeY + playerYOffset, p.z);
        }

        // Align spawn points referenced by any EnemyFactory instances
        var factories = FindObjectsOfType<EnemyFactory>();
        foreach (var f in factories)
        {
            if (f.spawnPoints == null) continue;
            for (int i = 0; i < f.spawnPoints.Length; i++)
            {
                if (f.spawnPoints[i] == null) continue;
                Vector3 s = f.spawnPoints[i].position;
                f.spawnPoints[i].position = new Vector3(s.x, planeY + spawnPointYOffset, s.z);
            }
        }
    }
        // Switch detected planes to occlusion so game appears grounded
        foreach (var plane in arPlaneManager.trackables)
        {
            CustomPlaneVisualizer viz = plane.GetComponent<CustomPlaneVisualizer>();
            if (viz != null) viz.SwitchToOcclusion();
            else plane.gameObject.SetActive(false);
        }

        // Stop detecting new planes
        arPlaneManager.enabled = false;

        // Hide scanning UI
        if (scanningUI) scanningUI.SetActive(false);

        placed = true;
    }
}
