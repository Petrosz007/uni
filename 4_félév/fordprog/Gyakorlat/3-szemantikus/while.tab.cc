// A Bison parser, made by GNU Bison 3.0.4.

// Skeleton implementation for Bison LALR(1) parsers in C++

// Copyright (C) 2002-2015 Free Software Foundation, Inc.

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// As a special exception, you may create a larger work that contains
// part or all of the Bison parser skeleton and distribute that work
// under terms of your choice, so long as that work isn't itself a
// parser generator using the skeleton or a modified version thereof
// as a parser skeleton.  Alternatively, if you modify or redistribute
// the parser skeleton itself, you may (at your option) remove this
// special exception, which will cause the skeleton and the resulting
// Bison output files to be licensed under the GNU General Public
// License without this special exception.

// This special exception was added by the Free Software Foundation in
// version 2.2 of Bison.


// First part of user declarations.

#line 37 "while.tab.cc" // lalr1.cc:404

# ifndef YY_NULLPTR
#  if defined __cplusplus && 201103L <= __cplusplus
#   define YY_NULLPTR nullptr
#  else
#   define YY_NULLPTR 0
#  endif
# endif

#include "while.tab.hh"

// User implementation prologue.

#line 51 "while.tab.cc" // lalr1.cc:412


#ifndef YY_
# if defined YYENABLE_NLS && YYENABLE_NLS
#  if ENABLE_NLS
#   include <libintl.h> // FIXME: INFRINGES ON USER NAME SPACE.
#   define YY_(msgid) dgettext ("bison-runtime", msgid)
#  endif
# endif
# ifndef YY_
#  define YY_(msgid) msgid
# endif
#endif

#define YYRHSLOC(Rhs, K) ((Rhs)[K].location)
/* YYLLOC_DEFAULT -- Set CURRENT to span from RHS[1] to RHS[N].
   If N is 0, then set CURRENT to the empty location which ends
   the previous symbol: RHS[0] (always defined).  */

# ifndef YYLLOC_DEFAULT
#  define YYLLOC_DEFAULT(Current, Rhs, N)                               \
    do                                                                  \
      if (N)                                                            \
        {                                                               \
          (Current).begin  = YYRHSLOC (Rhs, 1).begin;                   \
          (Current).end    = YYRHSLOC (Rhs, N).end;                     \
        }                                                               \
      else                                                              \
        {                                                               \
          (Current).begin = (Current).end = YYRHSLOC (Rhs, 0).end;      \
        }                                                               \
    while (/*CONSTCOND*/ false)
# endif


// Suppress unused-variable warnings by "using" E.
#define YYUSE(E) ((void) (E))

// Enable debugging if requested.
#if YYDEBUG

// A pseudo ostream that takes yydebug_ into account.
# define YYCDEBUG if (yydebug_) (*yycdebug_)

# define YY_SYMBOL_PRINT(Title, Symbol)         \
  do {                                          \
    if (yydebug_)                               \
    {                                           \
      *yycdebug_ << Title << ' ';               \
      yy_print_ (*yycdebug_, Symbol);           \
      *yycdebug_ << std::endl;                  \
    }                                           \
  } while (false)

# define YY_REDUCE_PRINT(Rule)          \
  do {                                  \
    if (yydebug_)                       \
      yy_reduce_print_ (Rule);          \
  } while (false)

# define YY_STACK_PRINT()               \
  do {                                  \
    if (yydebug_)                       \
      yystack_print_ ();                \
  } while (false)

#else // !YYDEBUG

# define YYCDEBUG if (false) std::cerr
# define YY_SYMBOL_PRINT(Title, Symbol)  YYUSE(Symbol)
# define YY_REDUCE_PRINT(Rule)           static_cast<void>(0)
# define YY_STACK_PRINT()                static_cast<void>(0)

#endif // !YYDEBUG

#define yyerrok         (yyerrstatus_ = 0)
#define yyclearin       (yyla.clear ())

#define YYACCEPT        goto yyacceptlab
#define YYABORT         goto yyabortlab
#define YYERROR         goto yyerrorlab
#define YYRECOVERING()  (!!yyerrstatus_)


namespace yy {
#line 137 "while.tab.cc" // lalr1.cc:479

  /// Build a parser object.
  parser::parser ()
#if YYDEBUG
     :yydebug_ (false),
      yycdebug_ (&std::cerr)
#endif
  {}

  parser::~parser ()
  {}


  /*---------------.
  | Symbol types.  |
  `---------------*/

  inline
  parser::syntax_error::syntax_error (const location_type& l, const std::string& m)
    : std::runtime_error (m)
    , location (l)
  {}

  // basic_symbol.
  template <typename Base>
  inline
  parser::basic_symbol<Base>::basic_symbol ()
    : value ()
  {}

  template <typename Base>
  inline
  parser::basic_symbol<Base>::basic_symbol (const basic_symbol& other)
    : Base (other)
    , value ()
    , location (other.location)
  {
      switch (other.type_get ())
    {
      case 32: // ID
        value.copy< std::string > (other.value);
        break;

      case 60: // expression
        value.copy< type > (other.value);
        break;

      default:
        break;
    }

  }


  template <typename Base>
  inline
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const semantic_type& v, const location_type& l)
    : Base (t)
    , value ()
    , location (l)
  {
    (void) v;
      switch (this->type_get ())
    {
      case 32: // ID
        value.copy< std::string > (v);
        break;

      case 60: // expression
        value.copy< type > (v);
        break;

      default:
        break;
    }
}


  // Implementation of basic_symbol constructor for each type.

  template <typename Base>
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const location_type& l)
    : Base (t)
    , value ()
    , location (l)
  {}

  template <typename Base>
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const std::string v, const location_type& l)
    : Base (t)
    , value (v)
    , location (l)
  {}

  template <typename Base>
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const type v, const location_type& l)
    : Base (t)
    , value (v)
    , location (l)
  {}


  template <typename Base>
  inline
  parser::basic_symbol<Base>::~basic_symbol ()
  {
    clear ();
  }

  template <typename Base>
  inline
  void
  parser::basic_symbol<Base>::clear ()
  {
    // User destructor.
    symbol_number_type yytype = this->type_get ();
    basic_symbol<Base>& yysym = *this;
    (void) yysym;
    switch (yytype)
    {
   default:
      break;
    }

    // Type destructor.
    switch (yytype)
    {
      case 32: // ID
        value.template destroy< std::string > ();
        break;

      case 60: // expression
        value.template destroy< type > ();
        break;

      default:
        break;
    }

    Base::clear ();
  }

  template <typename Base>
  inline
  bool
  parser::basic_symbol<Base>::empty () const
  {
    return Base::type_get () == empty_symbol;
  }

  template <typename Base>
  inline
  void
  parser::basic_symbol<Base>::move (basic_symbol& s)
  {
    super_type::move(s);
      switch (this->type_get ())
    {
      case 32: // ID
        value.move< std::string > (s.value);
        break;

      case 60: // expression
        value.move< type > (s.value);
        break;

      default:
        break;
    }

    location = s.location;
  }

  // by_type.
  inline
  parser::by_type::by_type ()
    : type (empty_symbol)
  {}

  inline
  parser::by_type::by_type (const by_type& other)
    : type (other.type)
  {}

  inline
  parser::by_type::by_type (token_type t)
    : type (yytranslate_ (t))
  {}

  inline
  void
  parser::by_type::clear ()
  {
    type = empty_symbol;
  }

  inline
  void
  parser::by_type::move (by_type& that)
  {
    type = that.type;
    that.clear ();
  }

  inline
  int
  parser::by_type::type_get () const
  {
    return type;
  }
  // Implementation of make_symbol for each symbol type.
  parser::symbol_type
  parser::make_PRG (const location_type& l)
  {
    return symbol_type (token::PRG, l);
  }

  parser::symbol_type
  parser::make_BEG (const location_type& l)
  {
    return symbol_type (token::BEG, l);
  }

  parser::symbol_type
  parser::make_END (const location_type& l)
  {
    return symbol_type (token::END, l);
  }

  parser::symbol_type
  parser::make_BOO (const location_type& l)
  {
    return symbol_type (token::BOO, l);
  }

  parser::symbol_type
  parser::make_NAT (const location_type& l)
  {
    return symbol_type (token::NAT, l);
  }

  parser::symbol_type
  parser::make_DATE (const location_type& l)
  {
    return symbol_type (token::DATE, l);
  }

  parser::symbol_type
  parser::make_REA (const location_type& l)
  {
    return symbol_type (token::REA, l);
  }

  parser::symbol_type
  parser::make_WRI (const location_type& l)
  {
    return symbol_type (token::WRI, l);
  }

  parser::symbol_type
  parser::make_IF (const location_type& l)
  {
    return symbol_type (token::IF, l);
  }

  parser::symbol_type
  parser::make_THE (const location_type& l)
  {
    return symbol_type (token::THE, l);
  }

  parser::symbol_type
  parser::make_ELS (const location_type& l)
  {
    return symbol_type (token::ELS, l);
  }

  parser::symbol_type
  parser::make_EIF (const location_type& l)
  {
    return symbol_type (token::EIF, l);
  }

  parser::symbol_type
  parser::make_WHI (const location_type& l)
  {
    return symbol_type (token::WHI, l);
  }

  parser::symbol_type
  parser::make_DO (const location_type& l)
  {
    return symbol_type (token::DO, l);
  }

  parser::symbol_type
  parser::make_DON (const location_type& l)
  {
    return symbol_type (token::DON, l);
  }

  parser::symbol_type
  parser::make_REP (const location_type& l)
  {
    return symbol_type (token::REP, l);
  }

  parser::symbol_type
  parser::make_UNT (const location_type& l)
  {
    return symbol_type (token::UNT, l);
  }

  parser::symbol_type
  parser::make_FOR (const location_type& l)
  {
    return symbol_type (token::FOR, l);
  }

  parser::symbol_type
  parser::make_UPTO (const location_type& l)
  {
    return symbol_type (token::UPTO, l);
  }

  parser::symbol_type
  parser::make_DWTO (const location_type& l)
  {
    return symbol_type (token::DWTO, l);
  }

  parser::symbol_type
  parser::make_FROM (const location_type& l)
  {
    return symbol_type (token::FROM, l);
  }

  parser::symbol_type
  parser::make_TRU (const location_type& l)
  {
    return symbol_type (token::TRU, l);
  }

  parser::symbol_type
  parser::make_FAL (const location_type& l)
  {
    return symbol_type (token::FAL, l);
  }

  parser::symbol_type
  parser::make_ASN (const location_type& l)
  {
    return symbol_type (token::ASN, l);
  }

  parser::symbol_type
  parser::make_OP (const location_type& l)
  {
    return symbol_type (token::OP, l);
  }

  parser::symbol_type
  parser::make_CL (const location_type& l)
  {
    return symbol_type (token::CL, l);
  }

  parser::symbol_type
  parser::make_SOP (const location_type& l)
  {
    return symbol_type (token::SOP, l);
  }

  parser::symbol_type
  parser::make_SCL (const location_type& l)
  {
    return symbol_type (token::SCL, l);
  }

  parser::symbol_type
  parser::make_PAR (const location_type& l)
  {
    return symbol_type (token::PAR, l);
  }

  parser::symbol_type
  parser::make_ID (const std::string& v, const location_type& l)
  {
    return symbol_type (token::ID, v, l);
  }

  parser::symbol_type
  parser::make_NUM (const location_type& l)
  {
    return symbol_type (token::NUM, l);
  }

  parser::symbol_type
  parser::make_DATELIT (const location_type& l)
  {
    return symbol_type (token::DATELIT, l);
  }

  parser::symbol_type
  parser::make_GETY (const location_type& l)
  {
    return symbol_type (token::GETY, l);
  }

  parser::symbol_type
  parser::make_GETM (const location_type& l)
  {
    return symbol_type (token::GETM, l);
  }

  parser::symbol_type
  parser::make_GETD (const location_type& l)
  {
    return symbol_type (token::GETD, l);
  }

  parser::symbol_type
  parser::make_GOTO (const location_type& l)
  {
    return symbol_type (token::GOTO, l);
  }

  parser::symbol_type
  parser::make_QW (const location_type& l)
  {
    return symbol_type (token::QW, l);
  }

  parser::symbol_type
  parser::make_CO (const location_type& l)
  {
    return symbol_type (token::CO, l);
  }

  parser::symbol_type
  parser::make_OR (const location_type& l)
  {
    return symbol_type (token::OR, l);
  }

  parser::symbol_type
  parser::make_AND (const location_type& l)
  {
    return symbol_type (token::AND, l);
  }

  parser::symbol_type
  parser::make_EQ (const location_type& l)
  {
    return symbol_type (token::EQ, l);
  }

  parser::symbol_type
  parser::make_LS (const location_type& l)
  {
    return symbol_type (token::LS, l);
  }

  parser::symbol_type
  parser::make_GR (const location_type& l)
  {
    return symbol_type (token::GR, l);
  }

  parser::symbol_type
  parser::make_LSE (const location_type& l)
  {
    return symbol_type (token::LSE, l);
  }

  parser::symbol_type
  parser::make_GRE (const location_type& l)
  {
    return symbol_type (token::GRE, l);
  }

  parser::symbol_type
  parser::make_ADD (const location_type& l)
  {
    return symbol_type (token::ADD, l);
  }

  parser::symbol_type
  parser::make_SUB (const location_type& l)
  {
    return symbol_type (token::SUB, l);
  }

  parser::symbol_type
  parser::make_MUL (const location_type& l)
  {
    return symbol_type (token::MUL, l);
  }

  parser::symbol_type
  parser::make_DIV (const location_type& l)
  {
    return symbol_type (token::DIV, l);
  }

  parser::symbol_type
  parser::make_MOD (const location_type& l)
  {
    return symbol_type (token::MOD, l);
  }

  parser::symbol_type
  parser::make_NOT (const location_type& l)
  {
    return symbol_type (token::NOT, l);
  }



  // by_state.
  inline
  parser::by_state::by_state ()
    : state (empty_state)
  {}

  inline
  parser::by_state::by_state (const by_state& other)
    : state (other.state)
  {}

  inline
  void
  parser::by_state::clear ()
  {
    state = empty_state;
  }

  inline
  void
  parser::by_state::move (by_state& that)
  {
    state = that.state;
    that.clear ();
  }

  inline
  parser::by_state::by_state (state_type s)
    : state (s)
  {}

  inline
  parser::symbol_number_type
  parser::by_state::type_get () const
  {
    if (state == empty_state)
      return empty_symbol;
    else
      return yystos_[state];
  }

  inline
  parser::stack_symbol_type::stack_symbol_type ()
  {}


  inline
  parser::stack_symbol_type::stack_symbol_type (state_type s, symbol_type& that)
    : super_type (s, that.location)
  {
      switch (that.type_get ())
    {
      case 32: // ID
        value.move< std::string > (that.value);
        break;

      case 60: // expression
        value.move< type > (that.value);
        break;

      default:
        break;
    }

    // that is emptied.
    that.type = empty_symbol;
  }

  inline
  parser::stack_symbol_type&
  parser::stack_symbol_type::operator= (const stack_symbol_type& that)
  {
    state = that.state;
      switch (that.type_get ())
    {
      case 32: // ID
        value.copy< std::string > (that.value);
        break;

      case 60: // expression
        value.copy< type > (that.value);
        break;

      default:
        break;
    }

    location = that.location;
    return *this;
  }


  template <typename Base>
  inline
  void
  parser::yy_destroy_ (const char* yymsg, basic_symbol<Base>& yysym) const
  {
    if (yymsg)
      YY_SYMBOL_PRINT (yymsg, yysym);
  }

#if YYDEBUG
  template <typename Base>
  void
  parser::yy_print_ (std::ostream& yyo,
                                     const basic_symbol<Base>& yysym) const
  {
    std::ostream& yyoutput = yyo;
    YYUSE (yyoutput);
    symbol_number_type yytype = yysym.type_get ();
    // Avoid a (spurious) G++ 4.8 warning about "array subscript is
    // below array bounds".
    if (yysym.empty ())
      std::abort ();
    yyo << (yytype < yyntokens_ ? "token" : "nterm")
        << ' ' << yytname_[yytype] << " ("
        << yysym.location << ": ";
    YYUSE (yytype);
    yyo << ')';
  }
#endif

  inline
  void
  parser::yypush_ (const char* m, state_type s, symbol_type& sym)
  {
    stack_symbol_type t (s, sym);
    yypush_ (m, t);
  }

  inline
  void
  parser::yypush_ (const char* m, stack_symbol_type& s)
  {
    if (m)
      YY_SYMBOL_PRINT (m, s);
    yystack_.push (s);
  }

  inline
  void
  parser::yypop_ (unsigned int n)
  {
    yystack_.pop (n);
  }

#if YYDEBUG
  std::ostream&
  parser::debug_stream () const
  {
    return *yycdebug_;
  }

  void
  parser::set_debug_stream (std::ostream& o)
  {
    yycdebug_ = &o;
  }


  parser::debug_level_type
  parser::debug_level () const
  {
    return yydebug_;
  }

  void
  parser::set_debug_level (debug_level_type l)
  {
    yydebug_ = l;
  }
#endif // YYDEBUG

  inline parser::state_type
  parser::yy_lr_goto_state_ (state_type yystate, int yysym)
  {
    int yyr = yypgoto_[yysym - yyntokens_] + yystate;
    if (0 <= yyr && yyr <= yylast_ && yycheck_[yyr] == yystate)
      return yytable_[yyr];
    else
      return yydefgoto_[yysym - yyntokens_];
  }

  inline bool
  parser::yy_pact_value_is_default_ (int yyvalue)
  {
    return yyvalue == yypact_ninf_;
  }

  inline bool
  parser::yy_table_value_is_error_ (int yyvalue)
  {
    return yyvalue == yytable_ninf_;
  }

  int
  parser::parse ()
  {
    // State.
    int yyn;
    /// Length of the RHS of the rule being reduced.
    int yylen = 0;

    // Error handling.
    int yynerrs_ = 0;
    int yyerrstatus_ = 0;

    /// The lookahead symbol.
    symbol_type yyla;

    /// The locations where the error started and ended.
    stack_symbol_type yyerror_range[3];

    /// The return value of parse ().
    int yyresult;

    // FIXME: This shoud be completely indented.  It is not yet to
    // avoid gratuitous conflicts when merging into the master branch.
    try
      {
    YYCDEBUG << "Starting parse" << std::endl;


    /* Initialize the stack.  The initial state will be set in
       yynewstate, since the latter expects the semantical and the
       location values to have been already stored, initialize these
       stacks with a primary value.  */
    yystack_.clear ();
    yypush_ (YY_NULLPTR, 0, yyla);

    // A new symbol was pushed on the stack.
  yynewstate:
    YYCDEBUG << "Entering state " << yystack_[0].state << std::endl;

    // Accept?
    if (yystack_[0].state == yyfinal_)
      goto yyacceptlab;

    goto yybackup;

    // Backup.
  yybackup:

    // Try to take a decision without lookahead.
    yyn = yypact_[yystack_[0].state];
    if (yy_pact_value_is_default_ (yyn))
      goto yydefault;

    // Read a lookahead token.
    if (yyla.empty ())
      {
        YYCDEBUG << "Reading a token: ";
        try
          {
            yyla.type = yytranslate_ (yylex (&yyla.value, &yyla.location));
          }
        catch (const syntax_error& yyexc)
          {
            error (yyexc);
            goto yyerrlab1;
          }
      }
    YY_SYMBOL_PRINT ("Next token is", yyla);

    /* If the proper action on seeing token YYLA.TYPE is to reduce or
       to detect an error, take that action.  */
    yyn += yyla.type_get ();
    if (yyn < 0 || yylast_ < yyn || yycheck_[yyn] != yyla.type_get ())
      goto yydefault;

    // Reduce or error.
    yyn = yytable_[yyn];
    if (yyn <= 0)
      {
        if (yy_table_value_is_error_ (yyn))
          goto yyerrlab;
        yyn = -yyn;
        goto yyreduce;
      }

    // Count tokens shifted since error; after three, turn off error status.
    if (yyerrstatus_)
      --yyerrstatus_;

    // Shift the lookahead token.
    yypush_ ("Shifting", yyn, yyla);
    goto yynewstate;

  /*-----------------------------------------------------------.
  | yydefault -- do the default action for the current state.  |
  `-----------------------------------------------------------*/
  yydefault:
    yyn = yydefact_[yystack_[0].state];
    if (yyn == 0)
      goto yyerrlab;
    goto yyreduce;

  /*-----------------------------.
  | yyreduce -- Do a reduction.  |
  `-----------------------------*/
  yyreduce:
    yylen = yyr2_[yyn];
    {
      stack_symbol_type yylhs;
      yylhs.state = yy_lr_goto_state_(yystack_[yylen].state, yyr1_[yyn]);
      /* Variants are always initialized to an empty instance of the
         correct type. The default '$$ = $1' action is NOT applied
         when using variants.  */
        switch (yyr1_[yyn])
    {
      case 32: // ID
        yylhs.value.build< std::string > ();
        break;

      case 60: // expression
        yylhs.value.build< type > ();
        break;

      default:
        break;
    }


      // Compute the default @$.
      {
        slice<stack_symbol_type, stack_type> slice (yystack_, yylen);
        YYLLOC_DEFAULT (yylhs.location, slice, yylen);
      }

      // Perform the reduction.
      YY_REDUCE_PRINT (yyn);
      try
        {
          switch (yyn)
            {
  case 5:
#line 77 "while.y" // lalr1.cc:859
    {
        declare(yystack_[1].location.begin.line, yystack_[0].value.as< std::string > (), boolean);
    }
#line 997 "while.tab.cc" // lalr1.cc:859
    break;

  case 6:
#line 82 "while.y" // lalr1.cc:859
    {
        declare(yystack_[1].location.begin.line, yystack_[0].value.as< std::string > (), natural);
    }
#line 1005 "while.tab.cc" // lalr1.cc:859
    break;

  case 7:
#line 87 "while.y" // lalr1.cc:859
    {
		declare(yystack_[1].location.begin.line, yystack_[0].value.as< std::string > (), date_type);
    }
#line 1013 "while.tab.cc" // lalr1.cc:859
    break;

  case 10:
#line 101 "while.y" // lalr1.cc:859
    {
        get_type(yystack_[1].location.begin.line, yystack_[1].value.as< std::string > ());
    }
#line 1021 "while.tab.cc" // lalr1.cc:859
    break;

  case 12:
#line 108 "while.y" // lalr1.cc:859
    {

		check_assignment(yystack_[2].location.begin.line, yystack_[2].value.as< std::string > (), yystack_[0].value.as< type > ());
        
    }
#line 1031 "while.tab.cc" // lalr1.cc:859
    break;

  case 13:
#line 115 "while.y" // lalr1.cc:859
    {
        check_condition(yystack_[4].location.begin.line, yystack_[3].value.as< type > ());
    }
#line 1039 "while.tab.cc" // lalr1.cc:859
    break;

  case 14:
#line 120 "while.y" // lalr1.cc:859
    {
        check_condition(yystack_[6].location.begin.line, yystack_[5].value.as< type > ());
    }
#line 1047 "while.tab.cc" // lalr1.cc:859
    break;

  case 15:
#line 125 "while.y" // lalr1.cc:859
    {
        check_condition(yystack_[4].location.begin.line, yystack_[3].value.as< type > ());
    }
#line 1055 "while.tab.cc" // lalr1.cc:859
    break;

  case 16:
#line 132 "while.y" // lalr1.cc:859
    {
        yylhs.value.as< type > () = natural;
    }
#line 1063 "while.tab.cc" // lalr1.cc:859
    break;

  case 17:
#line 137 "while.y" // lalr1.cc:859
    {
        yylhs.value.as< type > () = boolean;
    }
#line 1071 "while.tab.cc" // lalr1.cc:859
    break;

  case 18:
#line 142 "while.y" // lalr1.cc:859
    {
        yylhs.value.as< type > () = boolean;
    }
#line 1079 "while.tab.cc" // lalr1.cc:859
    break;

  case 19:
#line 147 "while.y" // lalr1.cc:859
    {
		yylhs.value.as< type > () = date_type;
    }
#line 1087 "while.tab.cc" // lalr1.cc:859
    break;

  case 20:
#line 152 "while.y" // lalr1.cc:859
    {
		if(yystack_[1].value.as< type > () == date_type) {
			yylhs.value.as< type > () = natural;
		} else {
			semantic_error(yystack_[3].location.begin.line, "getYear expect date type");
		}
    }
#line 1099 "while.tab.cc" // lalr1.cc:859
    break;

  case 21:
#line 162 "while.y" // lalr1.cc:859
    {
        yylhs.value.as< type > () = get_type(yystack_[0].location.begin.line, yystack_[0].value.as< std::string > ());
    }
#line 1107 "while.tab.cc" // lalr1.cc:859
    break;

  case 22:
#line 167 "while.y" // lalr1.cc:859
    {
		if(yystack_[2].value.as< type > () == natural && yystack_[0].value.as< type > () == date_type || yystack_[2].value.as< type > () == date_type && yystack_[0].value.as< type > () == natural) {
			yylhs.value.as< type > () = date_type;
		} else {
		    check_operation2(yystack_[1].location.begin.line, "+", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
			yylhs.value.as< type > () = natural;
		}

    }
#line 1121 "while.tab.cc" // lalr1.cc:859
    break;

  case 23:
#line 178 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "-", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = natural;
    }
#line 1130 "while.tab.cc" // lalr1.cc:859
    break;

  case 24:
#line 184 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "*", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = natural;
    }
#line 1139 "while.tab.cc" // lalr1.cc:859
    break;

  case 25:
#line 190 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "/", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = natural;
    }
#line 1148 "while.tab.cc" // lalr1.cc:859
    break;

  case 26:
#line 196 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "%", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = natural;
    }
#line 1157 "while.tab.cc" // lalr1.cc:859
    break;

  case 27:
#line 202 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "<", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1166 "while.tab.cc" // lalr1.cc:859
    break;

  case 28:
#line 208 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, ">", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1175 "while.tab.cc" // lalr1.cc:859
    break;

  case 29:
#line 214 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "<=", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1184 "while.tab.cc" // lalr1.cc:859
    break;

  case 30:
#line 220 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, ">=", natural, natural, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1193 "while.tab.cc" // lalr1.cc:859
    break;

  case 31:
#line 226 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "and", boolean, boolean, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1202 "while.tab.cc" // lalr1.cc:859
    break;

  case 32:
#line 232 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "or", boolean, boolean, yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1211 "while.tab.cc" // lalr1.cc:859
    break;

  case 33:
#line 238 "while.y" // lalr1.cc:859
    {
        check_operation2(yystack_[1].location.begin.line, "=", yystack_[2].value.as< type > (), yystack_[2].value.as< type > (), yystack_[2].value.as< type > (), yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1220 "while.tab.cc" // lalr1.cc:859
    break;

  case 34:
#line 244 "while.y" // lalr1.cc:859
    {
        check_operation1(yystack_[0].location.begin.line, "not", boolean, yystack_[0].value.as< type > ());
        yylhs.value.as< type > () = boolean;
    }
#line 1229 "while.tab.cc" // lalr1.cc:859
    break;

  case 35:
#line 250 "while.y" // lalr1.cc:859
    {
        yylhs.value.as< type > () = yystack_[1].value.as< type > ();
    }
#line 1237 "while.tab.cc" // lalr1.cc:859
    break;


#line 1241 "while.tab.cc" // lalr1.cc:859
            default:
              break;
            }
        }
      catch (const syntax_error& yyexc)
        {
          error (yyexc);
          YYERROR;
        }
      YY_SYMBOL_PRINT ("-> $$ =", yylhs);
      yypop_ (yylen);
      yylen = 0;
      YY_STACK_PRINT ();

      // Shift the result of the reduction.
      yypush_ (YY_NULLPTR, yylhs);
    }
    goto yynewstate;

  /*--------------------------------------.
  | yyerrlab -- here on detecting error.  |
  `--------------------------------------*/
  yyerrlab:
    // If not already recovering from an error, report this error.
    if (!yyerrstatus_)
      {
        ++yynerrs_;
        error (yyla.location, yysyntax_error_ (yystack_[0].state, yyla));
      }


    yyerror_range[1].location = yyla.location;
    if (yyerrstatus_ == 3)
      {
        /* If just tried and failed to reuse lookahead token after an
           error, discard it.  */

        // Return failure if at end of input.
        if (yyla.type_get () == yyeof_)
          YYABORT;
        else if (!yyla.empty ())
          {
            yy_destroy_ ("Error: discarding", yyla);
            yyla.clear ();
          }
      }

    // Else will try to reuse lookahead token after shifting the error token.
    goto yyerrlab1;


  /*---------------------------------------------------.
  | yyerrorlab -- error raised explicitly by YYERROR.  |
  `---------------------------------------------------*/
  yyerrorlab:

    /* Pacify compilers like GCC when the user code never invokes
       YYERROR and the label yyerrorlab therefore never appears in user
       code.  */
    if (false)
      goto yyerrorlab;
    yyerror_range[1].location = yystack_[yylen - 1].location;
    /* Do not reclaim the symbols of the rule whose action triggered
       this YYERROR.  */
    yypop_ (yylen);
    yylen = 0;
    goto yyerrlab1;

  /*-------------------------------------------------------------.
  | yyerrlab1 -- common code for both syntax error and YYERROR.  |
  `-------------------------------------------------------------*/
  yyerrlab1:
    yyerrstatus_ = 3;   // Each real token shifted decrements this.
    {
      stack_symbol_type error_token;
      for (;;)
        {
          yyn = yypact_[yystack_[0].state];
          if (!yy_pact_value_is_default_ (yyn))
            {
              yyn += yyterror_;
              if (0 <= yyn && yyn <= yylast_ && yycheck_[yyn] == yyterror_)
                {
                  yyn = yytable_[yyn];
                  if (0 < yyn)
                    break;
                }
            }

          // Pop the current state because it cannot handle the error token.
          if (yystack_.size () == 1)
            YYABORT;

          yyerror_range[1].location = yystack_[0].location;
          yy_destroy_ ("Error: popping", yystack_[0]);
          yypop_ ();
          YY_STACK_PRINT ();
        }

      yyerror_range[2].location = yyla.location;
      YYLLOC_DEFAULT (error_token.location, yyerror_range, 2);

      // Shift the error token.
      error_token.state = yyn;
      yypush_ ("Shifting", error_token);
    }
    goto yynewstate;

    // Accept.
  yyacceptlab:
    yyresult = 0;
    goto yyreturn;

    // Abort.
  yyabortlab:
    yyresult = 1;
    goto yyreturn;

  yyreturn:
    if (!yyla.empty ())
      yy_destroy_ ("Cleanup: discarding lookahead", yyla);

    /* Do not reclaim the symbols of the rule whose action triggered
       this YYABORT or YYACCEPT.  */
    yypop_ (yylen);
    while (1 < yystack_.size ())
      {
        yy_destroy_ ("Cleanup: popping", yystack_[0]);
        yypop_ ();
      }

    return yyresult;
  }
    catch (...)
      {
        YYCDEBUG << "Exception caught: cleaning lookahead and stack"
                 << std::endl;
        // Do not try to display the values of the reclaimed symbols,
        // as their printer might throw an exception.
        if (!yyla.empty ())
          yy_destroy_ (YY_NULLPTR, yyla);

        while (1 < yystack_.size ())
          {
            yy_destroy_ (YY_NULLPTR, yystack_[0]);
            yypop_ ();
          }
        throw;
      }
  }

  void
  parser::error (const syntax_error& yyexc)
  {
    error (yyexc.location, yyexc.what());
  }

  // Generate an error message.
  std::string
  parser::yysyntax_error_ (state_type, const symbol_type&) const
  {
    return YY_("syntax error");
  }


  const signed char parser::yypact_ninf_ = -39;

  const signed char parser::yytable_ninf_ = -1;

  const short int
  parser::yypact_[] =
  {
       6,   -17,    22,   -39,   -39,    52,   -39,    -9,    -8,    -6,
     -39,    -4,   -39,   -39,   -39,   -39,    34,    41,    30,    30,
       1,   -39,    37,    30,   -39,   -39,    30,   -39,   -39,   -39,
      43,    30,   -12,     0,    30,    44,    60,    85,    30,   -39,
     -39,    30,    30,    30,    30,    30,    30,    30,    30,    30,
      30,    30,    30,   -39,   140,   -39,   -39,   -39,    97,   141,
      72,   150,   120,   -31,   -31,   -31,   -31,   -38,   -38,   -39,
     -39,   -39,    -7,   -39,   -39,   -39,   -39,   148,   -39
  };

  const unsigned char
  parser::yydefact_[] =
  {
       0,     0,     0,     3,     1,     0,     8,     0,     0,     0,
       4,     0,     5,     6,     7,     2,     0,     0,     0,     0,
       0,     9,     0,     0,    17,    18,     0,    21,    16,    19,
       0,     0,     0,     0,     0,     0,     0,     0,     0,    34,
       8,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     8,    12,    10,    11,    35,     0,     0,
      32,    31,    33,    27,    28,    29,    30,    22,    23,    24,
      25,    26,     0,    20,     8,    13,    15,     0,    14
  };

  const signed char
  parser::yypgoto_[] =
  {
     -39,   -39,   -39,   -39,    13,   -39,    48
  };

  const signed char
  parser::yydefgoto_[] =
  {
      -1,     2,     5,    10,    11,    21,    32
  };

  const unsigned char
  parser::yytable_[] =
  {
      40,    15,    16,    17,    18,    16,    17,    18,    19,     1,
      76,    19,    50,    51,    52,     3,    53,    48,    49,    50,
      51,    52,     4,    12,    13,    20,    14,    34,    20,    41,
      42,    43,    44,    45,    46,    47,    48,    49,    50,    51,
      52,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    59,    24,    25,     6,    26,     7,     8,
       9,    22,    27,    28,    29,    30,    72,    33,    23,    35,
      38,    36,    55,     0,    37,     0,     0,     0,     0,    39,
       0,     0,    54,    31,     0,     0,    58,    77,    56,    60,
      61,    62,    63,    64,    65,    66,    67,    68,    69,    70,
      71,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    57,    42,    43,    44,    45,    46,    47,
      48,    49,    50,    51,    52,    73,    41,    42,    43,    44,
      45,    46,    47,    48,    49,    50,    51,    52,    41,    42,
      43,    44,    45,    46,    47,    48,    49,    50,    51,    52,
      16,    17,    18,     0,    74,    75,    19,    16,    17,    18,
       0,     0,    78,    19,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    20,     0,     0,     0,     0,     0,     0,
      20,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52
  };

  const signed char
  parser::yycheck_[] =
  {
      12,     5,     9,    10,    11,     9,    10,    11,    15,     3,
      17,    15,    50,    51,    52,    32,    16,    48,    49,    50,
      51,    52,     0,    32,    32,    32,    32,    26,    32,    41,
      42,    43,    44,    45,    46,    47,    48,    49,    50,    51,
      52,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    40,    24,    25,     4,    27,     6,     7,
       8,    27,    32,    33,    34,    35,    53,    19,    27,    32,
      27,    23,    28,    -1,    26,    -1,    -1,    -1,    -1,    31,
      -1,    -1,    34,    53,    -1,    -1,    38,    74,    28,    41,
      42,    43,    44,    45,    46,    47,    48,    49,    50,    51,
      52,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    28,    42,    43,    44,    45,    46,    47,
      48,    49,    50,    51,    52,    28,    41,    42,    43,    44,
      45,    46,    47,    48,    49,    50,    51,    52,    41,    42,
      43,    44,    45,    46,    47,    48,    49,    50,    51,    52,
       9,    10,    11,    -1,    13,    14,    15,     9,    10,    11,
      -1,    -1,    14,    15,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    32,    -1,    -1,    -1,    -1,    -1,    -1,
      32,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52
  };

  const unsigned char
  parser::yystos_[] =
  {
       0,     3,    55,    32,     0,    56,     4,     6,     7,     8,
      57,    58,    32,    32,    32,     5,     9,    10,    11,    15,
      32,    59,    27,    27,    24,    25,    27,    32,    33,    34,
      35,    53,    60,    60,    26,    32,    60,    60,    27,    60,
      12,    41,    42,    43,    44,    45,    46,    47,    48,    49,
      50,    51,    52,    16,    60,    28,    28,    28,    60,    58,
      60,    60,    60,    60,    60,    60,    60,    60,    60,    60,
      60,    60,    58,    28,    13,    14,    17,    58,    14
  };

  const unsigned char
  parser::yyr1_[] =
  {
       0,    54,    55,    56,    56,    57,    57,    57,    58,    58,
      59,    59,    59,    59,    59,    59,    60,    60,    60,    60,
      60,    60,    60,    60,    60,    60,    60,    60,    60,    60,
      60,    60,    60,    60,    60,    60
  };

  const unsigned char
  parser::yyr2_[] =
  {
       0,     2,     6,     0,     2,     2,     2,     2,     0,     2,
       4,     4,     3,     5,     7,     5,     1,     1,     1,     1,
       4,     1,     3,     3,     3,     3,     3,     3,     3,     3,
       3,     3,     3,     3,     2,     3
  };


#if YYDEBUG
  // YYTNAME[SYMBOL-NUM] -- String name of the symbol SYMBOL-NUM.
  // First, the terminals, then, starting at \a yyntokens_, nonterminals.
  const char*
  const parser::yytname_[] =
  {
  "$end", "error", "$undefined", "PRG", "BEG", "END", "BOO", "NAT",
  "DATE", "REA", "WRI", "IF", "THE", "ELS", "EIF", "WHI", "DO", "DON",
  "REP", "UNT", "FOR", "UPTO", "DWTO", "FROM", "TRU", "FAL", "ASN", "OP",
  "CL", "SOP", "SCL", "PAR", "ID", "NUM", "DATELIT", "GETY", "GETM",
  "GETD", "GOTO", "QW", "CO", "OR", "AND", "EQ", "LS", "GR", "LSE", "GRE",
  "ADD", "SUB", "MUL", "DIV", "MOD", "NOT", "$accept", "start",
  "declarations", "declaration", "commands", "command", "expression", YY_NULLPTR
  };


  const unsigned char
  parser::yyrline_[] =
  {
       0,    66,    66,    69,    72,    76,    81,    86,    93,    96,
     100,   105,   107,   114,   119,   124,   131,   136,   141,   146,
     151,   161,   166,   177,   183,   189,   195,   201,   207,   213,
     219,   225,   231,   237,   243,   249
  };

  // Print the state stack on the debug stream.
  void
  parser::yystack_print_ ()
  {
    *yycdebug_ << "Stack now";
    for (stack_type::const_iterator
           i = yystack_.begin (),
           i_end = yystack_.end ();
         i != i_end; ++i)
      *yycdebug_ << ' ' << i->state;
    *yycdebug_ << std::endl;
  }

  // Report on the debug stream that the rule \a yyrule is going to be reduced.
  void
  parser::yy_reduce_print_ (int yyrule)
  {
    unsigned int yylno = yyrline_[yyrule];
    int yynrhs = yyr2_[yyrule];
    // Print the symbols being reduced, and their result.
    *yycdebug_ << "Reducing stack by rule " << yyrule - 1
               << " (line " << yylno << "):" << std::endl;
    // The symbols being reduced.
    for (int yyi = 0; yyi < yynrhs; yyi++)
      YY_SYMBOL_PRINT ("   $" << yyi + 1 << " =",
                       yystack_[(yynrhs) - (yyi + 1)]);
  }
#endif // YYDEBUG

  // Symbol number corresponding to token number t.
  inline
  parser::token_number_type
  parser::yytranslate_ (int t)
  {
    static
    const token_number_type
    translate_table[] =
    {
     0,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     1,     2,     3,     4,
       5,     6,     7,     8,     9,    10,    11,    12,    13,    14,
      15,    16,    17,    18,    19,    20,    21,    22,    23,    24,
      25,    26,    27,    28,    29,    30,    31,    32,    33,    34,
      35,    36,    37,    38,    39,    40,    41,    42,    43,    44,
      45,    46,    47,    48,    49,    50,    51,    52,    53
    };
    const unsigned int user_token_number_max_ = 308;
    const token_number_type undef_token_ = 2;

    if (static_cast<int>(t) <= yyeof_)
      return yyeof_;
    else if (static_cast<unsigned int> (t) <= user_token_number_max_)
      return translate_table[t];
    else
      return undef_token_;
  }


} // yy
#line 1641 "while.tab.cc" // lalr1.cc:1167
#line 255 "while.y" // lalr1.cc:1168

