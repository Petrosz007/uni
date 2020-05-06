%option c++ noyywrap yylineno

%{
#include <iostream>
#include <cstdlib>
#include "while.tab.hh"

using namespace yy;
%}
ev    (19|[2-9][0-9])[0-9][0-9]
honap 0[1-9]|1[0-2]
nap   0[1-9]|[12][0-9]|3[01]

%%

program                     return parser::token::PRG;
begin                       return parser::token::BEG;
end                         return parser::token::END;
boolean                     return parser::token::BOO;
natural                     return parser::token::NAT;
date			    		return parser::token::DATE;
read                        return parser::token::REA;
write                       return parser::token::WRI;
getYear			    		return parser::token::GETY;
getMonth		    		return parser::token::GETM;
getDay			    		return parser::token::GETD;
if                          return parser::token::IF;
then                        return parser::token::THE;
else                        return parser::token::ELS;
endif                       return parser::token::EIF;
while                       return parser::token::WHI;
do                          return parser::token::DO;
done                        return parser::token::DON;
repeat			    		return parser::token::REP;
until			    		return parser::token::UNT;
for			    			return parser::token::FOR;
upto			    		return parser::token::UPTO;
downto 			    		return parser::token::DWTO;
from			    		return parser::token::FROM;
true                        return parser::token::TRU;
false                       return parser::token::FAL;
goto			    		return parser::token::GOTO;
":="                        return parser::token::ASN;
"+"                         return parser::token::ADD;
"-"                         return parser::token::SUB;
"*"                         return parser::token::MUL;
"/"                         return parser::token::DIV;
"%"                         return parser::token::MOD;
"<"                         return parser::token::LS;
">"                         return parser::token::GR;
"<="                        return parser::token::LSE;
">="                        return parser::token::GRE;
"="                         return parser::token::EQ;
and                         return parser::token::AND;
or                          return parser::token::OR;
not                         return parser::token::NOT;
"?"			    			return parser::token::QW;
":"			    			return parser::token::CO;
"("                         return parser::token::OP;
")"                         return parser::token::CL;
"["			    			return parser::token::SOP;
"]"			    			return parser::token::SCL;
"||"			    		return parser::token::PAR;
[a-zA-Z_][a-zA-Z0-9_]*      return parser::token::ID;
[0-9]+                      return parser::token::NUM;
{ev}-{honap}-{nap}	    	return parser::token::DATELIT;
[ \t\n]+                    // Whitespaces skipped.
#.*                         // Commens skipped.
.                           { std::cerr << "Line " << lineno() << ": Unexpected character: '" << YYText() << "'." << std::endl; exit(1); }
