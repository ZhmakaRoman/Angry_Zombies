using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField]
    private GameObject _skullPrefab;
    [SerializeField]
    private Transform _spawnSkull;

    public IEnumerator CallTheSkull()
    {
        yield return new WaitForSeconds(2);
        if (_skullPrefab != null)
        {
            _spawnSkull.transform.position = _spawnSkull.position;
        }
    }
}
