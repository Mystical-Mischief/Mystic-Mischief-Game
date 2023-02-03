using UnityEditor;
using UnityEngine;

// #if false
[CustomEditor(typeof(EnemyVision))]
public class EnemyVisionEditor : Editor
{
   private void OnSceneGUI()
    {
        EnemyVision enemy = (EnemyVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.viewRadius);

        Vector3 angleA = DirectionFromAngle(enemy.transform.eulerAngles.y, -enemy.viewAngle / 2);
        Vector3 angleB = DirectionFromAngle(enemy.transform.eulerAngles.y, enemy.viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemy.transform.position,enemy.transform.position + angleA * enemy.viewRadius);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + angleB * enemy.viewRadius);

        if(enemy.PlayerDetected)
        {
            Handles.color = Color.green;
            Handles.DrawLine(enemy.transform.position, enemy.Player.transform.position);
        }

    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees +=eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
// #endif
