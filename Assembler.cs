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
        public Assembler()
        {

        }

        enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND };

        //GLOBAL VARIABLES
        static string line;
        static string symbol;
        static string dest;
        static string comp;
        static string jump;
        Parser_CommandType commandType;
        bool keepGoing;
        bool secondTimeThrough = false; 


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PARSE FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public int parseLine(string line, int lineNo)
        { //takes out white space & comments

          //set most global variables to null 
            symbol = null;
            dest = null;
            comp = null;
            jump = null;
            Assembler assembler = new Assembler();
           

            //commandType = Parser_NO_COMMAND;
            keepGoing = true;
           
            string pattern = @"\s+";
            string replacement = "";
            Regex regex = new Regex(pattern);
            line = regex.Replace(line, replacement); //Takes out all whitespace!

            string result = null;
            for (int i = 0; i < 200; i++)
            {
                result = Regex.Replace(line, @"\s+", "");
            }
       
            //create char array to fill with 
            char[] parsedLine = new char[result.Length];
            int j = 0; //line counter for parsedLine

            for (int i = 0; i < result.Length && keepGoing == true; i++)
            {
                if (result[i] == '/')
                {
                    if (result[i + 1] == '/')
                    {
                        //comment has been found! Don't copy any of the rest of the line
                        keepGoing = false; //set keepGoing to false and will fall out of for loop
                    }
                }
                else if (line[i] == '\n')
                {
                    keepGoing = false;
                }
                else if (char.IsLetterOrDigit(result[i]) || result[i] == '@' || result[i] == '(' || result[i] == ')' || result[i] == '_' || result[i] == '-' || result[i] == '$' || result[i] == '+' || result[i] == ';' || result[i] == '*' || result[i] == '/' || result[i] == '=')
                {
                    parsedLine[j] = result[i];
                    j++; //only increment j if [a-zA-Z0-9]*$ has been found in line[i]
                }
                else
                {
                    Console.WriteLine("ERROR: cannot parse line : " + result); //error checking
                    keepGoing = false;
                }
            }

            int howFull = 0;
            for(int i = 0; i < result.Length; i++)
            {
                if(parsedLine[i] == '\0')
                {
                    //don't add to howFull
                }
                else
                {
                    howFull += 1;
                }
            }

            char[] newResult = new char[howFull];

            //copy contents of parsedLine into newResult
            for(int i = 0; i < newResult.Length; i++)
            {
                newResult[i] = parsedLine[i];
            }


            if (secondTimeThrough == false && newResult.Length != 0) //if parsedLine.Length == 0 means it's an empty array! Just skip this line.
            {
               /* Console.Write("after parsing... : ");
                Console.WriteLine("newResult.Length = " + newResult.Length);
                for (int i = 0; i < newResult.Length; i++)
                {
                    Console.Write(newResult[i]);// TODO CHECK WHAT PARSEDLINE IS
                }
                Console.WriteLine();*/

                lineNo = assembler.pass1(newResult, lineNo);
            }

             if(keepGoing == true && secondTimeThrough == true && newResult.Length != 0)
             {
                Console.WriteLine("GOT INTO PASS2");
                 assembler.pass2(newResult);
              }

            return lineNo;

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS1 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        int pass1(char[] parsedLine, int lineNo)
        { //takes out white space & comments

            //purpose:  to add new (SYMBOLS) to the SymbolTable            //add the (SYMBOLS) to the SymbolTable THIS SHOULD BE IN PARSE 1????????????
            if (parsedLine[0] == '(')
            {
                int startIndex = 0;
                int endIndex = parsedLine.Length;
                string parsedString = new string(parsedLine);
                string newSymbol = parsedString.Substring(startIndex + 1, endIndex - 2);
                //add to dictionary
                SymbolTable.symbolTable.Add(newSymbol, lineNo);
                lineNo += 1; //everytime new smymbol added we need to increment lineNo 

                Console.WriteLine("The symbol that was added was  : " + newSymbol + " , " + lineNo);
            }
            else
            {

            }

            return lineNo;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS2 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void pass2(char[] parsedLine)
        { //takes out white space & comments
          //purpose to convert line (parsed line?) to binary and write each line to the outfile

            Console.Write("IN pass 2... : ");
            Console.WriteLine("parsedLine.Length = " + parsedLine.Length);
            for (int i = 0; i < parsedLine.Length; i++)
            {
                Console.Write(parsedLine[i]);// TODO CHECK WHAT PARSEDLINE IS
            }
            Console.WriteLine();

            //A INSTRUCTION

            if (parsedLine[0] == '@')
            {
                string parsedString = new string(parsedLine);
                int startIndex = parsedLine[0];
                int endIndex = parsedLine.Length;
                string aInstruc = parsedString.Substring(1, parsedString.Length - 1);

                Console.WriteLine("A instruction found : " + aInstruc);
                //check if it is in the SymbolTable.symbolTable
                if (SymbolTable.symbolTable.ContainsKey(aInstruc))
                {
                    Console.ReadLine();

                }
            }//end of if

            //L () INSTRUCTION
            else if (parsedLine[0] == '(')
            {
                int startIndex = 0;
                int endIndex = parsedLine.Length;
                string parsedString = new string(parsedLine);
                string lInstruc = parsedString.Substring(startIndex + 1, endIndex - 2);
                Console.WriteLine("L instruction found : " + lInstruc);
                //find in dictionary

            }

            //C INSTRUCTION





            //open outfile 
            //write newly formed bytecode to outputfile
          


        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MAIN FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public static void Main(string[] args)
        {
            int lineNo = 16;
            Assembler assembler = new Assembler();
            Console.WriteLine("Enter in the .asm file you wish to convert to .hack : ");
            string asmFileName = Console.ReadLine();
            string line;
            StreamReader file = new StreamReader(asmFileName);

            while ((line = file.ReadLine()) != null)
            { //line by line each loop through
                lineNo = assembler.parseLine(line, lineNo);
            }
            assembler.secondTimeThrough = true;
            lineNo = 16;
            Console.Read();
            StreamReader fileAgain = new StreamReader(asmFileName);

            //change .asm to .hack
            char[] hackFileName = new char[asmFileName.Length - 3];
            for (int i = 0; i < asmFileName.Length - 3; i++)
            {
                hackFileName[i] = asmFileName[i];
            }

            string hackFileNameString = new string(hackFileName);
            hackFileNameString = string.Concat(hackFileNameString, "hack"); //.asm is now .hack
            StreamWriter fileOutput = new StreamWriter(hackFileNameString);

            while ((line = fileAgain.ReadLine()) != null)
            { //line by line each loop through
                lineNo = assembler.parseLine(line, lineNo);


              //  fileOutput.Write(line);
              //  Console.WriteLine("line to add to output file: " + line);

            }

  

    
            file.Close();
        }//end of main
    }//end of class Assembler
 }//end of namespace AssemblerLab



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
