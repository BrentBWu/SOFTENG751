# SOFTENG751: An educational application to illustrate parallel programming concepts 
This is the repository containing the project for SOFTENG751 project. The repository contains a unity3D project which is built in Unity3D version 5.6.1.

# Build Instructions
Clone the code or download the package version, then build the application following normal Unity Build instructions.

# Execution instructions
If you have downloaed the build version, exectue the SOFTENG751.exe to run the application.
If you have downloaded the sorce file, build the application in Unity3D following normal Unity Build instructions. Build the application in Unity3D version 5.6.1 is recommended

# Adding Extra Content
There are few built-in quiz and scheduleing game in the application. You can add more by having .txt written in the following format:
Contens in the brace [] is what you need to fill in, remove all () in the final version:
Quiz:
(Number of Option, form 2-4)
(Question)
(Option, 1 option per line)
(Index of correct answer)

Schedueling Game:
Processor (Number of processor, form 2-4) {
(node name)	 [Weight=(node weight)];
(node be dependet on) -> (node that has dependency)	 [Weight=(transfer time)];
}
Answer{
(node name) [Weight=(node weight), Start=(start time), Processor=(index of allocated processor)];
}

Download the source, add quiz files to Asset/resources/quiz, add Schedueling Game files to Asset/resources/game. Then rebuild the application following normal Unity Build instructions.
