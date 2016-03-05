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
      //  const string errorFileName = "AssemblerError.txt";
      //  StreamWriter errorFile = new StreamWriter(errorFileName);
   

        public Assembler()
        {

        }

        enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND };

        //GLOBAL VARIABLES
        string line;
        string symbol;
        string dest;
        string comp;
        string jump;
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
            line.Replace(" ", string.Empty);
            Regex rgx = new Regex("^[a-zA-Z0-9]*$@()"); //should @ be in here?

            //create char array to fill with 
            char[] parsedLine = new char[line.Length];
            int j = 0; //line counter for parsedLine

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
              //  else if (line[i] == '\n')
              //  {
              //      keepGoing = false;
             // }
                else if (char.IsLetterOrDigit(line[i]) || line[i] == '@' || line[i] == '(' || line[i] == ')' || line[i] == '_' || line[i] == '-' || line[i] == '$' || line[i] == '+' || line[i] == ';' || line[i] == '*' || line[i] == '/' || line[i] == '=')
                {
                    parsedLine[j] = line[i];
                    j++; //only increment j if [a-zA-Z0-9]*$ has been found in line[i]
                }
                else
                {
                    Console.WriteLine("ERROR: cannot parse line : " + line); //error checking
                    keepGoing = false;
                }

            }


            if (keepGoing == true && secondTimeThrough == false && parsedLine.Length != 0) //if parsedLine.Length == 0 means it's an empty array! Just skip this line.
            {
                Console.Write("after parsing... : ");
                for (int i = 0; i < parsedLine.Length; i++)
                {
                    Console.Write(parsedLine[i]);// TODO CHECK WHAT PARSEDLINE IS
                }
                Console.WriteLine();



                lineNo = assembler.pass1(parsedLine, lineNo);
            }

         //    if(keepGoing == true && secondTimeThrough == true)
        //     {
        //        Console.WriteLine("GOT INTO PASS2");
       //          assembler.pass2(parsedLine);
        //      }

            return lineNo;

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
            while ((line = fileAgain.ReadLine()) != null)
            { //line by line each loop through
                lineNo = assembler.parseLine(line, lineNo);
            }
            file.Close();
      
        }//end of main



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

                Console.Read();
            }
            else
            {

            }

            return lineNo;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS2 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void pass2(char [] parsedLine)
        { //takes out white space & comments
            //purpose to convert line (parsed line?) to binary and write each line to the outfile
            //A INSTRUCTION
            if (parsedLine[0] == '@')
            {
                string parsedString = new string(parsedLine);
                int startIndex = parsedLine[0];
                int endIndex = parsedLine.Length;
                string aInstruc = parsedString.Substring(1, parsedString.Length - 1);

                Console.WriteLine("A instruction found : " + aInstruc);
                //check if it is in the SymbolTable.symbolTable
               // string myValue;
              //  if (SymbolTable.symbolTable.TryGetValue(aInstruc, out myValue)) 
            //    {

             //   }
                
            }//end of if

            //C INSTRUCTION



            //L () INSTRUCTION
            if (parsedLine[0] == '(')
            {
                int startIndex = 0;
                int endIndex = parsedLine.Length;
                string parsedString = new string(parsedLine);
                string newSymbol = parsedString.Substring(startIndex + 1, endIndex - 2);

                //find in dictionary

            }


            //open outfile 
            //write newly formed bytecode to outputfile


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
