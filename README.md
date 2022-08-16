## Workflow Diagram -- Visual Programming

Workflow Diagram is used to create nodes with operations, and
connections between them, and then use runner which visits these nodes
and execute operations, associated to these nodes. Workflow diagram
comes with Visual Designer written for Windows Forms.

![](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/media/image1.png)

Simple example:
```csharp
WfDocument doc = new WfDocument();
WfConstantValueNode const1 = new WfConstantValueNode(1.0);
doc.Add(const1);
WfConstantValueNode const2 = new WfConstantValueNode(2.0);
doc.Add(const2);
WfConditionNode cond1 = new WfConditionNode(WfConditionalOperation.Equal);
doc.Add(cond1);

const1.Outputs[0].ConnectTo(cond1, "In1");
const2.Outputs[0].ConnectTo(cond1, "In2");

WfStorageValueNode stTrue = new WfStorageValueNode("Condition True");
doc.Add(stTrue);
WfStorageValueNode stFalse = new WfStorageValueNode("Condition False");
doc.Add(stFalse);

cond1.Connect("False", stFalse, "Run");
cond1.Connect("True", stTrue, "Run");

WfRunner runner = new WfRunner(doc);
bool result = runner.RunOnce(doc);
```
In this example we create two nodes, containing double constant value 1
and 2. Then we connect outputs of these nodes to node which has two
inputs and makes logical operation on values receiving through its
inputs. Then each output of this node connects to node, which saves
result of the operation to the global storage. Here is the visualization
of this workflow:

![Graphical user interface Description automatically
generated](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/media/image2.png)

Of course, this is very simple example that has a less logic, but you've
got the idea.

You can easily create your own nodes, define inputs which can accept
this node, and outputs that this node can produce. The base class from
your can create your own descendants is the WfNode class. However, if
you wish your node support Visual Designer for Windows Forms, please use
the WfVisualNodeBase class.

There is already exists a set of nodes WorkflowDiagram.Nodes.Base, which
you can use to make common arithmetic and logical operations and write
your custom script to operate on input values. You can consider this set
of nodes as source for visual programming. Though this set will be
constantly expanded, you already can use the following nodes:

WfAbortNode -- immediately stops the execution and returns true or false
value.

WfChangeNode -- check if input value was changed and pass execution to
one of the outputs: 'yes' or 'no'.

WfCollectionNode -- use it's input item to execute one of the following
operations: add (add item to collection if collection did not hold this
item), remove (remove value to collection if collection holds item),
reset (reset collection).

WfConditionNode -- performs one of the logical operations: and, or,
equal, not equal, less, less or equal, greater, greater or equal on its
two input values. Then depend on result, pass execution to one of its
branch output 'true' or false

WfConstantValueNode -- defines constant value of one of following types:
decimal, string or boolean.

WfForEachNode -- receives collection as input value and pass each item
in this collection to its subtree, connected to its ForEach output.
Think of it as foreach statement in c\#.

WfIndexerNode -- get item from collection by index, defined in this node
and pass it to next nodes. Supports: first, last and item by index.

WfRepeatNode -- repeat subtree execution by N times defined in this
node. Think of it as for statement in c\#.

WfScriptNode -- receives up to 6 input values and pass them through c\#
script. Used as base for arithmetic operations.

WfStorageValueNode -- allows you to store value in global storage and
get value from it.

WfSwitchNode -- analog of switch statement in c\#. Allows you to define
a set of values and connect subtree to them. Input value will compare to
the each of defined values and if it equals to the specific value the
execution will pass to the brunch connected to this value.

Each of these nodes can be created and designed in Workflow Diagram's
Visual Designer.

## Visual Designer

Visual Designer is the descendant of the UserControl class, which you
can use in your project, which allows your end-users to create and
execute workflow diagram.

Heare are the examples

You created your own set of nodes, which allows to make algorithmic
trading on crypto exchanges. Or you created advanced arithmetic
operation nodes, which operates on time series and visualizer the
operation results. Or your app allows to create application scheme and
generates application based on this scheme. Or you want to create
application which allows to create procedure generated landscapes.

The visual editor adds to your project a flexible customization and
design.

Here is my example. I created CryptoTradingFramework application which
supports Strategies. I created a set of nodes which allows my user
creates their own strategy, test them on historical data and, using only
visual design (ok, I not finished it yet ). 

![](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/media/image3.png)
