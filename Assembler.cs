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
            //no need of actual constructor
        }

        //GLOBAL VARIABLES
        static string line;
        static string symbol;
        static string dest;
        static string comp;
        static string jump;
        bool keepGoing;
        bool secondTimeThrough = false;
        static int ramAddress = 15;
        static int romAddress = 0;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~isValidSymbol FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool isValidSymbol(string str, StreamWriter logOutput) //$_:.
        {
            bool valid = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLetterOrDigit(str[i]) || str[i] == '@' || str[i] == '.' || str[i] == '(' || str[i] == ')' || str[i] == '_' || str[i] == '-' || str[i] == '$' || str[i] == '+' || str[i] == ';' || str[i] == '*' || str[i] == '/' || str[i] == '='|| str[i] == '!' || str[i] == '|' || str[i] == '&')
                {
                    valid = true;
                }
            }
            if (char.IsDigit(str[0]))
            {
                valid = false;
            }
            Console.WriteLine("isValidSymbol valid : " + valid);
            logOutput.WriteLine("isValidSymbol valid : " + valid);
            return valid;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PARSE FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void parseLine(string line, StreamWriter fileOutput, StreamWriter logOutput)
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
                if (result[i] == '/' && result[i + 1] == '/')
                {
                    //comment has been found! Don't copy any of the rest of the line
                    keepGoing = false; //set keepGoing to false and will fall out of for loop
                }
                else if (result[i] == '\n')
                {
                    keepGoing = false;
                }
                else if (char.IsLetterOrDigit(result[i]) || result[i] == '@' || result[i] == '.' || result[i] == '(' || result[i] == ')' || result[i] == '_' || result[i] == '-' || result[i] == '$' || result[i] == '+' || result[i] == ';' || result[i] == '*' || result[i] == '/' || result[i] == '=' || result[i] == '!' || result[i] == '|' || result[i] == '&')
                {
                    parsedLine[j] = result[i];
                    j++; //only increment j if [a-zA-Z0-9]*$ has been found in line[i]
                }
                else
                {
                    Console.WriteLine("ERROR: cannot parse line : " + result); //error checking
                    logOutput.WriteLine("ERROR: cannot parse line : " + result); 
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

            // START PASS 1
            if (secondTimeThrough == false && newResult.Length != 0) //if parsedLine.Length == 0 means it's an empty array! Just skip this line.
            {
                assembler.pass1(newResult, logOutput);
            }

            //START PASS 2
            if (secondTimeThrough == true && newResult.Length != 0)
            {
                assembler.pass2(newResult, fileOutput, logOutput);
            }

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS1 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void pass1(char[] parsedLine, StreamWriter logOutput)
        { 
            //purpose:  to add new (SYMBOLS) to the SymbolTable     
            if (parsedLine[0] == '(')
            {
                int startIndex = 0;
                int endIndex = parsedLine.Length;
                string parsedString = new string(parsedLine);
                string newSymbol = parsedString.Substring(startIndex + 1, endIndex - 2);
                //add to dictionary
                SymbolTable.symbolTable.Add(newSymbol, romAddress);

                Console.WriteLine("The symbol that was added was  : " + newSymbol + " , " + romAddress);
                logOutput.WriteLine("The symbol that was added was  : " + newSymbol + " , " + romAddress);
            }
            else
            {
                romAddress++;
            }
          
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PASS2 FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        void pass2(char[] parsedLine, StreamWriter fileOutput, StreamWriter logOutput)
        { //takes out white space & comments
          //purpose to convert line (parsed line?) to binary and write each line to the outfile
           
            string finalInstruction = "0";
            string parsedLineString = new string(parsedLine);

            for (int i = 0; i < parsedLine.Length; i++)
            {
                Console.Write(parsedLine[i]);// TODO CHECK WHAT PARSEDLINE IS
            }
            Console.WriteLine();

            string parsedString = new string(parsedLine);

            //A INSTRUCTION
            if (parsedLine[0] == '@')
            {       
                int startIndex = parsedLine[0];
                int endIndex = parsedLine.Length;
                string aInstruc = parsedString.Substring(1, parsedString.Length - 1);

                Console.WriteLine("A instruction found : " + aInstruc);
                logOutput.WriteLine("A instruction found : " + aInstruc);

                //check if it is in the SymbolTable.symbolTable
                if (SymbolTable.symbolTable.ContainsKey(aInstruc))
                {
                    int aValue = SymbolTable.symbolTable[aInstruc];
                    var aBinary = Convert.ToString(aValue, 2);

                    while (aBinary.Length < 16)
                    {
                        aBinary = string.Concat("0", aBinary);
                    }
                    finalInstruction = aBinary;
                }
                else if (!SymbolTable.symbolTable.ContainsKey(aInstruc) && isValidSymbol(aInstruc, logOutput) == true) //new variable found @newvariable add to symboltable
                {
                    ramAddress++;
                    SymbolTable.symbolTable.Add(aInstruc, ramAddress);
                    var aBinary = Convert.ToString(ramAddress, 2);
                    while (aBinary.Length < 16)
                    {
                        aBinary = string.Concat("0", aBinary);
                    }
                    finalInstruction = aBinary;

                }
                else
                {
                    int aInstrucInt;
                    Int32.TryParse(aInstruc, out aInstrucInt);
                    var aBinary = Convert.ToString(aInstrucInt, 2);
                    while (aBinary.Length < 16)
                    {
                        aBinary = string.Concat("0", aBinary);
                    }
                    finalInstruction = aBinary;
                }
            }//end of if

            //L () INSTRUCTION
            else if (parsedLine[0] == '(') //if L instruction just act like A instruc
            {
                int startIndex = 0;
                int endIndex = parsedLine.Length;
                string lInstruc = parsedString.Substring(startIndex + 1, endIndex - 2);
                //find in dictionary
             
                if (SymbolTable.symbolTable.ContainsKey(lInstruc))
                {
                    int lValue = SymbolTable.symbolTable[lInstruc];
                    var lBinary = Convert.ToString(lValue, 2);

                    while (lBinary.Length < 16)
                    {
                        lBinary = string.Concat("0", lBinary);
                    }
                    finalInstruction = "lInstruction";
                }

                else
                {
                    int lInstrucInt;
                    Int32.TryParse(lInstruc, out lInstrucInt);
                    var lBinary = Convert.ToString(lInstrucInt, 2);
                    while (lBinary.Length < 16)
                    {
                        lBinary = string.Concat("0", lBinary);
                    }
                    finalInstruction = lBinary;
                }
            }//end of else if L instruction


            //C INSTRUCTION
            else {
               
                dest = "";
                jump = "null";
                comp = "0";
                string[] splitted = parsedString.Split('=');
                Console.WriteLine("SPLITTED [0]: " + splitted[0]);
                if(splitted.Length == 2)
                {
                    dest = splitted[0];
                    Console.WriteLine("dest : " + splitted[0]);
                    string[] splittedWDest = splitted[1].Split(';');
                    if(splittedWDest.Length == 2) //means we have had dest = comp ; jump
                    {
                        comp = splittedWDest[0];
                        jump = splittedWDest[1];
                        Console.WriteLine("comp : " + splittedWDest[0]);
                        Console.WriteLine("jump : " + splittedWDest[1]);
                    }
                    else
                    {
                        comp = splittedWDest[0];
                        Console.WriteLine("comp : " + splittedWDest[0]);
                    }
                }
                else //no dest possible jump
                {
                 
                    comp = splitted[0];
                    Console.WriteLine("comp : " + splitted[0]);
                    string[] splittedWComp = splitted[0].Split(';');
                    if (splittedWComp.Length == 2) //means we have had dest = comp ; jump
                    {
                        comp = splittedWComp[0];
                        jump = splittedWComp[1];
                        Console.WriteLine("comp : " + splittedWComp[0]);
                        Console.WriteLine("jump : " + splittedWComp[1]);
                    }
                }

                string destValue = "000";
                string compValue = "101010";
                string jumpValue = "000";

                //dest comp and jump should be set now
                //find dest comp and jump in dictionary
                if (CTable.destTable.ContainsKey(dest))
                {
                    destValue = CTable.destTable[dest];
                    Console.WriteLine("dest value is : " + destValue);
                    logOutput.WriteLine("dest value is : " + destValue);
                }
                if (CTable.compTable.ContainsKey(comp))
                {
                    compValue = CTable.compTable[comp];
                    Console.WriteLine("comp value is : " + compValue);
                    logOutput.WriteLine("comp value is : " + compValue);
                }
                if (CTable.jumpTable.ContainsKey(jump))
                {
                    jumpValue = CTable.jumpTable[jump];
                    Console.WriteLine("jump value is : " + jumpValue);
                    logOutput.WriteLine("jump value is : " + jumpValue);
                }


                finalInstruction = string.Concat("111", compValue);
                finalInstruction = string.Concat(finalInstruction, destValue);
                finalInstruction = string.Concat(finalInstruction, jumpValue);
              


            } //end of else c instruction

            if(finalInstruction == "0")
            {
                Console.WriteLine("ERROR : final instruction could not be filled with proper hack code...");
                logOutput.WriteLine("ERROR : final instruction could not be filled with proper hack code...");
            }
            else if (finalInstruction == "lInstruction")
            {
                Console.WriteLine("This line was an lInstruction");
                logOutput.WriteLine("This line was an lInstruction");
            }
            else
            {
                Console.WriteLine("finalInstruction : " + finalInstruction);
                logOutput.WriteLine("finalInstruction : " + finalInstruction);
                fileOutput.WriteLine(finalInstruction);
            }

            //open outfile 
            //write newly formed bytecode to outputfile
             

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MAIN FUNCTION~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public static void Main(string[] args)
        {
            Assembler assembler = new Assembler();
            Console.WriteLine("Enter in the .asm file you wish to convert to .hack : ");
            string asmFileName = Console.ReadLine();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(asmFileName);

            //change .asm to .hack
            char[] hackFileName = new char[asmFileName.Length - 3];
            for (int i = 0; i < asmFileName.Length - 3; i++)
            {

                hackFileName[i] = asmFileName[i];
            }

            string hackFileNameString = new string(hackFileName);
            string logFileNameString = new string(hackFileName);
            logFileNameString = string.Concat(hackFileNameString, "log"); //making a .log to fill with same as what we Console.WriteLine();
            hackFileNameString = string.Concat(hackFileNameString, "hack"); //.asm is now .hack
            System.IO.StreamWriter fileOutput = new System.IO.StreamWriter(hackFileNameString);
            System.IO.StreamWriter logOutput = new System.IO.StreamWriter(logFileNameString);

            while ((line = file.ReadLine()) != null)
            { //line by line each loop through
                assembler.parseLine(line, fileOutput, logOutput);
            }

            assembler.secondTimeThrough = true;
            romAddress = 0;
            ramAddress = 15;
            Console.Read();
            StreamReader fileAgain = new StreamReader(asmFileName);

            while ((line = fileAgain.ReadLine()) != null)
            { //line by line each loop through
                assembler.parseLine(line, fileOutput, logOutput);
            }

            file.Close();
            fileOutput.Close();
            logOutput.Close(); //this is a text file to store messages and any error messages 
        }//end of main
    }//end of class Assembler
 }//end of namespace AssemblerLab

