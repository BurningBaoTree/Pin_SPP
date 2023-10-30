using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBase : MonoBehaviour
{
    PlayerInput input;
    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Test.TestButton1.performed += Test_1;
        input.Test.TestButton2.performed += Test_2;
        input.Test.TestButton3.performed += Test_3;
        input.Test.TestButton4.performed += Test_4;
        input.Test.TestButton5.performed += Test_5;
    }

    protected virtual void Test_1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void Test_2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void Test_3(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void Test_4(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void Test_5(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
}
