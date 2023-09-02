using DG.Tweening;
using Internal.Scripts.Raft;
using UnityEngine;

namespace Internal.Scripts.Camera
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera cam;
        [SerializeField] private float startSize = 5;
        [SerializeField] private float endSize = 7;
        [SerializeField] private float offset = 0.66f;
        private void Start()
        {
            PlaceToUpgradeRaft.OnInstantiatePiece += CameraScale;
        }

        public void CameraScale(GameObject raftPice)
        {
            Vector2 raftPicePos = raftPice.transform.position;
            Debug.Log(cam.orthographicSize == startSize);
            if ((raftPicePos == new Vector2(0, 0.5f) || raftPicePos == new Vector2(-1.5f, -1) || raftPicePos == new Vector2(0, -2.5f) || raftPicePos == new Vector2(1.5f, -1)) && cam.orthographicSize == startSize)
            {
                cam.DOOrthoSize(startSize + offset, 0.5f).SetEase(Ease.InQuad);
                return;
            }
            if ((raftPicePos == new Vector2(0, 2f) || raftPicePos == new Vector2(3, -1) || raftPicePos == new Vector2(0, -4) || raftPicePos == new Vector2(-3, -1)) && cam.orthographicSize.Equals(startSize + offset))
            {
                cam.DOOrthoSize(startSize + 2 * offset, 0.5f).SetEase(Ease.InQuad);
                return;
            }
            if ((raftPicePos == new Vector2(0, 3.5f) || raftPicePos == new Vector2(4.5f, -1) || raftPicePos == new Vector2(0, -5.5f) || raftPicePos == new Vector2(-4.5f, -1)) && cam.orthographicSize.Equals(startSize + 2 * offset))
            {
                cam.DOOrthoSize(endSize, 0.5f).SetEase(Ease.InQuad);
                return;
            }

        }
    }
}