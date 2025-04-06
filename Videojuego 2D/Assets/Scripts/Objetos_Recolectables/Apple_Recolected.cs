using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Recolected : MonoBehaviour
{

    [SerializeField] private string appleType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int value = FindObjectOfType<GameControllerScene1>().GetAppleValue(appleType);
            GameManager.Instance.SumValues(value);
            Destroy(gameObject);


        }
    }
}
