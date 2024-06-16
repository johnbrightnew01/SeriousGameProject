using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void DoPlay()
    {
        Controller.self.sequenceController.StartThisScene(Sequence.intreactive_intro_seq);
    }

    public void DoQuit()
    {
        Application.Quit();
    }
}
