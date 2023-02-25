using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DrawPotionProjection : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public Rigidbody SmokeBomb;

    [SerializeField]
    private int numPoints = 50; //points in the line

    [SerializeField]
    private float _timeBetweenPoints = 0.1f;

    public LayerMask CollideableLayers;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        DrawProjection();
    }

    void DrawProjection()
    {
        _lineRenderer.positionCount = Mathf.CeilToInt(numPoints/_timeBetweenPoints) + 1;
        Vector3 startPos = transform.forward;
        Vector3 startVelocity =10*transform.forward/SmokeBomb.mass;
        int i = 0;
        _lineRenderer.SetPosition(i,startPos);
        Vector3 point = Vector3.zero;
        for(float time = 0; time < numPoints; time += _timeBetweenPoints)
        {
            i++;
            point = startPos + time * startVelocity;
            point.y = startPos.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            _lineRenderer.SetPosition(i,point);
        }
    }
}
