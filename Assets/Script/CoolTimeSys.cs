using System;
using UnityEngine;


[Serializable]
public struct coolClock
{
    public float time;
    public bool coolStart;
    public bool coolEnd;
    public float timeCount;
}
public class CoolTimeSys : MonoBehaviour
{
    /// <summary>
    /// 시계 갯수
    /// </summary>
    public int coolClockCount = 1;
    int Startindex = 0;
    int indexcode = 0;

    /// <summary>
    /// 시계
    /// </summary>
    public coolClock[] coolclocks;

    /// <summary>
    /// update와 연결될 델리게이트
    /// </summary>
    Action[] Checker;

    /// <summary>
    /// update용 델리게이트
    /// </summary>
    Action updateCoolTime;

    Action CheckCoolTime;



    private void Awake()
    {
        Checker = new Action[coolClockCount];
        coolclocks = new coolClock[coolClockCount];
    }
    void Start()
    {
        updateCoolTime = WeWantNoNull;
        for (int i = 0; i < coolClockCount; i++)
        {
            coolclocks[i].time = 0f;
            coolclocks[i].coolStart = false;
            coolclocks[i].coolEnd = true;
            coolclocks[i].timeCount = 0;
        }
    }
    private void Update()
    {
        updateCoolTime();
    }

    /// <summary>
    /// 외부에서 실행되는 쿨타임 시작 코드
    /// </summary>
    /// <param name="index">시계 인덱스 코드</param>
    /// <param name="time">시간</param>
    public void CoolTimeStart(int index, float time)
    {
        Startindex = index;
        //감시자 설정
        Checker[index] += () => { timeCheck(index, time); };
        //중도 시작인지 검색
        if (startCheck(index))
        {
            //첫 시작일 경우
            coolclocks[index].time = time;
            updateCoolTime += timeCounting;
            coolclocks[index].coolStart = true;
            coolclocks[index].coolEnd = false;
        }
        else
        {
            //중도 시작일 경우 목표 시간 증가
            coolclocks[index].time += time;
        }
        //감시자 입력
        updateCoolTime += Checker[index];
    }

    /// <summary>
    /// 실시간으로 시간이 다 돌았는지 확인하는 코드
    /// </summary>
    /// <param name="index">시계 인덱스 코드</param>
    /// <param name="time">시간</param>
    void timeCheck(int index, float time)
    {
        indexcode = index;
        if (coolclocks[index].timeCount > time)
        {
            coolclocks[index].time = 0f;
            coolclocks[index].coolEnd = true;
            coolclocks[index].coolStart = false;
            updateCoolTime -= Checker[index];
            Checker[index] = null;
            if (EndCheck(index))
            {
                updateCoolTime -= timeCounting;
                coolclocks[index].timeCount = 0f;
            }
        }
    }
    protected void WeWantNoNull()
    {
    }
    void timeCounting()
    {
        coolclocks[indexcode].timeCount += Time.deltaTime;
    }

    /// <summary>
    /// 중도 시작인지 체크하는 bool 함수
    /// </summary>
    /// <returns></returns>
    bool startCheck(int index)
    {
        if (coolclocks[index].coolStart)
        {
            return false;
        }
        return true;
    }
    bool EndCheck(int index)
    {
        if (!coolclocks[index].coolEnd)
        {
            return false;
        }
        return true;
    }
}

