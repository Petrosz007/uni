module serialize2b

import StdEnv, StdMaybe

class serialize a where
  write :: a [String] -> [String]
  readB :: B [String] -> Maybe (a,[String])

read = readB N
//--
:: B = C String | B B B | N
//--
instance serialize Bool where
  write b c = [toString b:c]
  readB _ ["True":r] = Just (True,r)
  readB _ ["False":r] = Just (False,r)
  readB _ _ = Nothing

instance serialize Int where
  write i c = [toString i:c]
  readB _ [s:r]
    # i = toInt s
    | s == toString i
      = Just (i,r)
      = Nothing
  readB _ _ = Nothing

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
  readB _ l = Just (UNIT,l)

instance serialize (EITHER a b) | serialize, readC a & serialize, readC b where
  write (LEFT  a) c = write a c
  write (RIGHT b) c = write b c
  readB (B x y) l = case readB x l of
    Just (a,m) = Just (LEFT a,m)
    _ = case readB y l of
      Just (b,m) = Just (RIGHT b,m)
      _ = Nothing
  readB _ _ = Nothing

instance serialize (PAIR a b) | serialize a & serialize b where
  write (PAIR a b) c = write a (write b c)
  readB n l = case read l of
    Just (a,m) = case read m of
      Just (b,n) = Just (PAIR a b,n)
      _ = Nothing
    _ = Nothing

instance serialize (CONS a) | serialize, isUNIT, readC a where
  write (CONS s a) c | isUNIT a
  	= [s:c]
  	= ["(",s:write a [")":c]]
  readB (C b) l = readC read b l
  readB _ l = Nothing

:: READ a :== [String] -> Maybe (a,[String])

class readC a | serialize a where
	readC :: (READ a) String [String] -> Maybe (CONS a,[String])

instance readC UNIT where
	readC _ n [a:x] | n == a
		= Just (CONS n UNIT,x)
		= Nothing
	readC _ _ _ = Nothing

instance readC default where
	readC f n ["(",a:x] | n == a
		= case f x of
			Just (b,[")":y]) = Just (CONS n b,y)
			_ = Nothing
		= Nothing
	readC _ _ _ = Nothing

class isUNIT a :: a -> Bool
instance isUNIT UNIT where isUNIT _ = True
instance isUNIT a    where isUNIT _ = False

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
  (==) _  _  = False

instance serialize Coin where
  write coin cont = write (fromCoin coin) cont
  readB _ l = case readB (B (C "Head") (C "Tail")) l of
    Just (g,l) = Just (toCoin g,l)
    _      = Nothing

instance serialize [a] | serialize a where
  write l c = write (fromList l) c
  readB _ l = case readB (B (C NilString) (C ConsString)) l of
    Just (g,m) = Just (toList g,m)
    _ = Nothing

NilString  :== "Nil"
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
  readB _ l = case readB (B (C LeafString) (C BinString)) l of
    Just (a,m) = Just (toBin a,m)
    _ = Nothing

LeafString :== "Leaf"
BinString  :== "Bin"

instance == (Bin a) | == a where
  (==) Leaf Leaf = True
  (==) (Bin l a r) (Bin k b s) = l == k && a == b && r == s
  (==) _ _ = False

:: Box a = Box a
/*
//Overloading error [serialize2b.icl,173,readB]: internal overloading of "readB" could not be solved
//Overloading error [serialize2b.icl,172,write]: internal overloading of "write" could not be solved
:: BoxG a :== CONS a

fromBox :: (Box a) -> BoxG a
fromBox (Box a) = CONS "Box" a

toBox :: (BoxG a) -> Box a
toBox (CONS _ x) = Box x
*/
// oke
:: BoxG a :== CONS (PAIR a UNIT)

fromBox :: (Box a) -> BoxG a
fromBox (Box a) = CONS "Box" (PAIR a UNIT)

toBox :: (BoxG a) -> Box a
toBox (CONS _ (PAIR x _)) = Box x

instance == (Box a) | == a where (==) (Box x) (Box y) = x == y

instance serialize (Box a) | serialize a where
  write a c = write (fromBox a) c
  readB _ ["Box":l] = case readB N l of
  	Just (a,m) = Just (Box a,m)
  	_ = Nothing
  readB _ l = case readB (C "Box") l of
    Just (a,m) = Just (toBox a,m)
    _ = Nothing

Start = 
  [test True
  ,test False
  ,test 0
  ,test 123
  ,test -36
  ,test nil
  ,test (Box 36)
  ,test (Box Tail)
  ,test [42]
  ,test [0..4]
  ,test [[True],[]]
  ,test (Bin Leaf True Leaf)
  ,test [Bin (Bin Leaf [1] Leaf) [2] (Bin Leaf [3] (Bin Leaf [4,5] Leaf))]
  ,test [Bin (Bin Leaf [1] Leaf) [2] (Bin Leaf [3] (Bin (Bin Leaf [4,5] Leaf) [6,7] (Bin Leaf [8,9] Leaf)))]
  ,test Head
  ,test Tail
  ,test (Left Tail)
  ,test (Right False)
  ,([],["\n\nall tests done.\n"])
  ]

:: CoinOrBool = Left Coin | Right Bool
:: CoinOrBoolG :== EITHER (CONS Coin) (CONS Bool)

fromCoinOrBool :: CoinOrBool -> CoinOrBoolG
fromCoinOrBool (Left c) = LEFT (CONS "Left" c)
fromCoinOrBool (Right b) = RIGHT (CONS "Right" b)

toCoinOrBool :: CoinOrBoolG -> CoinOrBool
toCoinOrBool (LEFT (CONS _ c)) = Left c
toCoinOrBool (RIGHT (CONS _ b)) = Right b

instance serialize CoinOrBool where
	write x c = write (fromCoinOrBool x) c
	readB _ l = case readB (B (C "Left") (C "Right")) l of
		Just (g,l) = Just (toCoinOrBool g,l)
		_ = Nothing

instance == CoinOrBool where
	(==) (Left x) (Left y) = x == y
	(==) (Right x) (Right y) = x == y
	(==) _ _ = False


nil :: [Int]
nil = []

Start2 = tt [1..10]

tt = map id o map id

test :: a -> ([String],[String]) | serialize, == a
test a = 
  (if (isJust r)
    (if (fst jr == a)
      (if (isEmpty (tl (snd jr)))
        ["Oke "]
        ["Not all input is consumed! ":snd jr])
      ["Wrong result ":write (fst jr) []])
    ["readB result is Nothing "]
  , ["write produces ": s]
  )
  where
    s = write a ["\n"]
    r = read s
    jr = fromJust r
