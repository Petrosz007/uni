module ComplexPatterns where

f :: [[a]] -> (a,[a])
f ([asd]:[qwe]) = (asd,qwe)

{-
 desugared: f ((asd:[]):qwe:[])
 with more sugar: [[asd],qwe]
-}

-- runtime error, matches on non-empty lists
x0 = f []

{-
 runtime error, [[]] is non-empty, so it passes the first level,
 but []:[] ([[]] desugared) is a list with a single element, but
 the original pattern only matches on lists that have EXACTLY
 two elements of form [asd] and qwe (from the form with more sugar)
-}
x1 = f [[]]

{-
 runtime error: ["hello"] == "hello":[], 
 same reasoning as for the previous pattern
-}
x2 = f ["hello"]

{-
 runs without error: (1,[2,3])
 the expression can be compared to the form with more sugar: [[asd],qwe]
-}
x3 = f [[1],[2,3]]

{-
  runtime error:
  "ab":"cd":"ef"[] should match on ((asd:[]):qwe:[])
  this list does not only have more than two elements,
  but the first inner list contains two elements,
  but as we can see, it should hold only one (named asd)
-}
x4 = f ["ab","cd","ef"]

{-
  runtime error:
  now the first inner list does contain a single element,
  but the whole list still has length three,
  which means, the outermost pattern will fail
-}
x5 = f ["b","cd","ef"]

{-
  In general, f will only match on lists that have exactly two elements,
  two inner lists, of which the first one contains only a single element.
  We do not place any constraints on the second list.
-}