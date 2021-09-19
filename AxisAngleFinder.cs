using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAngleFinder : MonoBehaviour
{
    [SerializeField] bool useMouse= true;


    private static float AxisAngle;

    [SerializeField] Transform target;

    [SerializeField] Transform canon;
    [SerializeField] float moveSpeed = 5;

  static  float relativeForward;
  //public static Vector3 RigidbodyVelocity;
  //public static Vector3 RigidbodyAngularVelocity;
    

private void Update() 
{

    if(useMouse)
    {
        Vector3 WorldPosition = GetMouseToWorld();
        WorldPosition.y = target.position.y;
   
        Vector3 direction = WorldPosition - target.position;
        Vector3 normalWorldposition =  WorldPosition.normalized;
      
        float anglexxx = DirectionToAngle(target.forward,direction,Vector3.up,false);        //FindAngleFromPosition(mousepos,targetPoint,true);
        AxisAngle = anglexxx;
      // print(anglexxx);
       // print(Vector3.SignedAngle(Vector3.forward,target.transform.forward,Vector3.up));
       float Angleyyy = DirectionToAngle(Vector3.forward,target.transform.forward,Vector3.up,false);
        //Quaternion rot =   Quaternion.LookRotation(direction,Vector3.up);
    //    if(canon.rotation.y != anglexxx) canon.rotation = Quaternion.Slerp(canon.rotation, rot, moveSpeed * Time.deltaTime);
       // if(canon.rotation.y != anglexxx) canon.rotation = Quaternion.Slerp(canon.rotation, Quaternion.Euler(0, direction.y, 0), moveSpeed * Time.deltaTime);
       relativeForward = findRelativeToForward();

    }
    else
    {
       // Vector3 JoystickInput = new Vector3(Input.GetAxis("RHorizontal"),0,-Input.GetAxis("RVertical"));
       Vector3 playerDirection = Vector3.right * Input.GetAxis("RHorizontal") + Vector3.forward * -Input.GetAxis("RVertical");
        float anglexxx = DirectionToAngle(Vector3.forward,playerDirection,Vector3.up,false);
        float joystickRelative =  DirectionToAngle(Vector3.forward,target.forward,Vector3.up,false);
        float angleDiffrence = anglexxx -joystickRelative;
        // print(joystickRelative);
        // print(angleDiffrence);
        
    
           //anglexxx-180);
        AxisAngle=Mathf.Abs(angleDiffrence);

    }
    
}
// private void FixedUpdate() { // was suposed to find the velocity of hhe vehicle to add to the projectile..
// //     RigidbodyVelocity = target.gameObject.GetComponent<Rigidbody>().velocity;
// //     RigidbodyAngularVelocity = target.GetComponent<Rigidbody>().angularVelocity;
//  }



    public void setAngleController()  //mainly for ui opton and 
    {
        useMouse= !useMouse;
    }
   
   public static float  GetAxisAngle()
   {
       return AxisAngle;
   }
   private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

 private static Vector3 GetMouseToWorld()
        {
           
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
               
                return hit.point;
            } 

            return new Vector3(0,0,0);
            
           
        }

   public  float DirectionToAngle(Vector3 fromPosition ,Vector3 toPosition,Vector3 axis , bool signedAngle)
    {
        float angleDegrees = Vector3.SignedAngle(fromPosition,toPosition,axis);
      
 
    
    if(signedAngle)return angleDegrees;

    //normalizing to [0,360) if !signedAngle;

     if (angleDegrees<0)  angleDegrees+=360;

      return angleDegrees;
    }

    public static  float DirectionRelativeAxis(Vector3 targetForward,Vector3 targetPosition,Vector3 axis , bool signedAngle)
    {
    
    Vector3 worldPosition = GetMouseToWorld();
     worldPosition.y =targetPosition.y;
   
     Vector3 Direction = worldPosition - targetPosition;
    
    float angleDegrees = Vector3.SignedAngle(targetForward,Direction,axis);
      
 
    
    if(signedAngle)return angleDegrees;

    //normalizing to [0,360) if !signedAngle;

     if (angleDegrees<0)  angleDegrees+=360;

      return angleDegrees;

    }


    public float findRelativeToForward()
    {
        return Vector3.SignedAngle(Vector3.forward,target.forward,Vector3.up);
    } 
    public static float GetRelativeForwardAngle()
    {
        return  relativeForward;
    }

float joystickValuecorrector(float value)
{
    if(value<=0) return Mathf.Abs(value);
    else  return value + 180;
}

    
   
}
