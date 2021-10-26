using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public int GetStateIndex();
    public void Enter(int lastStateIndex);
    public void Update();
    public void Exit(int lastStateIndex);
}
