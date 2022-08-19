The WfNode class is the base class for all the nodes you create. The WfNode class has only one input connection point, called "Run". Even there is no other input connections you can connect output from previous node to the "Run" input, and pass execution to it. You can also consider "Run" input connection as Enabled property. 

##WfNode Members

### Methods
```charp
void Assign(WfNode node)
```
Assign all the properties from specified node

```charp
CheckDependency(int visitIndex) 
```
Determines whether all the output connection points from the all previous connected nodes were visited (or skipped) by Runner. Node will not be executed until all the nodes on which it depend are visited.

```charp
object Clone()
```
Create a copy of the node

```csharp
void Connect(string outputName, WfNode node, string inputName)
```
Connect output point with the name specified in outputName parameter to the node's input point, specified in inputName parameter.

```csharp
string ConstrainStringValue(string value)
```
Trim string value and if it null, replace with string.Empty

```csharp
virtual WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type)
```
Creates WfConnectionPointObject with point type (In,Out) specified in type parameter. This method can be overrided in WfNode descendant to create specific connection point.

```csharp
virtual object CreateImage()
```
Create an object which can be threated as image (in Win or Web), and used to identify WfNode in toolbar list. 

```csharp
virtual WfConnectionPointCollection CreateInputCollection()
```
Creates collection object which holds node's input connection points. This method can be overrided in descendant classes, to return specific collection

```csharp
virtual WfConnectionPointCollection CreateOutputCollection()
```
Creates collection object which holds node's output connection points. This method can be overrided in descendant classes, to return specific collection

```csharp
abstract List<WfConnectionPoint> GetDefaultInputs()
```
Create input connection points for node. This method is abstract, and should be overriden in descendant classes.

```csharp
abstract List<WfConnectionPoint> GetDefaultOutputs()
```
Create output connection points for node. This method is abstract, and should be overriden in descendant classes.

```csharp
List<WfConnector> GetInputConnectors()
```
Get all input connections for node.

```csharp
List<WfNode> GetNextNodes()
```
Get all output nodes connected to the node.

```csharp
List<WfNode> GetNodesFromVisitedPoints(int visitIndex)
```
For specific nodes, depend on input values and other parameters some output connections should be processed and some should be skipped. This method returns all next nodes connected to output points, that should be processed.

```csharp
List<WfConnector> GetOutputConnectors()
```
Get all output connections for node.

```csharp
bool IsVisited(int visitIndex)
```
Determines if node was visited by WfRunner. Each time WfRunner visit node, it marks node by setting the WfNode.VisitIndex property. The value specifed in the visitIndex parameter holds this mark.

```csharp
virtual void OnEndDeserialize()
```
This method called after WfNode deserialized from xml file. This is good place to made additional initializations for your custom node.

```csharp
abstract bool OnInitializeCore(WfRunner runner)
```
This method is called when WfRunner initialize all nodes before executing document and start visiting nodes. You can do additional initializations for your custom node.

```csharp
virtual void OnPointAdded(WfConnectionPoint point)
```
This method called when WfConnectionPoint added to the Inputs/Outputs point collections. 

```csharp
virtual void OnPointRemoved(WfConnectionPoint point)
```
This method called when WfConnectionPoint removed from the Inputs/Outputs point collections. 

```csharp
protected internal void OnPropertyChanged(string name)
```
Call this method when some custom property of your custom node is changed.

```csharp
virtual void OnRemoved()
```
This method is called when node removed from WfDocument.Nodes collection.

```csharp
void OnVisit(WfRunner runner)
```
This method is called when WfRunner visit node. Call this method if you implement your own WfRunner descendant and overrides RunOnce or Run methods. This method is not for override.

```csharp
abstract void OnVisitCore(WfRunner runner)
```
Override this method in your custom node. This method is called when WfRunner visits node. In it can execute it's custom operations.

```csharp
void Reset()
```
This method is called by the WfRunner before initialize nodes. 

```csharp
virtual void ResetCore()
```
Override this method to make additional reset logic for your custom node.

### Properties
```csharp
virtual string Category
```
This property returs category name which node is belongs to. All nodes will be grouped in nodes toolbar by Category.

```csharp
object DataContext
```
Additional data that associated with this node.

```csharp
string Description
```
Nodes description.

```csharp
List<WfDiagnosticInfo> Diagnostic
```
Collection of diagnostic messages (errors, warnings, information) which can happen during initialization and execution.

```csharp
string DisplayText
```

```csharp
string Header
```
Text displayed in the node's header area.

```csharp
WfDocument Document
```
This property contains document which node belongs to.

```csharp
bool Enabled
```
Returns value showing wether node is Enabled.

```csharp
bool HasErrors
```
Return true if Node contains errors in the Diagnostic property.

```csharp
bool HasInputConnections
```

```csharp
bool HasOutputConnections
```

```csharp
float Height
```
Height of the node

```csharp
float Width
```
Width of the node

```csharp
float X
```
X coordinate of the node

```csharp
float Y
```
Y coordinate of the node

```csharp
Guid Id
```
Nodes unique identifier. Initialized in the constructor.

```csharp
object Image
```
Object that represents and image for the node. Used in the node's toolbox

```csharp
WfConnectionPointCollection Inputs
```
Input connection points

```csharp
bool IsInitialized
```
Determines if node is initialized by WfRunner

```csharp
bool IsStartNode
```
Determines if node is start node, i.e. has no dependencies from other node. Such nodes executed first by the WfRunner

```csharp
string Name
```
Node's name.

```csharp
int OrderIndex
```
Node's order index. If multiple nodes can be executed at some time, you can manipulate execution order by settings this property.

```csharp
WfConnectionPointCollection Outputs
```
Output connection points.

```csharp
BindingList<WfConnectionPoint> Points
```
Returns all the input and output connection points.

```csharp
string Text
```

```csharp
string Type
```
Node's type. Consider it as a class name or type name.

```csharp
int VisitIndex
```
Node's visitIndex. When WfRunner visit node, it marks it by settings this value. Next time WfRunner visit node with VisitIndex higher that previous. 

```csharp
string VisualTemplateName
```
Name representing visual template for your custom node.



