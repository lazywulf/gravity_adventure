using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EventListener : IEventListener
{
    [SerializeField] private GameEvent @event;
	private Action response;

	/// <summary>
	/// Subscribe to the event. Need to pass the response func. of the event. Must be called when the monobehaviour obj. is enabled.
	/// </summary>
	/// <param name="eventResponse"></param> The function that is to be triggered when the event is raised.
	public void Subscribe(Action eventResponse) {
		@event?.AddListener(this);
		response = eventResponse;
	}

	/// <summary>
	/// Unsubscribe to the event. Must be called when the monobehaviour obj. is disabled/
	/// </summary>
	public void Unsubscribe() {
		@event?.RemoveListener(this);
		response = null;
	}

	public void OnEventTrigger() {
		response.Invoke();
	}
}
