using System;
using UnityEngine;

public static partial class InputManager {
public struct InputState {
public Vector2 direction;
public bool jump;
public bool interact;
}

public static void Update() {
prevState = currentState;

UpdateDirectionInput();
UpdateJumpInput();
UpdateInteractInput();
}

#region Direction
    public static event Action<Vector2> OnDirInput;
    static void UpdateDirectionInput() {
        currentState.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (currentState.direction != prevState.direction) {
            OnDirInput?.Invoke(currentState.direction);
        }
    }
    public static void RegisterDirectionConsumer(IDirectionInputConsumer consumer) {
        OnDirInput += consumer.HandleDirectionInput;
    }
    public static void UnregisterDirectionConsumer(IDirectionInputConsumer consumer) {
        OnDirInput -= consumer.HandleDirectionInput;
    }
    public interface IDirectionInputConsumer
    {
        public void HandleDirectionInput(Vector2 direction);
    }
    #endregion

#region Jump
public static event Action OnJumpInputDown;
public static event Action OnJumpInputUp;
static void UpdateJumpInput() {
currentState.jump = Input.GetKey(KeyCode.Space);
if (Input.GetKeyDown(KeyCode.Space)) {
OnJumpInputDown?.Invoke();
}
if (Input.GetKeyUp(KeyCode.Space)) {
OnJumpInputUp?.Invoke();
}
}
public static void RegisterJumpConsumer(IJumpInputConsumer consumer) {
OnJumpInputDown += consumer.HandleJumpInputDown;
OnJumpInputUp += consumer.HandleJumpInputUp;
}
public static void UnregisterJumpConsumer(IJumpInputConsumer consumer) {
OnJumpInputDown -= consumer.HandleJumpInputDown;
OnJumpInputUp -= consumer.HandleJumpInputUp;
}
public interface IJumpInputConsumer {
public void HandleJumpInputDown();
public void HandleJumpInputUp();
}
#endregion

#region Interact
public static event Action OnInteractInputDown;
public static event Action OnInteractInputUp;
static void UpdateInteractInput() {
currentState.interact = Input.GetKey(KeyCode.Q);
if (Input.GetKeyDown(KeyCode.Q)) {
OnInteractInputDown?.Invoke();
}
if (Input.GetKeyUp(KeyCode.Q)) {
OnInteractInputUp?.Invoke();
}
}
public static void RegisterInteractConsumer(IInteractInputConsumer consumer) {
OnInteractInputDown += consumer.HandleInteractInputDown;
OnInteractInputUp += consumer.HandleInteractInputUp;
}
public static void UnregisterInteractConsumer(IInteractInputConsumer consumer) {
OnInteractInputDown -= consumer.HandleInteractInputDown;
OnInteractInputUp -= consumer.HandleInteractInputUp;
}
public interface IInteractInputConsumer {
public void HandleInteractInputDown();
public void HandleInteractInputUp();
}
#endregion
}