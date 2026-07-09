WOOOF converts a scenario into an object model.
A scenario is sequence of events written in a text file using a simple language.
WOOOF display the objects in the object model as nested rectangles with their properties, including references to the objects which set the properties. 
See WOOOFNotes.odp for an example of the output.

WOOOF also writes the objects with their properties to a text file and has several experimental types of output such as the result of random walks 
through the object model.
This version of the program is called WOOOF because the first script described walking the dogs.

WOOOF is a Windows program and includes several example scenarios. To use on Windows:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a script, for example drops.txt, and press command button 'run script'.
	Create or modify scripts using an ascii text editor.

The WOOOF language uses some Object Oriented Programming (OOP) ideas; Inheritance, Properties and Containment,
allowing experiments with different approaches to modelling objects and object interaction. 

Scripting and visualisation of the script using WOOOF are 2 ways of looking at the same thing. On the otherhand it is easier to develop a 
convincing script, iteratively, if it can be checked using the WOOOF output. 

The release includes the scripts:
drops.txt 			
golf.txt			
PoolAndStone.txt	
dogs.txt	

A look at these examples is an easy way to learn the WOOOF language.
A formal description of the language follows.

{COMMAND}
COMMAND = RandomWalk x | DisplaySOs * | QuerySOs | NaturalText | GenerateScript


DisplaySOs *	//display all the objects.
RandomWalk 30   //make 30 random walks through the objects.
GenerateScript  //generate a script from the Objects to create new Objects with meta data.
QuerySOs [Parent], [Child] //look for SOs with original name [Child] which have a Parent (or Parent of a Parent etc) with name [Parent].
							//The original name is the SO Name preceding brackets. 

Commands are case insensitive.


The script language allows the following but they are all removed before processing. 
// is a comment
" a "
" the "
" of "
" and "
" is "
" has "
any number of consecutive spaces is replaced by a single space.
"." at the end of a line.
"the " at the start of a line.
"an " at the start of a line.

After removal of the above a line is split based on separators: a space or a tab.

Object1 includes object2  //object2 is added at the bottom of Object1. If Object1 is named for the first time it is added to the root to the right of the other root objects.
Object1 include_right object2  ///object2 is added at the right handside of Object1. If Object1 is named for the first time it is added to the root to the right of the other root objects.
							 //See PoolAndStone script for an example of how include_right can be used to create 2 or more independent threads of time. 	
Object1 excludes object2 //If object2 was included in Object1 it is moved from Object1 to the Context object which will usually be the parent of Object1.
//See PoolAndStoneObject script for examples include_right and excludes.

[An ]Object [has] property property =  {string}
Object property property is {string}
Object property property  {string}

Object [is] [a] type [of] Object

endobject Object	//The Object and all nested objects get the property ended = true and are greyed out.
context Object		//If a property is set in an object a reference property is added to the originating object. The originating object is the 'context'.
					//If 'Object1 includes Object2' the context will be Object1. Setting any a properties of Object2 will create a reference property in Object1.
					//The context command is useful if the context is no longer Object1 but a property is changed in Object2 and the script needs to show that Object1 
					//altered the property.
					//For example 
					//stone_water Includes stone
					//stone has property wet = true
					//.....
					//STONE_LEAVES_AIR Includes water	//context is STONE_LEAVES_AIR
					//stone has property wet = false	//reference property is in STONE_LEAVES_AIR
					//context stone_water				//context is stone_water
					//stone has property wet = true		//reference property is in stone_water

