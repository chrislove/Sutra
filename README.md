<img src="SutraLogo.png?raw=true" alt="Sutra" width="311px" height="81px"/>

# Functional C# Library & Pipe Forward Operator

## Introduction
Sutra is a functional library for C#. At its core is Pipe Forward Operator implementation.

C# 6.0 or higher is required to make use of Sutra.

One of the major limitations of the C# language is the lack of built-in function call chaining. A proposal has been added to add native support for [Pipe-forward operator](https://github.com/dotnet/csharplang/issues/96) to C#; however the implementation is most likely years away from now.

Sutra is trying to address this shortcoming by providing a working implementation of the pipe forward operator.

Sutra doesn't rely on dynamic functionality, and therefore can be used on AOT platforms (iOS/WebGL). Sutra is tested to work with Unity game engine.

### How it all started?
Sutra started as a small proof-of-concept that function calls can be chained in C# using the bitwise or (|) operator. The library then grew larger to support various functional transformations.

## Table Of Contents
* <a href="#quick-start">Quick Start</a>
* <a href="#sutra-types">Sutra Data Types</a>
* <a href="#commands">Commands</a>
* <a href="#seq-commands">Seq Commands</a>
* <a href="#conditions">Conditions</a>
* <a href="#currylib"></a>Curried Functions Library
* <a href="#other-info"></a>Other Information
* <a href="#contribution">Contribution</a>
* <a href="#license">License</a>

## <a id="quick-start"></a>Quick Start

### Starting a pipe
The first step is to start a pipe. The following example creates a pipe containing the string value "SUTRA".

'''
var pipe = start.pipe | "SUTRA";
'''

Simplified pipe initialization using 'start.pipe' is only supported for a handful of built-in types. For everything else the following variation should be used:
'''
var pipe = start.pipe.of<T>() | obj;
'''

A pipe can also be initialized with an existing object in the following manner:
'''
var pipe = start.pipe.with(obj);
'''


### Starting a sequence
To initialize a sequence with a set of objects:
'''
var seq = start.seq | Enumerable.Range(0, 3);
'''

### Retrieving the contents of a pipe.
Using the 'get' command will return the Option<T> value contained within the pipe. To return the actual object use the '!get' command. The exclamation mark modifier '!' instructs the get command to return the actual value, and is unsafe (will throw an exception if the pipe is empty).

'''
string str = start.pipe | "SUTRA" | !get;
'''

### A simple example

The following example gets the current date, subtracts one day, converts it to a short date string, and then writes the resulting value to the console:
'''
Func<DateTime, DateTime> AddDays( int days ) = d => d.AddDays(days);
PipeFunc<DateTime, string> getShortDate => fun((DateTime d) => d.ToShortDateString());
Action<string> write => i => Console.WriteLine(i);

var yesterday = start.pipe | DateTime.Now
                           | AddDays(-1)
                           | getshortdate
                           | (i => "Yesterday: " + i);
                           | tee | write;
'''

### Another example using Seq<T>

The following example generates integer values [0, 1, 2], converts them to a string and then concatenates the values into a single string.

'''
PipeFunc<int, string> ConvertToString => fun((int i) => i.ToString());

string str = start.seq
            | Enumerable.Range(0, 3)
            | map | ConvertToString
            | concat | !get;
'''

## <a id="sutra-types"></a>Sutra Data Types

### Pipe<T>
'Pipe<T>' is a core type in Sutra. It acts as a wrapper to allow chaining function calls.

### Seq<T>
'Seq<T>' is very similar to 'Pipe<T>', however it is being used to chain together function calls operating on collections.

### Option<T>
Option<T> is an optional data type that may or may not contain a value.

Use 'get' or 'getor' to retrieve a value:
'''
int valueA = option | get;          // Unsafe
int valueB = option | getor(10);    // Safe
int valueB = option | 10 | get;     // Safe, same as above
'''


'Option<T>' can be created implicitly from any object:
'''
Option<int> intOption = 10;
'''

To create an empty 'Option<T>':
'''
Option<DateTime> emptyOptionA = default; // Using C# 7.0 syntax.
Option<DateTime> emptyOptionB = none<DateTime>(); // Using the none command.
'''

There's support for the OR '|' operator for options. If the option on the left is empty, then the alternative value will be returned:
'''
Option<int> emptyOption = default;
Option<int> nonEmptyOption = emptyOption | 10;
'''

'Option<T>' can be converted to 'Some<T>' using the 'some' command. This operation is unsafe and will throw an exception if the option is empty.
'''
Option<int> option = 10;
Some<int> someInt = option | some;
'''

Calling 'Option<T>.ToString()' (even implicitly) is not allowed because the option is not guaranteed to contain a value. This will throw an exception.

### SeqOption<T>
'SeqOption<T>' wraps a set of 'Option<T>' objects. 'SeqOption<T>' is a shortcut for 'Option<IEnumerable<Option<T>>>'.

### Some<T>
'Some<T>' is a version of Option<T> that is guaranteed to contain a value.

### str
'str' is a shortcut for 'Option<string>' with a few nice features to simplify string manipulation.

'str' can be converted to 'somestr' using the 'some' command. This operation is unsafe and will throw an exception if the str is empty.
'''
str testStr = "test"; 
somestr testSomestr = testStr | some;
'''

There's support for the OR '|' operator for str. If the str on the left is empty, then the alternative value will be returned:
'''
str emptyStr = default;
str 
'''


Calling 'str.ToString()' (even implicitly) is not allowed because the str is not guaranteed to contain a value. This will throw an exception.

### somestr
'somestr' is a string that is guaranteed to contain a value. 'somestr' should never be set to 'default'.

'somestr' can be implicitly assigned from 'string'. However this operation is unsafe, and is only present for converting constant strings into somestr.

'''
somestr somestrA = "test"; // Ok
somestr somestrB = "test" | some; // Same as above

string testStr = "test";
somestr somestrC = testStr; // Allowed, but NOT recommended.
'''

'somestr' strings can be combined just like regular strings:
'''
somestr somestrB = somestrA + " test ";
somestr somestrC = somestrB + somestrA;
'''

'somestr' can be implicitly converted to 'string':
'''
string str = somestrA;
'''


Unlike 'str', it is allowed to call 'somestr.ToString()'. It is overriden to return the containing value.

### PipeFunc<TIn, TOut>
A wrapper around System.Func that allows the pipe to change its type after transformation.

The following example converts Pipe<DateTime> to Pipe<string>:
'''
PipeFunc<DateTime, string> getShortDate => fun((DateTime d) => d.ToShortDateString());

string yesterdayStr => dateTimePipe | getShortDate
                                    | (i => "Yesterday: " + i)
                                    | !get;
'''

## <a id="commands"></a>Commands
Commands constitute the core of the Sutra framework. To make use of the Commands you should import them first: 'using static Sutra.Commands;'

### fail
Throws an exception if the condition on the right is met. The 'fail'' command is always used alongside the 'when' command.
'''
    pipe | fail | when | strf.contains("test");
    seq  | fail | when | isempty;
    seq  | fail | when | notsingle;
    seq  | fail | new PipeUserException("Something bad happened") | when | any(isempty);    // Using a concrete exception.
'''

### failwith
Throws an exception with a given message if the condition on the right is met. A '$pipe' placeholder can be used to include the contents of the failing pipe.

'''
pipe | failwith("Something bad happened: $pipe") | when | (() => true);
'''


### fun
Used for creating a PipeFunc from a System.Func.
'''
PipeFunc<DateTime, string> getShortDateFunc = fun((DateTime d) => d.ToShortDateString());
'''

### get
Used for retrieving the value contained within the pipe. Can be used with the **!** modifier to retrieve the actual value (unsafe).

#### For Pipe<T>
| Command | Returned Value Type | Safe? |
| --- | --- |
| get | Option<T> | Safe |
| !get | T | Unsafe |

#### For Seq<T>
| Command | Returned Value Type | Safe? |
| --- | --- |
| get | SeqOption<T> | Safe |
| !get | Option<IEnumerable<T>> | Safe |
| !!get | IEnumerable<Option<T>> | Unsafe |
| !!!get | IEnumerable<T> | Unsafe |

'''
IEnumerable<T> enm = seq | !!!get;
'''

#### For Option<T> and str
| Command | Returned Value Type | Safe? |
| --- | --- |
| get | T | Unsafe |

'''
Option<int> opt = 10;
int value = opt | get;
'''

### getor
A safe alternative to 'get' that can be used with 'Option<T>' or 'str'

'''
int valueB = option | getor(10);
'''
    
### map
Projects each element of a sequence into a new form using a function on the right. Equivalent to Select() in LINQ.

'''
string result = start.seq | Enumerable.Repeat("A", 3)
                          | map | (i => $"[{i}]")
                          | concat | !get;
                          
// result is "[A][A][A]"
'''

### mapf
Projects each element of a sequence into a new form using a function. Equivalent to Select() in LINQ.

Similar to 'map', but 'mapf' takes a generic Func as a parameter.

'''
Func<int, string> toStringFunc = i => i.ToString();

string str = start.pipe
             | 10
             | mapf(toStringFunc)
             | !get;
'''

### none<T>()
Equivalent to default(Option<T>)

'''
Option<int> emptyOption = none<int>();
'''

### opt
Converts a value to Option<T>. Currently supported only for a handful of built-in types.

'''
Option<int> option = 10 | opt;
str testStr = "test" | opt;
''' 

### or
This command is used to replace the contents of the pipe if the pipe is empty.

'''
start.pipe | (string) null | or("ALT")
'''
        
### put
Replaces contents of the pipe with a new value of a different type.
'start.pipe | "A" | put(0)'
        
### some
Converts a value to Some<T>. Unsafe, will throw if the value is null or an empty Option<T>.
'''
somestr somestr = "SUTRA" | some;
Some<int> someInt = 10 | some;
'''
    
### start.pipe
Starts a new pipe.

'''
Pipe<int> intPipe       = start.pipe | 10;
Pipe<string> stringPipe = start.pipe | "test";
Pipe<MyType> myPipe     = start.pipe.of<MyType>() | obj;
'''
        
### start.seq
Starts a new sequence.

'''
Seq<string> stringSeq = start.seq | Enumerable.Repeat("A", 3);
Seq<MyType> mySeq     = start.seq.of<MyType>() | Enumerable.Repeat(obj, 3);
'''
    
### tee
Performs an action on the contents of the pipe.

'''
Action<string> write = i => Console.WriteLine(i);
var pipe = start.pipe | "SUTRA"
                      | tee | write;
'''
        
### teef
Same as 'tee', but uses a Unit function instead of an action.
    
### to.seq

Converts a pipe to a sequence using the supplied function.
'''
Func<int, IEnumerable<string>> converter => inval => Enumerable.Range(0, 3)
                                                              .Select(i => inval + i)
                                                              .Select(i => i.ToString());

string result = start.pipe | 100
                | to.seq(converter)
                | strf.join(";") | !get;
                
// result is "100;101;102"
'''

### when
Evaluates to true if the pipe or sequence contents match the condition on the right.

'''
Seq<string> seq = start.seq | paths
                  | fail | when | any( not(pathf.ispathrooted) );
'''

### where
Filters a sequence or a pipe.

'''
str emptyStr = start.pipe | "TEST"
              | where | (i => i == "ABC")
              | get;
           
           
string result = start.seq | new[] {"A", "B", "C"}
                                | where | notequals("B")
                                | where | (i => !i.Contains("B"))
                                | concat | !get;
                                
// result is "AC"
'''      
 

## <a id="seq-commands"></a>Seq Commands
There's a handful of commands that can only be applied to 'Seq<T>':

### append
Appends new items to a sequence.

'''
IEnumerable<string> defEnumerable = new[] {"D", "E", "F"}.Select(i => i);
Seq<string> xyzSeq = start.seq | new[] {"X", "Y", "Z"};


Seq<string> testSeq = ABCSeq
                        | append | defEnumerable
                        | append | new[] {"G", "H", "I"}
                        | append | xyzSeq;
                        | append | "@";
'''

### collect
Projects each element of a sequence and flattens the resulting sequences into one sequence. Equivalent to SelectMany() in LINQ.

'''
IEnumerable<string> func( string str ) => Enumerable.Repeat(str, 3);

string result = start.seq | new[] {"A", "B", "C"}
                | collect | func
                | strf.concat | !get;

// result is "AAABBBCCC"
'''

### collectf
Projects each element of a sequence and flattens the resulting sequences into one sequence. Equivalent to SelectMany() in LINQ.

Similar to 'collect', but uses a function supplied as a parameter.

'''
Func<int, IEnumerable<string>> func = i => Enumerable.Repeat(i.ToString(), 3);

str str = start.seq | Enumerable.Range(0, 3)
          | append | default(Option<int>)
          | collectf(func)
          | concat | get;
'''

### distinct
Filters sequence to return distinct values.

### first
Returns the first item of a sequence.

### getarray
Converts the contents of sequence into T[] and returns. Unsafe.

### getlist
Converts the contents of a sequence into List<T> and returns. Unsafe.

### iter
Performs an action on each element of a sequence.

'''
Action<string> write => i => Console.WriteLine(i);

Seq<string> strSeq = start.seq | Enumerable.Range("A", 3)
                        | iter | write;

'''
        
### single
Returns a single value from a sequence or throws an excepton.

'''
string result = start.seq | new[] {"A", "B", "C"}
                    | where | equals("B")
                    | single | !get;
'''

## <a id="conditions"></a>Conditions
Conditions specify the circumstances under which a command is executed. To make use of the Conditions you should import them first: 'using static Sutra.Conditions;'
### any
Evaluates to true if any value within the sequence matches the predicate.
'seq | fail | when | any(isempty);'
        
### isempty
Evaluates to true if the pipe or sequence is empty.
        
### notempty
Evaluates to true if the pipe or sequence is not empty.
    
### issingle
Evaluates to true if the sequence contains a single element.
    
### notsingle
Evaluates to true if the sequence contains zero or more than one elements.

### equals
Evaluates to true if the value within the pipe is equal to obj.

### notequals
Evaluates to true if the value within the pipe is not equal to obj.

### not
Inverts a condition
'seq | fail | when | any( not(pathf.ispathrooted) );'
    
## <a id="currylib"></a>Curried Functions Library
Sutra includes a library of curried functions that can be used for chaining function calls together.

Currently it only includes two classes:
* pathf - curried functions for System.IO.Path
* strf - curried functions for System.String

### Example

The following example will concatenate a string array into a single string:
'''
string result = start.seq | stringArray
                          | strf.concat  | !get;
'''  

## <a id="other-info"></a>Other Information
The library is under active development, therefore some breaking changes should be expected.

## <a id="contribution"></a>Contribution
Everyone is welcome to contribute to the project. Simply fork the project, make the changes, and then create a pull-request.

### Unit Tests are needed for:
* str
* somestr

XML documentation can also be improved.

## <a id="license"></a>License
    MIT License

    Copyright 2017 Ilya Suzdalnitski

    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.