using UnityEngine;
using System.Collections;

public class ElevatorContoller : MonoBehaviour {

    static int personNumber = 10;
    static int curFloor = 1;
    public float duration = 5.0f;

    private void Start()
    {
        StartCoroutine(moveElevator((new Vector3(0, 5, 0)),duration));
    }

    private IEnumerator moveElevator(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float elapsedTime = 0.0f;

        while(transform.position != target)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            yield return null; 
        }
    }
}
