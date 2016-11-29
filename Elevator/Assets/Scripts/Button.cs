using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

    public GameObject toClone;
    public void OnClick()
    {
        Object.Instantiate(toClone, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
    }
}