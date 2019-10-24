using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static List<Transform> allPlayers = new List<Transform>();
    public float minDist;
    private void Start()
    {
        var playerGOs = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject e in playerGOs){
            allPlayers.Add(e.transform);
        }
    }
    private Transform[] FindtwoFarthestPlayers()
    {
        Transform[] returnPlayers = new Transform[] {allPlayers[0], allPlayers[1]};
        float mostDist = Vector2.Distance(returnPlayers[0].position, returnPlayers[1].position);
        foreach(Transform e in allPlayers)
        {
            foreach(Transform f in allPlayers)
            {
                if (e.name != f.name)
                {
                    if (Vector2.Distance(e.position, f.position) > mostDist)
                    {
                        mostDist = Vector2.Distance(e.position, f.position);
                        returnPlayers[0] = e;
                        returnPlayers[1] = f;
                    }
                }
            }
        }
        return returnPlayers;
    }
    // Follow Two Transforms with a Fixed-Orientation Camera
    private void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        // How many units should we keep from the players
        float zoomFactor = 1.05f;
        float followTimeDelta = 0.05f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;
        if(distance < minDist)
        {
            distance = minDist;
        }
        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            // The camera's forward vector is irrelevant, only this size will matter
            cam.orthographicSize = distance;
        }
        // You specified to use MoveTowards instead of Slerp
        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraDestination, followTimeDelta);

    }
    private void Update()
    {
        var twoCurrentFarthestPlayers = FindtwoFarthestPlayers();
        FixedCameraFollowSmooth(Camera.main, twoCurrentFarthestPlayers[0], twoCurrentFarthestPlayers[1]);
    }
}
