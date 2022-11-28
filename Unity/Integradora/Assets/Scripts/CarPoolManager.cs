using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPoolManager : MonoBehaviour {
    public static CarPoolManager Instance {
        get;
        private set;
    }

    private float _dummy;
    public float Dummy{
        get{
            return _dummy;
        }
        set{
            _dummy = value;
        }
    }

    [SerializeField]
    private GameObject[] _tiposCarros;
    
    [SerializeField]
    private int _tamanioDePool;
    
    private Queue<GameObject> _pool;

    void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _pool = new Queue<GameObject>();
        for(int i = 0; i < _tamanioDePool; i++){

            GameObject nuevoObjeto = Instantiate<GameObject>(_tiposCarros[Random.Range(0, 4)]);
            _pool.Enqueue(nuevoObjeto);
            nuevoObjeto.SetActive(false);
        }
    }

    public GameObject ActivarObjeto(Vector3 posicion){

        if(_pool == null || _pool.Count == 0){
            Debug.LogError("SE ACABO EL POOL");
            return null;
        }

        GameObject objetoActivado = _pool.Dequeue();
        objetoActivado.SetActive(true);
        objetoActivado.transform.position = posicion;
        return objetoActivado;
    }

    public void DesactivarObjeto(GameObject objetoADesactivar){
        objetoADesactivar.SetActive(false);
        _pool.Enqueue(objetoADesactivar);
    }
}
