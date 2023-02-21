using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  
    public bool PrimaryKeyDown()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

}
