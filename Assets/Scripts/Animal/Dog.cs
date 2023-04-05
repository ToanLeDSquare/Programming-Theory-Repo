using System.Collections;
using UnityEngine;

namespace Animal
{
    public class Dog : Animal.Base.Animal
    {
        private static readonly int SpeedF = Animator.StringToHash("Speed_f");
        private static readonly int BarkB = Animator.StringToHash("Bark_b");

        protected override IEnumerator MoveRoutine(Vector3 direction)
        {
            AnimController.SetFloat(SpeedF, 1f);
            yield return base.MoveRoutine(direction);
            AnimController.SetFloat(SpeedF, 0f);
        }

        protected override void AnimalVoice()
        {
            MoveEnabled = false;
            var dialog = DialogManager.Instance.ShowDialog(gameObject.name, "Bark");
            StartCoroutine(dialog);
        }

        protected override IEnumerator AnimalAction()
        {
            AnimController.SetBool(BarkB ,true);
            yield return new WaitForSeconds(2.5f);
            AnimController.SetBool(BarkB ,false);
            MoveEnabled = true;
        }
    }
}
