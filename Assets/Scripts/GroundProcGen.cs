using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProcGen : MonoBehaviour
{
    [SerializeField] private GameObject landingPad;
    private EdgeCollider2D edgeCollider2D;
    private LineRenderer lineRenderer;

    private Vector2[] points;
    //private Transform[] pointsV3;
    private Vector2 cachedPoint;
    private Vector3 cachedPointV3;
    private Vector2 landingR;

    private int edgeColliderSize;
    private int pointsIndex;
    private int landingSpotR;
    private int landingSpotL;

    private GameObject currentLandingPad;


    void Start()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        edgeColliderSize = edgeCollider2D.pointCount;
        lineRenderer.positionCount = edgeColliderSize;
        Debug.Log("edgeColliderSize =" + edgeColliderSize);
        points = edgeCollider2D.points;
        pointsIndex = 0;
        SetPoints();
    }

    void SetPoints()
    {
        foreach (Vector2 point in points)
        {
            points[pointsIndex] = new Vector2(pointsIndex, Random.Range(1, 30));
            cachedPoint = points[pointsIndex];
            cachedPointV3 = cachedPoint;
            lineRenderer.SetPosition(pointsIndex, cachedPointV3);

            pointsIndex++;
        }
        landingSpotR = Random.Range(5, edgeColliderSize - 5);
        landingSpotL = landingSpotR - 1;
        landingR = points[landingSpotR];
        points[landingSpotL] = new Vector2(landingR.x - 1, landingR.y);
        cachedPoint = points[landingSpotL];
        cachedPointV3 = cachedPoint;
        lineRenderer.SetPosition(landingSpotL, cachedPointV3);
        edgeCollider2D.points = points;
        PlaceLandingPad();
        Debug.Log("landing Spot =" + landingSpotL + landingSpotR);
    }

    void PlaceLandingPad()
    {
        currentLandingPad = Instantiate(landingPad, new Vector2((landingR.x * transform.localScale.x) - 2, landingR.y + 0.8f), Quaternion.identity);
    }

   /* public void ResetGround()
    {
        pointsIndex = 0;
        Destroy(currentLandingPad);
        currentLandingPad = null;
        SetPoints();
    }*/
}

