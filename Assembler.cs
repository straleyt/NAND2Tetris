//TEGAN STRALEY & CATIE COOK
//FILE: Assembler.cs
//PROJECT: created for project 6 of NAND2Tetris course

//File converts .asm input file to hack machine code. The resulting code
//in displayed out to user and also written to a corresponding output file. 

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExperssions;
using System.Collections.Generic;

namespace Assembler
{
     class Assembler
     {
     	enum Parser_CommandType {Parser_NO_COMMAND=0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND };
     	
     	//GLOBAL VARIABLES
     	int lineNo = 0; // should start out at zero?
     	string line;
     	string symbol;
     	string dest;
     	string comp;
     	string jump;
     	Parser_CommandType commandType;
     	
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MAIN FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
          public static void Main(string[] args)
          {
		string asmFileName;
		Console.WriteLine("Enter in the .asm file you wish to convert to .hack : ");
		asmFileName = Console.ReadLine();
			
		if(File.Exists(asmFileName)){
                	while ((line = asmFileName.ReadLine()) != null) { //line by line each loop through
                	
                		parseLine(); 
                		pass1();
                		
                		lineNo++; 
                	}
                
               	}//end of if
		else{//incorrect file name
			Console.WriteLine("Sorry you entered an invalid file name!\n Program terminating...\n");
		}//end of else
		
		//surround this with a while like before ??? 
		parseLine(); 
                pass2(); //writes to the output file .hack      
		
          }//end of main

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PARSE FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
void parseLines(){ //takes out white space & comments
	//set most global variables to null 
     	symbol = NULL;
     	dest = NULL;
     	comp = NULL;
     	jump = NULL
     	commandType = Parser_NO_COMMAND;
     	bool keepGoing = true;

     	//create char array to fill with 
     	char[] parsedLine = new char[line.length];
     	int j = 0; //line counter for parsedLine
     
     	//get rid of white space
     	for (int i = 0; i < line.length && keepGoing == true; i++){
     		
     		if(line[i] == '/'){
     			if(line[i+1] == '/'){
     				//comment has been found! Don't copy any of the rest of the line
     				keepGoing = false; //set keepGoing to false and will fall out of for loop
     			}
     		}
     		else(line[i].isAlpha()){ //isAlpha in c#?
     			parsedLine[j] = line[i];
     		}
     		
     	}
     	
     	//get rid of comments
     	
	
}
   
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS1 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
void pass1(){ //takes out white space & comments

	//purpose:  to add new (SYMBOLS) to the SymbolTable
	
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS2 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
void pass2(){ //takes out white space & comments

     //purpose to convert line (parsed line?) to binary and write each line to the outfile
}
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~END OF PASS2 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
/*		
//*********** CLASS DEC/DEF *************
public static class ExtensionsSystem //new class to take in 
{
	List<string> StripComments(this string inputFileList) //takes in the input file
	{
		List<string> result = new List<string>(inputFileList.Split(new string[] {"\r", "\n"}, 
		StringSplitOptions.RemoveEmptyEntries) 
		); //uses array to look through file
	result = result
		.Where(line => !(line.StartsWith("//") || line.StartsWith("#"))) // where there is a line comment
		.ToList(); // not sure...
		
		return result; 
			
	}//end of StripComments definition
	
}//end of class dec/def

     }//end of Assembler
}//end of namespace



//**** Options? ****
// Found this, we could write to a new file that exclused both white space and comments
// var t= Path.GetTempFileName();
// var l= File.ReadLines(fileName).Where(l => !l.StartsWith("//") || !l.StartsWith("#"));
// File.WriteAllLines(t, l);
// File.Delete(fileName);
// File.Move(t, fileName);
*/
