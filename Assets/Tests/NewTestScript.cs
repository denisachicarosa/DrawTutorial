using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {

        public TextToImg textToImg;
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator FontAndTextAreAssignedPasses()
        {
            Font fontMock = Resources.Load<Font>("Fonts/orange-juice");
            string textMock = "Hello";
            Text text = GameObject.Find("TextForTexture").GetComponent<Text>();

            textToImg.createMaterial(fontMock, textMock);

            UnityEngine.Assertions.Assert.AreEqual(text.font, fontMock);
            UnityEngine.Assertions.Assert.AreEqual(text.text, textMock);
            yield return null;
        }
    }
}
