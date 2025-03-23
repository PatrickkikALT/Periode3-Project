using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable {
	public bool PickUp();
	public int GetWorth();
}
