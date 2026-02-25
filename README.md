WOOOF converts a scenario into an object model.
A scenario is written in a text file using a simple language.
The object model is shown as nested rectangles with properties in text and a network of relationships. 
A text file is produced with the objects and experimental output representing random walks through the model and queries.

WOOOF is a Windows program. To use on Windows:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a script, for example drops.txt, and press command button 'run script'.
	Create or modify scripts using an ascii text editor.

Ideas related to Object-oriented Ontology, see the book Object-oriented Ontology: A New Theory of Everything by Graham Harman (GH) can also be tested.
This version is called WOOOF because the first script was one describing walking the dogs.

Several Object Oriented Ontology (OOO) ideas are:
1. OOO speculates about the existence of Real Objects (ROs) with Real Qualities (RQs) and Sensual Objects (SOs) with Sensual Qualities (SQs).
2. A flat ontology, implying that interaction between inanimate objects without an observer is valid. 
3. Vicarious causation: an SO is needed to facilitate interaction between objects. 
4. Undermining; (real) objects cannot be reduced to only their parts. 
5. Overmining; an object is less than its effects. 

Several Object Oriented Programming (OOP) concepts are supported: Inheritance, visibility of properties, containment.

These ideas are illustrated in the scripts drops.txt, golf.txt, dogs.txt, WaveAndStone.txt and chess.txt.
They are the result of a series of experiments with different approaches to structuring objects. 
An alternative, but in my opinion less convincing one, is in chess_alternative.txt.
An early script called ACW.txt has been included in the release. ACW stands for the American Civil War and 
illustrates GH's idea of object development using symbiosis. 

Several conventions have been used in the scripts: 

A Real Object has prefix RO_.

WOOOF supports inheritance using the command 'type' or 'type of'. 
The example scripts use inheritance to express the relationship between an SO and an RO; one or more SO's inherit from an RO. 
Inheritance works well because an SO may inherit some but not all of the RO properties and may add new properties not present in the RO. 

In some ealier experiments on representing knowledge of an observer, an SO inherited from a generic SO. 
For example in first version od the dogs.txt script, the dog called My_Nida inherited from SO 'dog'. 
The SO 'dog' represents the observers knowledge of dogs and having recognised that My_Nida is a dog, he/she applies this knowledge to My_Nida.
However in the last version of the dogs.txt script it is more convincing to let My_Nida inherit from RO_Nida and to use containment of 
My_Nida within the SO dog to show that My_Nida is an example of a dog.
	
	dog includes My_Nida
	
and 
	
	My_Nida is a type of RO_Nida.

If a 2nd dog is known to the observer it can also be included in dog.

PROPERTY VISIBILITY In OOP properties of an inherited class may be Private and therefore not accessible from the inheriting class.  
Related is that Public Properties in an inherited class are accessible from the inheriting class. 
At this time there is no explicit private/public modifier in WOOOF however something similar may be implied.

CONTAINMENT Containment is supported using the command Includes.

	Me Includes dog
	dog includes My_Nida

	
TIME and EVENTS Time is represented from left to right and has 2 implementations:
1. A time line is a Sensual Object having a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
2. A discrete Event. An RO_Event or SO_Event has no duration, has a time stamp and is unique.
Potentially there could be a none countable infinite number of Events. In practice an Event is instantiated 
when a relevant property changes or objects are destroyed or become part of new objects.
For example in the drops script 2 drops of water collide and form a new drop in the event called THE_COLLISION.
In the golf script the club and ball make contact in an event which is also called THE_COLLISION and form a new SO called club_ball. 
'club_ball' contains the club and ball but also includes more than that, avoiding undermining.
In the WaveAndStone script the Event also called THE_COLLISION, is the moment the stone makes contact with the wave of water.

Events SOs occur at the beginning and end of a timeline SO.
The time stamp of the Event at the beginning of the timeline is the start time of the timeline SO.
The time stamp of the Event at the end of the timeline is the end time of the timeline SO.

OBJECT LIFETIME.
If an SO, for example game1 in chess.txt, has been included in a timeline SO and is then included in the next event SO, 
the game1 SO is reproduced and the version in the timeline SO is ended (by showing it greyed out). 
As the SO proceeds through the timelines and events its state changes but the copies in preceding timeline and events SOs do not. 
We can think of a 'snapshot' of the SO being made at the moment the containing SO timeline or event ends. 

INIT
The SO INIT is derived from RO_Event and sets up the initial Objects. This convention avoids having to describe how each 
object has come into existence. For example it would be tedious to have to describe how the 2 drops in drops.txt came into 
existence via a series of events involving nucleation, condensation and collisions.

Inanimate interaction and Observers.
The WaveAndStone, drops and golf scripts represent interactions without an explicit observer.
The dogs script introduces an observer, called Me. Me instantiates an SO of a dog called My_Nida.
This SO resides in the observer in an SO called dog. The SO dog is Me's idea of a generic dog; 4 legs, a bark etc. 
It seems logical to let My_Nida inherit from RO_Nida.

Adding another Nida SO in Me to represent 'the image of Nida' as she runs around looks logical.
Me has a My_Nida representing a continuous idea 'Me' has of Nida the dog. 'The image of Nida' is Me's visual of 
Nida during the interactions, present in a timeline if Nida is visible, ending in an Event 
when she dissappears into the woods.

The ACW script is an early version of GH's chapter on the American Civil War.
It introduces generals, 2 presidents and several battles.
At the end of the script, GH's symbiosis idea is added showing how the ACW state changes from Development to Maturity.
Decadence and Death still need to be added.

FIRST CONCLUSIONS
The scripts are representations of how objects could relate to each other and for this reason properties have been reduced to a minimum.
Scripts of inanimate interactions includes definitions of coordinates, velocities and closed systems. 
In retrospect this looks like an artifact of scripting; or the limitation of an engineering background? 
Looks safe to conclude this is a limitation of scripting rather than real interactions, which have no need of this sort of decoration.

The division of the timeline into 'timelines' with finite duration and instantaneous Events, has the potential for introducing ever more detail.
In a similar manner objects can be split into ever more detail potentially down to atoms and further.
The aim of analysis drives the degree to which time and objects are detailed.

Considering the scripts drops, WaveAndStone and golf: 
Defining the moment an event takes place is similar to defining a maximum or minimum without using differentiation: we know there is a maximum and can 
choose a position on a curve before the maximum and another after. Then by stepping from both directions the length of the line on which the maximum must
be gets shorter and shorter. In a similar way we know there must be an instant when 2 objects become one and the original 2 objects cease to exist,
but have trouble defining it. 
We can choose an instant when there are 2 objects and another when there is one and then move in both directions to isolate the exact instant.
However in this case finding the exact moment is elusive because of the complexity of the contact.
In some ways contact is better replaced by the moment that energy transfer starts. 

Brownian motion and a drops script with atomisation instead of coalescence would be scripted using many events separated by short timelines, 
the assumption being that no 2 events occur in the same instance. This assumption looks safe enough and was perhaps the insight which 
resulted in the correct analysis of Brownian Motion (by Albert E).










 
  




