module a7

import StdEnv

import qualified Data.List as List
import Data.Func
import Control.Applicative

import iTasks

:: Job =
	{ id        :: Int
	, jobName   :: String
	, skills    :: [Skill]
	, blocked   :: Bool
	, children  :: [Job]
	}
:: Skill = Programming | Design | Testing
:: Worker =
	{ username :: String
	, password :: String
	, skills   :: [Skill]
	, loggedIn :: Bool
	}
instance == Worker where == w1 w2 = w1.Worker.username == w2.Worker.username
instance == Skill where == s1 s2 = s1 === s2
instance == Job where == s1 s2 = s1.jobName == s2.jobName
derive class iTask Worker, Skill, Job
derive JSONEncode Set
derive JSONDecode Set
derive gText Set
derive gEditor Set
derive gDefault Set

usersStore :: Shared [Worker]
usersStore = sharedStore "Users" [{Worker|username="mart",password="mart",loggedIn=False,skills=[Design,Testing]}]
userStore :: String -> Shared Worker
userStore u = mapReadWriteError
	(\wl->case filter (\w->w.Worker.username == u) wl of
		[u] = Ok u
		u = Error (exception ("worker not found: " +++ toSingleLineText u))
	,\nw wl->Ok $ Just [nw:filter (\w->w.Worker.username <> u) wl]
	) usersStore

jobsStore :: Shared [Job]
jobsStore = sharedStore "Jobs"
	[{id=1,jobName="Testing",skills=[Testing],children=[],blocked=False}
	,{id=2,jobName="Bigjob",skills=[Testing],blocked=False,children=
		[{id=3,jobName="subjob1",skills=[Testing],children=[],blocked=False}
		,{id=4,jobName="subjob1",skills=[Testing],children=[],blocked=False}
		]}
	]	
jobStore :: Int -> Shared Job
jobStore i = mapReadWriteError
	(findJob i
	,\nj jl->Ok $ Just $ map (updateJob nj) jl
	) jobsStore
where
	findJob i [] = Error (exception "Job not found")
	findJob i [j=:{Job|id}:_] | id == i = Ok j
	findJob i [j:js] = case findJob i j.Job.children of
		Error e = findJob i js
		Ok u = Ok u

	updateJob nj j=:{Job|id} | id == i = nj
	updateJob nj j = {Job|j & children=map (updateJob nj) j.Job.children}

removeJob :: Int [Job] -> [Job]
removeJob i [] = []
removeJob i [j=:{Job|id}:js] | i == id = js
removeJob i [j:js] = [{Job|j & children=removeJob i j.Job.children}:removeJob i js]
	
jobID :: Shared Int
jobID = sharedStore "JobIDS" 4

Start w = doTasks ((login -||- register <<@ ArrangeWithTabs False) >>- work) w

register :: Task Worker
register = tune (Title "Register") 
	$ enterInformation "Enter credentials" [EnterUsing id credentialEditor]
	-&&- enterInformation "Enter Skills" []
	>>= \((u, p),s)->get usersStore
	@   filter (\a->a.Worker.username==u)
	>>- \exists->case exists of
		[] = let user = {loggedIn=True,username=u,password=p,skills=removeDup s}
		     in  upd (\l->[user:l]) usersStore >>| treturn user
		_ = viewInformation "User already exists" [] () >>| register

login :: Task Worker
login = tune (Title "Login")
	$ enterInformation "Enter credentials" [EnterUsing id credentialEditor]
	>>= \(u,p)->get usersStore
	@   filter (\a->a.Worker.username==u && a.Worker.password==p)
	>>- \u->case u of
		[] = viewInformation "User not found or incorrect password" [] () >>| login
		[{loggedIn=True}] = viewInformation "User is already logged in" [] () >>| login
		[u] = upd (\w->{w & loggedIn=True}) (userStore u.Worker.username)

credentialEditor = panel2
	(textField <<@ labelAttr "Username")
	(passwordField <<@ labelAttr "Password")

work :: Worker -> Task [()]
work {Worker|username} = allTasks
	[ viewJobs
	, createNewJobs
	, updateSkills
	] <<@ Title ("Welcome " +++ username) <<@ ArrangeWithTabs False
where
	viewJobs
		= tune (Title "View jobs")
		$ editSelectionWithShared "Select a job" False
			(SelectInTree toCN fromSel)
			(jobsStore >*< userStore username)
			(\_->[])
		>^*
			[OnAction (Action "Execute") $ ifValue (\j->j=:[_]) \[j]->
				    upd (\j->{j & blocked=True}) (jobStore j.Job.id)
				>>| viewSharedInformation "Job" [] (jobStore j.Job.id)
				>>*
					[OnAction (Action "Finish") $ always $
						upd (removeJob j.Job.id) jobsStore
					,OnAction (Action "Split") $ always $
						    enterInformation "Enter sub jobs" [EnterUsing id (gEditor{|*->*|} jobEditor gText{|*|} JSONEncode{|*|} JSONDecode{|*|})]
						>>= \sjs->sequence (take (length sjs) (repeat (upd inc jobID)))
						>>= \ids->upd (\j->{Job|j & blocked=False,children=[{Job|id=i,jobName=n,skills=s,blocked=False,children=[]}\\(n,s)<-sjs & i<-ids]}) (jobStore j.Job.id)
						@! []
					,OnAction (Action "Cancel") $ always $
						upd (\j->{j & blocked=False}) (jobStore j.Job.id)
						@! []
					]
			]
		@! ()
	
	toCN ::([Job],Worker) -> [ChoiceNode]
	toCN (jobs, worker) =
		[
			{id=if (job.Job.blocked || not (job.Job.children =: []) || not (matchSkills job)) (~job.Job.id) job.Job.id
			,label=job.Job.jobName +++ "(" +++ toSingleLineText job.Job.skills +++ ")"
			,icon=if (job.Job.blocked || not (matchSkills job)) (Just "document-error") Nothing
			,expanded=True
			,children=toCN (job.Job.children, worker)
			}
		\\job<-jobs
		]
	where
		matchSkills job = 'List'.difference job.Job.skills worker.Worker.skills == []

	fromSel :: ([Job],Worker) [Int] -> [Job]
	fromSel (jobs,w) ids
		=  [j\\j<-jobs | isMember j.Job.id ids]
		++ flatten [fromSel (j.Job.children, w) ids\\j<-jobs]

	createNewJobs
		= tune (Title "Create new jobs")
		$ forever
		$ enterInformation "Enter job" [EnterUsing id jobEditor]
		>>= \(name, skills)->upd inc jobID
		>>= \i->upd ((++) [{Job|id=i,skills=skills,jobName=name,blocked=False,children=[]}]) jobsStore
		@! ()
	
	jobEditor :: Editor (String, [Skill])
	jobEditor = panel2
		(gEditor{|*|} <<@ labelAttr "Name")
		(gEditor{|*|} <<@ labelAttr "Skills")

	updateSkills
		= tune (Title "Edit skills")
		$ updateSharedInformation "Skills" [UpdateAs (\w->w.Worker.skills) (\w s->{Worker|w & skills=s})] (userStore username)
		@! ()
