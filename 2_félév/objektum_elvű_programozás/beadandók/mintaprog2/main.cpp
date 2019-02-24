//Author:   Gregorics Tibor
//Date:     2018.09.23.
//Title:    Anglers

#include <iostream>
#include "angler.h"

using namespace std;

//Activity:  Looking for a skillful angler who has caught at least two catfish
bool search(const string &name, string &id)
{
    AnglerEnor t(name);
    bool l = false;
    for(t.first(); !l && !t.end(); t.next()){
        l = t.current().skillful;
        id = t.current().id;
    }
    return l;
}

//#define NORMAL_MODE
#ifdef NORMAL_MODE

//Activity:  Looking for a skillful angler who has caught at least two catfish
int main()
{
    string id;
    if(search("input.txt", id)) cout << id << " is a";
    else cout << "there is no";
    cout << " skillful angler.\n";
    return 0;
}

#else
#define CATCH_CONFIG_MAIN
#include "catch.hpp"

// linear search
TEST_CASE("empty file", "t0.txt") {
    string id;
    CHECK_FALSE(search("t0.txt", id));
}

TEST_CASE("1 angler", "t2.txt") {
    string id;
    CHECK(search("t2.txt", id));
}

TEST_CASE("more angler more contests", "t4.txt") {
    string id;
    CHECK(search("t4.txt", id));
}

TEST_CASE("first angler is skillful", "t4.txt") {
    string id;
    CHECK(search("t4.txt", id));
}

TEST_CASE("last angler is skillful", "t5.txt") {
    string id;
    CHECK(search("t5.txt", id));
}

TEST_CASE("one skillful among more anglers more contests ", "t4.txt") {
    string id;
    CHECK(search("t4.txt", id));
}

TEST_CASE("no skillful anglers more contests ", "t6.txt") {
    string id;
    CHECK_FALSE(search("t6.txt", id));
}

TEST_CASE("more skillful anglers more contests ", "t7.txt") {
    string id;
    CHECK(search("t7.txt", id));
}

// optimist linear search

TEST_CASE("no contest", "t1.txt") {
    AnglerEnor t("t1.txt");
    t.first();
    CHECK(t.current().skillful);
}

TEST_CASE("1 angler 1 contest", "t2.txt") {
    AnglerEnor t("t2.txt");
    t.first();
    CHECK(t.current().skillful);
}

TEST_CASE("1 angler more contest", "t3.txt") {
    AnglerEnor t("t3.txt");
    t.first();
    CHECK_FALSE(t.current().skillful);
}

TEST_CASE("1 angler on his first contest did not catch 2 catfish", "t3.txt") {
    AnglerEnor t("t3.txt");
    t.first();
    CHECK_FALSE(t.current().skillful);
}

TEST_CASE("1 angler on his last contest did not catch 2 catfish", "t9.txt") {
    AnglerEnor t("t9.txt");
    t.first();
    CHECK_FALSE(t.current().skillful);
}

TEST_CASE("there is a skillful angler", "t11.txt") {
    AnglerEnor t("t11.txt");
    t.first();
    CHECK(t.current().skillful);
}

TEST_CASE("no skillful angler", "t10.txt") {
    AnglerEnor t("t10.txt");
    t.first();
    CHECK_FALSE(t.current().skillful);
}

TEST_CASE("there are more skillful anglers", "t7.txt") {
    AnglerEnor t("t7.txt");
    t.first();
    CHECK(t.current().skillful);
}

// counting

TEST_CASE("no catch", "t8.txt") {
    ContestEnor tt("t8.txt");
    tt.first();
    CHECK(tt.current().counter == 0);
}

TEST_CASE("1 catch", "t3.txt") {
    ContestEnor tt("t3.txt");
    tt.first();
    CHECK(tt.current().counter == 1);
}

TEST_CASE("more catches", "t2.txt") {
    ContestEnor tt("t2.txt");
    tt.first();
    CHECK(tt.current().counter == 2);
}

TEST_CASE("first catch is catfish", "t2.txt") {
    ContestEnor tt("t2.txt");
    tt.first();
    CHECK(tt.current().counter == 2);
}

TEST_CASE("last catch is catfish", "t2.txt") {
    ContestEnor tt("t2.txt");
    tt.first();
    CHECK(tt.current().counter == 2);
}

TEST_CASE("no catfish", "t13.txt") {
    ContestEnor tt("t13.txt");
    tt.first();
    CHECK(tt.current().counter == 0);
}

TEST_CASE("1 catfish", "t12.txt") {
    ContestEnor tt("t12.txt");
    tt.first();
    CHECK(tt.current().counter == 1);
}

TEST_CASE("more catfishes", "t2.txt") {
    ContestEnor tt("t2.txt");
    tt.first();
    CHECK(tt.current().counter == 2);
}
#endif
