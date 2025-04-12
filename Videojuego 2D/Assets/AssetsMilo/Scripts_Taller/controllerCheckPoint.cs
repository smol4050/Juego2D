using UnityEngine;

public class controllerCheckPoint : MonoBehaviour
{
    private Transform ultPosi;

    float posix, posiy;



    void Start()
    {
        ultPosi = transform;
        posix = ultPosi.position.x;
        posiy = ultPosi.position.y;

        Debug.Log("Posicion inicial: " + ultPosi.position);
    }

    

    public void volverAlCheckPoint()
    {
        Debug.Log("Volviendo al checkpoint: " + ultPosi.position);
        transform.position = new Vector2(posix, posiy);   
    }

    public void ActualizarCheckPoint(Transform res)
    {
        Debug.Log("Actualizando checkpoint: " + res.position);
        ultPosi.position = res.position;
        posix = ultPosi.position.x;
        posiy = ultPosi.position.y;
    }

    
}
