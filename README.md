# NAND2Tetris

IN MAIN 
enum Parser_Command{ All the parsers can be passed into here };
while ((line = file.ReadLine()) != null)) //grabs a line
  
    increment lineNum
    call ParseLine(); 
    begind swtich statement that will determine what to do 

GLOBALS 

int lineNo;
string line;
string symbol;
string dest, comp, jmp; 
commandType = Parser.CommandType();


create a function for FIRSTPASS //get rid of white space and build symbol table (we made this with r0-r15) dest, cmp and comp, go in a c-table class with all the c commands, dest table with all restinations, and jump table with all jump commands


create function for SECONDPASS 
make a switch statement that deals with the A, C, L and no commands 

Ask user for input file name 
	take input file, open it 
		if (InFile) //if file opens 
			{
				Begin ParseLine Method
				
			}

ParseLine Methods
	WhiteSpace Eliminator 
	Comment Eliminator 
	
Case statements 
  A-Instruc
    determine is A a # or a symb
    
  C-Instruc
     determind dest=comp;jmp
     
  L-Instruc
    check for parin
  
  Parser_NO_Command 
  
  
  parseLine()
  {
  set dest comp jmp and  symb to NULL 
  commandType = Parser.CommandType.Parser_NO_COMMAND // this give the type back to the global and is used for the switch statement 


