module serialize2

import StdEnv, StdMaybe

class serialize a where
  write :: a [String] -> [String]
  read  :: [String] -> Maybe (a,[String])

instance serialize Bool where
  write b c = [toString b:c]
  read ["True":r] = Just (True,r)
  read ["False":r] = Just (False,r)
  read _ = Nothing

instance serialize Int where
  write i c = [toString i:c]
  read [s:r]
    # i = toInt s
    | s == toString i
      = Just (i,r)
      = Nothing
  read _ = Nothing

:: UNIT     = UNIT
:: EITHER a b = LEFT a | RIGHT b
:: PAIR   a b = PAIR a b
:: CONS   a   = CONS String a

:: ListG a :== EITHER (CONS UNIT) (CONS (PAIR a [a]))

fromList :: [a] -> ListG a
fromList []  = LEFT  (CONS NilString  UNIT)
fromList [a:x] = RIGHT (CONS ConsString (PAIR a x))

toList :: (ListG a) -> [a]
toList (LEFT  (CONS NilString  UNIT)) = []
toList (RIGHT (CONS ConsString (PAIR a x))) = [a:x]

instance serialize UNIT where
  write UNIT c = c
  read l = Just (UNIT,l)

instance serialize (EITHER a b) | serialize a & serialize b where
  write (LEFT  a) c = write a c
  write (RIGHT b) c = write b c
  read l = case read l of
    Just (a,m) = Just (LEFT a,m)
    _ = case read l of
      Just (b,m) = Just (RIGHT b,m)
      _ = Nothing

instance serialize (PAIR a b) | serialize a & serialize b where
  write (PAIR a b) c = write a (write b c)
  read l = case read l of
    Just (a,m) = case read m of
      Just (b,n) = Just (PAIR a b,n)
      _ = Nothing
    _ = Nothing
/*
instance serialize (CONS UNIT) where
  write (CONS s _) c = [s:c]
  read  [s:l] = Just (CONS s UNIT,l)
  // Error [serialize2.icl,60,serialize]: multiply defined
*/

class serializeCONS a | serialize a where
  writeCONS :: (CONS a) [String] -> [String]
  readCONS  :: [String] -> Maybe (CONS a,[String])

instance serializeCONS UNIT where
  writeCONS (CONS s a) c = [s:c]
  readCONS ["(":l] = Nothing
  readCONS [s:l] = Just(CONS s UNIT, l)
  readCONS _   = Nothing

instance serializeCONS (PAIR a b) | serialize a & serialize b where
  writeCONS (CONS s a) c = ["(",s:write a [")":c]]
  readCONS ["(",s:l] = case read l of
    Just (a,[")":m]) = Just (CONS s a, m)
    _ = Nothing
  readCONS _ = Nothing

instance serializeCONS (EITHER a b) | serialize a & serialize b where
  writeCONS (CONS s a) c = ["(",s:write a [")":c]]
  readCONS ["(",s:l] = case read l of
    Just (a,[")":m]) = Just (CONS s a, m)
    _ = Nothing
  readCONS _ = Nothing

instance serialize (CONS a) | serialize, serializeCONS a where
  write a c = writeCONS a c
  read l = readCONS l
/*
  write (CONS s a) c = ["(",s:write a [")":c]]
  read ["(",s:l] = case read l of
    Just (a,[")":m]) = Just (CONS s a, m)
    _ = Nothing
  read _ = Nothing
*/

:: Coin = Head | Tail
:: CoinG :== EITHER (CONS UNIT) (CONS UNIT)

fromCoin :: Coin -> CoinG
fromCoin Head = LEFT (CONS "Head" UNIT)
fromCoin Tail = RIGHT (CONS "Tail" UNIT)

toCoin :: CoinG -> Coin
toCoin (LEFT (CONS _ UNIT)) = Head
toCoin (RIGHT (CONS _ UNIT)) = Tail

instance == Coin where
  (==) Head Head = True
  (==) Tail Tail = True
  (==) _    _    = False

instance serialize Coin where
  write coin cont = write (fromCoin coin) cont
  read list = case read list of
    Just (g,l) = Just (toCoin g,l)
    _      = Nothing

instance serialize [a] | serialize a where
  write l c = write (fromList l) c
  read l = case read l of
    Just (g,m) = Just (toList g,m)
    _ = Nothing

/*
  write [] c = [NilString: c]
  write [a:x] c = ["(",ConsString: write a (write x [")":c])]
  read  [NilString:r] = Just ([],r)
  read  ["(",ConsString:r] =
    case read r of
      Just (a,s) = case read s of
                    Just (x,[")":t]) = Just ([a:x],t)
                    _ = Nothing
      _ = Nothing
  read  _ = Nothing
*/
NilString :== "Nil"
ConsString :== "Cons"

:: Bin a = Leaf | Bin (Bin a) a (Bin a)

:: BinG a :== EITHER (CONS UNIT) (CONS (PAIR (Bin a) (PAIR a (Bin a))))

fromBin :: (Bin a) -> BinG a
fromBin Leaf = LEFT (CONS LeafString UNIT)
fromBin (Bin l a r) = RIGHT (CONS BinString (PAIR l (PAIR a r)))

toBin :: (BinG a) -> Bin a
toBin (LEFT (CONS _ UNIT)) = Leaf
toBin (RIGHT (CONS _ (PAIR l (PAIR a r)))) = Bin l a r

instance serialize (Bin a) | serialize a where
  write a c = write (fromBin a) c
  read l = case read l of
    Just (a,m) = Just (toBin a,m)
    _ = Nothing

/*
  write Leaf c = [LeafString: c]
  write (Bin l a r) c = ["(",BinString:write l (write a (write r [")":c]))]
  read  [LeafString:r] = Just (Leaf,r)
  read  ["(",BinString:r] =
    case read r of
      Just (l,s) =
        case read s of
          Just (a,t) =
            case read t of
              Just (r,[")":u]) = Just (Bin l a r,u)
              _ = Nothing
          _ = Nothing
      _ = Nothing
*/
LeafString :== "Leaf"
BinString :== "Bin"

instance == (Bin a) | == a where
  (==) Leaf Leaf = True
  (==) (Bin l a r) (Bin k b s) = l == k && a == b && r == s
  (==) _ _ = False

Start = 
  [test True
  ,test False
  ,test 0
  ,test 123
  ,test -36
  ,test [42]
  ,test [0..4]
  ,test [[True],[]]
  ,test (Bin Leaf True Leaf)
  ,test [Bin (Bin Leaf [1] Leaf) [2] (Bin Leaf [3] (Bin Leaf [4,5] Leaf))]
  ,test [Bin (Bin Leaf [1] Leaf) [2] (Bin Leaf [3] (Bin (Bin Leaf [4,5] Leaf) [6,7] (Bin Leaf [8,9] Leaf)))]
  ,test Head
  ,test Tail
  ]

test :: a -> ([String],[String]) | serialize, == a
test a = 
  (if (isJust r)
    (if (fst jr == a)
      (if (isEmpty (tl (snd jr)))
        ["Oke "]
        ["Not all input is consumed! ":snd jr])
      ["Wrong result ":write (fst jr) []])
    ["read result is Nothing "]
  , ["write produces ": s]
  )
  where
    s = write a ["\n"]
    r = read s
    jr = fromJust r

/*
test :: a -> (Bool,[String]) | serialize, == a
test a = (isJust r && fst jr == a && isEmpty (tl (snd jr)), s)
  where
    s = write a ["\n"]
    r = read s
    jr = fromJust r
*/
