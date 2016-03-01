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
     	//GLOBAL VARIABLES
     	int lineNo;
     	string line;
     	string symbol;
     	string dest;
     	string comp;
     	string jump;
     	commandType = Parser.CommandType();
     	
     	
          public static void Main(string[] args)
          {
		string asmFileName;
		Console.WriteLine("Enter in the .asm file you wish to convert to .hack : ");
		asmFileName = Console.ReadLine();
			
		if(File.Exists(asmFileName)){
                	//start reading line by line until EOF 
                	string[] lines = System.IO.File.ReadAllLines(asmFileName);
               	}//end of if
		else{
			Console.WriteLine("Sorry you entered an invalid file name!\n Program terminating...\n");
		}//end of else
		
		
		parseLine();
		pass1();
		parseLine();
		pass2();

          }//end of main

         
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
