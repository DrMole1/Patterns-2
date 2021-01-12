using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sphere
{
    public void Reception(object sender, EventArgs e)
    {
        Debug.Log("Sphere a reçu: {1} de: {0} " + sender.ToString() + e.ToString());
    }
}

class MessageEventArgs : EventArgs
{
    public uint Messages;
    // Rajoute un s s'il y a plusieurs messages
    public override string ToString()
    {
        return string.Format("{0} message{1}", (Messages > 0) ? Messages.ToString() : "Plus de",(Messages > 1) ? "s" : string.Empty);
    }
}

public class ObserverPattern : MonoBehaviour
{

    // ======== VARIABLES ========

    public Sphere sphere;

    // ===========================

    static event EventHandler observable;

    private void Start()
    {
        sphere = new Sphere();

        // Enregistrement de la sphere dans la reception
        observable += new EventHandler(sphere.Reception);

        // Le nombre de messages est compris entre 1 et 3 inclu
        uint nMessage = (uint)UnityEngine.Random.Range(1, 4);

        // Observe puis print
        if (observable != null)
            observable(AppDomain.CurrentDomain, new MessageEventArgs() { Messages = nMessage });
    }
}
