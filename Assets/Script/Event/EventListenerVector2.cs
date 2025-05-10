using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventListenerVector2 : IGenericEventListener<Vector2>
{
	[SerializeField] private GameEventVector2 @event;
	private Action<Vector2> response;

	public void Subscribe(Action<Vector2> eventResponse) 
	{
		@event?.AddListener(this);
		response = eventResponse;
	}

	public void Unsubscribe() 
	{
		@event?.RemoveListener(this);
		response = null;
	}

	public void OnEventTrigger()
	{
		OnEventTrigger(default(Vector2));
	}

	public void OnEventTrigger(Vector2 input) {
		response.Invoke(input);
	}

	void IEventListener.Subscribe(Action eventResponse)
	{
		throw new NotImplementedException();
	}
}
