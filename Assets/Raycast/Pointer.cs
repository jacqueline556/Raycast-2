using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pointer : MonoBehaviour
{
    public LineRenderer line;
    public Color defaultColor = Color.yellow;
    public Color collisionColor = Color.magenta;
    public float maxDistance = 3f; 

    void Start()
    {
        line = GetComponent<LineRenderer>();

        if (line == null)
        {
            Transform controller = transform.Find("Controller");
            if (controller != null)
            {
                line = controller.GetComponent<LineRenderer>();
            }
        }
    }
    void Update()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + transform.forward;//punto donde comienza el rayo
        Vector3 direction = transform.forward;//direccion del rayo en este caso de frente/forward

        // define los puntos del renderer
        Vector3[] positions = new Vector3[2];
        positions[0] = origin;
        positions[1] = origin + direction * maxDistance;

        // comprueba la informacion del objeto con el que se colisiona
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            positions[1] = hit.point; // Colocar el punto final donde se detectó la colisión

            // cambia el color del objeto hijo "controller"
            if (hit.collider.gameObject.name == "Objetivo")
            {
                line.startColor = collisionColor;
                line.endColor = collisionColor;
            }
        }
        else
        {
            // devolover al color default(sin colisionar)
            line.startColor = defaultColor;
            line.endColor = defaultColor;
        }

        line.SetPositions(positions);
    }
}

