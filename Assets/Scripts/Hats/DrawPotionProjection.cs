using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DrawPotionProjection : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField][Range(3f, 100f)]
    private int _lineSegments = 60;

    [SerializeField][Range(1f, 10f)]
    private float _timeOfTheFlight = 5;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectoryLine(Vector3 startpoint, Vector3 startVelocity)
    {
        float timeStep = _timeOfTheFlight / _lineSegments;

        Vector3[] lineRenderPoints = CalculateTrajectoryLine(startpoint, startVelocity, timeStep);
        
        lineRenderer.positionCount = _lineSegments;
        lineRenderer.SetPositions(lineRenderPoints);
    }

    private Vector3[] CalculateTrajectoryLine(Vector3 startpoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRenderPoints = new Vector3[_lineSegments];
        lineRenderPoints[0] = startpoint;

        for(int i = 1; i < _lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startpoint + progressBeforeGravity - gravityOffset;

            lineRenderPoints[i] = newPosition;
        }
        return lineRenderPoints;

    }
}
