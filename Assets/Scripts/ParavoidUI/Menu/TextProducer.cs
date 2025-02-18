using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ParavoidUI
{
    /**
    * When using this script, make sure to attach it to the desired gameobject that has a text component, otherwise
    * the script will not work as intended.
    */
    public class TextProducer : MonoBehaviour
    {
        public Text textBox;

        public void Awake()
        {
            textBox = this.GetComponent<Text>();
        }

        public void RunText(string text, Effect effect, float effectAmount)
        {
            switch (effect)
            {
                case Effect.Type:
                    StartCoroutine(TypeText(text, effectAmount));
                    break;

                case Effect.None:
                default:
                    textBox.text = text;
                    break;
            }
        }

        public void RevertText(Effect effect, float effectAmount)
        {
            switch (effect)
            {
                case Effect.Type:
                    StartCoroutine(TypeRevertText(effectAmount));
                    break;

                case Effect.None:
                default:
                    RemoveText();
                    break;
            }
        }

        public void RemoveText()
        {
            textBox.text = "";
        }

        /**
        * Using Javadoc style for calrity
        *
        * @param text a string that is the input text for to be written on the textbox
        * @param effect an enum of type Effect that determines the text transmission.
        * @param effectAmount a float that is used as input for whatever the effect may be
        * @param sec a float that determines how long the text will stay on screen
        * @param revert a boolean value that determines whether the text will revert with the same effect or revert
        * with no effect.
        */
        public void RunTextFor(string text, Effect effect, float effectAmount, float sec, bool revert)
        {
            RunText(text, effect, effectAmount);

            if (revert)
            {
                StartCoroutine(TimedText(sec, effect, effectAmount));
            }
            else
            {
                StartCoroutine(TimedText(sec));
            }
        }

        private IEnumerator TimedText(float time)
        {
            yield return new WaitForSeconds(time);
            RemoveText();
        }

        private IEnumerator TimedText(float time, Effect effect, float effectAmount)
        {
            yield return new WaitForSeconds(time);
            RevertText(effect, effectAmount);
        }


        #region Text Effect Methods

        private IEnumerator TypeText(string text, float delay)
        {
            for (int i = 0; i < text.Length; i++)
            {
                textBox.text += text.Substring(i, 1);
                yield return new WaitForSeconds(delay);
            }

        }

        private IEnumerator TypeRevertText(float delay)
        {
            while (textBox.text.Length > 0)
            {
                textBox.text = textBox.text.Remove(textBox.text.Length - 1);
                yield return new WaitForSeconds(delay);
            }

        }


        #endregion

    }
}
