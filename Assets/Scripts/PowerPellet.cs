using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using TMPro;
>>>>>>> Stashed changes
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
<<<<<<< Updated upstream
    public int points = 50;
    public float timer = 10f;
    public PacStudentController pacStudent;


    private void Awake()
    {
        if (pacStudent == null)
        {
            pacStudent = Object.FindFirstObjectByType<PacStudentController>();
        }
    }
    public void Collect()
    {
        pacStudent.score += points;
        Destroy(gameObject);
    }
}
=======
    
}


>>>>>>> Stashed changes
