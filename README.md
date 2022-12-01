# FiniteStateMachine
This is an example project for Unity showcasing a finite state machine using MonoBehaviors as states
orchestrated by FiniteStateMachine component.

Take a look at the NPC prefab to see how it's setup and you can see the scripts in the scripts folder.

To create a new state you inherit from the State component and override the virtual methods as needed.

The FSM features CanEnter and CanExit hooks to prevent a state from being entered or exited.  
The StunnedState for example will prevent the agent from leaving the state while the stun timer is still running.
