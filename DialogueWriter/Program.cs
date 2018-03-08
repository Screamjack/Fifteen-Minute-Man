using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

/*XML FORMAT
 * Element is either "Dialogue" or "Option"
 * Element Attributes:
 *      Dialogue
 *          Speed
 *      Options
 *          OptionType
 *              If Trigger then also trigger name
 *          Number
 *  InnerText
 *      Dialogue: The Dialogue
 *      Options: The rebuttal text
 */

namespace DialogueWriter
{
    class Program
    {
        const string ROOT = "Talker";
        const string DIALOGUE = "Dialogue";
        const string OPTION = "Option";
        const string SPEECH = "Speech";
        const string SPEED_ATTRIBUTE = "Speed";
        const string OPTION_TYPE_ATTRIBUTE = "Type";
        const string OPTION_GAMEOBJECT_ATTRIBUTE = "GameObject";
        const string OPTION_NUMBER = "Number";
        const string TRIGGER_ATTRIBUTE = "TriggerName";
        const string CONTAINER_ATTRIBUTE = "Container";

        const string SPEED_DEFAULT = "3";
        const string OPTION_BASIC = "Basic";
        const string OPTION_TRIGGER = "Trigger";

        static readonly string DIRECTORY = Directory.GetCurrentDirectory() + "/dialogue/";
        //Because switch statements aren't fun
        //"UP",
        //"DOWN", //0,1,2,3
        //"NODE", //o, d | Creates new node. Set pointer to that node.
        //"SET", //s, t, n, i   
        //"DELETE",   
        //"SAVE",
        //"HELP"
        //"PRINT"



        static void Writer(string document)
        {
            string docName = document;
            bool unsaved = false;
            XmlNode currentItem = null;
            XmlNode root = null;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(DIRECTORY + docName);
                root = doc.FirstChild;
                currentItem = root;
            }
            catch (Exception e)
            {
                if(e is FileNotFoundException)
                {
                    Console.WriteLine("No document found. Creating a new one.");
                    doc = new XmlDocument();
                    Console.WriteLine("Who does this dialogue belong to? ");
                    string name = Console.ReadLine();
                    root = doc.CreateElement(ROOT);
                    XmlAttribute att1 = doc.CreateAttribute(CONTAINER_ATTRIBUTE);
                    att1.Value = name;
                    root.Attributes.Append(att1);
                    doc.AppendChild(root);
                    currentItem = root;
                    Console.WriteLine("");
                    unsaved = true;
                }
                else
                {
                    Console.WriteLine("Could not load the XML file.\nEither the document was corrupted or cannot be accessed due to security." + e);
                }
            }
            //We can assume from here that the file is opened and that there is a root and current item = root.
            string input = "MEME";
            while(input != "STOP") //Command loop
            {
                input = Console.ReadLine();
                string[] tokens = input.Split(' ');
                switch(tokens[0].ToUpper())
                {
                    case "PRINT": //Shows node information. 
                        XmlNode speechNode = null;
                        Console.WriteLine("");
                        Console.WriteLine("CURRENT NODE: " + currentItem.Name);
                        foreach (XmlAttribute x in currentItem.Attributes)
                        {
                            Console.WriteLine("ATTRIBUTE:  " + x.Name.PadRight(20)+ " | VALUE: " + x.Value);
                        }
                        if(currentItem.ParentNode != null)
                            Console.WriteLine("PARENT: " + currentItem.ParentNode.Name);
                        foreach (XmlNode x in currentItem.ChildNodes)
                        {
                            if (x.Name != SPEECH)
                                Console.WriteLine("CHILD NODE: " + x.Name);
                            else
                                speechNode = x;
                        }
                        if(speechNode != null)
                        {
                            Console.WriteLine("NODE TEXT: " + speechNode.InnerText);
                        }
                        Console.WriteLine("");
                        break;
                    case "UP": //UP
                        if (currentItem.ParentNode != null && currentItem.ParentNode.Name != "#document")
                        {
                            currentItem = currentItem.ParentNode;
                            Console.WriteLine("Moved up to parent.");
                        }
                        else
                            Console.WriteLine("Cannot go up any further.");
                        break;
                    case "DOWN": //Go down to a child.
                        if(tokens.Length < 2)
                        {
                            Console.WriteLine("Not enough arguments");
                        }
                        else
                        {
                            int which;
                            if (Int32.TryParse(tokens[1], out which))
                            {
                                List<XmlNode> options = new List<XmlNode>();
                                foreach(XmlNode n in currentItem.ChildNodes)
                                {
                                    if (n.Name == DIALOGUE || n.Name == OPTION)
                                        options.Add(n);
                                }
                                if (which < options.Count)
                                {
                                    currentItem = options[which];
                                    Console.WriteLine("Moved down to child " + (which + 1));
                                }
                                else
                                    Console.WriteLine("Node " + which + " does not exist. Remember it's by index.");
                            }
                            else
                                Console.WriteLine("Could not parse the argument.");
                        }
                        break;
                    case "NODE": //Node creation parsing
                        if (tokens.Length < 2)
                        {
                            Console.WriteLine("Not enough arguments");
                        }
                        else
                        {
                            string what = tokens[1].ToUpper();
                            if (what == "O") //Option Creation parsing
                            {
                                if (currentItem.Name != DIALOGUE)
                                    Console.WriteLine("You can only connect Options to a Dialogue choice.");
                                else
                                {
                                    XmlNode child = doc.CreateElement(OPTION);
                                    XmlAttribute att1 = doc.CreateAttribute(OPTION_TYPE_ATTRIBUTE);
                                    XmlAttribute att2 = doc.CreateAttribute(TRIGGER_ATTRIBUTE);
                                    XmlAttribute att3 = doc.CreateAttribute(OPTION_NUMBER);
                                    XmlAttribute att4 = doc.CreateAttribute(OPTION_GAMEOBJECT_ATTRIBUTE);
                                    att1.Value = OPTION_BASIC;
                                    att2.Value = " ";
                                    att3.Value = currentItem.ChildNodes.Count.ToString();
                                    att4.Value = "";
                                    child.Attributes.Append(att1);
                                    child.Attributes.Append(att2);
                                    child.Attributes.Append(att3);
                                    child.Attributes.Append(att4);
                                    currentItem.AppendChild(child);
                                    currentItem = child;
                                    Console.WriteLine("Basic Option Node created. Pointer set to it.");
                                    unsaved = true;
                                }
                            }
                            else if (what == "D") //Dialogue Creation parsing
                            {
                                Console.WriteLine(currentItem.ChildNodes.Count);
                                if (currentItem.Name != OPTION && currentItem.Name != ROOT)
                                    Console.WriteLine("You can only connect Dialogue to an Option or the Root node.");
                                else
                                {
                                    XmlNode child = doc.CreateElement(DIALOGUE);
                                    XmlAttribute att1 = doc.CreateAttribute(SPEED_ATTRIBUTE);
                                    att1.Value = SPEED_DEFAULT;
                                    child.Attributes.Append(att1);
                                    currentItem.AppendChild(child);
                                    currentItem = child;
                                    Console.WriteLine("Basic Dialogue Node created. Pointer set to it.");
                                    unsaved = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Argument not recognized.");
                            }
                        }
                        break;
                    case "SET": //Set values
                        //Speed ,Trigger, InnerText
                        if (tokens.Length < 2)
                        {
                            Console.WriteLine("Not enough arguments");
                        }
                        else
                        {
                            if (tokens[1].ToLower() != "t" && tokens[1].ToLower() != "s" && tokens[1].ToLower() != "i" && tokens[1].ToLower() != "g")
                            {
                                Console.WriteLine("Unknown argument");
                            }
                            else
                            {
                                Console.WriteLine("Insert the new value here");
                                if(tokens[1].ToLower() == "t")
                                {
                                    Console.WriteLine("Setting the trigger to \" \" (SPACE) will force the option to be \"Basic\".");
                                }
                                string setInput = Console.ReadLine();
                                int x; //Dummy variable to 
                                if (tokens[1].ToLower() == "i") //Set inner text
                                {
                                    XmlNode textNode = null;
                                    foreach(XmlNode node in currentItem.ChildNodes)
                                    {
                                        if(node.Name == SPEECH)
                                        {
                                            textNode = node;
                                            break;
                                        }
                                    }
                                    if(textNode == null)
                                    {
                                        textNode = doc.CreateElement(SPEECH);
                                        textNode.InnerText = setInput;
                                        currentItem.AppendChild(textNode);
                                        Console.WriteLine("Constructed text element and set it.");
                                    }
                                    else
                                    {
                                        textNode.InnerText = setInput;
                                        Console.WriteLine("Updated text element.");
                                    }
                                    unsaved = true;
                                }
                                else if (tokens[1].ToLower() == "s")
                                {
                                    if (!Int32.TryParse(setInput, out x))
                                    {
                                        Console.WriteLine("Input is not a number.");
                                    }
                                    else if (currentItem?.Attributes[SPEED_ATTRIBUTE] == null)
                                    {
                                        Console.WriteLine("You cannot set this here.");
                                    }
                                    else
                                    {
                                        currentItem.Attributes[SPEED_ATTRIBUTE].Value = setInput;
                                        Console.WriteLine("Set the speed of the dialogue.");
                                        unsaved = true;
                                    }
                                }
                                else if (tokens[1].ToLower() == "t")
                                {
                                    if (currentItem?.Attributes[TRIGGER_ATTRIBUTE] == null)
                                        Console.WriteLine("You cannot set this here.");
                                    else
                                    {
                                        currentItem.Attributes[TRIGGER_ATTRIBUTE].Value = setInput;
                                        Console.WriteLine("Set the Trigger of the dialogue option.");
                                        if(currentItem.Attributes[TRIGGER_ATTRIBUTE].Value != " ")
                                        {
                                            currentItem.Attributes[OPTION_TYPE_ATTRIBUTE].Value = OPTION_TRIGGER;
                                            Console.WriteLine("Switched option type to \"Trigger\"");
                                        }
                                        else
                                        {
                                            currentItem.Attributes[OPTION_TYPE_ATTRIBUTE].Value = OPTION_BASIC;
                                            Console.WriteLine("Switched option type to \"Basic\"");
                                        }
                                        unsaved = true;
                                    }
                                }
                                else if (tokens[1].ToLower() == "g")
                                {
                                    if (currentItem?.Attributes[OPTION_GAMEOBJECT_ATTRIBUTE] == null)
                                        Console.WriteLine("You cannot set this here.");
                                    else
                                    {
                                        currentItem.Attributes[OPTION_GAMEOBJECT_ATTRIBUTE].Value = setInput;
                                        Console.WriteLine("Set the GameObject of the dialogue option.");
                                        unsaved = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "SAVE": //Save current file
                        doc.Save(DIRECTORY+docName);
                        Console.WriteLine("Fukkin' Saved");
                        unsaved = false;
                        break;
                    case "HELP":
                        Console.WriteLine("\nThis stupid thing is an XML writer for that game.");
                        Console.WriteLine("Commands:");
                        Console.WriteLine("\tSTOP: Halts the system.");
                        Console.WriteLine("\tNODE: Adds a new node. ");
                        Console.WriteLine("\t\td argument: Makes the node a dialog node.");
                        Console.WriteLine("\t\to argument: Makes the node an option node.");
                        Console.WriteLine("\tPRINT: Prints the node information.");
                        Console.WriteLine("\tSAVE: Saves the current file.");
                        Console.WriteLine("\tLOAD: Loads a new file.");
                        Console.WriteLine("\tUP: Move up to the parent node.");
                        Console.WriteLine("\tDOWN: Move down to a child node.");
                        Console.WriteLine("\t\t0-3 argument: Which child to go down to.");
                        Console.WriteLine("\tSET: Sets a value in a node.");
                        Console.WriteLine("\t\tt: Sets the Trigger name. Also handles the option type.");
                        Console.WriteLine("\t\ti: Sets the text value for the node. IE) The dialogue text or rebuttal text.");
                        Console.WriteLine("\t\ts: Sets the speed of the dialogue tick.");
                        Console.WriteLine("\t\tg: Sets the gameobject for a trigger");
                        Console.WriteLine("");
                        break;
                    case "DELETE": //Delete node and all children
                        if (currentItem.Name != DIALOGUE && currentItem.Name != OPTION)
                            Console.WriteLine("You cannot delete this.");
                        else
                        {
                            Console.WriteLine("This action will delete all child nodes on the selected node. Type \"YES\" to confirm. \n Anything else cancels delete.");
                            string confirmation = Console.ReadLine();
                            if (confirmation == "YES")
                            {
                                XmlNode tempNode = currentItem;
                                currentItem = currentItem.ParentNode;
                                currentItem.RemoveChild(tempNode);
                                Console.WriteLine("Deleted node. Pointer at parent.");
                                unsaved = true;
                            }
                            else
                            {
                                Console.WriteLine("Deletion Cancelled");
                            }
                        }
                        break;
                    case "STOP": //End program
                        input = input.ToUpper();
                        if(unsaved)
                        {
                            Console.WriteLine("You have unsaved work. Type \"YES\" to force exit. Any other input will cancel the exit.");
                            string confirmation = Console.ReadLine();
                            if (confirmation != "YES")
                                input = "MEME";
                        }
                        break;
                    case "LOAD": //Load in a new file.
                        if (tokens.Length <= 1)
                        {
                            Console.WriteLine("Not enough arguments.");
                        }
                        else
                        {
                            if (unsaved)
                            {
                                Console.WriteLine("You have unsaved work. Type \"YES\" to force a load. \n Type \"SAVE\" to save and load. All other input cancels the load.");
                                string confirmation = Console.ReadLine();
                                if (confirmation == "YES")
                                {
                                    try
                                    {
                                        docName = tokens[1] + ".xml";
                                        doc.Load(DIRECTORY+docName);
                                        unsaved = false;
                                    }
                                    catch (Exception e)
                                    {
                                        if (e is FileNotFoundException)
                                        {
                                            Console.WriteLine("File not found");
                                            Console.WriteLine("Creating new file");
                                            docName = tokens[1] + ".xml";
                                            doc = new XmlDocument();
                                            Console.WriteLine("Who does this dialogue belong to? ");
                                            string name = Console.ReadLine();
                                            root = doc.CreateElement(ROOT);
                                            XmlAttribute att1 = doc.CreateAttribute(CONTAINER_ATTRIBUTE);
                                            att1.Value = name;
                                            root.Attributes.Append(att1);
                                            doc.AppendChild(root);
                                            currentItem = root;
                                            Console.WriteLine("");
                                            unsaved = true;

                                        }
                                            
                                        else
                                            Console.WriteLine("Something bad happened. " + e);
                                    }
                                }
                                if (confirmation == "SAVE")
                                {
                                    try
                                    {
                                        doc.Save(DIRECTORY + docName);
                                        docName = tokens[1] + ".xml";
                                        doc.Load(DIRECTORY + docName);
                                    }
                                    catch(Exception e)
                                    {
                                        if (e is FileNotFoundException)
                                            Console.WriteLine("File not found");
                                        else
                                            Console.WriteLine("Something bad happened. " + e);
                                    }
                                }
                            }
                            else
                            {
                                docName = tokens[1] + ".xml";
                                doc.Load(DIRECTORY + docName);
                            }
                        }
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("##########################################");
            Console.WriteLine("# A REALLY BAD XML TREE BUILDER FOR GAME #");
            Console.WriteLine("##########################################");
            Console.WriteLine("Type in the name of the document to begin.");
            Console.WriteLine("Don't include the file extension.");
            if(!Directory.Exists(DIRECTORY))
            {
                Directory.CreateDirectory(DIRECTORY);
            }
            string input = Console.ReadLine();
            input += ".xml";
            Writer(input);
            Console.WriteLine("Exiting...");
        }
    }
}
