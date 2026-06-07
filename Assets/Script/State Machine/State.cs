using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected string m_stateID;

    protected State(string stateID)
    {
        m_stateID = stateID;
    }

    public virtual void Enter()
    {

    }

	public virtual void Update(double dt)
    {

    }

	public virtual void Exit()
    {

    }

    public string GetStateID()
    {
	    return m_stateID;
    }
}
