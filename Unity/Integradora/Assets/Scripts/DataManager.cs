using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    
    [SerializeField]
    private Car[] _carros;
    private GameObject[] _carrosGO;
    
    [SerializeField]
    private TrafficLight[] _lights;
    [SerializeField]
    private Light[] _lightsGO;

    // Start is called before the first frame update
    void Start() {
        _carrosGO = new GameObject[_carros.Length];
        for(int i = 0; i < _carros.Length; i++) {
            _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector3.zero);
        }
        PosicionarCarros();

        CambiarColor();
    }

    private void PosicionarCarros() {
        for(int i = 0; i < _carros.Length; i++) {
            _carrosGO[i].transform.position = new Vector3(_carros[i].x, 0, _carros[i].z);
            _carrosGO[i].transform.rotation = Quaternion.Euler(new Vector3(0, -_carros[i].dir, 0));
        }
    }

    private void CambiarColor(){
        for(int i = 0; i < _lights.Length; i++){
            if(_lights[i].color == "green"){
                _lightsGO[i].color = Color.green;
            }
            else if(_lights[i].color == "yellow"){
                _lightsGO[i].color = Color.yellow;
            }
            else if(_lights[i].color == "red"){
                _lightsGO[i].color = Color.red;
            }
        }
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            for(int i = 0; i < _carros.Length; i++){
                _carros[i].x = Random.Range(0f, 10f);
                _carros[i].z = Random.Range(0f, 10f);
            }

            PosicionarCarros();
        }
    }

    public void EscucharRequestSinArgumentos() {
        print("Request Sin Argumentos");
    }

    public void EscucharRequestConArgumentos(ListaCarros datos){
        print("DATOS: " + datos);
        StartCoroutine(ConsumirSteps(datos));
    }


    private IEnumerator ConsumirSteps(ListaCarros datos) {

        for(int i = 0; i < datos.frames.Length; i++){
            _carros = datos.frames[i].cars;
            PosicionarCarros();

            _lights = datos.frames[i].traffic_lights;
            CambiarColor();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
