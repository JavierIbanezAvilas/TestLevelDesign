using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float tiempoParada;
    GameObject player;
    
    private int i;
    private float tolerancia = 0.1f;
    private bool wait;
    

    private void Start()
    {
        player = GameObject.Find("MyCharacter");

    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[i].position,speed*Time.fixedDeltaTime);

        if(Vector3.Distance(transform.position, waypoints[i].position) <= tolerancia && !wait)
        {
            StartCoroutine(TimeWait());
            if(waypoints.Length-1> i) 
            {
                i++;
            }
            else
            {
                i = 0;
            }
            
        }
    }
        IEnumerator TimeWait()
    {
        wait = true;
        yield return new WaitForSeconds(tiempoParada);
        wait = false;
    }

    private void OnTriggerStay(Collider other)
    {
      
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(transform);
        }
          
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(null);
       
        }
    }

}
