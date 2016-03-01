using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblerLab
{
    class CTable
    {
          public static Dictionary<string, int> destTable = new Dictionary<string, int>()
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
            {"SCREEN", 0x4000},
            {"KBD", 0x6000}
        };
        
        public static Dictionary<string, int> compTable = new Disctionary<string, int>() 
        {
            
        if(a=0) // not the right syntax
        {
            {"0", 0101010},
            {"1", 0111111},
            {"-1", 0111010},
            {"D", 0001100},
            {"A", 0110000},
            {"!D", 0001101},
            {"!A", 0110001},
            {"-D", 0001111},
            {"-A", 0110011},
            {"D+1", 0011111},
            {"A+1", 0110111},
            {"D-1", 0001110},
            {"A-1", 0110010},
            {"D+A", 0000010},
            {"D-A", 0010011},
            {"A-D", 0000111},
            {"D&A", 0000000},
            {"D|A", 01010101},
        }
        if(a=1) //this isn't the correct syntax 
        {
            {"M", 1110000},
            {"!M", 1110001},
            {"-M", 1110011},
            {"M+1", 1110111},
            {"M-1", 1110010},
            {"D+M", 1000010},
            {"D-M", 1010011},
            {"M-D", 1000111},
            {"D&M", 1000000},
            {"D|M", 11010101},
        }
        };
        
        public static Dictionary<string, int> jumpTable = new Dictionary<string, int>()
        {
       
            {"null", 000},  //no jump
            {"JGT", 001},   //out > 0
            {"JEQ", 010},   //out = 0
            {"JGE", 011},   //out>=0
            {"JLT", 100},   //out < 0
            {"JNE", 101},   //out != 0
            {"JLE", 110},   //out <= 0
            {"JMP", 111},   //jump
           
        };

        


      

    }
}
