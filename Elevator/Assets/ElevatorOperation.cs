using UnityEngine;
using System.Collections;

public class ElevatorOperation : MonoBehaviour {

    public static int top = 10;
    public static int bottom = 1;
    public int current_floor;// 1~ 10
    public int current_weight;
    public int max_weight;
    public int dest; // current Top & bottom  1~10
    public bool[] stop = new bool[10]; // 이 곳에서 서야하는가
    public bool goingUp;// 전체적인 방향성
    public bool stopping;// 손님을 내리기 위해서 멈추었는가
    public bool isWork; // 일하고 있는지 아닌지
    public Person[] passenger = new Person[20]; // 탑승객 리스트

    void Start()
    {
        int i;
        current_floor = 1;
        current_weight = 0;
        max_weight = 1000;
        goingUp = true;
        dest = 1;
        stopping = true;
        isWork = false;
        for (i = 0; i < top; i++)
            stop[i] = false;
    }

    public void check_arrive()
    {
        if (stop[current_floor - 1])
        {
            stopping = true;
            stop[current_floor - 1] = false;
        }
    }

    public void departure()
    { // isArrive를 완료시켰다면...이것을 이용하여...추울발!
        stopping = false;
    }

    public bool boarding(int person_weight)
    { // 사람들이 탈수있는지 보는 것 입니다.
        if (current_weight + person_weight <= max_weight)
            return true; // 탔음을 알립니다.
        else
            return false; // 몸무게 때매 못탔어요.
    }

    public void UpdateDest(int person_dest) // 현재 진행 방향에서 가장 높거나 낮은 곳을 업데이트, 지금 탄 고객이 내려야할 곳 업데이트
    {
        stop[person_dest - 1] = true; // 지금 탄 고객이 내려야 할 곳 체크
        if(goingUp == true) // 올라 갈 때
        {
            if(person_dest > dest) // 지금 탄 사람의 목적지가 제일 높으면 업데이트
            {
                dest = person_dest;
            }
        }
        else // 내려 갈 때
        {
            if(person_dest < dest) // 지금 탄 사람의 목적지가 제일 낮으면 업데이트
            {
                dest = person_dest;
            }
        }
    }

    public void minum_weight(int person_weight)
    { // 내리는 사람 몸무게를 빼주어...엘리베이터에서 내리게 하는 것입니다...
        current_weight = current_weight - person_weight;
    }

    public int estimate_time(int request_floor, bool goUp)
    {
        int time = 0;

        //방향

        if (stopping == true)
        {// 일 안하고 있을 경우
            time = 0;// stopping -> isWork로 바꿔줘요
        }
        else if (goUp == goingUp && goingUp == true)
        { //가는 방향이 같고 위로 가는 경우
            if (current_floor > request_floor)
            { // 경로상에 없는 경우
                time += dest - current_floor;
                for (int i = current_floor - 1; i < dest; i++)
                {
                    if (stop[i] == true)
                    {
                        time++;
                    }
                }
                time += dest - request_floor;
            }
            else
            {// 태울수 있는 경우
                time += request_floor - current_floor;

            }

        }
        else if (goUp == goingUp && goingUp == false)
        {//가는 방향이 같고 아래로 가는 경우
            if (current_floor < request_floor)
            {// 경로상에 없는 경우
                time += current_floor - dest;
                for (int i = current_floor - 1; i > dest; i--)
                {
                    if (stop[i] == true)
                    {
                        time++;
                    }
                }
                time += request_floor - dest;

            }
            else
            {// 태울수 있는 경우
                time += current_floor - request_floor;

            }
        }
        else if (goingUp == true)
        {// 위로 올라가고 눌러진 버튼은 아래
            time += dest - current_floor;
            for (int i = current_floor - 1; i < dest; i++)
            {
                if (stop[i] == true)
                {
                    time++;
                }
            }
            time += dest - request_floor;
        }
        else if (goingUp == false)
        {
            time += current_floor - dest;
            for (int i = current_floor - 1; i > dest; i--)
            {
                if (stop[i] == true)
                {
                    time++;
                }
            }
            time += request_floor - dest;
        }

        return time;
    }
}
