using UnityEngine;

public class CookedFood : MonoBehaviour {



    public void TriggeredWith(Collider2D collision) {

        if ("Bin" == collision.gameObject.name) {

            Destroy(gameObject);

        }

        

    }





}