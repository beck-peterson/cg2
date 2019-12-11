Beck Peterson
Created as a final project for Computer Graphics II with Dr Haim Levkowitz in fall of 2019

** Intro **
For my final project, I decided to make a visualization tool for program execution to assist new programmers to
understand how the program runs. For this, I made a visualization of the stack. This assists new programmers to both
understand recursion as well as iteration. To allow new or non-programmers to get the same sense I have as a
programmer, I created a simple interactive interface that allows the user to play around with the code.

** How to use **
To interface with the code viewer, there are three different elements. There are boards that display the stack, a
button panel to modify how the program is running, and an informative display. The boards contain the function that is
being run on them. In addition to this code, there is a line that is shown in orange. This line is a pointer to the
current line of code being executed. At any point in the program, the orange lines on each of the boards represent the
stack trace. One benefit this program has over looking at a traditional stack trace is that this shows you the full
function that the line of code is sitting in throughout the entire duration of execution. Next, there is the button
panel. The panel contains five buttons. There are start and stop buttons that start or stop the execution of the
selected program. There are increase and decrease buttons that change functionality based on the scenario. If there is
not currently a program running, the increase and decrease buttons affect the value of the integer being passed into
the functions. For convenience, I restricted the range of the input to be between 1 and 10 inclusive. When there is a
program running, these two buttons will increase and decrease the speed that the program is running at. The text on the
buttons reflects their current behavior. And finally, there is a program selection button. Pressing this button will
loop through all currently available functions for viewing. Lastly, there is an informative display that assists the
user in playing with the functions. The display contains the name of the function that is currently selected. The
display also contains the value that is going to be passed into the function for execution. Lastly, the display
contains the output of the function. For each of the demo functions, this value is an integer. There is also a board
that shows the code for the currently selected function to suggest to the user how the viewer is to be used as well as
give an idea for the execution when run.

** How to run **
The program was developed using a first generation version of the Oculus Rift with hand controllers. Additionally, the
system specs on the computer this was developed on were an i7-6700k cpu and GTX 1070 gpu operating under Windows 10,
though these factors should not play a role. This can either be run from the executable, or from the source code
through the Unity editor running version 2018.4.11f1. The system should be plug and play such that having a connected
Oculus rift that is set up with sensors and hand controllers is sufficient to get the program to run through the
headset and behave properly.

** Things that didn't make it in **
Unfortunately, like all good projects, this one attempted to shoot further than it could reach. Originally, a stretch
goal of mine was to create this program give or take exactly as it stands today, but with the added feature that any c#
program could be loaded into the viewer to watch its execution. This task would involve doing syntax tree parsing to
determine the seperate chunks of code, string manipulation to inject the existing program with code that can drive the
display, and execute the auto generated code in place. This would have been achievable despite several issues not
lining up. According to my research, c#'s auto code generation has to be done through precompile directives and
annotations, and my lack of experience with this type of programming along with the desired program needing to be
loaded into the viewer before starting was a major drawback I wasn't able to overcome. Luckily, Unity not only supports
scripts written in c#, but JavaScript as well. JavaScript as it is an interpreted language allows code to be compiled
in line from a string and run. This lead to several weeks or planning and designing a paradigm to allow code to be
generated that would be capable of driving the viewer. Unfortunately, Unity let go of support for JavaScript in January
of 2017. Going back in time to the newest version of Unity that still supported JavaScript, there were some hurdles
that were not overcomeable. For one, none of the assets that were created by Oculus or Steam for VR projects had been
created yet. Additionally, Unity did not have an option for turning the project into a VR project as they added in the
first version of Unity that no longer supported JavaScript. Additionally, Unity only refers to the supported scripting
language as JavaScript when it is actually UnityScript; a lightweight version of JavaScript that Unity developed. So
this language was missing the features that had been implied when it was referred to as JavaScript. All in all, I had to
forgo my desire to hit my stretch goal and power through with the same idea, just with canned functions.

** External resources **
There were a few things that I leveraged from the Unity community. I used a free asset from the asset store to get a
pleasant floor texture for the room so that it felt somewhat more comfortable to spend some time in the viewer. Also,
I used the Oculus asset from the asset store to allow a player controller to be controlled by the Oculus hand
controllers as well as being able to see the controllers to ground you in the simulation. There were other assets that
I had installed to this project, but I removed them when they were not as advertised or no longer needed; though they
may have left fingerprints behind in the meta data of the project. On the scripting side of things, there were two
places I took code from. One was from Ted-Bigham responding to a question on the Unity forums about a system that would
allow coroutines to return values in addition to IEnumerators
(https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html). Next, I took some code from
a youtube channel called Slide Factory for the use of functional buttons for my panel
(https://www.youtube.com/watch?v=I8Pb6Nhb4zE). Unity does not give easy support for buttons, and the only free buttons
in the asset store looked bad and did not work. This code however had two major bugs in it that I fixed. One was that
the button would not press unless repeatedly swatted at. At this point, the button would come free and be able to be
used as normal. A second issue was that the button was firing rapidly when it was fully depressed. This apparently
wasn't an issue for their original purpose, but for mine it broke the program. I implemented fixes for both of these
issues that are well documents in the scripts, and reported back to the author of the code my findings and fixes for
the issues. Aside from the aforementioned assets and short scripts, everything was created by me in full.

** Side Project **
Out of my own curiosity and desire to get used to Unity as a software, I set out to make a project that I had wanted to
make since the Oculus first came out. When I was younger, I came across an interesting article and video about the
inability for humans to perceive the color yellowish-blue due to the nature of the human eye and the way the brain
processes colors. Despite the eye not being able to see this color, one could trick their brain into trying to see the
color by overlapping the two colors, one in each eye. This was done originally by pulling up a large image on a
computer screen and staring through it to cause the eyes to try to merge the colors. As expected, by brain struggled to
merge the two colors as it was something it had never done before and I was curious if the assistance of VR would make
it easier to meld as there would be objects with textures attempting to be these colors. The initial project consisted
of learning how to place objects in unity, how to get a project running, and how to incorporate scripts with objects to
get them to behave as desired. In the side project, much of the code and assets were taken from online or from the
asset store, but I thought I would share it as it is a very interesting experience to go through.