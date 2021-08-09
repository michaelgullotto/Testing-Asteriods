using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class testsuite
{
    

    [UnityTest]
    public IEnumerator AsteriodsSpawns()
    {
        Spawner spawner = MonoBehaviour.Instantiate(Resources.Load<Spawner>("spawner"));
       
        GameObject asteriod = spawner.testAsteriod();

        yield return new WaitForSeconds(5f);

        UnityEngine.Assertions.Assert.IsNotNull(asteriod);

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

    }

    [UnityTest]
    public IEnumerator PlayerTakesDamage()
    {
        
        bool notFullLife = false;
        int startHealth = 3;
        //MonoBehaviour.Instantiate(Resources.Load("GameOverPanel"));
        Player player = MonoBehaviour.Instantiate(Resources.Load<Player>("player"));
        Spawner spawner = MonoBehaviour.Instantiate(Resources.Load<Spawner>("spawner"));
        Player.testing = true;
        GameObject asteriod = spawner.testAsteriod();
        
        yield return new WaitForSecondsRealtime(2);
        asteriod.transform.position = player.transform.position;
        yield return new WaitForSecondsRealtime(3);
       
        if (startHealth > Player.life)
        {
            notFullLife = true;
        }
    
        UnityEngine.Assertions.Assert.IsTrue(notFullLife);

    }

    [UnityTest]
    public IEnumerator PlayerDies()
    {
        
        bool isdeadTest = false;
        Player player = MonoBehaviour.Instantiate(Resources.Load<Player>("player"));
        Player.testing = true;
        Player.life = 0;
        yield return new WaitForSecondsRealtime(3);
        
        
        if (Player.isDead)
        {
            isdeadTest = true;
        }
        UnityEngine.Assertions.Assert.IsTrue(isdeadTest);
    }
    [UnityTest]
    public IEnumerator GameRestartsOnDeath()
    {
        
        bool Respawned = false;
        Player player = MonoBehaviour.Instantiate(Resources.Load<Player>("player"));
        Player.testing = true;
        yield return new WaitForSecondsRealtime(3);
        Player.life = 0;
        yield return new WaitForSecondsRealtime(3);
        player.RestartGame();
        yield return new WaitForSecondsRealtime(3);
        if (!Player.isDead && Time.timeScale == 1)
        {
            Respawned = true;
        }
    
        UnityEngine.Assertions.Assert.IsTrue(Respawned);
    }
    
    
}
