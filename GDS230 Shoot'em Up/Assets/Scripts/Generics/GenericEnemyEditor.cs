using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenericEnemy))]
public class GenericEnemyEditor : Editor
{
    private void OnSceneGUI()
    {
        GenericEnemy GE = target as GenericEnemy;
        Handles.ArrowHandleCap(0 ,GE.transform.position , Quaternion.LookRotation(-GE.transform.right), 0.4f, EventType.Repaint);
        if (GE.EnemySpotted)
        {
            Handles.color = Color.red;
        }
        else
        {

            Handles.color = Color.green;
        }
        Handles.SphereHandleCap(0, GE.transform.position + new Vector3(0,0.4f), Quaternion.identity, 0.1f, EventType.Repaint);
    }
}
