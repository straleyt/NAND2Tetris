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

namespace assembler
{
     class SymbolTable
     {
          public static Dictionary<string, int> symbolTable = new Dictionary<string, int>()
		{
			{"SP", 0},
			{"LCL", 1},
			{"ARG", 2},
			{"THIS", 3},
			{"THAT", 4},
			{"WRITE", 18},
			{"R0", 0},
			{"R1", 1},
			{"R2", 2},
			{"R3", 3},
			{"R4", 4},
			{"R5", 5},
			{"R6", 6},
			{"R7", 7},
			{"R8", 8},
			{"R9", 9},
			{"R10", 10},
			{"R11", 11},
			{"R12", 12},
			{"R13", 13},
			{"R14", 14},
			{"R15", 15},
			{"SCREEN" 0x4000},
			{"KBD" 0x6000}
		};

     }//end of SymbolTable

     class Assembler
     {
          public static void Main(string[] args)
          {
               var inputFileList = new List<String>();
               var outputFileList = new List<String>();

               openFiles(inputFileList);

          }//end of main

          public static List<String> openFiles(List<String> inputFileList)
		{
			string asmFileName;
			Console.WriteLine("Enter in the .asm file you wish to convert to .hack : ");
			asmFileName = Console.ReadLine();
			
			if(File.Exists(asmFileName)){
                    //start reading line by line until EOF 
                    string[] lines = System.IO.File.ReadAllLines(asmFileName);
                    for (int i = 0; i < lines.Length; i++)
                    {
                         string noWhiteSpacesLine = lines[i].Replace(" ", "");
                         Console.WriteLine(noWhiteSpacesLine); // will this save to file or just write it out onto screen? 
                         
                        List<string> fileContents = controlFile.text.StripComments();
			foreach (string ln in lines) Debug.Log(ln); //don't know if this line is effective. 
			//foreach steps through each line or element of array like a for loop
                    }
				    
			  
               }//end of if
			else{
				Console.WriteLine("Sorry you entered an invalid file name!\n Program terminating...\n");
			}//end of else

               return inputFileList;
				
		}//end of readFile
		
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
