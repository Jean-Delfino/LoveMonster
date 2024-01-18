using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests.PlayMode
{
    public class MonsterTestingPlayMode
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TryFibonacci()
        {
            var fibonacci = UtilMath.GetFibonacciRecursively(20);
            // Use the Assert class to test conditions
            Assert.AreEqual(fibonacci, 6765);
        }
        
        [UnityTest]
        public IEnumerator TrySpawner()
        {
            yield return new WaitUntil(() => Spawner.Instance != null);
            var go = Spawner.Spawn(new GameObject());
            
            Assert.AreNotEqual(go, null);
        }
    }
}
