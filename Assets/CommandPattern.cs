using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICommand
{
    void Execute();
}

// Etat de la lumière
public enum EtatLumiere
{
    On,
    Off
}

public class Switch
{
    // Liste de commandes
    private List<ICommand> _commands = new List<ICommand>();

    public void StoreAndExecute(ICommand command)
    {
        _commands.Add(command);
        command.Execute();
    }
}

public class Light
{
    // Etat de la lumière
    public EtatLumiere etat = EtatLumiere.Off;

    // Instance de la lumière en jeu
    public GameObject lightInstance;

    public void TurnOn()
    {
        etat = EtatLumiere.On;
        lightInstance.SetActive(true);
    }

    public void TurnOff()
    {
        etat = EtatLumiere.Off;
        lightInstance.SetActive(false);
    }
}

// Commande pour allumer
public class FlipUpCommand : ICommand
{
    private Light _light;

    public FlipUpCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}

// Commande pour éteindre
public class FlipDownCommand : ICommand
{
    private Light _light;

    public FlipDownCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}



public class CommandPattern : MonoBehaviour
{
    // ====== VARIABLES ======

    Light lamp;
    Switch s;

    ICommand switchUp;
    ICommand switchDown;

    // =======================


    private void Start()
    {
        // Affectations
        lamp = new Light();
        switchUp = new FlipUpCommand(lamp);
        switchDown = new FlipDownCommand(lamp);
        s = new Switch();

        // L'instance de la lumière est l'enfant de l'interrupteur
        lamp.lightInstance = transform.GetChild(0).gameObject;

        // Pour informer le joueur en jeu
        print("Cliquez sur l'interrupteur !");
    }

    private void OnMouseDown()
    {
        if(lamp.etat == EtatLumiere.On)
        {
            s.StoreAndExecute(switchDown);
            print("La lumière est éteinte.");
        }
        else
        {
            s.StoreAndExecute(switchUp);
            print("La lumière est allumée.");
        }
    }
}
