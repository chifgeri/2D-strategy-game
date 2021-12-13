using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MainStateManagerTests
{
    GameObject mainStateManager = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Managers/MainStateManager"));

    [UnityTest]
    public IEnumerator TestMainStateManagerStartGame()
    {
        yield return null;

        // Prevent level loading, add a dummy level list
        MainStateManager.Instance.Levels = new List<Model.Map>()
        {
            new Model.Map(
                "asd",
                new List<string>(),
                new Vector2(0,0),
                10,
                10,
                4)
        };

        MainStateManager.Instance.StartNewGame();
        Assert.NotNull(MainStateManager.Instance);


        Assert.NotNull(MainStateManager.Instance.GameState.Inventory);
        Assert.AreEqual(15, MainStateManager.Instance.GameState.Inventory.GetItems().Count);
        Assert.AreEqual(2, MainStateManager.Instance.GameState.PlayableCharacters.Count);
        Assert.AreEqual(10000, MainStateManager.Instance.GameState.Money);

    }
}
