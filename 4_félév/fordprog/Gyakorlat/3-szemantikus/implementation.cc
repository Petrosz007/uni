#include "implementation.hh"
#include "while.tab.hh"
#include <iostream>

void semantic_error(int line, std::string text) {
    std::cerr << "Line " << line << ": Error: " << text << std::endl;
    exit(1);
}

std::map<std::string, type> symbol_table;

void declare(int line, std::string name, type typ) {
    if(symbol_table.find(name) == symbol_table.end()) {
        symbol_table[name] = typ;
    } else {
        semantic_error(line, "Re-declared variable: '" + name + "'.");
    }
}

type get_type(int line, std::string name) {
    if(symbol_table.find(name) != symbol_table.end()) {
        return symbol_table[name];
    } else {
        semantic_error(line, "Undeclared variable: '" + name + "'.");
    }
}

void check_assignment(int line, std::string left_name, type right) {
    type left = get_type(line, left_name);
    if(left != right) {
        semantic_error(line, "Type error in assignment.");
    }
}

void check_condition(int line, type typ) {
    if(typ != boolean) {
        semantic_error(line, "Condition must be boolean.");
    }
}

void check_operation1(int line, std::string op, type expected, type actual) {
    if(expected != actual) {
        semantic_error(line, "Type error in argument of '" + op + "'.");
    }
}

void check_operation2(int line, std::string op, type left_expected, type right_expected, type left_actual, type right_actual) {
    if(left_expected != left_actual) {
        semantic_error(line, "Type error in left argument of '" + op + "'.");
    }
    if(right_expected != right_actual) {
        semantic_error(line, "Type error in right argument of '" + op + "'.");
    }
}
