WOOOF converts a scenario into an object model.
A scenario is sequence of events written in a text file using a simple language.
WOOOF displays the object model as nested rectangles with their properties, including references to the objects which set the properties. 
See OOOandWOOOF.pdf for examples of the output.

WOOOF also writes the objects with their properties to a text file and has several experimental types of output such as the result of random walks 
through the object model.
This version of the program is called WOOOF because the first script described walking the dogs.

WOOOF is a Windows program and includes several example scenarios. To use on Windows:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a script, for example drops.txt, and press command button 'run script'.
	Create or modify scripts using an ascii text editor.

The WOOOF language uses some Object Oriented Programming (OOP) ideas; Inheritance, Properties and Containment/Encapsulation.
WOOOF allows experiments with different approaches to modelling objects and object interaction. 

The release includes the following scripts:
drops.txt 			2 drops of water coalesce. The resulting drop splits in a gust of wind.
golf.txt			A golfclub hits a golf ball and the club and ball form a new object. 
					After a period of intense energy transfer the club and ball separate.
PoolAndStone.txt	Under the influence of gravity a stone falls through air towards the surface of a pool of water. The stone enters the water and eventually
					comes to rest at the bottom of the pool.

The above represent inanimate interactions without an observer. The dogs.txt introduces an observer.					
dogs.txt			A dog is sitting then running and then barking at an area where dogs can run freely. 
					The observer recognises the running dog and hears it then barking.

ACW.txt				ACW stands for the American Civil War and is an early script which was inspired by Graham Harman's analysis and ideas on object development using symbiosis. 
PoolAndStoneObject.txt	An alternative to the PoolAndStone.txt script.


1. In OOP an object may include other objects. 
In WOOOF this is implemented using the 'includes' command. 
	Me includes My_Nida

2. INHERITANCE. WOOOF supports inheritance using the 'type' or 'type of' command. 
Inheritance is an OOP term: Child classes may inherit (or be derived) from a parent class. 
     
3. PROPERTY VISIBILITY. In OOP languages such as C#, properties of an parent class may be Private or Public. 
Private properties are not accessible from the child class. Public Properties in an parent class are accessible from the child class. 
At this time there is no explicit Private/Public modifier in WOOOF but properties can be overridden. The parent 'classes' such as animal, 
dog and human have properties which apply to most of the derived objects, for example, 
a 'dog' represents my knowledge of dogs and could have properties: 
	dog has the property 	BackLeftLeg
	dog has the property 	FrontRightLeg
	dog has the property 	BackRightLeg
	dog has the property 	Barks = true	//true is assumed if there is no true or false specified.

If My_Nida does not bark the bark property of dog is overridden
	My_Nida has property Bark = false
	
If this override is not present then My_Nida inherits the Barks property from 'dog'; she barks!

4. TIME and EVENTS. Time is represented from top to bottom in a script and from left to right in the WOOOF output. 
Time has 2 implementations:
	1. A 'Timeline' is a Sensual Object with a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
	2. A discrete Event is an Object with no duration, with a time stamp and is unique. ("un momento dado"; Johan Cruyff).
Potentially there could be a none countable, infinite number of Events. In practice an Event is instantiated when 
an object ends or an object becomes part of a new object or if a property of an object changes. 
By convention Events names are in capital letters.

An Event may have an input and/or an output object. 
The scripts use objects which are called INPUTx and OUTPUTx, by convention, to emphasize the Event input and output; see PoolsAndStone for examples.

Examples of Events:
In the drops script 2 drops of water collide and form a new drop in the Event called THE_COLLISION.
In the golf script the club and ball make contact in an Event which is also called THE_COLLISION and form a new SO called club_ball. 
'club_ball' contains the club and ball and is a composite object (which, to avoid undermining, will include more than that).
In the PoolAndStone script the Event, called STONE_TOUCHES_WATER, is the moment the stone makes contact with the pool of water.

Event SOs occur at the beginning and end of a Timeline SO.
The time stamp of the Event at the beginning of the Timeline is the start time of the Timeline SO.
The time stamp of the Event at the end of the Timeline is the end time of the Timeline SO.

5. OBJECT LIFETIME.
If an SO, for example My_Nida in dogs.txt, has been included in a Timeline SO and is then included in the next Event SO, 
the My_Nida SO is reproduced by the WOOOF program and the version in the Timeline SO is ended by showing it greyed out. 
As the SO proceeds through the Timelines and Events its properties change but the copies in preceding Timelines and Events SOs do not. 
We can think of SOs as 'snapshots' made at the moment the containing SO Timeline ends. 

6. INIT
The SO INIT is derived from RO_Event and it is used to initialise objects. This convention avoids having to describe how each 
object has come into existence. For example it would be tedious to have to describe how the 2 drops in drops.txt came into 
existence via a series of events involving nucleation, condensation and collisions.

7. Independent Scenarios.
Placeholders are used to contain independent scenarios within a scenario. This idea is illustrated in the PoolAndStone and dogs scripts.
At a certain moment in the PoolAndStone script, the stone is fully submerged and it continues to fall through the water. 
The air above the water comes to rest in a separate independent timeline.
The dogs script introduces a human observer. Real events occur in one placeholder and the observer notices these events,
with a time delay, in another placeholder/independent scenario.

WOOOF as a Tool.
Scripting and visualisation of the script using WOOOF are 2 ways of looking at the same thing. On the otherhand it is easier to develop a 
convincing script, iteratively, if it can be checked using the WOOOF output. 

///////////////////////////THE LANGUAGE USED IN THE SCRIPTS./////////////////////////

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








 
  




