//TEGAN STRALEY & CATIE COOK
//FILE: Assembler.cs
//PROJECT: created for project 6 of NAND2Tetris course

//File converts .asm input file to hack machine code. The resulting code
//in displayed out to user and also written to a corresponding output file. 

using System;
using System.IO;
using System.Text;
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
                         Console.WriteLine(noWhiteSpacesLine);
                    }
				    
			  
               }//end of if
			else{
				Console.WriteLine("Sorry you entered an invalid file name!\n Program terminating...\n");
			}//end of else

               return inputFileList;
				
			
			
			
			
		}//end of readFile



     }//end of Assembler
}//end of namespace

