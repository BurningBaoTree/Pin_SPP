using System;
using System.Runtime.CompilerServices;
using UnityEngine;


[Serializable]
public struct coolClock
{
    public float time;
    public bool coolStart;
    public bool coolEnd;
    public float timeCount;
    public Action CoolEndAction;
}
public class CoolTimeSys : MonoBehaviour
{
    /// <summary>
    /// 시계 갯수
    /// </summary>
    public int coolClockCount = 1;

    /// <summary>
    /// 시계
    /// </summary>
    public coolClock[] coolclocks;

    /// <summary>
    /// update와 연결될 감시자 델리게이트
    /// </summary>
    Action[] Checker;

    /// <summary>
    /// update용 델리게이트
    /// </summary>
    Action updateCoolTime;


    /// <summary>
    /// 감시자 델리게이트, 시계 구조체 설정 갯수만큼 생성
    /// </summary>
    private void Awake()
    {
        Checker = new Action[coolClockCount];
        coolclocks = new coolClock[coolClockCount];
    }

    /// <summary>
    /// null방지 및 각 시계 초기화
    /// </summary>
    private void OnEnable()
    {
        updateCoolTime = () => { };
        AllClocksReset();
    }

    /// <summary>
    /// 시간 카운트용 업데이트 델리게이트
    /// </summary>
    private void Update()
    {
        updateCoolTime();
    }

    /// <summary>
    /// 실시간으로 시간이 다 돌았는지 확인하는 코드(감시자)
    /// </summary>
    /// <param name="index">시계 인덱스 코드</param>
    /// <param name="time">시간</param>
    void timeCheck(int index, float time)
    {
        //시계에 돌고있는 계산용 시간이 입력 시간보다 클 경우
        if (coolclocks[index].timeCount > time)
        {
            //입력시간을 0으로 만들고
            coolclocks[index].time = 0f;

            //쿨타임이 끝났음
            coolclocks[index].coolEnd = true;
            coolclocks[index].coolStart = false;
            coolclocks[index].CoolEndAction?.Invoke();
            coolclocks[index].CoolEndAction = null;

            //감시자 제거
            updateCoolTime -= Checker[index];

            //감시자 비우기
            Checker[index] = null;

            //계산용 시간 초기화
            coolclocks[index].timeCount = 0f;
        }
    }

    /// <summary>
    /// 시간 중첩용 함수
    /// </summary>
    void timeCounting(int index)
    {
        coolclocks[index].timeCount += Time.deltaTime;
    }

    /// <summary>
    /// 외부에서 실행되는 쿨타임 시작 코드
    /// </summary>
    /// <param name="index">시계 인덱스 코드</param>
    /// <param name="time">시간</param>
    public void CoolTimeStart(int index, float time)
    {
        //감시자 설정
        Checker[index] += () =>
        {
            timeCounting(index);
            timeCheck(index, time);
        };
        //중도 시작인지 검색
        if (!coolclocks[index].coolStart)
        {
            //첫 시작일 경우
            coolclocks[index].time = time;
            coolclocks[index].coolStart = true;
            coolclocks[index].coolEnd = false;
        }
        else
        {
            //중도 시작일 경우 목표 시간 증가
            coolclocks[index].time += time;
        }
        //감시자와 쿨타임 입력
        updateCoolTime += Checker[index];
    }
    public void CoolTimeStart(int index, float time, Action del)
    {
        //감시자 설정
        Checker[index] += () =>
        {
            timeCounting(index);
            timeCheck(index, time);
        };
        //중도 시작인지 검색
        if (!coolclocks[index].coolStart)
        {
            //첫 시작일 경우
            coolclocks[index].time = time;
            coolclocks[index].CoolEndAction += del;
            coolclocks[index].coolStart = true;
            coolclocks[index].coolEnd = false;
        }
        else
        {
            //중도 시작일 경우 목표 시간과 효과 증가
            coolclocks[index].time += time;
            coolclocks[index].CoolEndAction += del;
        }
        //감시자와 쿨타임 입력
        updateCoolTime += Checker[index];
    }

    public void AddAction(int index, Action del)
    {
        //중도 시작인지 검색
        if (!coolclocks[index].coolStart)
        {
            Debug.LogWarning("쿨타임이 시작되지 않은 상태입니다. 시간을 다시 계산해주세요.");
        }
        else
        {
            coolclocks[index].CoolEndAction += del;
        }
    }

    /// <summary>
    /// 모든 쿨타임 초기화
    /// </summary>
    public void AllClocksReset()
    {
        for (int i = 0; i < coolClockCount; i++)
        {
            coolclocks[i].time = 0f;
            coolclocks[i].coolStart = false;
            coolclocks[i].coolEnd = true;
            coolclocks[i].timeCount = 0;
        }
    }
}

