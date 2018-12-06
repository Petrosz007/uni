module assignment5b

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

derive class iTask Student, BaMa

task1 :: Task Student
task1 = enterInformation "student" []

task1t :: Task (Student, Student)
task1t = enterInformation "student" []

task2 :: Task [Student]
task2 = enterInformation "student" []

task3 :: Task Student
task3 = updateInformation "student" [] student

task4 :: Task Student
task4 = enterChoice "favorite student" [] students

task5 :: Task Student
//task5 = enterChoice "favorite student" [ChooseFromCheckGroup \{Student|name}.name] students
task5 = enterChoice "favorite student" [ChooseFromCheckGroup \s.s.Student.name] students

task6 :: Task Student
task6 = enterChoice "favorite student" [ChooseFromCheckGroup gToString{|*|}] students

task7 :: Task [Student] 
task7 = enterMultipleChoice "potential partners" [ChooseFromCheckGroup \{Student|name,bama}.name + " " + gToString{|*|} bama] students

task8 :: Task Student
task8 = updateInformation ("change name of " + gToString{|*|} student) [UpdateAs (\{Student|name}.name) (\s n.{Student|s & name = n})] student

Start w = startEngine task1 w

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
gToString{|Int|} a = toString a
gToString{|Bool|} a = toString a
gToString{|String|} a = a
gToString{|UNIT|} a = ""
gToString{|EITHER|} f g (LEFT a) = f a 
gToString{|EITHER|} f g (RIGHT a) = g a 
gToString{|PAIR|} f g (PAIR a b)  = f a + " " + g b
gToString{|OBJECT|} f (OBJECT a) = f a 
gToString{|CONS of {gcd_name,gcd_arity}|} f (CONS a) | gcd_arity == 0
	= gcd_name
	= "(" + gcd_name + " " + f a + ")"
gToString{|FIELD of {gfd_name}|} f (FIELD a) = " " + gfd_name + "=" + f a
gToString{|RECORD of {grd_name}|} f (RECORD a) = "{" + grd_name + "|" + f a + "}"



derive gToString BaMa, Student
instance + String where + s t = s +++ t
