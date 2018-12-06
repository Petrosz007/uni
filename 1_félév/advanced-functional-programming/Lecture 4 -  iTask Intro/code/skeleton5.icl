module skeleton5

import iTasks

/*
	Pieter Koopman, pieter@cs.ru.nl
	Advanced Programming. Skeleton for assignment 5
 -	use this a project with environment iTasks
 -	executable must be in Examples/iTasks or a subdirectory
	You can also use the -sdk commandline flag to set the path
 -	check Project Options -> Profiling -> Dynamics to prevent recompilation
*/

:: Student =
	{ name :: String
	, snum :: Int
	, bama :: BaMa
	, year :: Int
	}

:: BaMa = Bachelor | Master

Start w = 42

students :: [Student]
students =
	[{name = "Alice"
	 ,snum = 1000
	 ,bama = Master
	 ,year = 1
	 }
	,{name = "Bob"
	 ,snum = 1003
	 ,bama = Master
	 ,year = 1
	 }
	,{name = "Carol"
	 ,snum = 1024
	 ,bama = Master
	 ,year = 2
	 }
	,{name = "Dave"
	 ,snum = 2048
	 ,bama = Master
	 ,year = 1
	 }
	,{name = "Eve"
	 ,snum = 4096
	 ,bama = Master
	 ,year = 1
	 }
	,{name = "Frank"
	 ,snum = 1023
	 ,bama = Master
	 ,year = 1
	 }
	]

student :: Student
student = students !! 0

generic gToString a :: a -> String

instance + String where + s t = s +++ t
