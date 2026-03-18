WOOOF converts a scenario into an object model.
A scenario is written in a text file using a simple language.
On conversion the objects are shown as nested rectangles with their properties, including references to the object which set the property. 

In addition to the graphical presentation, the objects with their properties are written to a text file. 
There are also several more experimental and optional output forms such as the result of random walks through the model.
This version of the program is called WOOOF because the first script was one describing walking the dogs.

WOOOF is a Windows program. To use on Windows:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a script, for example drops.txt, and press command button 'run script'.
	Create or modify scripts using an ascii text editor.

Ideas related to Object-oriented Ontology, see the book Object-oriented Ontology: A New Theory of Everything by Graham Harman can also be tested.
Several Object Oriented Ontology (OOO) ideas are:
1. OOO speculates about the existence of Real Objects (ROs) with Real Qualities (RQs) and Sensual Objects (SOs) with Sensual Qualities (SQs).
2. A flat ontology implying, amoung other things, that interaction between inanimate objects without an observer is valid. 
3. Vicarious causation: an SO is needed to facilitate interaction between objects. 
4. Undermining; (real) objects cannot be reduced to only their parts. 
5. Overmining; an object is less than its effects. 

Several Object Oriented Programming (OOP) concepts are supported: Inheritance, visibility of properties, containment.

These ideas are illustrated in the scripts drops.txt, golf.txt, dogs.txt, PoolAndStone.txt, WaveAndStone.txt and chess.txt.
They are the result of a series of experiments with different approaches to structuring objects. 
An alternative, but in my opinion less convincing one, is in chess_alternative.txt.
An early script called ACW.txt has been included in the release. ACW stands for the American Civil War and 
is based on Graham Harman's analysis and his idea of object development using symbiosis. 

Several conventions have been used in the scripts: 

A Real Object has prefix RO_.

INHERITANCE and CONTAINMENT. WOOOF supports inheritance using the command 'type' or 'type of' and containment using 'includes'. 
The example scripts use inheritance to express the relationship between an SO and an RO; an SO is a type of RO and one or more SO's inherit from an RO. 
Inheritance works well because an SO may inherit some but not all of the RO properties and may add new properties not present in the RO. 
In some earlier experiments on representing knowledge of an observer, an SO inherited from a generic SO. 
For example in first version od the dogs.txt script, the dog called My_Nida inherited from SO 'dog'. 
The SO 'dog' represents the observers knowledge of dogs and having recognised that My_Nida is a dog, he/she applies this knowledge to My_Nida.
However in the last version of the dogs.txt script it is more convincing to let SO My_Nida inherit from RO_Nida and to use containment of 
My_Nida within the SO dog to show that My_Nida is an example of a dog.
	
	dog includes My_Nida  //containment
	
and 
	
	My_Nida is a type of RO_Nida  //inheritance

If a 2nd dog is known to the observer it can also be included in dog.

PROPERTY VISIBILITY In OOP properties of an inherited class may be Private and therefore not accessible from the inheriting class.  
Related is that Public Properties in an inherited class are accessible from the inheriting class. 
At this time there is no explicit private/public modifier in WOOOF however something similar may be implied.

TIME and EVENTS Time is represented from left to right and has 2 implementations:
1. A Timeline is a Sensual Object having a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
2. A discrete Event. An Event SO has no duration, has a time stamp and is unique.
Potentially there could be a none countable infinite number of Events. In practice an Event is instantiated when a relevant property changes 
or the life of an object ends or an object becomes part of a new object.
For example in the drops script 2 drops of water collide and form a new drop in the event called THE_COLLISION.
In the golf script the club and ball make contact in an event which is also called THE_COLLISION and form a new SO called club_ball. 
'club_ball' contains the club and ball but, to avoid undermining, it will includes more than that.
In the WaveAndStone script the Event, also called THE_COLLISION, is the moment the stone makes contact with the wave of water.

Events SOs occur at the beginning and end of a Timeline SO.
The time stamp of the Event at the beginning of the Timeline is the start time of the Timeline SO.
The time stamp of the Event at the end of the Timeline is the end time of the Timeline SO.

Defining the moment an event takes place, for example in the scripts drops, PoolAndStone and golf, 
has some similarity with finding a maximum or minimum on a smooth curve without using differentiation: We know there is a maximum and can 
choose a position on a curve before the maximum and another after. Then by stepping from both directions the length of the line on which the maximum must
be gets shorter and shorter. In a similar way we know there must be an instant when 2 objects become one and the original 2 objects cease to exist,
but have trouble finding it (and defining it because the timestamp is likely to be infinitely long!). 
In some ways contact is better replaced by the moment the rate of energy transfer changes. 
Brownian motion and a drops script with atomisation instead of coalescence would be scripted using many Events separated by short Timelines, 
the assumption being that no 2 events occur in the same instant. This assumption looks safe enough and was perhaps one of the insights which 
resulted in the correct analysis of Brownian Motion (by Albert E).

OBJECT LIFETIME.
If an SO, for example game1 in chess.txt, has been included in a Timeline SO and is then included in the next Event SO, 
the game1 SO is reproduced and the version in the Timeline SO is ended (by showing it greyed out). 
As the SO proceeds through the Timelines and Events its properties change but the copies in preceding Timelines and Events SOs do not. 
We can think of a 'snapshot' of the SO being made at the moment the containing SO Timeline or Event ends. 

INIT
The SO INIT is derived from RO_Event and it is used to initialise objects. This convention avoids having to describe how each 
object has come into existence. For example it would be tedious to have to describe how the 2 drops in drops.txt came into 
existence via a series of events involving nucleation, condensation and collisions.

Inanimate interaction and Observers.
The PoolAndStone, WaveAndStone, drops and golf scripts represent interactions without an explicit observer.
The dogs script introduces an observer, called Me. Me contains an SO called dog which contains My_Nida.
The SO 'dog' is Me's idea of a generic dog; 4 legs, a bark etc. 
It seems logical to let My_Nida inherit from RO_Nida.
The dogs script introduces a time delay (a Timeline) between something happening in an Event and the observer noticing it has happened in a 2nd Event.

Adding another Nida SO in Me to represent 'the image of Nida' as she runs around looks logical but has not been added (yet).
My_Nida represents an idea 'Me' has of Nida the dog. 'The image of Nida' is Me's visual of 
Nida during the interactions, present in a Timeline if Nida is visible, ending in an Event when she disappears into the woods.

FIRST CONCLUSIONS
The scripts are representations of how objects could relate to each other and for this reason properties have been reduced to a minimum.
Scripts of inanimate interactions include coordinate and velocity properties. In retrospect this looks like an artifact 
of scripting; (and an engineering background). It looks safe to conclude this is a limitation of scripting rather than real interactions, 
which have no need of this sort of decoration.

The division of the timeline into 'Timelines' and instantaneous Events, has the potential for introducing ever more detail.
In a similar way objects can be split into ever more detail, potentially to atoms and further.
OOO is concerned with objects which interact with each other so a macroscopic approach seems more convincing than trying to explain things from the 
bottom up. 

'Composite' objects with different characteristics are formed in the drops, golf and PoolAndStone scripts. 
The composite drop formed when 2 smaller drops coalesce looks convincing. The original 2 drops are mixed into the new drop; 
the original drops can no longer be recovered.
In the golf script club and ball both start deforming and energy starts to be transferred at the instant of contact. 
It seems reasonable to consider the club and ball a single object although club and ball are still recognisable. 

Composite objects in the PoolAndStone script just about work but it took some time to work out how.
A stone falls through air towards a pool of water. It touches the water and moves into it until it is fully immersed.
It then falls to the bottom of the pool. Water and air motion damps out.
By analogy with other scripts the SO water and SO air are defined as the volumes in motion because of the passage of the stone.
Here is the PoolAndStone scenario with these SOs:
1. Before the stone touches the water, the air and stone are a composite SO called stone_air and the water SO does not exist.
2. When the stone touches the water, the air SO, water SO and stone form a new composite object called stone_water_air. 
3. The water SO has no volume when it is created and grows rapidly as the stone moves into and through the water. 
Before contact with the stone, the pool is a continuum. On contact with the stone the water SO
becomes a discrete increasing volume as the stone enters and moves through the water. 
4. When the stone is just fully immersed the air SO separates from the composite and becomes an independent SO.
SOs water and stone continue as a new composite SO called stone_water.
5. The air SO begins to shrink as viscosity damps out the disturbances caused by the passage of the stone. (Interaction between air and water at the water surface is ignored here).
6. Eventually the air is at rest and the air SO has zero volume and ends.
7. When the stone comes to rest at the bottom of the pool the stone_water composite ends and the water and stone SOs are separate.
We choose the moment the stone comes to rest to separate water and stone because there is no longer interaction between 
stone and water.
8. The water SO begins to shrink as viscosity damps out the disturbances caused by the passage of the stone.
9. Eventually the water is at rest and the water SO ends: All the water in the pool is again part of the continuum.

In the instant that the stone is fully immersed 2 independent, parallel timeline/event trajects start:
1. the air Timeline as the motion damps out and 
2. a Timeline as the stone falls to the bottom of the pool, followed by an Event when the stone comes to rest followed by a Timeline 
while the motion of the water SO damps out.

The WavesAndStone scenario can be approached in a similar way.
Another variation is a drop of water being dropped on to a pool of water with the well known column and new drop formation. 

A DECRIPTION OF THE LANGUAGE USED IN THE SCRIPTS.
{COMMAND}
COMMAND = RandomWalk x | DisplaySOs * | QuerySOs | NaturalText | GenerateScript


DisplaySOs *	//display all the objects.
RandomWalk 30   //make 30 random walks through the objects.
GenerateScript  //generate a script from the Objects to create new Objects with meta data.
QuerySOs [Parent], [Child] //look for SOs with original name [Child] which have a Parent (or Parent of a Parent etc) with name [Parent].
							//The original name is the SO Name preceding brackets. 

Commands are case insensitive.


The script language allows the following but they are all removed before processing is started. 
//a comment
" a "
" the "
" of "
" and "
" is "
" has "
any number of consecutive spaces is replaced by a singel space.
"." at the end of a line.
"the " at the start of a line.
"an " at the start of a line.

After removal of the above a line is split based on separators space and tab.

Object includes object  //an object in the root is added to the right of the other root objects. A nested object is added at the bottom of the parent.
Object include_right object  ///an object in the root is added to the right of the other root objects. A nested object is added at the right handside of the parent.
							 //See PoolAndStone script for an example of how include_right can be used. 	

[An ]Object [has] property property =  {string}
Object property property is {string}
Object property property  {string}

Object [is] [a] type [of] Object

endobject Object
context Object







 
  




