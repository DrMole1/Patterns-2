using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IStrategie
{
    void MettreEnOeuvre(GameObject _go);
}

public class Cube
{
    public GameObject cubeInstance;

    // Stratégie actuelle
    IStrategie _strategie;

    // Mutateur de la stratégie
    public IStrategie Strategie { set { _strategie = value; } }

    public void Agir()
    {
        _strategie.MettreEnOeuvre(cubeInstance);
    }
}

class DevenirInvisible : IStrategie
{
    public void MettreEnOeuvre(GameObject _go)
    {
        _go.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Le cube devient invisible.");
    }
}
class SubirLaGravite : IStrategie
{
    public void MettreEnOeuvre(GameObject _go)
    {
        _go.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("Le cube subit la gravité.");
    }
}
class Grossir : IStrategie
{
    public void MettreEnOeuvre(GameObject _go)
    {
        _go.transform.localScale = new Vector3(3, 3, 3);
        Debug.Log("Le cube grossit.");
    }
}
class Rapetisser : IStrategie
{
    public void MettreEnOeuvre(GameObject _go)
    {
        _go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Debug.Log("Le cube rapetisse.");
    }
}

public enum Etat
{
    TropVisible,
    TropStable,
    TropPetit,
    TropGros
}


public class StrategyPattern : MonoBehaviour
{
    // ===== VARIABLES =====

    Cube monCube = null;
    [SerializeField] Etat etat;

    // =====================

    private void Start()
    {
        // Notre cube
        monCube = new Cube();

        // On lui affecte l'instance du cube visible dans la scène
        monCube.cubeInstance = gameObject;

        // On lui affecte aléatoirement un état
        int choice = UnityEngine.Random.Range(0, 4);
        etat = (Etat)choice;

        // Appelle de la méthode de strategy
        AppliquerStrategie();
    }

    // Application de la stratégie
    public void AppliquerStrategie()
    {
        switch (etat)
        {
            case Etat.TropVisible:
                monCube.Strategie = new DevenirInvisible(); break;
            case Etat.TropStable:
                monCube.Strategie = new SubirLaGravite(); break;
            case Etat.TropPetit:
                monCube.Strategie = new Grossir(); break;
            case Etat.TropGros:
                monCube.Strategie = new Rapetisser(); break;
            default:
                throw new Exception("Erreur");
        }

        monCube.Agir();
    }
}