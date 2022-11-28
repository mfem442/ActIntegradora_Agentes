using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.IO;


[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListaCarros> {}

public class RequestManager : MonoBehaviour {

    [SerializeField]
    private UnityEvent _requestRecibidaSinArgumentos;

    [SerializeField]
    private RequestConArgumentos _requestConArgumentos;
    
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(HacerRequest());
    }

    public IEnumerator HacerRequest() {

        yield return new WaitForSeconds(1);

        string jsonSource = File.ReadAllText("Assets/data.json");
        ListaCarros listaCarros = JsonUtility.FromJson<ListaCarros>(jsonSource);
        
        ListaCarros dummy = new ListaCarros();
        dummy.frames = new Frame[listaCarros.frames.Length];

        for(int i = 0; i < dummy.frames.Length; i++){

            dummy.frames[i] = new Frame();
            
            // carros
            dummy.frames[i].cars = new Car[10];
            for(int j = 0; j < dummy.frames[i].cars.Length; j++){
                dummy.frames[i].cars[j] = new Car();
                dummy.frames[i].cars[j].x = listaCarros.frames[i].cars[j].x;
                dummy.frames[i].cars[j].z = listaCarros.frames[i].cars[j].z;
                dummy.frames[i].cars[j].dir = listaCarros.frames[i].cars[j].dir;
            }

            // semáforos
            dummy.frames[i].traffic_lights = new TrafficLight[listaCarros.frames[i].traffic_lights.Length];
            for(int j = 0; j < dummy.frames[i].traffic_lights.Length; j++){
                dummy.frames[i].traffic_lights[j] = new TrafficLight();
                dummy.frames[i].traffic_lights[j].color = listaCarros.frames[i].traffic_lights[j].color;
            }
        }

        _requestConArgumentos?.Invoke(dummy);
    }
}
