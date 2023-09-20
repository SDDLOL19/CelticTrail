using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField, Range(1, 100)] protected float speed = 20;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)           
    }

    public void MovementLeft()
    {
        transform.eulerAngles = new Vector3(0f, 0, 90); //rota el objeto a izquierda
    }

    public void MovementRight()
    {
        transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
    }

    public void MovementUp()
    {
        transform.eulerAngles = new Vector3(0f, 0, 0); //rota el objeto hacia arriba
    }

    public void MovementDown()
    {
        transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
    }
}
