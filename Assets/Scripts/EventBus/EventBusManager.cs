using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBusManager : System.EventArgs
{
    private string m_Message;

    public string Messgae { get { return m_Message; } }

    private GameObject m_enemyManager;
    public GameObject MEnemyManager { get { return m_enemyManager; } }
    public EventBusManager(string m)
    {
        m_Message = m;
    }
    public EventBusManager(GameObject m)
    {
        m_enemyManager = m;
    }
}
