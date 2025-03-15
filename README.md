Having got the program OOO3.exe program working I tried to make some notes. While doing that I realised how few of the OOO ideas are implemented
and of those implemented how difficult it is to decide if they match OOO ideas and terminology.
I did notice how developing the program and using it forced me to think hard!
Here are those notes.

Object Oriented Ontology describes Real Objects (ROs), Real Qualities (RQs), Sensual Objects (SOs) and Sensual Qualities (SQs).
The following should clarify how I view the difference between these:

In his book, Object-oriented Ontology, Graham Harman includes a chapter on the American Civil War (ACW).
He uses the ACW as an example of an RO. I assume here that he wrote the chapter based on his SO(s) of this war.
I further assume that he created an SO, I call it an Event, which records his experience while writing the chapter.
On completion, the chapter in the book is an RO. The RO includes well established facts, dates and figures but also opinions, for example,
on the relative importance of generals and battles. These are RQs (and in some cases separate ROs?) of the chapter in the book.
In OOO an RQ seems to be a single statement while an RO is not only a collection of RQs but also something more which is ultimately unknowable.

While reading the chapter I create an Event SO, let's call it ReadingChapter.
By chance I read a book about the ACW last year. I therefore had an existing ACW SO.
The information in the chapter augmented this SO with SQs and made references between the ReadingChapter SO
and my existing ACW SO, recording not only the SQs as stand-alone statements, but also where they came from. 

In the software and what follows I use Event with a capital letter when referring to an SO and event when referring to the sub-events in an Event.
The 'Event' SO is like a theatre with sub-events describing changes to SOs and interactions between them.

This software is an attempt to simulate creation of SOs and SQs using a stream of data in a script file.
It uses object oriented software ideas such as inheritance, overriding and properties.
I have chosen to illustrate this by describing several Events starring 2 dogs we own and local places for dogs to roam and play.
These Events are in a script file called script.txt. It is possible to describe other Events in other script files.

In my experience the stream of data in a (subject-object) script may have different sources: spoken language, written language, thoughts, actions,
and emotion and external events and processes involving visual stimulus, smells, sound.... 
In a subject-object Event I may switch rapidly between different sources 
but I seem to process data in a sequence as opposed to, for example, multiple parallel streams.  
My stream depends on my interpretation of ROs and on my 'state' or in this context, on the state of my SOs including my emotional state. 
One day I will be receptive to the external stimuli the next I may be thinking about something 
and the external world makes less of an impression.

I have not spent time on using sophisticated streams of data from different sources; besides being very difficult/impossible it is not the focus here.
The stream in a script must conform to a simple syntax which is explained in the file Program.cs.
The example script in the file called script.txt, is annotated and is probably more than enough information to allow writing of other scripts 
which conform to the syntax.
The script as a 'reality' simulator is an extreme simplification and it may be such a simplification that the idea of this program 
has no point at all.
On the other hand some of the results and conclusions look interesting; see below.

Use by entering 

        OOO3.exe script.txt > t.txt 

on the command line.
The program interprets the script.txt and writes the output to the console or to a file using redirection.

SOME OOO IDEAS:
A flat ontology implies that interaction between inanimate objects without an observer is valid. 
The example script implies Events with myself as observer.
There is no reason not to create a script with combinations of animate and inanimate objects with no observer. For example a script describing a Dogs 
interaction with food and may be one with 2 or more inanimate objects with no observer. Or even quantum events?

Vicarious causation: OOO requires an SO to facilitate interaction between (inanimate) objects. In my interpretation this occurs in the Event SO.
Writing a script with only inanimate object immediately raises questions about the nature of the Event. Does it have qualities: a Place, datetime...?

Undermining means that objects can't be reduced to their parts. The Nida and Siena dogs in the script are only sketched out with a limited number of SQs. 
By using names and derivation from Dog the objects are clearly more than their parts/more than their SQs.
Overmining means that objects can't be reduced to their effects. To a certain extent the script is a list of effects. The conversion of the script into
SOs introduces a structure which records the effects described in the script and more besides.
 
The Events and the events in each Event could be seen as the ROs interacting through their sensual qualities.

SOME SOFTWARE IDEAS:

1. Inheritance means that an object may be derived from another, often, more abstract object. 
For example Nida inherits from Dog, Me inherits from Person and Dog and Person both inherit from Animal.
Inheritance can be thought of as a 'type of' relationship: Nida is a type of Dog and Dog is a type of Animal. 
The convention 'Nida : Dog'  and 'Dog : Animal' is used to represent inheritance in a script.

A predefined class hierarchy is used for the class Sensual Object.
    SensualObject inherits from BaseClass and
    SensualQuality inherits from BaseClass
    BaseClass encapsulates properties which are part of the SensualObject and the SensualQuality. For example SensualObject and SensualQuality 
both have references to SOs. This list of references is stored in the BaseClass.

In the Object Oriented Software Language called c#, classes are predefined and objects are instantiated from the classes when the software runs.
In this software all SOs are instantiated from the predefined SensualObject class but inheritance from one SO to another is 
simulated using pointers in the BaseClass.
For example Nida is an SO. Nida is a Dog, and Dog is an SO. Dog is an Animal and Animal is an SO.
These inheritance relationships are captured using a pointer from Nida to Dog and from Dog to Animal.

2. Overriding in c# means replacement or enhancement of a function or property value in a derived object.
Here overriding is supported by allowing an SQ defined in an SO to be overridden in a derived SO. 
For example a Dog may have an SQ called Bark = True, implying that (nearly) all dogs bark.
Nida who is derived from Dog may have SQ Bark = Loud; and may also have another SQ: Bark = High.
A dog which cannot/does not bark would have SQ Bark = False.

3. References: An SO or SQ may refer to a number of SOs.
For example an Event called MeetBakkeveen (MeetBakkeveen : Event) may refer to
the SO Nida and SO Siena. After processing of the script these SOs are coupled to the Event using references in the MeetBakkeveen SO.
*Bakkeveen is a place in Friesland.

This captures any observations about an SO which occur during the Event.
    Nida = swimming is a quality of Nida which occurred during the MeetBakkeveen : Event.
	
Sometimes a quality is added to an existing SO in an Event:	
		Nida {
		FrontLeftLeg	
The SQ is added to Nida and an SQ is added to the Event to record that happening (see redundancy below).

Sometimes an SQ may refer to an SO. For example

MeetBakkeveen: Event {
        Place = Bakkeveen

Bakkeveen is a pre-existing SO defined as Bakkeveen : Place
In this case the reference is stored in the SQ of the MeetBakkeveen Event.

4. Names: An SO and an SQ are identified by a unique name or phrase (a string which may include white space). 
There is no attempt to enumerate possible values of SQs. For example there is no list of possible values for the Dog.Breed.
However it is possible to query the SOs for all the values used in SQ Breed.

5. Timestamping: The BaseClass has property DateTime. By default this is set to the computer DateTime when the SO or SQ 
is created. It may also be overridden to create a more realistic time sequence.
The time stamp looks like this in the script:

    Time 2025-02-04 16:30:00.000

This time will be recorded with every following state change of SOs or SQs until the following Time value occurs in the script.

OOO proposes a flat ontology and interaction between inanimate objects is as valid as the Events used in the example script. 
The Event object used in this program could be used to coordinating this sort of interaction.
While distinct events may occur in these types of Event, processes are also common.
The script language does not support descriptions of processes except approximately by using time stamps. For example growth of a crystal
could be described by 
    Crystal = size 5
    Time 2000-02-04 06:30:00.000
    Crystal = size 6
    Time 2025-02-04 20:30:00.000

6. No forward referencing. The program processes the script step by step without forward references.
For example: The script starts with an Event called Init before the class Event has been defined.
Init {
...

//The class Event is defined in Event Init with several SQ's.
	Event {
	Place
	Weather
	End
	}

//Only then can Init inherit from Event.
Init : Event			//INHERIT_SENSUALOBJECT


Initial Conclusions.
One idea of the experiment is to look at the data structure, sub-routines and functions needed to turn the list of input data into 
objects and to assess whether this overhead is reasonable. The BuildSOs class processes input data and builds the SOs in a List array 
called 'theSOs' in a predefined class called SOs. The BuildSOs and SOs classes can be thought of as 'Me' as it includes all the SOs and
the processing required to manage them. What to call this when a script contains only inanimate SOs?

One problem with the result is that the SOs and SQs are not 'visible' and can only be inferred from the output (sounds familiar!).
For this reason the SOs class also includes several subroutines which query the object model. These use similar approaches by iterating through SOs and SQs.

    DisplaySOs(string Parent) Display the SOs with SQs filtering on Parent. For example SOs.PrintSOs("*"); displays all the SOs. 
    SOs.PrintSOs("Dog"); Displays SOs derived from Dog.
	Here is the SO Nida at the end of the script (Nida undergoes several changes during the script; check out the time stamps.
	
	2025-02-02 14:00:00.000 Nida : Dog
	SO Nida has qualities.
		2025-02-02 14:00:00.011 MeetNida => Tail = Short
		2025-02-02 14:00:00.011 MeetNida => Colour = Black
			2025-01-01 14:00:00.000 SO Reference Black : Colour
		2025-02-02 14:00:00.011 MeetNida => Name = Nida
			2025-02-02 14:00:00.000 SO Reference Nida : Dog
		2025-02-02 14:00:00.011 MeetNida => Name = Nidaatje
		2025-02-04 14:00:00.014 DogsAreDogs => INHERIT_SENSUALOBJECT Dog = True
		2025-02-04 14:00:00.011 DogsAreDogs => Breed = None
		2025-02-04 14:00:00.011 DogsAreDogs => Size = Medium
		2025-02-04 16:00:00.011 MeetBakkeveen => faster = Siena
			2025-02-03 14:00:00.000 SO Reference Siena : Dog
		2025-02-04 16:00:00.011 MeetBakkeveen => Colour = Black
			2025-01-01 14:00:00.000 SO Reference Black : Colour
		2025-03-05 09:48:48.069 Generated => faster = Siena
			2025-02-03 14:00:00.000 SO Reference Siena : Dog
		2025-03-05 09:48:48.069 Generated => playing = Siena
			2025-02-03 14:00:00.000 SO Reference Siena : Dog
		2025-03-05 09:48:48.069 Generated => sleeping = True
		2025-03-05 09:48:48.069 Generated => playing = Black Dog
			2025-02-04 16:10:00.000 SO Reference Black Dog : Dog
		2025-03-05 09:48:48.069 Generated => coming = True
		2025-03-05 09:48:48.069 Generated => sitting = True
		2025-03-05 09:48:48.069 Generated => eating = True
		2025-03-05 09:48:48.069 Generated => swimming = True

	SO Nida has references.
		2025-01-01 14:00:00.000 SO Reference Colour
		2025-01-01 14:00:00.000 SO Reference Black : Colour
		2025-02-02 14:00:00.000 SO Reference Nida : Dog
		2025-02-03 14:00:00.000 SO Reference Siena : Dog
		2025-02-04 17:40:00.000 SO Reference faster : Adverb
		2025-03-05 09:48:48.058 SO Reference playing : Verb
		2025-02-04 16:10:00.000 SO Reference Black Dog : Dog

    
    QuerySOs(string _parent, string _child) Query SOs for example SOs.QuerySOs("Event", "Dog"); looks for SOs with a Parent = "Event" and referenced SOs 
    with a Parent = "Dog".
	Query Parent: 'Event' for Child: 'Dog'
		 Parent: 2025-02-02 14:00:00.000 MeetNida Child: 2025-02-02 14:00:00.000 Nida
		 Parent: 2025-02-03 14:00:00.000 MeetSiena Child: 2025-02-03 14:00:00.000 Siena
		 Parent: 2025-02-03 14:00:00.000 MeetSiena Child: 2025-02-02 14:00:00.000 Nida
		 Parent: 2025-02-04 14:00:00.000 DogsAreDogs Child: 2025-02-04 14:00:00.000 Dog
		 Parent: 2025-02-04 14:00:00.000 DogsAreDogs Child: 2025-02-02 14:00:00.000 Nida
		 Parent: 2025-02-04 14:00:00.000 DogsAreDogs Child: 2025-02-03 14:00:00.000 Siena
		 Parent: 2025-02-04 15:00:00.000 MeetBakkeveen Child: 2025-02-03 14:00:00.000 Siena
		 Parent: 2025-02-04 15:00:00.000 MeetBakkeveen Child: 2025-02-02 14:00:00.000 Nida
		 Parent: 2025-02-04 15:00:00.000 MeetBakkeveen Child: 2025-02-04 16:10:00.000 Black Dog
		 Parent: 2025-03-05 09:48:48.058 Generated Child: 2025-02-02 14:00:00.000 Nida
		 Parent: 2025-03-05 09:48:48.058 Generated Child: 2025-02-03 14:00:00.000 Siena
    
    RandomSQs() Display a list of SQs by starting with a random SQ then moving to an SQ with the same Name but another SO which has not been printed yet. 
    When there are no SQs meeting the above criterion switch to another SO which is referenced by the last SO.
	This results in a sort of declamation of related qualities: 
	SOs RandomSQs 
	Using 235 qualities and starting with index = 198
		SO Feel with SQ Root = Feel
		SO Verb with SQ Root = True
		SO playing with SQ Root = play
		SO sleeping with SQ Root = sleep
		SO running with SQ Root = runn
		SO call with SQ Root = call
		SO come with SQ Root = come
		SO coming with SQ Root = com
		SO sit with SQ Root = sit
		SO sitting with SQ Root = sitt
		SO eating with SQ Root = eat
		SO swimming with SQ Root = swimm
	
	SWITCH from swimming.Root using reference Generated to Me.Feel
		SO Me with SQ Feel = Cold
	
	SWITCH from Me.Feel using reference InitEvent to Me.Gender
		SO Me with SQ Gender = Male
		SO Animal with SQ Gender = True
		SO M with SQ Gender = Female
	
	SWITCH from M.Gender using reference MeetNida to MeetNida.Weather
		SO MeetNida with SQ Weather = Cold
		SO Event with SQ Weather = True
		SO MeetSiena with SQ Weather = Cold
		SO MeetBakkeveen with SQ Weather = Cold
	
	SWITCH from MeetBakkeveen.Weather using reference Black Dog to Black Dog.Size
		SO Black Dog with SQ Size = Small
		SO Nida with SQ Size = Medium
		SO Siena with SQ Size = Large
		SO Dog with SQ Size = True
	
	SWITCH from Dog.Size using reference DogsAreDogs to DogsAreDogs.Place
		SO DogsAreDogs with SQ Place = Home
		SO Event with SQ Place = True
		SO MeetNida with SQ Place = Bakkeveen
		SO MeetSiena with SQ Place = Dino Bos
		SO MeetBakkeveen with SQ Place = Bakkeveen
		SO T2 with SQ Place = Home
	
	SWITCH from T2.Place using reference Verb to Verb.Root
		SO Verb with SQ Root = True
	

Inheritance: I remember Fons commenting that some people with autism are not able to generalise; which could mean that every dog is a new
SO. 

Juliet's immediate response was that all the output could be generated directly from the script.
This must be True but is it a problem?

1. This could also be true of humans. The hidden nature of SOs and the number of SOs in our brain coupled with memory loss makes this difficult/impossible to judge.

2. A 'Me' SO could include emotions which influence creation of other SOs. I think that memory and attention to detail depend on emotion. No emotion no memory!? 
There is no attempt to simulate this in the software.

3. An Init event is used to populate the 'Me' SOs before processing of other Events takes place. Altering the Init SO will influence creation of other SOs.

4. The object structure is easier to use by the output routines such as DisplaySOs, QuerySOs and RandomSQs, than event lists in a script. 
The object model includes redundancy. For example Nida.Tail = Short occurs in Event MeetNida. 
An SQ is included in Nida which couples the SQ in Nida to an SQ in the event MeetNida. 
Referencing MeetNida from Nida and vice-versa is easy and that seems analogous to thinking about Nida brings to mind MeetNida and vice-versa.

5. Complexity and emergence. 
Is turbulence in faster flowing water an emergent property? Computer simulation of turbulent flowing water is only approximate, the Navier Stokes Equations do
describe it accurately but are proving impossible to solve. Perhaps this program highlights the extreme simplifications required 
of any simulation of thought processes. Using this program with 10000 SOs (5 SOs per day * 365 * 50 years of SOs) and 20 SQs per SO would also not help 
and would still be an extreme simplification of what is essentially an analogue process. 
The dependence of our experience on emotion excludes any form of experience in a simulation. The most we can expect is 
surprise at relationships a program can find hidden in the input data.

6. Simulation of Thinking. 
The program and script simulate this in 2 ways by defining generalised objects like Dog on the fly in the script and 
by analysing Events looking for characteristics of other SOs.
(CHECK: think about modifying the script to make 'thinking' Events and actual Events explicit using derivation)

In more detail: 
a. Create objects like Dog, on the fly, simulating the realisation that several SOs are the same type: Nida and Siena are a type of Dog.

The Dogs Nida and Siena are defined before the SO Dog has been defined. Here is Nida who is defined in event MeetNida.
		Nida {
		FrontLeftLeg
		BackLeftLeg
		FrontRightLeg
		BackRightLeg
		Tail = Short
		Bark
		LeftEar
		RightEar
		Colour = Black
...
		}

In Event DogsAreDogs : Event {

Dog is defined
		Dog : Animal {    //At home I realise that Nida and Siena are a type of Animal called a Dog.
		Breed
		Size
		}

And only then is the derivation of Nida from Dog defined:
Nida : Dog	{	//Because Nida already exists the default qualities (qualities with Value True of not defined) are moved to Dog
		Breed = None
		Size = Medium
		}

The result is the following showing how default properties are now in Dog and the none default properties of Nida remain in Nida.
	SO Nida inherits from Dog which has qualities
		2025-02-04 14:00:00.000 SO: Dog
		SO Dog inherits from Animal which has qualities
		SO Dog has qualities
			2025-02-04 14:00:00.003  Dog.INHERIT_SENSUALOBJECT Animal = True
			2025-02-04 14:00:00.001  Dog.FrontLeftLeg = True
			2025-02-04 14:00:00.001  Dog.BackLeftLeg = True
			2025-02-04 14:00:00.001  Dog.FrontRightLeg = True
			2025-02-04 14:00:00.001  Dog.BackRightLeg = True
			2025-02-04 14:00:00.001  Dog.Bark = True
			2025-02-04 14:00:00.001  Dog.LeftEar = True
			2025-02-04 14:00:00.001  Dog.RightEar = True

	SO Nida has qualities
		2025-02-02 14:00:00.005  Nida.Tail = Short
		2025-02-02 14:00:00.009  Nida.Colour = Black

NOTE: There is an SO called Black but how a colour is perceived depends on the object. Nida.Black is different from the SO Black.

b. Analysing Events looking for characteristics of other SOs.
A stream of data is extracted from the SOs and is written to a separate script in an Event called Generated.txt.
The subroutine QuerySOSQ() looks for SQs of Events which are SOs and 
creates a script to add the quality to the subject.
For example:
        MeetBakkeveen : Event {
        Nida = playing Siena
        
is used to imply that Nida can play with Siena and will create a new SQ for Nida:
        Nida{
         playing = Siena
        }

I have made an initial attempt at creating SOs to hold types of speech (verbs, adverbs, nouns) and to eventually
add 'production' rules which described how the words should be used in output.
Stem of swimming is swimm in the existing script. I could simulate reading a book about grammar to improve. 

Finally; software requirements can make use of scenarios. These are sequences of statements which describe the use and actions
of users, computer, mouse, software modules and complete programs to achieve the required tasks.
It is tempting to develop a scenario in a script and eventually to generate c# code to implement it.
