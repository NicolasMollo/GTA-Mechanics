public static class MessageManager {
    public delegate void TakeTheCoin();
    public static event TakeTheCoin OnTakeTheCoin;
    public static void CallOnTakeTheCoin() {
        OnTakeTheCoin?.Invoke();
    }



    public delegate void CrashIntoCollider();
    public static event CrashIntoCollider OnCrashIntoCollider;
    public static void CallOnCrashIntoCollider() {
        OnCrashIntoCollider?.Invoke();
    }



    public delegate void CollisionWithTheCar();
    public static event CollisionWithTheCar OnCollisionWithTheCar;
    public static void CallOnCollisionWithTheCar() {
        OnCollisionWithTheCar?.Invoke();
    }



    public delegate void PressEnterTheCarButton();
    public static event PressEnterTheCarButton OnPressEnterTheCarButton;
    public static void CallOnPressEnterTheCarButton() {
        OnPressEnterTheCarButton?.Invoke();
    }



    public delegate void PressStayOutOfTheCarButton();
    public static event PressStayOutOfTheCarButton OnPressStayOutOfTheCarButton;
    public static void CallOnPressStayOutOfTheCarButton() {
        OnPressStayOutOfTheCarButton?.Invoke();
    }



    public delegate void PlayerSelectionArea();
    public static event PlayerSelectionArea OnPlayerSelectionArea;
    public static void CallOnPlayerSelectionArea() {
        OnPlayerSelectionArea?.Invoke();
    }



    public delegate void GameArea();
    public static event GameArea OnGameArea;
    public static void CallOnGameArea() {
        OnGameArea?.Invoke();
    }
}
