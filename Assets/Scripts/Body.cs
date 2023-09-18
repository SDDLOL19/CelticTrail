using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Head
{
    [SerializeField, Range(0.1f, 20f)] float timeMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeMove -= Time.deltaTime;
        if (timeMove <= 0)
        {
            transform.Translate(Vector2.up * moveVelocity * Time.deltaTime);

            if (Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles += new Vector3(0f, 0, -rotationVelocity) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles += new Vector3(0f, 0, rotationVelocity) * Time.deltaTime;
            }
        }
    }
}
