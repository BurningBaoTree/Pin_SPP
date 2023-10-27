using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput input;
    Rigidbody2D rb;
    Action action;
    Vector2 dir;
    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.moveBar.performed += MoveBar;
        input.Player.moveBar.canceled += MoveBar;
        rb = GetComponent<Rigidbody2D>();
        action += moveActive;
    }
    private void Update()
    {
        action();
    }

    private void MoveBar(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        dir = Vector2.right * context.ReadValue<Vector2>();
    }
    void moveActive()
    {
        rb.AddForce(dir,ForceMode2D.Impulse);
    }
}
