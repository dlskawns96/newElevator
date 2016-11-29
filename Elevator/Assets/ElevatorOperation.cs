﻿using UnityEngine;
using System.Collections;

public class ElevatorOperation : MonoBehaviour {

    private static int top = 10;
    private static int bottom = 1;
    private int current_floor;// 1~ 10
    private int current_weight;
    private int max_weight;
    private int dest; // current Top & bottom  1~10
    private bool[] stop = new bool[10];
    private bool goingUp;// 전체적인 방향성
    private bool stopping;// 손님을 내리기 위해서 멈추었는가
    private bool isWork; // 일하고 있는지 아닌지
    private Person[] passenger = new Person[20]; // 탑승객 리스트

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

    // request(Person next_person) 만들기 - 탑승이 허락된 승객의 정보를 업데이트 하여 dest와 stop[]을 업데이트 해주어 어디서 정지해야할지 결정
    // Run()만들기 - 엘리베이터가 다음 턴에 수행할 작업을 처리해주는 메소드
    // Person의 isArrive와 isBoarding을 처리해 줄 방법 만들기

    public int getDest()
    {
        return dest;
    }

    public void setDest(int dest)
    { // 목적지를 받아주는 Setter method
        this.dest = dest;
    }

    public bool getStopping()
    {
        return stopping;
    }

    public bool getGoingUp()
    {
        return goingUp;
    }

    public int getCurrentFloor()
    {
        return current_floor;
    }

    public bool getisWork()
    {
        return isWork;
    }

    public void up()
    { //엘리베이터가 올라갈 때 실행
        if (current_floor < dest && stopping == false)
        { // 제일 마지막 목적지에 도달하지 않고 움직이고 있다면...실행됩니다...
          //모든 사람들은 본인의 목적지를 가지고 올라가기 시작합니다...
            current_floor = current_floor + 1;
        }
        else
        {
            //또한 꼭대기에 올라왔으니....내려가야 겠지요...
            goingUp = false;
        }
    }

    public void down()
    { //엘리베이터가 내려갈 때 실행
        if (current_floor > dest && stopping == false)
        {// 제일 마지막 목적지에 도달하지 않고 움직이고 있다면...실행됩니다...
         //모든 사람들이 목적지를 가지고 내려가기 시작합니다.
            current_floor = current_floor - 1;
        }
        else
        {
            //또한 밑바닥을 찍었으니...올라가야겠지요...
            goingUp = true;
        }
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

    public bool boarding(int person_weight, int person_dest)
    { // 사람들이 탈수있는지 보는 것 입니다.
        if (current_weight + person_weight <= max_weight)
        { // 만약 몸무게가 허용가능하다면 탈 수 있겠지요...
            current_weight = current_weight + person_weight; // 몸무게를 업데이트 해줍니다.
            stop[person_dest - 1] = true; // 그 사람이 내릴 곳을 체크해줍니다.
            if (goingUp == true && person_dest > dest)
            { // 방향이 같고 방금 탄 사람의 목적지가 제일 높으면 업데이트 해줍니다.
                dest = person_dest;
            }
            else if (goingUp == false && person_dest < dest)
            { // 위와 같이 이건 목적지가 제일 낮으면 업데이트 하는 것.
                dest = person_dest;
            }
            return true; // 탔음을 알립니다.
        }
        else
        {
            return false; // 몸무게 때매 못탔어요.
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
