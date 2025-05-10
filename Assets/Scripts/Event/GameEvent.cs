using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject {
    protected readonly List<IEventListener> listeners = new();
    
    /// <summary>
    /// Raise this event and trigger all listeners.
    /// </summary>
    public virtual void Raise() {
        foreach (var listener in listeners) {
            listener.OnEventTrigger();
		}
	}

    /// <summary>
    /// Add a listener to the listener list.
    /// </summary>
    /// <param name="listener"></param>
    public void AddListener(IEventListener listener) {
        if (!listeners.Contains(listener)) listeners.Add(listener);
	}

    /// <summary>
    /// Remove a listener from the listener list.
    /// </summary>
    /// <param name="listener"></param>
    public void RemoveListener(IEventListener listener) {
        if (!listeners.Contains(listener)) listeners.Remove(listener);
	}
}
