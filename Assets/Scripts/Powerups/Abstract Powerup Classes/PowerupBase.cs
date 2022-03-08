using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class PowerupBase : MonoBehaviour
{
    /// <summary>
    /// The powerup's effect upon player collision.
    /// </summary>
    protected virtual void Effect(GameObject player) {}
}
