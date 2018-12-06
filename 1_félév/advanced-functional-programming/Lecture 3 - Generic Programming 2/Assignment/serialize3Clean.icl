module serialize3Clean

import StdEnv, StdMaybe, StdGeneric, GenEq

:: Write a :== a [String] -> [String]
:: Read a  :== [String] -> Maybe (a,[String])
 
class serialize a | read{|*|}, write{|*|} a

generic write a :: a [String] -> [String]
generic read a  :: [String] -> Maybe (a, [String])

write{|Bool|} b c = [toString b:c]
write{|Int|}  i c = [toString i:c]
write{|UNIT|} _ c = c
write{|PAIR|} wx wy (PAIR x y) c = wx x [" ":wy y c]
write{|EITHER|} wx wy (LEFT x) c = wx x c
write{|EITHER|} wx wy (RIGHT y) c = wy y c
write{|CONS of {gcd_name,gcd_arity}|} wa (CONS a) c | gcd_arity > 0
	= ["(",gcd_name," ":wa a [")":c]] 
	= [gcd_name: wa a c]
//write{|OBJECT of {gtd_name}|} wa (OBJECT a) c = ["OBJECT ",gtd_name," ":wa a c]
write{|OBJECT|} wa (OBJECT a) c = wa a c

read{|Bool|} ["True":r] = Just (True,r)
read{|Bool|} ["False":r] = Just (False,r)
read{|Bool|} _ = Nothing
read{|Int|} [s:r]
  # i = toInt s
  | s == toString i
    = Just (i,r)
    = Nothing
read{|Int|} _ = Nothing
read{|UNIT|} l = Just (UNIT,l)
read{|CONS of {gcd_name,gcd_arity}|} ra [s:l] | gcd_arity == 0 && s == gcd_name
	= case ra l of
		Just (g, m) = Just (CONS g, m)
		_ = Nothing
read{|CONS of {gcd_name,gcd_arity}|} ra ["(",s," ":l] | gcd_arity > 0 && s == gcd_name
	= case ra l of
		Just (g, [")":m]) = Just (CONS g, m)
		_ = Nothing
read{|CONS|} _ _ = Nothing
read{|OBJECT|} ra l = case ra l of
	Just (g, m) = Just (OBJECT g, m)
	_ = Nothing
read{|PAIR|} ra rb l = case ra l of
	Just (a, [" ":m]) = case rb m of
		Just (b, n) = Just (PAIR a b, n)
		_ = Nothing
	_ = Nothing
read{|EITHER|} ra rb l = case ra l of
	Just (a, m) = Just (LEFT a, m)
	_ = case rb l of
		Just (b, n) = Just (RIGHT b, n)
		_ = Nothing
	_ = Nothing

write{|(,)|} wa wb (a,b) c = ["(":wa a [",":wb b [")":c]]]
read{|(,)|} ra rb ["(":l] = case ra l of
	Just (a,[",":m]) = case rb m of
		Just (b,[")":n]) = Just ((a,b),n)
		_ = Nothing
	_ = Nothing
read{|(,)|} ra rb l = Nothing

// ---

:: Bin a = Leaf | Bin (Bin a) a (Bin a)
:: Coin = Head | Tail

derive read [], Bin, Coin
derive write [], Bin, Coin
derive gEq Bin, Coin

// ---

Start = 
  [test True
  ,test False
  ,test 0
  ,test 123
  ,test -36
  ,test [42]
  ,test [0..4]
  ,test [[True],[]]
  ,test [[[1]],[[2],[3,4]],[[]]]
  ,test (Bin Leaf True Leaf)
  ,test [Bin (Bin Leaf [1] Leaf) [2] (Bin Leaf [3] (Bin Leaf [4,5] Leaf))]
  ,test [Bin (Bin Leaf [1] Leaf) [2] (Bin Leaf [3] (Bin (Bin Leaf [4,5] Leaf) [6,7] (Bin Leaf [8,9] Leaf)))]
  ,test Head
  ,test Tail
  ,test (7,True)
  ,test (Head,(7,[Tail]))
  ,["End of the tests.\n"]
  ]

test :: a -> [String] | serialize, gEq{|*|} a
test a = 
  (if (isJust r)
    (if (fst jr === a)
      (if (isEmpty (tl (snd jr)))
        ["Oke "]
        ["Not all input is consumed! ":snd jr])
      ["Wrong result ":write{|*|} (fst jr) []])
    ["read result is Nothing "]
  ) ++ ["write produces ": s]
  where
    s = write{|*|} a ["\n"]
    r = read {|*|} s
    jr = fromJust r

