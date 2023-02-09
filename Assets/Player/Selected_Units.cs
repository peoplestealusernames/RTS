using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selected_Units : MonoBehaviour
{
    //TODO: Add selection update event
    private HashSet<Transform> Selected = new HashSet<Transform>();
    public UnityEvent Update = new UnityEvent();

    private void Start()
    {
        Update.AddListener(() => { Debug.Log("Update"); });
    }

    public void Set()
    {
        Selected = new HashSet<Transform>();
        Update.Invoke();
    }
    public void Set(HashSet<Transform> _Selected)
    {
        Selected = _Selected;
        Update.Invoke();
    }
    public void Set(Transform _Selected)
    {
        Selected = new HashSet<Transform>();
        Selected.Add(_Selected);
        Update.Invoke();
    }
    public void Add(Transform _Selected)
    {
        Selected.Add(_Selected);
        Update.Invoke();
    }
    public void Remove(Transform _Selected)
    {
        Selected.Remove(_Selected);
        Update.Invoke();
    }
}
