using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<string, State> m_stateMap;
    private State m_currState;
    private State m_nextState;

    public StateMachine()
    {
        m_stateMap = new Dictionary<string, State>();
        m_currState = null;
        m_nextState = null;
    }

    ~StateMachine()
    {
        m_stateMap.Clear();
    }

    public void AddState(State newState)
    {
        if (newState == null)
            return;
        if (m_stateMap.ContainsValue(newState))
            return;
        if (m_currState == null)
            m_currState = m_nextState = newState;
        m_stateMap.Add(newState.GetStateID(), newState);
    }

    public void SetNextState(string nextStateID)
    {
        if (m_stateMap.ContainsKey(nextStateID))
        {
            m_nextState = m_stateMap[nextStateID];
        }
    }

    public string GetCurrentState()
    {
        string temp = m_currState.GetStateID();
        if (m_currState != null)
            return m_currState.GetStateID();
        return "<No states>";
    }

    public void Update(double dt)
    {
        if (m_nextState != m_currState)
        {
            m_currState.Exit();
            m_currState = m_nextState;
            m_currState.Enter();
        }
        m_currState.Update(dt);
    }
}