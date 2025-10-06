using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatformer : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BoxCollider2D playerCollider2D;
    
    private Vector2 direction;

    private void Update() {
        if (playerController.GetDirectionVector() != null)
        {
            direction = playerController.GetDirectionVector();
        }
        if (direction.y < 0)
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider2D,platformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider2D,platformCollider,false);
    }

}
