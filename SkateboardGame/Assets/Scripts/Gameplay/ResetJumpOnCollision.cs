using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJumpOnCollision : MonoBehaviour
{
    public bool canBeCollected = true;
    [SerializeField] private SpriteRenderer sprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            DriveSkateboard driveSkateboard = collision.GetComponent<DriveSkateboard>();
            if (driveSkateboard != null && canBeCollected)
            {
                driveSkateboard.canJump = true;
                canBeCollected = false;

                Color newColor = sprite.color;
                newColor.a = 0.1f;
                sprite.color = newColor;

                StartCoroutine(ReEnableCollectible());
            }
        }
    }

    private IEnumerator ReEnableCollectible()
    {
        yield return new WaitForSeconds(7.5f);
        canBeCollected = true;

        Color newColor = sprite.color;
        newColor.a = 1f;
        sprite.color = newColor;
    }
}
