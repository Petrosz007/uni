#ifndef IMPLEMENTATION_HH
#define IMPLEMENTATION_HH

#include <map>
#include <string>

void semantic_error(int line, std::string text);

enum type {boolean, natural, date_type};

extern std::map<std::string, type> symbol_table;

void declare(int line, std::string name, type typ);
type get_type(int line, std::string name);
void check_assignment(int line, std::string left, type right);
void check_condition(int line, type typ);
void check_operation1(int line, std::string op, type expected, type actual);
void check_operation2(int line, std::string op, type left_expected, type right_expected, type left_actual, type right_actual);

#endif // IMPLEMENTATION_HH

