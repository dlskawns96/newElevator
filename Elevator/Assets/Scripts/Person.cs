using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour
{

    private int startF = 0;// 1 ~ 10
    private int depF = 0;// 1 ~10
    private int weight = 0;
    private bool evenUp = false;// 가려는 목적지가 짝수 위 버튼
    private bool evenDown = false;// 짝수 아래 버튼
    private bool oddUp = false;//홀수 위로 버튼 
    private bool oddDown = false;//홀수 아래 버튼
    private bool useTotal = false;// 전체층수를 해야되는가.

    private bool isArrival = false;// 원하는층에 갔는가
    private bool isBoarding = false;// 탑승상태

    private int startTime = 0;
    static int max = 10;// 1~ 10

    Random random = new Random();

    void Start()
    {
        while (true)
        {

            startF = makeFloor();
            depF = makeFloor();

            if (startF != depF)
                break;

        }

        weight = makeWeight();

        isArrival = false;
        isBoarding = false;
        startTime = 0;

        evenUp = false;
        evenDown = false;
        oddUp = false;
        oddDown = false;
        useTotal = false;
        makeBoolean();
        this.transform.Translate(0, startF, 0);

    }

    public int makeWeight()
    {// weight값을 랜덤 값으로 만든다.
        int tmp = 0;
        tmp = Random.Range(50, 70);// 50 ~ 70
        return tmp;
    }
    public int makeFloor()
    {// 전체층수에서 가려는 층을 랜덤으로 만든다.
        int tmp = 0;
        tmp = Random.Range(1, 10);

        return tmp;
    }

    public void makeBoolean()
    {// 누르는 버튼을 초기화 ㅎ준다.
        if ((depF % 2) == 1)
        { // 목적지가 홀수층 
            if (depF - startF > 0)
            {// 위로
                oddUp = true;
            }
            else
            {
                oddDown = true;
            }
        }
        else
        {// 목적지 짝수층
            if (depF - startF > 0)
            {// 위로
                evenUp = true;
            }
            else
            {
                evenDown = true;
            }
        }
        if (startF != 0)
        {
            if ((depF - startF) % 2 == 1)
            {
                useTotal = true;
            }
        }
    }
    public int getTime()
    {
        return startTime;
    }
    public int getWeight()
    {
        return weight;
    }
    public int getStart()
    {
        return startF;
    }
    public int getDep()
    {
        return depF;
    }
    public int makeTime()
    {
        int tmp = 0;
        tmp = Random.Range(60, 60);

        return tmp;
    }
    public bool getEvenDown()
    {
        return evenDown;
    }
    public bool getEvenUp()
    {
        return evenDown;
    }
    public bool getOddDown()
    {
        return oddDown;
    }
    public bool getOddUp()
    {
        return oddUp;
    }
    public bool getTotal()
    {
        return useTotal;
    }
}