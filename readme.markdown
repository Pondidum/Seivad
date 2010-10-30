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

