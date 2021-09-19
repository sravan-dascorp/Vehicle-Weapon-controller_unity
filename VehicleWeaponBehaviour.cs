using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;
#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public class VehicleWeaponBehaviour : MonoBehaviour
{
    
    [SerializeField] GameObject Weapon = null;
    [SerializeField] Transform aim;
    [SerializeField] Transform instantiatePoint;

    [SerializeField] GameObject projectilePrefab;
     [SerializeField] GameObject MuzzleflashPrefab;
    [SerializeField] string Weaponname;

    projectileActor firingScript;

   [SerializeField] bool  isRotatable = false;

   [SerializeField]public bool free360Roational;

    [SerializeField] bool DebugFOV;
    [SerializeField] float Distance = 5f;
   [SerializeField] float minAngle;
   [SerializeField] float maxAngle;

   [SerializeField] float rotationSpeed= 5f;

   [SerializeField]public float minWeaponSelectAngle;
   [SerializeField]public float maxWeaponSelectAngle;


Vector3  vector;
Vector3 head;
private void Start() {

    if(Weapon==null) Weapon=this.gameObject;
      vector = Weapon == null ? transform.forward : Weapon.transform.forward;
            head = Weapon == null ? (transform.position + Vector3.up * 2) : transform.position + Vector3.up * 2;
  
  
}
    public  VehicleWeaponBehaviour checkQualification(float angleToTest)
    {
       // if(maxWeaponSelectAngle>360) maxWeaponSelectAngle =-360;
       if(free360Roational) return this;
        if(angleToTest >= minWeaponSelectAngle && angleToTest <= maxWeaponSelectAngle) return this;

        return null;
    }

    public void  RotateToAxis()
    {
        if(!isRotatable) return;
       // float angle = AxisAngleFinder.DirectionRelativeAxis(Weapon.transform.forward,Weapon.transform.position,Vector3.up,false);
       float angle = AxisAngleFinder.GetAxisAngle();
        Mathf.Clamp(angle,minAngle,maxAngle);
        //Debug.Log(name + " : " +  angle);
        
        if(Weapon.transform.rotation.y != angle )
         Weapon.transform.localRotation =Quaternion.Slerp(Weapon.transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
    }    
    public void fire()
    {
        firingScript =aim.transform.GetComponent<projectileActor>(); 

        firingScript.Fire();
    }
    public void setFiring(bool isfiring)
    {
         firingScript =aim.transform.GetComponent<projectileActor>(); 
        firingScript.SetFiring(isfiring);

        if(!isfiring) firingScript.ResetFireTimer();
    }

    public void FireProjectile(Transform target=null)
    {
        GameObject  Muzzleflash  = Instantiate(MuzzleflashPrefab,instantiatePoint.position,aim.rotation);
       GameObject  projectileInstance  = Instantiate(projectilePrefab,instantiatePoint.position,aim.rotation);
      if(target!=null) projectileInstance.GetComponent<ProjectileBehaviour>().SetDirection(target);

    }
    public bool isMultiTarget;
    public bool isswarm=false;
    public void FireMultiTarget(List<GameObject> targetList )
    {
       
        GameObject  projectileInstance  = Instantiate(projectilePrefab,instantiatePoint.position,instantiatePoint.rotation);
      if (isswarm) { projectileInstance.GetComponent<MissileBehaviours.Actions.SpawnSwarm>().SetTargets(targetList);}
        else  {projectileInstance.GetComponent<MissileBehaviours.Controller.MissileController>().Target = targetList[0].transform;}

    }

    [SerializeField] float aimMinAngle ,aimMaxAngle;   
    public void   RotatetoTarget(GameObject target)
    {
        
        float xangle;
        float yangle;
        
        Vector3 directipn = target.transform.position - aim.transform.position;
         xangle = Vector3.SignedAngle(aim.transform.forward,directipn,-aim.transform.right);
       // if(xangle <0) xangle+=360f;

        yangle = Vector3.SignedAngle(aim.transform.forward,directipn,aim.transform.up);
        
       
        
        //aim.transform.rotation =Quaternion.Slerp(aim.transform.rotation, Quaternion.Euler(xangle, yangle,0 ), rotationSpeed * Time.deltaTime);
      // aim.transform.Rotate(xangle, 0, 0, Space.Self);
     
    // if(xangle>0) xangle=Mathf.Clamp(xangle,aimMinAngle,aimMaxAngle); else if(xangle<0) xangle=Mathf.Clamp(xangle,-aimMaxAngle,-aimMinAngle);

       aim.LookAt(target.transform,aim.right);
    //    aim.LookAt(target.transform,aim.up);
     

      //aim.transform.Rotate(xangle, 0, 0, Space.Self);
      //aim.transform.RotateAroundLocal(-aim.transform.right,xangle); //transformRotateAround(transform.position, TransformDirection(Vector3.up), Time.deltaTime * 90f);
    }
    public void ResetAImRotation()
    {
        aim.transform.localRotation = Quaternion.identity;
        aimClamper.localRotation = Quaternion.identity;
    }

    public GameObject getWeaponGameObject()
    {
        return this.Weapon;
    }
[SerializeField] Transform aimClamper;
[SerializeField] float aimMin,aimMax;
   

    public void RotatetoTarget2(GameObject target)
   {
       aimClamper.transform.LookAt(target.transform,aim.transform.right);
      Quaternion temp = aimClamper.rotation;
      Vector3 tempEuler = temp.eulerAngles;

    //    Vector3 directipn = target.transform.position - aim.transform.position;
    //     float xangle = Vector3.SignedAngle(aim.transform.forward,directipn,aim.transform.right);
       float  xangletemp=Mathf.Clamp(tempEuler.z,aimMin,aimMax);
      if(xangletemp <0) xangletemp+=360f;

      
      aim.rotation = Quaternion.Euler(xangletemp,tempEuler.y,tempEuler.x);

   }

    // void method(GameObject targett)
    // {
    //      Vector3 targetVector = new Vector3(targett.transform.position.x - aim.transform.position.x, 0f, targett.transform.position.z - aim.transform.position.z);
 
    //     Vector3 relativeVector = new Vector3(vehicleT.forward.x, 0f, vehicleT.forward.z);
 
    //     float wheelAngle = Vector3.SignedAngle(relativeVector, targetVector, Vector3.up);
 
    //     wheelAngle = Mathf.Clamp(wheelAngle, -maxWheelAngle, maxWheelAngle);
 
    //     Vector3 wheelForward = Quaternion.Euler(0f, wheelAngle, 0f) * relativeVector;
 
    //     frontDriverT.forward = wheelForward;
 
 
    // }
 
 
 
 
 
 
 
 
  #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (DebugFOV)
                drawFOV();
        }

        private void OnDrawGizmosSelected()
        {
            if (DebugFOV)
                drawFOV();
        }
       
    //
        private void drawFOV()
        {
        
            if(!Application.isPlaying){
            vector = Weapon == null ? transform.forward : Weapon.transform.forward;}
             head = Weapon == null ? (transform.position + Vector3.up * 2) : transform.position + Vector3.up * 2;
            
            var angle = Uti.HorizontalAngle(vector);

           float FieldOfView = Mathf.Abs(maxAngle-minAngle);
            var left = Uti.HorizontalVector(angle - FieldOfView * 0.5f);

            Handles.color = new Color(0.5f, 0.3f, 0.3f, 0.1f);
            Handles.DrawSolidArc(head, Vector3.up, left, FieldOfView, Distance);

            Handles.color = new Color(1, 0.3f, 0.3f, 0.75f);
            Handles.DrawWireArc(head, Vector3.up, left, FieldOfView, Distance);
            Handles.DrawLine(head, head + left * Distance);
            Handles.DrawLine(head, head + Uti.HorizontalVector(angle + FieldOfView * 0.5f) * Distance);
        }

        #endif

        
    }





