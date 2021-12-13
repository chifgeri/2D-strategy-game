using System.Collections;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoundTests
{
    Group<PlayerCharacter> playerGroup;
    Group<EnemyCharacter> enemyGroup;

    PlayerCharacter player1;
    PlayerCharacter player2;
    PlayerCharacter player3;
    PlayerCharacter player4;

    EnemyCharacter enemy1;
    EnemyCharacter enemy2;
    EnemyCharacter enemy3;
    EnemyCharacter enemy4;

    GameObject textManager = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Managers/TextManager"));
    GameObject mainStateManager = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Managers/MainStateManager"));

    [SetUp]
    public void Setup()
    {
        playerGroup = new Group<PlayerCharacter>();
        enemyGroup = new Group<EnemyCharacter>();

        player1 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Axeman"))).GetComponent<PlayerCharacter>();
        player2 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Axeman"))).GetComponent<PlayerCharacter>();
        player3 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Axeman"))).GetComponent<PlayerCharacter>();
        player4 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Axeman"))).GetComponent<PlayerCharacter>();

        enemy1 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Skeleton"))).GetComponent<EnemyCharacter>();
        enemy2 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Skeleton"))).GetComponent<EnemyCharacter>();
        enemy3 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Skeleton"))).GetComponent<EnemyCharacter>();
        enemy4 = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Skeleton"))).GetComponent<EnemyCharacter>();
    }

    [TearDown]
    public void Teardown()
    {
        playerGroup = null;
        enemyGroup = null;

        MonoBehaviour.DestroyImmediate(player1);
        MonoBehaviour.DestroyImmediate(player2);
        MonoBehaviour.DestroyImmediate(player3);
        MonoBehaviour.DestroyImmediate(player4);

        MonoBehaviour.DestroyImmediate(enemy1);
        MonoBehaviour.DestroyImmediate(enemy2);
        MonoBehaviour.DestroyImmediate(enemy3);
        MonoBehaviour.DestroyImmediate(enemy4);

    }


    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator TestGroupCanAddCharacter()
    {
        yield return null;

        var group = new Group<PlayerCharacter>();
        group.AddCharacter(player1);

        Assert.AreEqual(1, group.Characters.Count);
        Assert.True(group.Characters.Contains(player1));
    }

    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator TestGroupCanRemoveCharacter()
    {
        yield return null;
      
        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);
        Assert.AreEqual(4, playerGroup.Characters.Count);
        playerGroup.RemoveCharacter(player1);
        Assert.AreEqual(3, playerGroup.Characters.Count);
    }

    [UnityTest]
    public IEnumerator TestGroupAddCharacterWhenFull()
    {
        yield return null;
        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);

        Assert.AreEqual(4, playerGroup.Characters.Count);

        playerGroup.AddCharacter(player1);
        Assert.AreEqual(4, playerGroup.Characters.Count);
    }

    [UnityTest]
    public IEnumerator TestGroupAddCharacterNull()
    {
        yield return null;
        Assert.DoesNotThrow(() => playerGroup.AddCharacter(null));
        Assert.AreEqual(0, playerGroup.Characters.Count);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestRoundCreation()
    {
        yield return null;

        player1.Speed = 10;
        player2.Speed = 8;
        player3.Speed = 6;
        player4.Speed = 4;

        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);

        var round = new Round(playerGroup, null);

        Assert.NotNull(round);
    }

    [UnityTest]
    public IEnumerator TestRoundInit()
    {
        yield return null;

        player1.Speed = 10;
        player2.Speed = 8;
        player3.Speed = 6;
        player4.Speed = 4;

        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);

        enemyGroup.AddCharacter(enemy1);
        enemyGroup.AddCharacter(enemy2);
        enemyGroup.AddCharacter(enemy3);
        enemyGroup.AddCharacter(enemy4);

        var round = new Round(playerGroup, enemyGroup);
        round.InitRound();

        Assert.AreEqual(1, round.RoundNumber);
        Assert.AreEqual(player1, round.Current);
        Assert.AreEqual(7, round.CharacterOrder.Count);

    }

    [UnityTest]
    public IEnumerator TestRoundSetNext()
    {
        yield return null;

        player1.Speed = 10;
        player2.Speed = 8;
        player3.Speed = 6;
        player4.Speed = 4;

        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);

        var round = new Round(playerGroup, enemyGroup);
        round.InitRound();

        Assert.AreEqual(player1, round.Current);
        round.SetNext();
        Assert.AreEqual(player2, round.Current);
        Assert.IsTrue(player2.IsNext);

    }

    [UnityTest]
    public IEnumerator TestRoundResetRound()
    {
        yield return null;

        player1.Speed = 10;
        player2.Speed = 8;
        player3.Speed = 6;
        player4.Speed = 4;

        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);

        var round = new Round(playerGroup, enemyGroup);
        round.InitRound();

        Assert.AreEqual(player1, round.Current);
        round.SetNext();
        round.SetNext(); 
        round.SetNext();
        Assert.AreEqual(player4, round.Current);

        round.ResetRound();
        Assert.AreEqual(player1, round.Current);
        Assert.AreEqual(3, round.CharacterOrder.Count);
    }

    [UnityTest]
    public IEnumerator TestRoundCharacterDie()
    {
        yield return null;

        player1.Speed = 10;
        player2.Speed = 8;
        player3.Speed = 6;
        player4.Speed = 4;

        playerGroup.AddCharacter(player1);
        playerGroup.AddCharacter(player2);
        playerGroup.AddCharacter(player3);
        playerGroup.AddCharacter(player4);
        enemyGroup.AddCharacter(enemy1);

        var round = new Round(playerGroup, enemyGroup);
        round.InitRound();
        Assert.IsTrue(round.PlayerGroup.Characters.Contains(player1));

        round.OnCharacterDied(player1);
        Assert.IsFalse(round.PlayerGroup.Characters.Contains(player1));
    }


    [UnityTest]
    public IEnumerator TestRoundCharacterActionDone()
    {
        yield return null;

        playerGroup.AddCharacter(player1);
        enemyGroup.AddCharacter(enemy1);
        var round = new Round(playerGroup, enemyGroup);
        round.InitRound();

        Assert.AreEqual(player1, round.Current);

        round.CharacterActionDone(player1);
        Assert.AreEqual(enemy1, round.Current);
        Assert.False(player1.IsNext);
        Assert.True(enemy1.IsNext);
    }

    [UnityTest]
    public IEnumerator TestRoundCharacterActionDoneNewRound()
    {
        yield return null;

        playerGroup.AddCharacter(player1);
        enemyGroup.AddCharacter(enemy1);
        var round = new Round(playerGroup, enemyGroup);
        round.InitRound();

        Assert.AreEqual(player1, round.Current);

        round.CharacterActionDone(player1);
        round.CharacterActionDone(enemy1);
        Assert.AreEqual(player1, round.Current);
        Assert.AreEqual(2, round.RoundNumber);
    }
}


