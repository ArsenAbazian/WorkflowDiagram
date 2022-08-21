The WfDocument class contains information about flowchart. It contains two main collections: 
1.	Nodes: a collection of nodes 
2.	Connectors: a collection of connectors that connect nodes to each other.
The WfDocument class can save its content to a file and load it from a file. You can use Save() and Load() methods for this purposes.

## WfDocument members

### Methods

```csharp
WfNode AddNode(WfNode node)
```
Add node to document's nodes collection.

```csharp
WfConnector AddConnector(WfConnector c)
```
Add connecto to the document's connectors collection

```csharp
Clear()
```
Remove all nodes and connectors from the document.

```csharp
WfConnectionPoint FindConnectionPoint(Guid pointId)
```

```csharp
WfConnector FindConnector(Guid connectorId)
```

```csharp
List<Type> GetAvailableNodeTypes()
```
Search in all referenced assemblies and collect all the WfNode descendant classes and return them in list.

```csharp
List<WfNode> GetAvailableToolbarItems()
```
Return all nodes that can be used in the Visual Designer's toolbar. Visual Designer get this list from dociment and initialize it's toobar with this list. Then the nodes from the toolbar can be added to document via the drag-n-drop operation. 

```csharp
List<WfNode> GetStartNodes()
```
Return all nodes from document which has not input connectors. These nodes will be executed first.

```csharp
List<WfNode> GetEndNodes()
```
Return all nodes which has no output connectors. 

```csharp
bool Load(string fileName)
```
Load document's content from file

```csharp
protected OnPropertyChanged(string name)
```
Call this method in your own WfDocument descendant class, when one of it's custom property is changed.

```csharp
InitializeVisualData()
```
This method is called when nodes load in the Visual Editor's nodes toolbar.  During this method the QueryVisualData event is raised for each node.

```csharp
RemoveNode(WfNode node)
```

```csharp
RemoveUnusedConnectors()
```
Remove all connectors that has no connections to the node's points.

```csharp
Reset()
```
Clear diagnostic messages, data, nad calls Reset methods for each node and connector. Used to prepare document for new initialization and execution.

```csharp
Save()
```
Save document's content to a file, specified in the WfDocument.FullPathName property.

```csharp
Save(string fullPath)
```
Save document's content to a file, spceified in the fullPath parameter.


### Properties
```csharp
List<WfConnector> Connectors
```

```csharp
List<WfDataInfo> Data
```
This property is for internal use.

```csharp
WfDiagnosticHelper DiagnosticHelper
```
The DiagnosticHelper property contains object, representing diagnostics information: warnings, errors, which happens during initialization and execution. 

```csharp
List<WfDiagnosticInfo> Diagnostics
```
Diagnostics messages from DiagnosticHelper class

```csharp
string FileName { get; set; }
```
Document's file name

```csharp
int FontSizeDelta
```
This property is for internal use

```csharp
string FullPath
```
Document's full path name

```csharp
Guid Id
```

```csharp
string Name
```
Name of the document. Can differs from FileName :)

```csharp
WfNodeCollection Nodes
```
The nodes collection

```csharp
IWfDocumentOwner Owner
```

### Events
```csharp
Loaded
```
Document loaded it's content from file

```csharp
INotifyPropertyChanged.PropertyChanged
```
Raises when document's property changed. 

```csharp
WfNodeEventHandler QueryNodeVisualData
```
Raises when an additional visual data should be initialized for the node.

```csharp
Save
```

