WOOOF converts a scenario into an object model.
A scenario is a sequence of sentences in a text file written in a simple language.
The object model is shown as nested rectangles with properties in text and a network of relationships. 
A text file is produced with the objects and experimental output representing Random walks through the model and queries.

To use (only on Windows):
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a scenario, for example drops.txt, and press command button 'run script'.
	Create or modify scenarios using an ascii text editor.


Ideas related to Object-oriented Ontology (see the book Object-oriented Ontology by Graham Harman) can also be tested.
This version is called WOOOF because I started with a script describing walking the dogs.

Several Object Oriented Ontology (OOO) ideas are:
OOO speculates about the existence of Real Objects (ROs) with Real Qualities (RQs) and Sensual Objects (SOs) with Sensual Qualities (SQs).
A flat ontology, implying that interaction between inanimate objects without an observer is valid. 
Vicarious causation: an SO is needed to facilitate interaction between objects. 
Undermining; which means that (real) objects cannot be reduced to only their parts. 
Overmining; which means that an object is less than its effects. 

Several OOP concepts are supported in WOOOF: Inheritance, visibility of properties, containment.

These ideas are illustrated in the scripts drops.txt, golf.txt, dogs.txt and chess.txt.
They have been useful in directing an iterative series of experiments. 
Alternatives to the approach in drops.txt, golf.txt, dogs.txt and chess.txt are possible.
An alternative, but in my opinion less convincing one, is in chess_alternative.txt.

Several conventions have been used in the scripts: 

A Real Object has prefix RO_.

INHERITANCE
WOOOF supports inheritance using the command 'type' or 'type of'.
One or more SO can inherit from an RO. Inheritance works well because an SO 
may inherit some but not all of the RO properties and may add new properties not present in the RO. 


In some experiments on representing knowledge of an observer, an SO inherits from a generic SO. 
For example in the dogs.txt script the dog called My_Nida could have inherited from SO 'dog'. 
The SO 'dog' represents the observers knowledge of dogs and having recognised that My_Nida is a dog, he/she applies this knowledge to My_Nida.
However in the dogs.txt script it is more convincing to let My_Nida inherit from RO_Nida.
While multiple inheritance could be implemented (My_Nida is a type of dog and a type of RO_Nida), containment has been used.
	
	dog includes My_Nida
	
	and 
	
	My_Nida is a type of RO_Nida.

If a 2nd dog should arrive on the scene then it can also be included in dog.

VISIBILITY OF PROPERTIES
In OOP properties of an inherited class may be Private and therefore not accessible from the inheriting class.  
Related is that Public Properties in an inherited class are accessible from the inheriting class. 
At this time there is no explicit private/public modifier in WOOOF however something similar may be implied.

CONTAINMENT
Containment is supported using the command Includes.

	Me Includes dog
	dog includes My_Nida
	
TIME and EVENTS
Time is represented from left to right and has 2 implementations:
1. A time line is a Sensual Object having a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
2. A discrete Event. An RO_Event or SO_Event has no duration, has a time stamp and is unique.
Potentially there could be a none countable infinite number of events. In practice an event is instantiated 
when a relevent property changes or objects are destroyed or become part of new objects.
For example in the drops script 2 drops of water collide and form a new drop in the event called THE_COLLISION.
In the golf script the club and ball make contact in an event which is also called THE_COLLISION and form a new SO called club_ball. 
'club_ball' contains the club and ball but also includes more than that (avoiding undermining).

SO_Events occur at the beginning and end of a timeline SO.
The time stamp of the SO_Event at the beginning of the timeline is the start time of the time line SO.
The time stamp of the SO_Event at the end of the timeline is the end time of the time line SO.

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
The drops and golf scripts represent ineteractions without an explicit observer.
Having made the drops and golf scripts it seemed natural to include an SO in the MeetNida timeline. This SO is called Nida.

	MeetNida includes Nida
	Nida is a type of RO_Nida
	
The dogs script goes a step further by introducing an observer who also instantiates a Nida SO called My_Nida.
This SO resides in the brain of the observer. It seems most logical to let My_Nida inherit from RO_Nida and not for example from the SO Nida.

