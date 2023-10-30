using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCode : TestBase
{
    public Player player;
    protected override void Test_1(InputAction.CallbackContext context)
    {
        player.HP += 10;
    }
    protected override void Test_2(InputAction.CallbackContext context)
    {
        player.HP -= 10;
    }

}
