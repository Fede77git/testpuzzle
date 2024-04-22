using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    public float movementSpeed;
    public Vector2 sens;
    public new Transform camera;

    private int cont;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        cont = 0;
    }

    
    private void UpdateMov()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;

            velocity = direction * movementSpeed;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }


    private void MouseCamera()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sens.x, 0);
        }

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sens.y + 360) % 360;
            if (rotation.x > 75 && rotation.x <180)
            {
                rotation.x = 75;
            }
            else
                if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }
            camera.localEulerAngles = rotation; 
        }
    }

    void Update()
    {
        UpdateMov();
        MouseCamera();
    }


    private void Collects()
    {
        if (cont >= 1)
        {
            
            
            SceneManager.LoadScene("SampleScene");
            Cursor.lockState = CursorLockMode.None;


        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin") == true)
        {
            cont = cont + 1;
            Collects();
            Destroy(other.gameObject);
        }
    }
}
