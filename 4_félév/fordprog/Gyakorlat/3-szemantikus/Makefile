while: lex.yy.cc while.cc while.tab.cc while.tab.hh implementation.hh implementation.cc
	g++ lex.yy.cc while.cc while.tab.cc implementation.cc -o while

lex.yy.cc: while.l
	flex while.l

while.tab.cc while.tab.hh: while.y
	bison -d while.y

.PHONY: clean
clean:
	rm -f lex.yy.cc while.tab.cc while.tab.hh location.hh position.hh stack.hh while 
