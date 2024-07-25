using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    private void Update()
    {
        Vector3 inputVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.z += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.z -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;
        transform.position += Time.deltaTime * moveSpeed * inputVector;
        transform.forward = Vector3.Slerp(transform.forward, inputVector, Time.deltaTime * rotateSpeed);
    }
}