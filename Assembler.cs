//TEGAN STRALEY & CATIE COOK
//FILE: Assembler.cs
//PROJECT: created for project 6 of NAND2Tetris course

//File converts .asm input file to hack machine code. The resulting code
//in displayed out to user and also written to a corresponding output file. 

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AssemblerLab
{
    class Assembler
    {
        enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND };

        //GLOBAL VARIABLES
        int lineNo = 16; // should start out at zero?
        string line;
        string symbol;
        string dest;
        string comp;
        string jump;
        Parser_CommandType commandType;
        bool keepGoing;
        bool secondTimeThrough = false; 


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PARSE FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void parseLine()
        { //takes out white space & comments
          //set most global variables to null 
            symbol = null;
            dest = null;
            comp = null;
            jump = null;
            Assembler assembler = new Assembler();

            //commandType = Parser_NO_COMMAND;
            keepGoing = true;
            Regex regex = new Regex("^[a-zA-Z0-9]*$@()"); //should @ be in here?

            //create char array to fill with 
            char[] parsedLine = new char[line.Length];
            int j = 0; //line counter for parsedLine

            //get rid of white space
            for (int i = 0; i < line.Length && keepGoing == true; i++)
            {

                if (line[i] == '/')
                {
                    if (line[i + 1] == '/')
                    {
                        //comment has been found! Don't copy any of the rest of the line
                        keepGoing = false; //set keepGoing to false and will fall out of for loop
                    }
                }
                else if (regex.IsMatch(line[i].ToString()))
                {
                    parsedLine[j] = line[i];
                    j++; //only increment j if [a-zA-Z0-9]*$ has been found in line[i]
                }

            }

            string parsedString = new string(parsedLine);
            Console.WriteLine("ParsedLine: " + parsedString);


            if (keepGoing == true && secondTimeThrough == false)
            {
                assembler.pass1(parsedLine);
            }

            if(keepGoing == true && secondTimeThrough == true)
            {
                assembler.pass2(parsedLine);
            }


        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MAIN FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public static void Main(string[] args)
        {
            Assembler assembler = new Assembler();
            Console.WriteLine("Enter in the .asm file you wish to convert to .hack : ");
            string asmFileName = Console.ReadLine();
            string line;

            /*IN CLASS how to 

            StreamReader file = new StreamReader(inFileName);


            */

            string filePath = System.IO.Path.GetFullPath( asmFileName + ".asm");
            Console.WriteLine("FILE:  \n" + filePath);
           StreamReader sr = new StreamReader(filePath);
            //asmFileName = "C:\\Users\\Tegan\\Desktop\\COLLEGE WORK\\Spring 2016\\NAND2Tetris\nand2tetris\nand2tetris\\projects\06\\AssemblerLab\\" + asmFileName + ".asm";
            //Console.WriteLine("FILE:  \n" + asmFileName);
            if (File.Exists(filePath))
            {
                Console.WriteLine("YAY!");
                //System.IO.StreamReader file = new System.IO.StreamReader(asmFileName);
               /* while ((line = filePath.ReadLine()) != null)
                { //line by line each loop through
                    Console.WriteLine("FILE IS OKAY AND FOUND ! \n");
                    asmFileName = Console.ReadLine();
                    assembler.parseLine();

                } 
                */
            }//end of if
            else {//incorrect file name
                Console.WriteLine("Sorry you entered an invalid file name!\n Program terminating...\n");
                asmFileName = Console.ReadLine(); // just here so VS window doesn't close as quick
            }//end of else  

        }//end of main



        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS1 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void pass1(char[] parsedLine)
        { //takes out white space & comments

            //purpose:  to add new (SYMBOLS) to the SymbolTable            //add the (SYMBOLS) to the SymbolTable THIS SHOULD BE IN PARSE 1????????????
            if (parsedLine[0] == '(' && keepGoing == true)
            {
                //read til the next ')'
                int startIndex = line.IndexOf('(');
                int endIndex = line.IndexOf(')');
                string parsedString = new string(parsedLine);
                string newSymbol = parsedString.Substring(startIndex + 1, endIndex - 1);
                //add to dictionary
                SymbolTable.symbolTable.Add(newSymbol, lineNo);
                lineNo++; //everytime new smymbol added we need to increment lineNo 

                Console.WriteLine("The symbol that was added was  : " + newSymbol + " , " + lineNo);
            }

            secondTimeThrough = true;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS2 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void pass2(char [] parsedLine)
        { //takes out white space & comments
            //purpose to convert line (parsed line?) to binary and write each line to the outfile
            //A INSTRUCTION
            if (parsedLine[0] == '@')
            {
                string parsedString = new string(parsedLine);
                int startIndex = parsedString.IndexOf('@');
                int endIndex = parsedString.IndexOf(parsedString[parsedString.Length]);
                string aInstruc = parsedString.Substring(startIndex + 1, endIndex);

                //check if it is in the SymbolTable.symbolTable
               /* if (SymbolTable.symbolTable[aInstruc] != null)
                {

                }
                */
            }

            //C INSTRUCTION



            //L () INSTRUCTION






        }
    }//end of class Assembler
}//end of namespace AssemblerLab
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
