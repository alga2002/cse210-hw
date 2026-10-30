
public class ChecklistGoal : Goal
{
    private int _target;
    private int _amountDone;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points,
        int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountDone = 0;
    }

    public ChecklistGoal(string name, string description, int points,
        int target, int bonus, int amountDone)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountDone = amountDone;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountDone++;

        if (IsComplete())
        {
            return GetPoints() + _bonus;
        }

        return GetPoints();
    }

    public override bool IsComplete()
    {
        return _amountDone >= _target;
    }

    public override string GetDetailsString()
    {
        string status = IsComplete() ? "[X]" : "[ ]";

        return $"{status} {GetName()} ({GetDescription()}) -- Completed {_amountDone}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"Checklist|{GetName()}|{GetDescription()}|{GetPoints()}|{_target}|{_bonus}|{_amountDone}";
    }
}
