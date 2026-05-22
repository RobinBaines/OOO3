WOOOF converts a scenario into an object model.
A scenario is sequence of events written in a text file using a simple language.
On conversion the objects are shown as nested rectangles with their properties, including references to the objects which set the properties. 
See WOOOFNotes.odp for an example of the output.

The objects with their properties are also written to a text file. 
There are several experimental types of output such as the result of random walks through the model.
This version of the program is called WOOOF because the first script described walking the dogs.

WOOOF is a Windows program. To use on Windows:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a script, for example drops.txt, and press command button 'run script'.
	Create or modify scripts using an ascii text editor.

Ideas related to Object-oriented Ontology, see the book Object-oriented Ontology: A New Theory of Everything by Graham Harman, can be tested.
Several Object Oriented Ontology (OOO) ideas are:
1. Real Objects (ROs) with Real Qualities (RQs) and Sensual Objects (SOs) with Sensual Qualities (SQs) exist.
2. A flat ontology implying, amoung other things, that interaction between inanimate objects without an observer is valid. 
3. Vicarious causation: an SO is needed to facilitate interaction between objects. 
4. Undermining; real objects cannot be reduced to only their parts (the sum is more than the parts). 
5. Overmining; a real object is less than its effects. 

The WOOOF language uses Object Oriented Programming (OOP) ideas; Inheritance, Properties and Containment.
WOOOF allows experiments with different approaches to modelling objects and object interaction. 
The scripts drops.txt, golf.txt, dogs.txt, PoolAndStone.txt and chess.txt use the same approach.
Chess_alternative.txt is an alternative, but less convincing approach.
An early script called ACW.txt has been included in the release. ACW stands for the American Civil War and 
is inspired by Graham Harman's analysis and ideas on object development using symbiosis. 

//////////////CONVENTIONS///////////////////////////
Several conventions have been used in the scripts. 

1. A Real Object has prefix RO_.

2. INHERITANCE. WOOOF supports inheritance using the 'type' or 'type of' command. 
The drops, golf, dogs, PoolAndStone and chess scripts use inheritance to express the relationship between an SO and an RO implying that an SO is a type of RO 
and that one or more SOs can inherit from the same RO. 
See * below for alternatives.
Inheritance works well because an SO may inherit some but not all of the RO properties and may add new properties not present in the RO. 
	Nida is a type of RO_Nida  //inheritance

3. CONTAINMENT. WOOOF supports containment using the 'includes' command. 
Containment is used to represent generic knowledge in an observer: In the dogs script the object 'Me' includes a 'dog' SO representing my knowledge of dogs (4 legs, barks..).
	Me includes dog
	dog has property bark = true 	
	
The SO My_Nida is an example of a 'dog' and is contained in 'dog'.
	dog includes My_Nida  //containment

If a 2nd dog is known to the observer it can be included/contained in dog.
	dog includes Another_dog
	
4. PROPERTY VISIBILITY. In OOP properties of an inherited class may be Private or Public. Private properties are not accessible from the inheriting class.  
Public Properties in an inherited class are accessible from the inheriting class. 
At this time there is no explicit Private/Public modifier in WOOOF but properties can be overridden:
	Another_dog has property bark = false	//override dog.Barks = true if Me thinks Another_dog does not bark. 

5. TIME and EVENTS. Time is represented from top to bottom in a script and from left to right in the WOOOF output. 
Time has 2 implementations:
1. A 'Timeline' is a Sensual Object having a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
2. A discrete Event is an Object with no duration, has a time stamp and is unique. ("un momento dado"; Johan Cruyff).
Potentially there could be a none countable infinite number of Events. In practice an Event is instantiated when 
an object ends or an object becomes part of a new object or if a property of an object changes. 
An Event has input and may have output. 
The scripts use objects which are called INPUTx and OUTPUTx, by convention, to emphasize the Event input and output; see PoolsAndStone for examples.
By convention Events are given names in capital letters.
Examples of Events:
In the drops script 2 drops of water collide and form a new drop in the Event called THE_COLLISION.
In the golf script the club and ball make contact in an Event which is also called THE_COLLISION and form a new SO called club_ball. 
'club_ball' contains the club and ball and is a composite object which, to avoid undermining, will include more than that.
In the PoolAndStone script the Event, called STONE_TOUCHES_WATER, is the moment the stone makes contact with the pool of water.

Event SOs occur at the beginning and end of a Timeline SO.
The time stamp of the Event at the beginning of the Timeline is the start time of the Timeline SO.
The time stamp of the Event at the end of the Timeline is the end time of the Timeline SO.

Defining the moment an Event takes place, for example in the scripts drops, PoolAndStone and golf, 
has some similarity with finding a maximum or minimum on a smooth curve without using differentiation: We know there is a maximum and can 
choose a position on a curve before the maximum and another after. Then by stepping from both directions the length of the curve on which the maximum must
be gets shorter and shorter. In a similar way we know there must be an instant when 2 objects become one and the original 2 objects cease to exist,
but have trouble finding it (and defining it because the exact timestamp is likely to be infinitely long!). 
Events seem also to be associated with a step change in the rate of energy transfer. 

Brownian motion and a drops script with atomisation instead of coalescence would be scripted using many Events separated by short Timelines, 
the assumption being that no 2 events occur in the same instant. This assumption looks safe enough and was perhaps one of the insights which 
resulted in the "random walk" and the correct analysis of Brownian Motion by Albert Einstein.

6. OBJECT LIFETIME.
If an SO, for example game1 in chess.txt, has been included in a Timeline SO and is then included in the next Event SO, 
the game1 SO is reproduced by the WOOOF program and the version in the Timeline SO is ended by showing it greyed out. 
As the SO proceeds through the Timelines and Events its properties change but the copies in preceding Timelines and Events SOs do not. 
We can think of these SOs as 'snapshots' made at the moment the containing SO Timeline ends. 

7. INIT
The SO INIT is derived from RO_Event and it is used to initialise objects. This convention avoids having to describe how each 
object has come into existence. For example it would be tedious to have to describe how the 2 drops in drops.txt came into 
existence; via a series of events involving nucleation, condensation and collisions.

8. PLACEHOLDERS.
Placeholders are used to contain independent time trajects. This idea is well illustrated in the PoolAndStone and dogs scripts.
The dogs script introduces a human observer and the idea of a delay between real events occurring in one placeholder and the observer noticing the events
in another placeholder.

//////////////COMPOSITE OBJECTS///////////////////////////
'Composite' objects are formed from 2 or more other objects and occur in the drops, golf, PoolAndStone and PoolAndStoneObject scripts. 
The composite drop formed when 2 smaller drops coalesce looks convincing. The original 2 drops are mixed into the new drop; 
the original drops can no longer be recovered. The drops have similar sizes and alternatives, for example letting drop2 merge into drop1 
or the other way around, look arbitrary.
A logical consequence is that any coalescing drops should always form a new drop even when one drop is 
significantly larger than the other. If this were not the case then an arbitrary, relative size would be needed to mark the point at which the larger drop 
is so much larger that it absorbs the smaller one.

In the golf script club and ball both start deforming and energy starts to be transferred at the moment contact is made. 
It seems reasonable to consider the club and ball a single new object in the short period they have contact and energy transfer is taking place.
Alternatives are to let the club include the ball or the ball the club. Again choosing one or the other looks arbitrary and does not seem to improve or simplify the script. 

Composite objects in the PoolAndStone script just about work. Here is the simplified scenario:
1. A stone falls through air towards a pool of water. 
2. It touches the water and moves into it until it is fully immersed.
3. It falls to the bottom of the pool. 
4. Water and air motion caused by the passage of the stone damp out.

By analogy with the drops and golf scripts the SO water and SO air are defined as the volumes in motion because of the passage of the stone.
Here is the above PoolAndStone scenario with these SOs included:
1. Before the stone touches the water, the air and stone are a composite SO called stone_air and the water SO does not exist.
2. When the stone touches the water, the air SO, water SO and stone form a new composite object called stone_air_water. 
3. The water SO has no volume when it is created and grows rapidly as the stone moves into and through the water. 
Before contact with the stone, the pool is a continuum. On contact with the stone the water SO
becomes a discrete, increasing volume as the stone enters and moves through the water. 
4. When the stone is just fully immersed, the air SO separates from the composite and becomes an independent SO.
SOs water and stone continue as a new composite SO called stone_water.
5. The air SO begins to shrink as viscosity damps the disturbances caused by the passage of the stone. 
(Interaction between air and water at the water surface is ignored).
6. Eventually the air is at rest and the air SO has zero volume and ends.
7. When the stone comes to rest at the bottom of the pool, the stone_water composite object ends and the water and stone SOs are separate.
We choose the moment the stone comes to rest to separate water and stone because there is no longer interaction between 
these 2 objects, or to put it another way, the stone no longer transfers energy to the water.
8. The water SO begins to shrink as viscosity damps the motion caused by the passage of the stone.
9. Eventually the water is at rest and the water SO ends: All the water in the pool is again part of the continuum.

When the stone is fully immersed 2 independent, parallel, time trajects begin:
1. the air Timeline as the motion damps out and the Event when the air motion has ended and 
2. a Timeline as the stone falls to the bottom of the pool and an Event when the stone comes to rest. At this point
another 2 independent trajects start:
	2a. the motion of the water SO damps out and the Event when the motion ends.
	2b. the stone remains stationary at the bottom of the pool.
The script uses "placeholders" to contain independent time trajects, see PoolAndStone script and the output in WOOOFNotes.odp.
A Waves and Stone scenario which describes how a stone is thrown into an approaching wave, would be similar to the Pool and Stone scenario.

The PoolAndStoneObject.txt script is a variation on PoolAndStone.txt.
Instead of making composite objects, stone_air, stone_air_water, stone_water and stone, the stone is given a 'leading role'.
The stone retains its identity as a stone and air and water are included and excluded as the script progresses. 
This approach simplifies and feels natural because the stone includes entrained air and water when they are still in contact with the stone.
On the otherhand if the air - water interaction at the surface of the pool were included in the script complications would arise
if these damped out after the stone came to rest at the bottom of the pool. 
In a shallow pool it is likely that energy transfer between water and air, as ripples spread across the pool, 
is significant after the stone has come to rest.

Similar complications occur when trying to make SO air or water play a 'leading role'.
Comparable is including stone_air as a composite object in the object stone_air_water. This would lead to problems if properties depend only on 
air and water or stone and water. 
A radical approach but even more complicated is to allow composite objects to contain sensual objects with all combinations of contained objects. 

This discussion of composite objects seems to imply that composite objects should be created by combining other objects to form a new object without an internal 
hierarchy of composite objects.
Giving one existing object a leading role by including the other objects leads to complications. 

//////////////INSTANTIATION///////////////////////////
Instantiation of a class in OOP can be used for processing data. Consider a class describing an invoice (unique invoice number,
date, sender, receiver, a list of products being billed, ...). As each new invoice is created an object is instantiated from the class 
using parameters (invoice number, date...). The resulting invoices would all have the structure defined in the class.
A similar approach works in OOO. Imagine many pairs of drops colliding and coalescing in a thunder storm and a simplified version of the drops script 
with only coalescence and no drop splitting. 
This simplified drops script could be converted into a class with parameters (drop sizes, collision time, coordinates, velocities etc) and 
could be instantiated for each of the pairs of colliding drops. It is also possible to imagine the output of two of these scripts being used as the input for 
another script, creating a chain reaction of small drops coalesing to form 'rain' drops.

///////////////////////////CONCLUSIONS and NOTES/////////////////////////
Level of Detail.
The division of time into Timelines and instantaneous Events, has the potential for introducing ever more detail.
In a similar way other types of object can be split into ever more detail, potentially to atoms and further.
When considering inanimate scripts it looks difficult to fix this granularity without slipping an observer into the picture to define the intention of the analysis. 
On the otherhand, having made several scripts, the changes in the structure of objects and step changes in the rate of energy transfer seem to be concrete pointers to 
a consistent level of granularity. 
An observer could conclude that there is little point in creating Events for the arrival of every photon on the surface of a pollen particle 
when looking at Brownian motion. The same conclusion is reached by comparing the rate of energy transfer caused by a photon and 
the collision of a molecule of water with the pollen.
Photon collision could influence the motion but is not discernable or relevant.

Properties.
The scripts are representations of how objects could relate to each other and to keep the scripts short and simple properties have been kept to a minimum.
Scripts of inanimate interactions include coordinate and velocity properties. In retrospect this looks like an artifact of scripting and 
real interactions would not need this sort of decoration.

Timelines and Events.
The Timelines have been implemented as SOs and as chunks of time derived (inherited) from the single Real continuous Timeline. 
This looks logical particularily when Timeline SOs overlap when parallel independent trajects are part of an interaction.
The relationship between the Real continuous Timeline and Events is less clear. The golf script uses Real and Sensual Events for the same Event but as this  
complicates the script while adding little, only Sensual Events have been used in PoolsAndStone script.

A Generic Approach to Inanimate Interactions.
Dividing time into Timeline and Event SOs and the use of less obvious types of SO in the PoolAndStone script both contribute to a generic approach to 
inanimate interactions. In OOP the idea of a Pattern is used to make the generic aspects of similar processes explicit.  
A generic approach to scenarios looks like a requirement for objects existing outside an observer.

Composite Objects.
When 2 or more objects combine a new object should be created without a internal hierarchy.
This approach is closer to the OOO idea of a flat ontology.

Scripts with Observers.
The dogs script introduces a human observer and the idea of a delay between something real happening and the observer noticing it has happened. 
The 'external' scenario of a dog sitting, running and then barking occurs with Timelines and Events and with no observer interaction.
The human observer experiences the dog sitting, running and then barking but in a separate 'placeholder' with Timelines and Events 
which mirror the external Timelines and Events but with a delay to represent the observer seeing these Events shortly after they have actually happened.
A human observer object could be introduced to simulate the delay between something happening and the observer seeing it: 
	See something black moving.
	Identify it as a running dog.
	Identify it as my dog Nida.
	Change the state/property of MyNida to running.
It also looks reasonable to introduce a MyNida dog object which captures everything I already knew about Nida and a separate Bakkeveen-MyNida which tracks my experiences
of the visit to Bakkeveen (a place in Friesland). If I see Nida do something for the first time, for example jump 2 meters into the air, the Bakkeveen-MyNida 
would record this using an Event and I could then update MyNida, in another Event, with a new property: "can jump 2 meters into the air".

It looks promising to introduce an observer into the PoolAndStone script. We could imagine an observer anticipating a stone touching the surface of a pool of water 
and the observer claiming that the event occurred before it did, instead of afterwards. 

Scripts without Observers.
An observer has knowledge from direct experience and knowledge aquired from sources such as books, influencers, teachers..... For example I experience a yellow daffodil
and I also know that what I call yellow is my way of experiencing a narrow band of wavelengths of the electromagnetic spectrum.
Some observers are not inclined to consider inanimate interactions such as those in the PoolAndStone script. This is based on the idea that everything we 
consider in this type of interaction depends on the interpretation of an observer.
Embracing the speculative qualifier of OOO I am inclined to apply aquired knowledge to these interactions. In my case this would be scientific  
knowlegde as opposed to, for example, magic or the influence of a god.

Object hierarchy inside an Observer.
A difference between objects outside and inside an observer is the hierarchy in the latter, see the object dog which stores generic dog properties 
in the dogs script. Storing generic information about animals which applies to dogs and other animals, and information about dogs which applies to a dog called Nida 
is an efficient way to store information and makes it available for a quick interpretation of new experiences.
I have discussed this with Fons who works with autists. He feels that some autists are not able to create these type of object
resulting in confusion if a new dog appears or that they are sometimes unable to use previous knowledge about, for example, a red bus when they see a yellow one.

Why do Objects seem to work outside and inside an observer?
Objects outside and inside an observer seem both to be useful for analysing interactions and events and that raises the question, 
why objects with the same OOO characteristics should work in both cases?
If objects work in an observer while we know that neural networks are the underlying brain architecture then we could expect a mapping between the networks and objects.
Trying to define this mapping looks useful because objects offer an accessible interface for humans while neural networks are often difficult to analyse.

In OOP an "abstract" object, meaning an object which cannot be instantiated, is often used to define functions, properties and interfaces all objects have (to have).
In OOO a real abstract object which is inherited by all real objects will not exist although it may be possible to imagine one. 
However the OOO idea that real objects interact using different, context sensitive sensual objects is a very useful tool for analysis. 

Could there be a mapping onto an 'interface' object which observers use when interpreting reality, see note above on the dogs script? 
This object could be responsible for preparing existing SOs for use when a new experience occurs: The Nida-Bakkeveen object is derived from what the observer (Me) knows
about Nida and Bakkeveen. It could also be responsible for updating existing SOs should a new property occur during an experience: see above Nida can "jump 2 meters into the air".
It is probably going too far to call this a "sentience" object and I am finding it difficult to imagine how this could work in inanimate objects like a water drop.

An 'interface' object could process external real objects and properties into the observer objects which map to neural networks.
Are the characteristics of objects described in OOO such as under and over mining be a logical consquence of the mapping implied in the above?

WOOOF as a Tool.
Scripting and visualisation of the script using WOOOF are 2 ways of looking at the same thing. On the otherhand it is easier to develop a 
convincing script, iteratively, if it can be checked using the WOOOF output. 

*The scripting is useful for testing alternatives approaches. In some earlier experiments on representing knowledge of an observer, an SO inherited from a generic SO. 
For example in first version of the dogs.txt script, the dog called My_Nida inherited from SO 'dog'. 
The SO 'dog' represents the observers knowledge of dogs and having recognised that My_Nida is a dog, he/she applies this knowledge to My_Nida.
However in the last version of the dogs.txt script it is more convincing to let SO My_Nida inherit from RO_Nida and to use containment of 
My_Nida within the SO dog to show that the observer thinks My_Nida is an example of a dog.
An alternative is to use inheritance in an observer to represent a knowledge hierarchy such as animal - dog - MyNida and to use inheritance for RO - SO outside observers.
///////////////////////////END OF CONCLUSION/////////////////////////

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








 
  




