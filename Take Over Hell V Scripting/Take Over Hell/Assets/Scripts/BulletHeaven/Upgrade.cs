using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    [Header("Info.")]
    
    public Sprite icon;

    [Header("Parametros Pasiva")] 
    public int lifeReg = 0;
    public int damage = 0;
    public float reload = 0; 
    public int speed = 0;

    [Header("Parametros Proyectil")] 
    
    public int damageProyectil = 0;

    public bool firstTime = true;
}
