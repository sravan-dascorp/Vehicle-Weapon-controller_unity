using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> vehiclesList = new List<GameObject>();

    GameObject currentVehicle = null;
    [SerializeField] bool copyVelocity = true;
    int index=0;
 private void Awake() {
     currentVehicle = vehiclesList[index]; 
     
        
    }
   private void Start() 
    {
        
       FindObjectOfType<MouseOrbitImproved>().SetCameraTarget(currentVehicle.transform); 
        setActiveState(vehiclesList);
        print(currentVehicle);
    }
    public void VehicleChangeButton()
    {
        ChangeModel(vehiclesList);
    }



    public void ChangeModel(List<GameObject> playerObjects )
    {
        if(index < vehiclesList.Count-1) index++;
        else if(index >= vehiclesList.Count-1) index = 0;
        Vector3 currentPosition = currentVehicle.transform.position;
        Vector3 newRot = new Vector3 (currentVehicle.transform.eulerAngles.x, currentVehicle.transform.eulerAngles.y, transform.eulerAngles.z);
         Vector3 currentVelocity = currentVehicle.GetComponent<Rigidbody>().velocity;
                    
        currentVehicle = vehiclesList[index];
        
         currentVehicle.transform.position = currentPosition;
         currentVehicle.transform.rotation = Quaternion.Euler (newRot);
         FindObjectOfType<MouseOrbitImproved>().SetCameraTarget(currentVehicle.transform); 
        setActiveState( vehiclesList);
         if(copyVelocity)   currentVehicle.GetComponent<Rigidbody>().velocity = currentVelocity;
        

    }

    private void setActiveState(List<GameObject> playerObjects )
    {
          
        foreach (GameObject playerObject in playerObjects)
        {
         
            if(playerObject != currentVehicle)
            {
                if(playerObject.activeInHierarchy)
                {
                    playerObject.GetComponent<Rigidbody>().isKinematic=true; 
                 playerObject.gameObject.SetActive(false);
                 }
             }
            else  {
                if(!playerObject.activeInHierarchy) 
                    playerObject.gameObject.SetActive(true);
                     

                        playerObject.GetComponent<Rigidbody>().isKinematic=false;
                       
                  
                  }
            
        }
    }
}   
