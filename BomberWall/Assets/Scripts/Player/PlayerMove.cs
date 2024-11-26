using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 _inputMovement;
    private bool _canMove = true;

    /// <summary>
    /// D�placement du joueur
    /// </summary>
    /// <param name="callbackContext"></param>
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _inputMovement = callbackContext.ReadValue<Vector2>();
        DetectHit(gameObject.transform.position, 1, _inputMovement);
        if (callbackContext.started && _canMove)
        {
            transform.position += new Vector3(_inputMovement.x, _inputMovement.y ,0);
        }
    }

    /// <summary>
    /// G�re les collision du joueur avec le mur afin qu'il ne clip pas � travers
    /// </summary>
    /// <param name="startPos">Position de d�part</param>
    /// <param name="distance">Distance de d�placement</param>
    /// <param name="direction">direction du d�placement</param>
    void DetectHit(Vector3 startPos, float distance, Vector3 direction)
    {
        RaycastHit2D hit;
        Vector3 endPos = startPos + (distance * direction);
        int layerMask = 1 << 6;
        hit = Physics2D.Raycast(startPos, direction, distance, layerMask);
        if (hit.collider == null) //V�rification que l'on touche bien un collider (sinon null ref)
        {
            _canMove = true;
            return;
        }
        if (hit.collider.gameObject.layer == 6)
        {
            _canMove = false;
            endPos = hit.point;
        }
        Debug.DrawLine(startPos, endPos, Color.red);
    }
}
