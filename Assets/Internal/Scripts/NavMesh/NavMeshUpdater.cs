using Internal.Scripts.Raft;
using NavMeshPlus.Components;
using UnityEngine;

namespace Internal.Scripts.NavMesh
{
    public class NavMeshUpdater : MonoBehaviour
    {
        public NavMeshSurface Surface2D;
        private void OnEnable()
        {
            PlaceToUpgradeRaft.OnBuyPiece += UpdateNavMesh;
        }

        private void OnDisable()
        {
            PlaceToUpgradeRaft.OnBuyPiece -= UpdateNavMesh;
        }

        private void UpdateNavMesh(int value)
        {
            Surface2D.BuildNavMeshAsync();
            Debug.Log("UpdateNavMesh");
        }
    }
}