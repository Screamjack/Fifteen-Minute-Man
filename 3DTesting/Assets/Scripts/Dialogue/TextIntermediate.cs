using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIntermediate : MonoBehaviour {
    bool writing = false;
    public bool isWriting
    {
        get { return writing; }
    }
    bool allowDump = false;
    public bool AllowDump
    {
        get { return allowDump; }
    }
    Text body;
    public Text Body
    {
        get { return body; }
    }
    string writingString = "";
    int index = 0;

    int framesPerCharacter = 1;
    int bufferFrames = 0;

    void Awake()
    {
        body = GetComponent<Text>();
    }

    /// <summary>
    /// Writes to the text this is attached to
    /// </summary>
    /// <param name="s">The string to write</param>
    /// <param name="speed">The amount of frames between characters</param>
    public void Write(string s,int speed)
    {
        if(!writing)
        {
            body.text = "";
            writing = true;
            writingString = s;
            index = 0;
            framesPerCharacter = speed;
        }
    }

    /// <summary>
    /// Stop a text write so that another can start.
    /// </summary>
    public void StopWrite()
    {
        if(writing)
        {
            writing = false;
            writingString = "";
            index = 0;
        }
    }

    /// <summary>
    /// Stop the character by character write and dump the entire string into the text.
    /// </summary>
    public void DumpWrite()
    {
        body.text = writingString;
        writing = false;
        Debug.Log("Dumped the text string into body");
    }

    void Update()
    {
        if (!writing || index >= writingString.Length) return;
        if (Input.anyKeyDown && allowDump)
            DumpWrite();
        else if (!Input.anyKey)
            allowDump = true;
        else
            allowDump = false;
        if (bufferFrames >= framesPerCharacter)
        {
            body.text += writingString[index];
            index++;
            bufferFrames = 0;
            Debug.Log(body.text);
        }
        else
            bufferFrames++;
    }
}
