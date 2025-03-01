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
Object Oriented Ontology describes Real Objects (ROs) with Real Qualities (RQs) and
Sensual Objects (SOs) with Sensual Qualities (SQs).

To illustrate: In his book, Object-oriented Ontology, Graham Harman includes a chapter on the American Civil War (ACW).
He uses the ACW as an example of an RO.
I assume here that he has a Sensual Object of this war and has written a chapter based on it in his book.
I further assume that he created an SO of type Event which in some way stores his experience while writing the chapter.
On completion, the chapter in the book is an RO. 
This RO includes well established facts, dates and figures but also some opinions, for example,
on the relative importance of generals and battles.

While reading the chapter I created an SO which I have called an SO of type Event. 
This SO records my sitting down and reading the chapter.
By chance I read a book about the ACW last year. I therefore had an existing ACW SO.
The information in the chapter augmented this SO and made references between the chapter SO of type Event
and my existing ACW SO.

This software is an attempt to simulate a part of this process by using a stream of data
to build (Sensual) Objects using object oriented software ideas such as inheritance, overriding and properties.
I have chosen to do this by using 2 dogs we own and local places for dogs to roam and play.

I have started the simulation with a stream of data instead of trying to simulate the "unknowable" RO and the associated RQs.
In my reality the stream may have different sources: spoken language, written language,
visual stimulus, smells, emotion and thoughts. I may switch rapidly between different sources 
but my concious brain seems to process data in a sequence as opposed to, for example, multiple parallel streams.  

My stream depends on my interpretation of external RO and on the state of my own conciousness
or in this context, on the state of my SOs.
It is inevitable that my interpretation of an RO differs from someone elses.

I have not spent time on processing ROs/RQs to create streams from different sources.
Creating streams is not easy but it is not the focus here.
The stream is simulated with a list of commands. 

Some relevant software terms:

1. Inheritance means that an object may be derived from a more abstract object. Nida is a type of Dog. Me is a type of Person.
Dog and Person both inherit (are derived) from Animal.

A predefined class hierarchy is used for Sensual Objects.
SensualObject : BaseClass and
SensualQuality : BaseClass
BaseClass encapsulates properties which are part of the SensualObject and the SensualQuality.
In Object Oriented Software like c#, classes are predefined and are instantiated when the software runs.

In this software all SOs are instantiated from the predefined SensualObject class and inheritance is 
simulated while the stream of data is being processed using pointers in the BaseClass.
For example Nida is an SO. Nida is a Dog, and Dog is an SO. Dog is an Animal and Animal is an SO.
This inheritance relationship is captured using a pointer from Nida to Dog and from Dog to Animal.
The convention 'Nida : Dog'  and 'Dog : Animal' is used to represent inheritance.

(I remember Fons commenting that some people with autism are not able to generalise; which could mean that every dog is a new
object. Inheritance is a way of structuring objects which have much in common.) 

2. Overriding in c# supports replacement of a function or property value in a derived object.
Here overriding is supported by allowing a quality defined in an SO to be overridden in a derived SO. 
For example a Dog may have an SQ called Bark = True.
Nida who is derived from Dog may have SQ Bark = Loud; and may also have a 2nd SQ Bark = High.

3. References: An SO may refer to a number of other SOs.
For example an event called MeetBakkeveen (MeetBakkeveen : Event) may refer to
the SO Nida and SO Siena. These SOs are coupled to the event using references in the Event SO.
This captures any observations about an SO which occurs during the Event:
    Nida = swimming is a quality of Nida which occurred during MeetBakkeveen : Event.

Sometimes an SQ may refer to an SO. For example
MeetBakkeveen: Event {
        Place = Bakkeveen
Bakkeveen is a preexisting SO defined as Bakkeveen : Place
In this case the reference is stored in the SQ.

Names: An SO and an SQ is identified by a unique name or phrase (a string which may include white space). 
There is no attempt to enumerate possible values of SQs.
For example there is no list of possible values for the Dog.Breed.
However it is possible to query the object model for all the values used in SQ Breed.

4. Timestamping: The BaseClass has property DateTime. By default this is set to the computer DateTime when the SO or SQ 
is created. It may also be overridden to create a more realistic timed sequence.
The time stamp looks like this:
    Time 2025-02-04 16:30:00.000

The SW builds a list of SOs called 'theSOs', encapsulated in predefined class called SOs.
This class also includes the main processing of the input data and can be thought of as 'Me' as it includes all the SOs and
the processing required to manage them.

Simulation of Thinking.
A stream of data may come from ROs but can also come from 'thinking about the SOs'.
The program and script simulate this in 2 ways 
1. by defining generalised objects like Dog on the fly and 
2. by analysing Events looking for characteristics of other SOs.

In more detail: 
1. Create objects like Dog, on the fly, simulating the realisation that 
SOs are of the same type: Nida and Siena are a type of Animal called Dog.

The Dogs Nida and Siena are defined before the SO Dog has been defined. Here is Nida.
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
In Event 
DogsAreDogs	: Event {

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

2. Analysing Events looking for characteristics of other SOs.
The subroutine QuerySOSQ() looks for 'SOFrom' pointers in the SQ of Events and 
creates a script to add the quality to the subject.
For example:
        MeetBakkeveen : Event {
        Nida = playing Siena
        
is used to imply that Nida can swim and will create a new SQ for Nida:
        Nida{
         playing = Siena
        }

I have made an initial attempt at creating SOs to hold types of speech (verbs, adverbs, nouns) and to eventually
add 'production' rules which described how the words should be used in output.


The idea of the experiment is to look at the data structure, sub-routines and functions needed to turn the list of input data in to 
objects and to assess whether this overhead is reasonable.
A possible criticism is that all the output could be generated directly from the original list of input data.
The argument against is that the object structure is easier to use either by adding new SOs and SQs or for analysis of SOs/SQs; 
and that our brain is more likely to structure data in objects instead of processing event lists.


This class includes functions which
  build the model from the stream of data. 
  print the list of SOs.
  query the list of SOs.

The incoming stream is a collection of Events. An event is a collection of detailed 'events'.
{InterfaceEvent {ADD_SENSUALOBJECT |  INHERIT_SENSUALOBJECT | AddQuality | SWITCH_TO_SENSUALOBJECT }
The first Event is called Init and it creates a number of SOs which are a model of the SO's which are already present
before a new Event occurs.
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
            SOs.PrintEvents("*");
            //SOs.PrintEvents("Dog");



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
