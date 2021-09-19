using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileBehaviour : MonoBehaviour
{
    //public GameObject projectile;

    [SerializeField] float speed;

    [SerializeField]bool isHoming;
    
    bool targetLocked = false;

    Vector3 DirectiontoMove;

    Transform trackedtarget;






    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHoming && trackedtarget!=null)
        {
            transform.LookAt(trackedtarget);

        }
      //transform.LookAt(DirectiontoMove);
         // Vector3.MoveTowards(transform.position,DirectiontoMove*100,speed*Time.deltaTime); //transform.forward * speed * Time.deltaTime;
   transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        
    }
    // private void OnCollisionEnter(Collision other) {
    //     GameObject.Destroy(this.gameObject,0.3f);
    // }

    public void SetDirection(Transform target )
    {
        if(!targetLocked)
        {
           if(target!=null) {
           DirectiontoMove = target.position -  this.transform.position;
           transform.LookAt(target);print("using lookAt"); trackedtarget= target;  }
         
            targetLocked=true;
        }
    }

    
}
