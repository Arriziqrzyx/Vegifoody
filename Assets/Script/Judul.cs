using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Judul : MonoBehaviour
{

    void Start()
    {
        transform.DOScale(0.9f, 3.0f)
            .SetEase(Ease.InOutBack)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
