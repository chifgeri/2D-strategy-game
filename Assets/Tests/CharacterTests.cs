using System.Collections;
using System.Collections.Generic;
using Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestCharacterHeal()
    {
        var gameObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Axeman"));

        var character = gameObject.GetComponent<PlayerCharacter>();
        character.Health = 0;

        character.Heal(10);

        Assert.Equals(character.Health, 10);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
