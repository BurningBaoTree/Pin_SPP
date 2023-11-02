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
    /// �ð� ����
    /// </summary>
    public int coolClockCount = 1;
    int Startindex = 0;
    int indexcode = 0;

    /// <summary>
    /// �ð�
    /// </summary>
    public coolClock[] coolclocks;

    /// <summary>
    /// update�� ����� ��������Ʈ
    /// </summary>
    Action[] Checker;

    /// <summary>
    /// update�� ��������Ʈ
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
    /// �ܺο��� ����Ǵ� ��Ÿ�� ���� �ڵ�
    /// </summary>
    /// <param name="index">�ð� �ε��� �ڵ�</param>
    /// <param name="time">�ð�</param>
    public void CoolTimeStart(int index, float time)
    {
        Startindex = index;
        //������ ����
        Checker[index] += () => { timeCheck(index, time); };
        //�ߵ� �������� �˻�
        if (startCheck(index))
        {
            //ù ������ ���
            coolclocks[index].time = time;
            updateCoolTime += timeCounting;
            coolclocks[index].coolStart = true;
            coolclocks[index].coolEnd = false;
        }
        else
        {
            //�ߵ� ������ ��� ��ǥ �ð� ����
            coolclocks[index].time += time;
        }
        //������ �Է�
        updateCoolTime += Checker[index];
    }

    /// <summary>
    /// �ǽð����� �ð��� �� ���Ҵ��� Ȯ���ϴ� �ڵ�
    /// </summary>
    /// <param name="index">�ð� �ε��� �ڵ�</param>
    /// <param name="time">�ð�</param>
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
    /// �ߵ� �������� üũ�ϴ� bool �Լ�
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

