%language "c++"
%locations
%define api.value.type variant

%code requires {
#include "implementation.hh"
}

%code provides {
int yylex(yy::parser::semantic_type* yylval, yy::parser::location_type* yylloc);
}

%token PRG
%token BEG
%token END
%token BOO
%token NAT
%token DATE
%token REA
%token WRI
%token IF
%token THE
%token ELS
%token EIF
%token WHI
%token DO
%token DON
%token REP
%token UNT
%token FOR
%token UPTO
%token DWTO
%token FROM
%token TRU
%token FAL
%token ASN
%token OP
%token CL
%token SOP
%token SCL
%token PAR
%token <std::string> ID
%token NUM
%token DATELIT
%token GETY
%token GETM
%token GETD
%token GOTO



%left QW CO
%left OR
%left AND
%left EQ
%left LS GR LSE GRE
%left ADD SUB
%left MUL DIV MOD
%precedence NOT

%type <type> expression

%%

start:
    PRG ID declarations BEG commands END
;

declarations:
    // empty
|
    declarations declaration
;

declaration:
    BOO ID
    {
        declare(@1.begin.line, $2, boolean);
    }
|
    NAT ID
    {
        declare(@1.begin.line, $2, natural);
    }
|
    DATE ID
    {
		declare(@1.begin.line, $2, date_type);
    }

;

commands:
    // empty
|
    commands command
;

command:
    REA OP ID CL
    {
        get_type(@3.begin.line, $3);
    }
|
    WRI OP expression CL
|
    ID ASN expression
    {

		check_assignment(@1.begin.line, $1, $3);
        
    }
|
    IF expression THE commands EIF
    {
        check_condition(@1.begin.line, $2);
    }
|
    IF expression THE commands ELS commands EIF
    {
        check_condition(@1.begin.line, $2);
    }
|
    WHI expression DO commands DON
    {
        check_condition(@1.begin.line, $2);
    }
;

expression:
    NUM
    {
        $$ = natural;
    }
|
    TRU
    {
        $$ = boolean;
    }
|
    FAL
    {
        $$ = boolean;
    }
|
    DATELIT
    {
		$$ = date_type;
    }
|
    GETY OP expression CL
    {
		if($3 == date_type) {
			$$ = natural;
		} else {
			semantic_error(@1.begin.line, "getYear expect date type");
		}
    }

|
    ID
    {
        $$ = get_type(@1.begin.line, $1);
    }
|
    expression ADD expression
    {
		if($1 == natural && $3 == date_type || $1 == date_type && $3 == natural) {
			$$ = date_type;
		} else {
		    check_operation2(@2.begin.line, "+", natural, natural, $1, $3);
			$$ = natural;
		}

    }
|
    expression SUB expression
    {
        check_operation2(@2.begin.line, "-", natural, natural, $1, $3);
        $$ = natural;
    }
|
    expression MUL expression
    {
        check_operation2(@2.begin.line, "*", natural, natural, $1, $3);
        $$ = natural;
    }
|
    expression DIV expression
    {
        check_operation2(@2.begin.line, "/", natural, natural, $1, $3);
        $$ = natural;
    }
|
    expression MOD expression
    {
        check_operation2(@2.begin.line, "%", natural, natural, $1, $3);
        $$ = natural;
    }
|
    expression LS expression
    {
        check_operation2(@2.begin.line, "<", natural, natural, $1, $3);
        $$ = boolean;
    }
|
    expression GR expression
    {
        check_operation2(@2.begin.line, ">", natural, natural, $1, $3);
        $$ = boolean;
    }
|
    expression LSE expression
    {
        check_operation2(@2.begin.line, "<=", natural, natural, $1, $3);
        $$ = boolean;
    }
|
    expression GRE expression
    {
        check_operation2(@2.begin.line, ">=", natural, natural, $1, $3);
        $$ = boolean;
    }
|
    expression AND expression
    {
        check_operation2(@2.begin.line, "and", boolean, boolean, $1, $3);
        $$ = boolean;
    }
|
    expression OR expression
    {
        check_operation2(@2.begin.line, "or", boolean, boolean, $1, $3);
        $$ = boolean;
    }
|
    expression EQ expression
    {
        check_operation2(@2.begin.line, "=", $1, $1, $1, $3);
        $$ = boolean;
    }
|
    NOT expression
    {
        check_operation1(@2.begin.line, "not", boolean, $2);
        $$ = boolean;
    }
|
    OP expression CL
    {
        $$ = $2;
    }
;

%%
