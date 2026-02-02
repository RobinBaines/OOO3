WOOOF animates creation of an object model using a scenario.
A scenario is a sequence of sentences in a text file written in a simple language.
The object model is shown as rectangles with properties in text and a network of relationships. In the background
a text file is produced with the objects and experimental output representing Random walks through the model and queries on parts of the model.

To use:
	Download the executable files and several examples of scenarios from https://github.com/RobinBaines/OOO3/releases/tag/v1.0.4
	Run WOOOF.exe from the command line.
	Select a scenario, for example drops.txt, and press command button 'run script'.
	Create or modify scenarios using an ascii text editor.


Ideas related to Object-oriented Ontology (see the book Object-oriented Ontology by Graham Harman) can also be tested.

Several Object Oriented Ontology (OOO) ideas are:
OOO speculates about the existence of Real and Sensual Objects. Real Objects (ROs) with Real Qualities (RQs) and Sensual Objects (SOs) with Sensual Qualities (SQs).
A flat ontology implies that interaction between inanimate objects without an observer is valid. 
Vicarious causation: OOO requires an SO to facilitate interaction between objects. 
Undermining means that (real) objects cannot be reduced to only their parts. 
Overmining means that an object is less than it's effects. 

OOP ideas in the scenarios are:
Inheritance, visibility of properties, containment.

These ideas are illustrated in the scripts drops.txt, golf.txt, dogs.txt and chess.txt.
Aside from the question of whether they are related to reality they have been useful in directing 
an iterative series of experiments. Alternatives to the approach in drops.txt, golf.txt, dogs.txt and chess.txt are possible.
An alternative but in my opinion less convincing is in chess_alternative.txt.

Several conventions have been used in the scripts: 

A Real Object has prefix RO_.

INHERITANCE
WOOOF supports the Object Oriented Programming (OOP) idea of inheritance using the command 'type' or 'type of'. 
Inheritance has been used in 2 ways:
1. One or more SO can inherit from an RO. An SO is the way a RO presents itself to the world. Inheritance works well because an SO 
may inherit some but not all of the RO properties and may add new properties not present in the RO. There may also be multiple SO derived
from the same RO as the context changes.
2. An SO can inherit from a generic SO within another SO. For example in the dogs.txt script the dog called My_Nida 
is an SO contained within an SO called Me. My_Nida inherits from the SO dog. The SO dog represents my knowledge of dogs 
and that I know My_Nida is a type of dog. This 2nd way of inheriting is confined to the representation
of knowledge in an SO representing an observer.

	My_Nida is a type of dog

VISIBILITY OF PROPERTIES
In OOP properties of an inherited class may be Private and therefore not accessible from the inheriting class.  
Related is that Public Properties in an inherited class are accessible from the inheriting class. 
At this time there is no explicit private/public modifier in WOOF however something similar may be implied.

	dog has the property BackLeftLeg
	dog has the property Tail
	My_Nida is a type of dog
	dog has the property Tail = Short

CONTAINMENT
Containment is supported using the command Includes.

	Me Includes dog
	Me Includes My_Nida 
	
TIME and EVENTS
Time is represented from left to right and has 2 implementations:
1. A time line is a Sensual Object having a finite duration. It is derived from RO_timeline, which is a single RO representing the time continuum. 
2. A discrete SO_Event is derived from an RO_event. Each RO_Events / SO_Events has no duration, has a time stamp and is unique.
Potentially there could be a none countable infinite number of events. In practice an event is instantiated 
when a relevent property changes or objects are destroyed or become part of new objects.
For example in the drops script 2 drops of water collide and form a new drop in the event called THE_COLLISION.
In the golf script the club and ball make contact in THE_COLLISION event and form a new SO called club_ball. club_ball contains the 
club and ball (but also includes more than that).

RO_events/SO_Events occur at the beginning and end of a timeline SO.
The SO_Event time stamp will be the start time of the time line SO which follows the event. 
An SO_Event may succeed a time line SO and the time stamp of this SO_Event is the end time of the preceding timeline SO.

OBJECT LIFETIME.
If an SO for example game1 in chess.txt has been included in a timeline SO and is then included in the next event SO, 
the game1 SO is reproduced and the version in the timeline SO is ended (by showing it greyed out). 
As the SO proceeds through the timelines and events its state changes but the copies in preceding timeline and events SOs do not. 
We can think of a 'snapshot' of the SO being made at the moment the containing SO timeline or event ended. 

INIT
The SO INIT is derived from RO_Event and sets up the initial Objects. This convention avoids having to describe how each 
object has come into existence. For example it would be tedious to have to describe how the 2 drops in drops.txt came into 
existence via a series of events.

