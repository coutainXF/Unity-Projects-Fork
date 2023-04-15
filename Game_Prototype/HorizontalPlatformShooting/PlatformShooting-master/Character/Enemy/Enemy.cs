using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyClass
{
    Patrol,
    RunToLeft,
    RunToRight,
    Mine
}
public class Enemy : Character
{
    public EnemyClass _enemyClass;
    

}