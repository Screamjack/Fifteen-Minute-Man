This is a stupid little XML writer that writes some really bad XML to get parsed as dialogue.
Every dialogue written will appear in a "dialogue" directory in the same folder as the .exe.

Some notes on how to get this trainwreck to work properly
-Make sure everything is set before moving on.
-If a Speech element is missing, it will break a lot of things.
-You might need to run this with special permissions to let it edit files. 
-It's pretty unintuitive right now to move around and understand where you are in the tree
--I will likely change this in a little bit.

Typing "HELP" will show you a list of commands and arguments.
There is no autosave, but hopefully it will tell you if you are about to abandon unsaved work.

Alongside the exe is a .cs file. This is the source code.
This is covered under the same license as the rest of the repo.

I'm sorry.
-Noah