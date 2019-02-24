//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000

#pragma once

#include <string>

// structure of a book
struct Book{
    int id;
    std::string author;
    std::string title;
    std::string publisher;
    std::string year;
    int nc;
    std::string isbn;
};

