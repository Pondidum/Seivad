Seivad is a lightweight DI (Dependency Injection) container for .Net

Todo List:
=========

1. Parameters
----------
* named parameters

2. Parameter Resolving
-------------------
* Depends on Parameters
* checks to see if any unsupplied parameter can be created
* recursive? eg: A requires B which requires a C and Detc.
* how to deal with cyclicity - A requires B which requires A 

* when there is no public ctor 
 * throw an exception
  
* when there are arguments passed in, and there is only a default constructor
 * throw an exception
 
* when there are no arguments passed in, and there is a default constructor
 * use the default ctor
  
* when there are no arguments passed in, and there is no a default ctor
 * resolve dependencies on the other ctors

* when there are mutliple ctors with the same arguments in a different order
 * it should pick the matching ordered one
 
* when no args are passed, there is a default ctor and there is a parameter we can resolve
 * it should use the default ctor --should there be a preference setable?
 
``` 
Constructors: None Available, Default, Parametered
Arguments: Zero, Many
Resolvables: Zero, Many

C		A		R		Result
------------------------------
N		Z		Z		No Constructor Exception
N		Z		M		No Constructor Exception
N		M		Z		No Constructor Exception
N		M		M		No Constructor Exception
D		Z		Z		Use Default Ctor
D		Z		M		Use Default Ctor
D		M		Z		Unused Argument Exception?
D		M		M		Unused Argument Exception?
P		Z		Z		Unspecified Argument Exceptions
P		Z		M		Use matching Ctor
P 		M		Z		Use matching Ctor
P 		M		M		Use matching Ctor
```

```
Constructors			: Default, Parameterised, Both
Arguments				: Zero, Partial, Exact
Resolveable (remaining)	: Zero, Partial, All


Order of Presedence:
Handle no constructors
Handle Default Constructor and Zero Arguments
Handle Parameterised Constructor, with exact arguments
Handle Parameterised Constructor, with partial arguments and resolved arguments
Handle Parameterised Constructor, with partial arguments and no resolved arguments
Handle Parameterised Constructor, with no arguments, and no resolved arguments
Handle Parameterised Constructor, with no arguments, and resolved arguments
```


 
3. Object creation of unregistered types 
----------------------------------------
* Depends on parameter resolving
* `.GetInstance<frmMain>()`
* `frmMain` would need a blank constructor

4. Convention resolving 
-----------------------
* Register("I*View").Return("frm\1View") etc
* Possible resolving based on two object names:
  `.Register<IMainFormView>().Return<frmMainFormView>().InferConvention();`
* This would see that `MainForm` is the changeable part
  * It could require certain suffixes and prefixes defined (eg I and View)

5. Configuration save & load
----------------------------
* saving output to some kind of key value pair
* JSON doc maybe?

