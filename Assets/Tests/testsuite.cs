using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class testsuite
{
    //public Player Player;
    [UnityTest]
    public IEnumerator AsteriodsSpawns()
    {
        Spawner spawner = MonoBehaviour.Instantiate(Resources.Load<Spawner>("spawner"));
       
        GameObject asteriod = spawner.testAsteriod();

        yield return new WaitForSeconds(5f);

        UnityEngine.Assertions.Assert.IsNotNull(asteriod);
        
        
        //MonoBehaviour.Destroy(spawner);
    }

    [UnityTest]
    public IEnumerator AsteriodMoves()
    {
        bool moved = false;
        Spawner spawner = MonoBehaviour.Instantiate(Resources.Load<Spawner>("spawner"));
      
        GameObject asteriod = spawner.testAsteriod(); 
        var startpos = asteriod.transform.position;

        yield return new WaitForSeconds(5);
        var currentpos = asteriod.transform.position;
        if (currentpos != startpos)
        {
            moved = true;
        }
        UnityEngine.Assertions.Assert.IsTrue(moved);
        
        //MonoBehaviour.Destroy(spawner);
    }

    [UnityTest]
    public IEnumerator PlayerTakesDamage()
    {
        
        bool notFullLife = false;
        int startHealth = 3;
     
        Player player = MonoBehaviour.Instantiate(Resources.Load<Player>("player"));
        Spawner spawner = MonoBehaviour.Instantiate(Resources.Load<Spawner>("spawner"));
        player.testing = true;
        GameObject asteriod = spawner.testAsteriod();
        
        Debug.Log(player.life.ToString());
       
        yield return new WaitForSecondsRealtime(5);
        asteriod.transform.position = player.transform.position;
        
        Debug.Log(player.life.ToString());

        yield return new WaitForSecondsRealtime(3);
        
        if (startHealth > player.life)
        {
            notFullLife = true;
        }
    
        UnityEngine.Assertions.Assert.IsTrue(notFullLife);
        MonoBehaviour.Destroy(player);
        MonoBehaviour.Destroy(spawner);
        
    }

    [UnityTest]
    public IEnumerator PlayerDies()
    {
        bool isdeadTest = false;
        Player player = MonoBehaviour.Instantiate(Resources.Load<Player>("player"));
        player.testing = true;
        player.life = 0;
        yield return new WaitForSecondsRealtime(1);

        if (player.isDead)
        {
            isdeadTest = true;
        }
        
        UnityEngine.Assertions.Assert.IsTrue(isdeadTest);
        MonoBehaviour.Destroy(player);
    }
    [UnityTest]
    public IEnumerator GameRestartsOnDeath()
    {
        bool notRespawned = true;
        Player player = MonoBehaviour.Instantiate(Resources.Load<Player>("player"));
        player.testing = true;
        yield return new WaitForSecondsRealtime(1);
        player.life = 0;
        yield return new WaitForSecondsRealtime(1);
        player.RestartGame();
        yield return new WaitForSecondsRealtime(1);
        if (!player.isDead)
        {
            notRespawned = false;
        }
    
        UnityEngine.Assertions.Assert.IsFalse(notRespawned); 
        
        MonoBehaviour.Destroy(player);
    }
    
}
