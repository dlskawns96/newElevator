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

    // Boarding이 허락이 나면 Elevator_Opration에 있는 request()를 만들어서 Person객체 넘겨주기
    // Run() 만들기 - odd, even, total 중 어느 엘리베이터의 run()을 불러올지 결정
}
