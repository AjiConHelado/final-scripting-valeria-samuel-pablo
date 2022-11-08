using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="EnemyData", menuName="enemy")]
public class EnemyData :ScriptableObject
{
   public int ExpReward;
   public int timer;
   public float speed;
   public int damage;
}
