using OOO3;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

/*

Some relevant software terms:

1. Inheritance means that an object may be derived from another, often, more abstract object. 
For example Nida inherits from Dog, Me inherits from Person and Dog and Person both inherit from Animal.
Inheritance can be thought of as a 'type of' relationship: Nida is a type of Dog. 
The convention 'Nida : Dog'  and 'Dog : Animal' is used to represent inheritance.

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

2. Overriding in c# supports replacement of a function or property value in a derived object.
Here overriding is supported by allowing a SQ defined in an SO to be overridden in a derived SO. 
For example a Dog may have an SQ called Bark = True, implying that (nearly) all dogs bark.
Nida who is derived from Dog may have SQ Bark = Loud; and may also have a 2nd SQ Bark = High.
A dog which cannot/does not bark would have SQ Bark = False.

3. References: An SO or SQ may refer to a number of SOs.
For example an Event called MeetBakkeveen (MeetBakkeveen : Event) may refer to
the SO Nida and SO Siena. After processing of the script these SOs are coupled to the Event using references in the MeetBakkeveen SO.
*Bakkeveen is a place in Friesland.

This captures any observations about an SO which occur during the Event.
    Nida = swimming is a quality of Nida which occurred during the MeetBakkeveen : Event.

Sometimes an SQ may refer to an SO. For example

MeetBakkeveen: Event {
        Place = Bakkeveen

Bakkeveen is a preexisting SO defined as Bakkeveen : Place
In this case the reference is stored in the SQ of the MeetBakkeveen Event.

4. Names: An SO and an SQ are identified by a unique name or phrase (a string which may include white space). 
There is no attempt to enumerate possible values of SQs. For example there is no list of possible values for the Dog.Breed.
However it is possible to query the SOs for all the values used in SQ Breed.

5. Timestamping: The BaseClass has property DateTime. By default this is set to the computer DateTime when the SO or SQ 
is created. It may also be overridden to create a more realistic time sequence.
The time stamp looks like this in the script:

    Time 2025-02-04 16:30:00.000

OOO proposes a flat ontology and interaction between inanimate objects, which does not need to be observed by someone, 
is as valid as the Events used in the example script. 
The Event object used in this program lends itself to coordinating this sort of interaction.
While distinct events may occur in these types of Event, processes are also common.
The script language does not support descriptions of processes except approximately by using time stamps. For example growth of a leaf
could be described by 
    Leaf.size=5
    Time 2025-02-04 06:30:00.000
    Leaf.size=6
    Time 2025-02-04 20:30:00.000

6. No forward referencing. The program processes the script step by step without forward references.

//The script starts with an Event called Init before the class Event has been defined.
Init {
...

//The class Event is defined with several SQ's.
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
called theSOs in a predefined class called SOs. These 2 classes can be thought of as 'Me' as it includes all the SOs and
the processing required to manage them.

One problem with the result is that the SOs and SQs are not 'visible' and can only be inferred from the output (sounds familiar!).
For this reason the SOs class also includes several subroutines which query the object model. These use similar approaches by iterating through SOs and SQs.

    DisplaySOs(string Parent) Display the SOs with SQs filtering on Parent. For example SOs.PrintSOs("*"); displays all the SOs. 
    SOs.PrintSOs("Dog"); Displays SOs derived from Dog.
    
    QuerySOs(string _parent, string _child) Query SOs for example SOs.QuerySOs("Event", "Dog"); looks for SOs with a Parent = "Event" and referenced SOs 
    with a Parent = "Dog".
    
    RandomSQs() Display a list of SQs by starting with a random SQ then moving to an SQ with the same Name but another SO which has not been printed yet. 
    When there are no SQs meeting the above criterion switch to another SO which is referenced by the last SO.

Inheritance: I remember Fons commenting that some people with autism are not able to generalise; which could mean that every dog is a new
SO. 

Juliet's immediate response was that all the output could be generated directly from the original list of input data.
This argument can be countered by
1. This could also be true of humans. The hidden nature of SOs and the number of SOs in our brain coupled with memory loss makes this difficult/impossible to judge.

2. A 'Me' SO could include emotions which influence creation of other SOs. I think that memory and attention to detail depend on emotion. No emotion no memory!? 
There is no attempt to simulate this in the software.

3. An Init event is used to populate the 'Me' SOs before processing of other Events takes place. Altering the Init SO will influence creation of other SOs.

4. The object structure is easier to use by the output routines such as DisplaySOs, QuerySOs and RandomSQs, than event lists. 
The object model includes redundancy. For example Nida.Tail = Short ocuurs in Event MeetNida. 
An SQ is included in Nida which couples the SQ in Nida to an SQ in the event MeetNida. 
This facilitates referencing MeetNida from Nida and vice-versa and that seems analogous to thinking about Nida brings to mind MeetNida and vice-versa.

5. Complexity and emergence. 
Is turbulence in faster flowing water an emergent property? Computer simulation of turbulent flowing water is only approximate, the Navier Stokes Equations do
describe it accurately but are proving impossible to solve. Perhaps this program highlights the extreme simplifications required 
of any simulation of thought processes. Using this program with 10000 SOs (5 SOs per day * 365 * 50 years of SOs) and 20 SQs per SO would also not help 
and would still be an extreme simplification of what is essentially an analog process. 
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

In Event DogsAreDogs	: Event {

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
A stream of data is extracted from the SOs and is written to a separate script in an Event called Generated.scr.
The subroutine QuerySOSQ() looks for 'SOFrom' pointers in the SQ of Events and 
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

The incoming stream is a collection of Events. 
An Event is a collection of detailed 'events'.
{InterfaceEvent {ADD_SENSUALOBJECT |  INHERIT_SENSUALOBJECT | AddQuality | DateTime }
*/

namespace OOO3
{
    static class Program
    {
    static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                BuildSOs.ProcessFile(args[0]);
            }
            else
            {

            }

            //Look for SOFrom pointers in Events and create a script to add the quality to the subject.
            SOs.QuerySOSQ("Event");

            ///
            // SOs.PrintSOs(2);
            SOs.DisplaySOs("*");
            SOs.DisplaySOs("Dog");



            ////Query Person present at an Event.
            //SOs.QuerySOs("Event", "Person");
            SOs.QuerySOs("Event", "Dog");

            for (int i = 0; i < 30; i++)
            {
                SOs.RandomSQs();
                Console.WriteLine("");
            }
                 

            //SOs.QuerySOs("Event", "Animal");
            //SOs.PrintInput();

        }

    }
}
