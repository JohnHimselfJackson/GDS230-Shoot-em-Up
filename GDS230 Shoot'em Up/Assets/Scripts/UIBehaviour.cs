using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public void Embiggen(Transform myObject)
    {
        Debug.Log("Mouse Entered!");
        myObject.transform.localScale += new Vector3(0.1F, 0, 0);
    }

    public void Smally(Transform myObject)
    {
        Debug.Log("Mouse Exit!");
        myObject.transform.localScale -= new Vector3(0.1F, 0, 0);
    }
}
