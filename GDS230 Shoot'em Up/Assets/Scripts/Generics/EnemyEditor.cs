using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenericEnemy))]
public class EnemyEditor : Editor
{
    private     void OnSceneGUI()
    {
        GenericEnemy GE = target as GenericEnemy;
        Handles.ArrowHandleCap(0 ,GE.transform.position , Quaternion.LookRotation(GE.transform.right), 1, EventType.Repaint);
    }
}
