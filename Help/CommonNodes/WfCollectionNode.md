## WfCollectionNode
The WfCollectionNode class used make standard operations on collection and collection item, such as: add, remove, insert, clear.

![WfCollectionNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Collection.png)

### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

**Item** - input object, on which 'add' or 'remove' collection operation will be executed. This object will be passed to the 'Item' output connection point.

**Collection** - input collection object. This object will be passed to the 'Collection' output connection point.

### Output Points

**Item** - The same input object

**Collection** - The same input collection object

### Properties

**WfCollectionOperation Operation** - specify operation: 

AddLast - Add object in 'Item' to the 'Collection'.
AddFirst - Insert object in 'Item' in the 'Collection' start.
Remove - Remove object in 'Item' from the 'Collection' if contains.
Clear - Clear 'Collection'