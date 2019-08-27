using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            GameEventManager.TriggerEvent("onPelletEaten");
            Destroy(this.gameObject);
        }
    }
}
