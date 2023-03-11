using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishingRodView : MonoBehaviour
{
    private float rodCastTimer = 1f;
    private float lineFlyTimer = 0.1f;

    [SerializeField] private FishingManager fishingManager;

    public IEnumerator Animate_CastRod() {
        Sequence seq = DOTween.Sequence();
        Vector3 originalRodPosition = transform.position;
        Vector3 originalScale = transform.localScale;


        seq.Append(transform.DOMove(new Vector3(transform.position.x + 1f, transform.position.y + 1.5f, transform.position.z), rodCastTimer));
        seq.Join(transform.DOScale(new Vector3(1.5f, 1.5f, 1), rodCastTimer));
        seq.Join(transform.DORotate(new Vector3(0,0,-30), rodCastTimer).OnComplete(() => {
            transform.DOMove(originalRodPosition, lineFlyTimer);
            transform.DOScale(originalScale, lineFlyTimer);
            transform.DORotate(Vector3.zero, lineFlyTimer);
        }));

        yield return new WaitForSeconds(rodCastTimer + lineFlyTimer);

        fishingManager.ChangeState(FishingManager.fishingSubGameStates.waitingForBite);
        Debug.Log("line cast!");

    }

    public IEnumerator Animate_FishIsBiting() {
        Tween tween = transform.DOShakePosition(1f, new Vector3(0.15f, 0f, 0f), 10, 0f, false, false);

        yield return tween.WaitForCompletion();

        fishingManager.ChangeState(FishingManager.fishingSubGameStates.biteRegistered);
        Debug.Log("you have a bite, now reel");
    }
}
