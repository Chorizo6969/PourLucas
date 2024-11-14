using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 _inputMovement;
    private bool _canMove = true;

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _inputMovement = callbackContext.ReadValue<Vector2>();
        DetectHit(gameObject.transform.position, 1, _inputMovement);
        if (callbackContext.started && _canMove)
        {
            transform.position += new Vector3(_inputMovement.x, _inputMovement.y ,0);
        }
    }

    void DetectHit(Vector3 startPos, float distance, Vector3 direction)
    {
        RaycastHit2D hit;
        Vector3 endPos = startPos + (distance * direction);
        int layerMask = 1 << 6;
        hit = Physics2D.Raycast(startPos, direction, distance, layerMask);
        if (hit.collider == null) //Vérification que l'on touche bien un collider (sinon null ref)
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
