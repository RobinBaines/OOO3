WOOOF converts a scenario into an object model.
A scenario is written in a text file using a simple language.
On conversion the objects are shown as nested rectangles with their properties, including references to the object which set the property. 

In addition to the graphical presentation, the objects with their properties are written to a text file. 
There are several experimental types of output such as the result of random walks through the model.
This version of the program is called WOOOF because the first script described walking the dogs.

WOOOF is a Windows program. To use on Windows:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a script, for example drops.txt, and press command button 'run script'.
	Create or modify scripts using an ascii text editor.

Ideas related to Object-oriented Ontology, see the book Object-oriented Ontology: A New Theory of Everything by Graham Harman, can be tested.
Several Object Oriented Ontology (OOO) ideas are:
1. OOO speculates about the existence of Real Objects (ROs) with Real Qualities (RQs) and Sensual Objects (SOs) with Sensual Qualities (SQs).
2. A flat ontology implying, amoung other things, that interaction between inanimate objects without an observer is valid. 
3. Vicarious causation: an SO is needed to facilitate interaction between objects. 
4. Undermining; (real) objects cannot be reduced to only their parts. 
5. Overmining; an object is less than its effects. 

Several Object Oriented Programming (OOP) concepts are supported: Inheritance, properties, containment.

These ideas are illustrated in the scripts drops.txt, golf.txt, dogs.txt, PoolAndStone.txt, WaveAndStone.txt and chess.txt.
They are the result of a series of experiments with different approaches to structuring objects. 
An alternative, but less convincing one, is in chess_alternative.txt.
An early script called ACW.txt has been included in the release. ACW stands for the American Civil War and 
is based on Graham Harman's analysis and his idea of object development using symbiosis. 

Several conventions have been used in the scripts: 

A Real Object has prefix RO_.

INHERITANCE and CONTAINMENT. WOOOF supports inheritance using the command 'type' or 'type of' and containment using 'includes'. 
The example scripts use inheritance to express the relationship between an SO and an RO; an SO is a type of RO and one or more SOs inherit from an RO. 
Inheritance works well because an SO may inherit some but not all of the RO properties and may add new properties not present in the RO. See * below for alternatives.

	My_Nida is a type of RO_Nida  //inheritance

Containment is used to represent generic 'dog' knowledge: In the dogs script I (the object 'Me') includes a 'dog' SO representing my knowledge of dogs (4 legs, barks..).

	Me includes dog
	dog has property bark = true 	
	
The SO My_Nida is an example of a 'dog' and is contained in 'dog'.
	
	dog includes My_Nida  //containment

If a 2nd dog is known to the observer it can be included/contained in dog.

	dog includes Another_dog
	
Adding another Nida SO in Me to represent 'the image of Nida' as she runs around looks logical but has not been added (yet).
My_Nida represents an idea 'Me' has of Nida the dog. 'The image of Nida' is Me's visual of 
Nida during the interactions, present in a Timeline if Nida is visible, ending in an Event when she disappears into the woods.	


PROPERTY VISIBILITY. In OOP properties of an inherited class may be Private and therefore not accessible from the inheriting class.  
Related; Public Properties in an inherited class are accessible from the inheriting class. 
At this time there is no explicit private/public modifier in WOOOF however something similar may be implied.

	Another_dog has property bark = false	//override dog.Barks = true if Me thinks Another_dog does not bark. 

TIME and EVENTS. Time is represented from top to bottom in a script and from left to right in the WOOOF output. 

Time has 2 implementations:
1. A Timeline is a Sensual Object having a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
2. A discrete Event is a Sensual Object. An Event SO has no duration, has a time stamp and is unique. ("un momento dado"; Johan Cruiff).
Potentially there could be a none countable infinite number of Events. In practice an Event is instantiated when 
an object ends or an object becomes part of a new object. 
This approach to time came from Graham Harman's entertaining podcast on continua and discrete objects: Waves and Stones.

In the drops script 2 drops of water collide and form a new drop in the Event called THE_COLLISION.
In the golf script the club and ball make contact in an Event which is also called THE_COLLISION and form a new SO called club_ball. 
'club_ball' contains the club and ball and is a composite object which, to avoid undermining, will include more than that.
In the PoolAndStone script the Event, also called THE_COLLISION, is the moment the stone makes contact with the pool of water.

Event SOs occur at the beginning and end of a Timeline SO.
The time stamp of the Event at the beginning of the Timeline is the start time of the Timeline SO.
The time stamp of the Event at the end of the Timeline is the end time of the Timeline SO.

Defining the moment an Event takes place, for example in the scripts drops, PoolAndStone and golf, 
has some similarity with finding a maximum or minimum on a smooth curve without using differentiation: We know there is a maximum and can 
choose a position on a curve before the maximum and another after. Then by stepping from both directions the length of the line on which the maximum must
be gets shorter and shorter. In a similar way we know there must be an instant when 2 objects become one and the original 2 objects cease to exist,
but have trouble finding it (and defining it because the timestamp is likely to be infinitely long!). 
Events seem to be associated with a step change in the rate of energy transfer. 

Brownian motion and a drops script with atomisation instead of coalescence would be scripted using many Events separated by short Timelines, 
the assumption being that no 2 events occur in the same instant. This assumption looks safe enough and was perhaps one of the insights which 
resulted in the "random walk" and the correct analysis of Brownian Motion (by Albert E).



OBJECT LIFETIME.
If an SO, for example game1 in chess.txt, has been included in a Timeline SO and is then included in the next Event SO, 
the game1 SO is reproduced by the WOOOF program and the version in the Timeline SO is ended (by showing it greyed out). 
As the SO proceeds through the Timelines and Events its properties change but the copies in preceding Timelines and Events SOs do not. 
We can think of these SOs as 'snapshots' made at the moment the containing SO Timeline or Event ends. 

INIT
The SO INIT is derived from RO_Event and it is used to initialise objects. This convention avoids having to describe how each 
object has come into existence. For example it would be tedious to have to describe how the 2 drops in drops.txt came into 
existence via a series of events involving nucleation, condensation and collisions.

INSTANTIATION.
Instantiation of a class in OOP is used for processing data. Consider a class describing an invoice (unique invoice number,
date, sender, receiver, a list of products being billed, ...). As each new invoice is created an object is instantiated from the class 
using parameters (invoice number, date...). The resulting invoices would all have the structure defined in the class.
A similar approach works in OOO. Imagine many pairs of drops colliding and coalescing in a thunder storm. 
The drops script could be converted into a class with parameters (drop sizes, collision time, etc) and 
could be instantiated for each of the pairs of colliding drops.

'Composite' OBJECTS.
'Composite' objects with different characteristics are formed in the drops, golf and PoolAndStone scripts. 
The composite drop formed when 2 smaller drops coalesce looks convincing. The original 2 drops are mixed into the new drop; 
the original drops can no longer be recovered.
In the golf script club and ball both start deforming and energy starts to be transferred at the instant of contact. 
It seems reasonable to consider the club and ball a single object while they have contact and energy transfer is taking place, although club and ball are still recognisable. 

Composite objects in the PoolAndStone script just about work but it took some time to work out how.
1. A stone falls through air towards a pool of water. 
2. It touches the water and moves into it until it is fully immersed.
3. It falls to the bottom of the pool. 
4. Water and air motion caused by the passage of the stone damps out.

By analogy with other scripts the SO water and SO air are defined as the volumes in motion because of the passage of the stone.
Here is the PoolAndStone scenario with these SOs:
1. Before the stone touches the water, the air and stone are a composite SO called stone_air and the water SO does not exist.
2. When the stone touches the water, the air SO, water SO and stone form a new composite object called stone_water_air. 
3. The water SO has no volume when it is created and grows rapidly as the stone moves into and through the water. 
Before contact with the stone, the pool is a continuum. On contact with the stone the water SO
becomes a discrete increasing volume as the stone enters and moves through the water. 
4. When the stone is just fully immersed the air SO separates from the composite and becomes an independent SO.
SOs water and stone continue as a new composite SO called stone_water.
5. The air SO begins to shrink as viscosity damps out the disturbances caused by the passage of the stone. 
(Interaction between air and water at the water surface is ignored here).
6. Eventually the air is at rest and the air SO has zero volume and ends.
7. When the stone comes to rest at the bottom of the pool the stone_water composite ends and the water and stone SOs are separate.
We choose the moment the stone comes to rest to separate water and stone because there is no longer interaction between 
stone and water. Or to put it another way the stone no longer causes energy transfer to the water.
8. The water SO begins to shrink as viscosity damps out the disturbances caused by the passage of the stone.
9. Eventually the water is at rest and the water SO ends: All the water in the pool is again part of the continuum.

In the instant that the stone is fully immersed 2 independent, parallel Timeline/Event trajects start:
1. the air Timeline as the motion damps out and the Event when the motion has ended and 
2. a Timeline as the stone falls to the bottom of the pool, followed by an Event when the stone comes to rest followed by a Timeline 
while the motion of the water SO damps out and the Event when the motion has ended.

The WavesAndStone scenario is similar.
Another variation is a drop of water being dropped on to a pool of water with the well known column and new drop formation. 

PROPERTIES.
The scripts are representations of how objects could relate to each other and for this reason properties have been reduced to a minimum.
Scripts of inanimate interactions include coordinate and velocity properties. In retrospect this looks like an artifact 
of scripting; (and caused by an engineering background). It looks safe to conclude this is a limitation of scripting rather than real interactions, 
which have no need of this sort of decoration.

FIRST CONCLUSIONS.
The division of time into Timelines and instantaneous Events, has the potential for introducing ever more detail.
In a similar way other objects can be split into ever more detail, potentially to atoms and further.
When considering inanimate scripts it looks difficult to fix this granularity without slipping an observer into the picture to define the intention of the analysis. 
On the otherhand having made several scripts the changes in the structure of objects and step changes in the rate of energy transfer seem to be concrete pointers to 
a consistent level of granularity. 

Is there any point in creating Events for the arrival of every photon on the surface of a pollen particle when looking at Brownian motion?
It is the collision of a molecule of water with the pollen which transfers sufficient energy to the pollen to alter its velocity. 
In steps:
1. Event when the molecule forms a composite object with the pollen, 
2. a Timeline as the kinetic energy of the molecule is transferred with the pollen
3. a 2nd event when the molecule of water no longer has contact (no energy transfer) with the pollen and the molecule-pollen composite object no longer exists.
Photon collision could influence the motion but is not discernable or relevant (for any observer!?).

Scripting inanimate interactions raises questions; are we imagining the collision of 2 drops using knowledge of physics, or has it been filmed or is it just a 
template-class for any 2 drop interaction?

Consistency between different scenarios looks like a requirement for speculative OOO; for the existence of objects outside human perception.
Dividing time into Timeline and Event SOs and the use of less obvious types of SO in the PoolAndStone script both contribute to a generic approach to 
any inanimate interaction.
Extending these approaches to scenarios where human and animal observers take part in interactions with each other and inanimate objects is the next step.
The dogs script introduces the idea of a time delay (a Timeline) between something real happening (in an Event)
and the observer noticing it has happened. It looks promising to introduce an observer and this idea into the PoolAndStone script.

Scripting and visualisation of the script using WOOOF are 2 ways of looking at the same thing. On the otherhand it is a good deal easier to develop a 
convincing script, iteratively, if it can be checked using the WOOOF output. 


A DECRIPTION OF THE LANGUAGE USED IN THE SCRIPTS.
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

Object includes object  //an object in the root is added to the right of the other root objects. A nested object is added at the bottom of the parent.
Object include_right object  ///an object in the root is added to the right of the other root objects. A nested object is added at the right handside of the parent.
							 //See PoolAndStone script for an example of how include_right can be used to create 2 or more independent threads of time. 	

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



*The scripting is useful for testing alternatives approaches.
In some earlier experiments on representing knowledge of an observer, an SO inherited from a generic SO. 
For example in first version of the dogs.txt script, the dog called My_Nida inherited from SO 'dog'. 
The SO 'dog' represents the observers knowledge of dogs and having recognised that My_Nida is a dog, he/she applies this knowledge to My_Nida.
However in the last version of the dogs.txt script it is more convincing to let SO My_Nida inherit from RO_Nida and to use containment of 
My_Nida within the SO dog to show that My_Nida is an example of a dog.




 
  




