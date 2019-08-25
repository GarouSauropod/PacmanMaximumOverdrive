using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            //Dispatch event to signal a pellet has been eaten
            Destroy(this.gameObject);
        }
    }
}
