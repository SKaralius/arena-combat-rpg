public enum AIOrders
{
    MoveLeft,
    MoveRight,
    Attack,
    UseSkill
}

public static class EnemyAI
{
    public static AIOrders DecideOrder(BattleSystem battleSystem)
    {
        return AIOrders.Attack;
    }
}