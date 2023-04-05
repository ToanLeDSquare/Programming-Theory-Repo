using System.Collections;
using UnityEngine;

namespace Animal
{
    public class Fox : Animal.Base.Animal
    {
        private static readonly int SpeedF = Animator.StringToHash("Speed_f");
        private static readonly int EatB = Animator.StringToHash("Eat_b");

        protected override IEnumerator MoveRoutine(Vector3 direction)
        {
            AnimController.SetFloat(SpeedF, 1f);
            yield return base.MoveRoutine(direction);
            AnimController.SetFloat(SpeedF, 0f);
        }

        protected override void AnimalVoice()
        {
            MoveEnabled = false;
            var dialog = DialogManager.Instance.ShowDialog(gameObject.name,"Eat");
            StartCoroutine(dialog);
        }

        protected override IEnumerator AnimalAction()
        {
            AnimController.SetBool(EatB ,true);
            yield return new WaitForSeconds(3f);
            AnimController.SetBool(EatB ,false);
            MoveEnabled = true;
        }
    }
}
