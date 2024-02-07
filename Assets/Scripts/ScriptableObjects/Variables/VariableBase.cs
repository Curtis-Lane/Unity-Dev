using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableBase<T> : ScriptableObject, ISerializationCallbackReceiver {
	public T initialValue;

	[InspectorReadOnly]
	public T value;

	public void OnAfterDeserialize() {
		value = initialValue;
	}

	public void OnBeforeSerialize() {
		//
	}

	public static implicit operator T(VariableBase<T> r) {return r.value;}
}
