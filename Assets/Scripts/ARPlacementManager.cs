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

    void Start()
    {
#if UNITY_EDITOR
        // In the Editor, skip AR entirely and just start the game normally
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
        // Place and activate the game world
        gameRoot.transform.SetPositionAndRotation(pose.position, pose.rotation);
        gameRoot.SetActive(true);

        // Disable ongoing plane scanning to save performance
        arPlaneManager.enabled = false;
        foreach (var plane in arPlaneManager.trackables)
            plane.gameObject.SetActive(false);

        // Hide scanning UI
        if (scanningUI) scanningUI.SetActive(false);

        placed = true;
    }
}
