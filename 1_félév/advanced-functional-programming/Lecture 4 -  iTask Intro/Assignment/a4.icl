module a4

import iTasks

/*
	Pieter Koopman, pieter@cs.ru.nl
	Advanced Programming. Skeleton for assignment 4 in 2018
 -	use this a project with environment iTasks
 -	executable must be in Examples/iTasks or a subdirectory
	You can also use the -sdk commandline flag to set the path
*/

:: Student =
	{ name :: String
	, snum :: Int
	, bama :: BaMa
	, year :: Int
	}

:: BaMa = Bachelor | Master

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

derive class iTask Student, BaMa
derive gToString Student, BaMa

generic gToString a :: a -> String
gToString{|Int|} i = toString i
gToString{|String|} s = s
gToString{|UNIT|} _ = ""
gToString{|RECORD|} fx (RECORD x) = "{" + fx x + "}"
gToString{|FIELD of {gfd_name}|} fx (FIELD x) = gfd_name + "=" + fx x + " "
gToString{|PAIR|} fx fy (PAIR x y) = fx x + fy y
gToString{|EITHER|} fx fy (LEFT x) = fx x
gToString{|EITHER|} fx fy (RIGHT y) = fy y
gToString{|CONS of {gcd_name}|} fx (CONS x) = gcd_name + fx x
gToString{|OBJECT|} fx (OBJECT x) = fx x

instance + String where + s t = s +++ t

Start w = doTasks (changeNameEdcomb student) w

enterStudent :: Task Student
enterStudent = enterInformation "Enter a student" []

enterStudentList :: Task [Student]
enterStudentList = enterInformation "Enter a student" []

updateStudent :: (Student -> Task Student)
updateStudent = updateInformation "Update a student" []

selectStudent :: ([Student] -> Task Student)
selectStudent = enterChoice "Pick a student" []

selectStudentOnlyName :: ([Student] -> Task Student)
selectStudentOnlyName = enterChoice "Pick a student" [ChooseFromDropdown \{Student|name}->name]

selectStudentFormat :: ([Student] -> Task Student)
selectStudentFormat = enterChoice "Pick a student" [ChooseFromDropdown gToString{|*|}]

selectPartner :: ([Student] -> Task [Student])
selectPartner = enterMultipleChoice "Pick a partner" [ChooseFromDropdown \{name,bama}->name + "(" + gToString{|*|} bama + ")"]

changeName :: Student -> Task Student
changeName s
	=   viewInformation "Student to change" [] s
	||- updateInformation "New name" [UpdateAs (\{Student|name}->name) (\s n->{Student | s & name=n})] s

changeNameEdcomb :: Student -> Task Student
changeNameEdcomb s
	=   updateInformation "New name" [UpdateUsing id (\_ v->v) nameEditor] s
where
	nameEditor :: Editor Student
	nameEditor = bijectEditorValue
		(\{name=n,snum=s,bama=b,year=y}->(n, s, b, y))
		(\(n,s,b,y)->{name=n,snum=s,bama=b,year=y})
		(container4
			(gEditor{|*|} <<@ labelAttr "name")
			(withChangedEditMode toView gEditor{|*|} <<@ labelAttr "snum")
			(withChangedEditMode toView gEditor{|*|} <<@ labelAttr "bama")
			(withChangedEditMode toView gEditor{|*|} <<@ labelAttr "year")
		)

	toView (Update a) = View a
	toView v = v
