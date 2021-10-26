using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandManager : MonoSingleton<CommandManager>
{
    public enum State { Empty, Processing};
    public State state;
    LinkedList<ICommand> commands = new LinkedList<ICommand>();
    float excutionTimer;

    private void Update()
    {
        switch(state)
        {
            case State.Empty:
                excutionTimer = 0.0f;
                break;
            case State.Processing:
                excutionTimer -= Time.deltaTime;
                if (excutionTimer <= 0)
                {
                    if (commands.Count > 0)
                    {
                        //print("execute");
                        ICommand currCommand = commands.First.Value;
                        commands.RemoveFirst();
                        excutionTimer = currCommand.Execute();
                    }
                    else
                    {
                        state = State.Empty;
                    }
                }
                break;
        }
    }

    public void AddFirst(ICommand command)
    {
        state = State.Processing;
        commands.AddFirst(command);
    }

    public void AddLast(ICommand command)
    {
        state = State.Processing;
        commands.AddLast(command);
    }

    public void PrintCurrentCommandNames()
    {
        int loopCount = 0;
        string log = "Command Names:";
        LinkedListNode<ICommand> commandNode = commands.First;
        while(commandNode != null)
        {
            if (loopCount++ >= 100) 
            {
                Debug.LogWarning("현재 대기 중인 Command 개수가 100개 이상이다. 무한루프인지 확인할 것.");
                Debug.Log(log);
                return; 
            }
            log += commandNode.Value.Name + "\n";
            commandNode = commandNode.Next;
        }
        Debug.Log(log);
    }

    public static Command<float> GetDelayCommand(float delay)
    {
        return new Command<float>(DelayAction, delay);
    }

    public static float DelayAction(float delay)
    {
        return delay;
    }

    public void Clear()
    {
        commands.Clear();
    }

    public void ClearAndReset()
    {
        Clear();
        state = State.Empty;
        excutionTimer = 0.0f;
    }
}

public interface ICommand
{
    float Execute();
    ICommand SetName(string str);
    public string Name { get; }
}

public class Command : ICommand
{
    private Func<float> command;
    public string Name { get; private set; }

    public Command(Func<float> command)
    {
        this.command = command;
    }

    public float Execute()
    {
        return command();
    }

    public ICommand SetName(string str)
    {
        Name = str;
        return this;
    }
}

public class Command<T> : ICommand
{
    private T arg;
    private Func<T, float> action;
    public string Name { get; private set; }

    public Command(Func<T, float> action, T arg)
    {
        this.action = action;
        this.arg = arg;
    }

    public float Execute()
    {
        return action(arg);
    }

    public ICommand SetName(string str)
    {
        Name = str;
        return this;
    }
}

public class Command<T1, T2> : ICommand
{
    private T1 arg1;
    private T2 arg2;
    private Func<T1, T2, float> action;
    public string Name { get; private set; }

    public Command(Func<T1, T2, float> action, T1 arg1, T2 arg2)
    {
        this.action = action;
        this.arg1 = arg1;
        this.arg2 = arg2;
    }

    public float Execute()
    {
        return action(arg1, arg2);
    }

    public ICommand SetName(string str)
    {
        Name = str;
        return this;
    }
}

public class Command<T1, T2, T3> : ICommand
{
    private T1 arg1;
    private T2 arg2;
    private T3 arg3;
    private Func<T1, T2, T3, float> action;
    public string Name { get; private set; }

    public Command(Func<T1, T2, T3, float> action, T1 arg1, T2 arg2, T3 arg3)
    {
        this.action = action;
        this.arg1 = arg1;
        this.arg2 = arg2;
        this.arg3 = arg3;
    }

    public float Execute()
    {
        return action(arg1, arg2, arg3);
    }

    public ICommand SetName(string str)
    {
        Name = str;
        return this;
    }
}

public class Command<T1, T2, T3, T4> : ICommand
{
    private T1 arg1;
    private T2 arg2;
    private T3 arg3;
    private T4 arg4;
    private Func<T1, T2, T3, T4, float> action;
    public string Name { get; private set; }

    public Command(Func<T1, T2, T3, T4, float> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        this.action = action;
        this.arg1 = arg1;
        this.arg2 = arg2;
        this.arg3 = arg3;
        this.arg4 = arg4;
    }

    public float Execute()
    {
        return action(arg1, arg2, arg3, arg4);
    }

    public ICommand SetName(string str)
    {
        Name = str;
        return this;
    }
}