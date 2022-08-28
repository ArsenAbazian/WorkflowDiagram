## WfIndexerNode
The WfIndexerNode class used to get item from collection by index.

![WfIndexerNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Indexer.png)

### Input Points

**In** - Input collection object

**Item** - Input object item, which will be set to the collection.

### Output Points

**Item** - Contains item from collection by index specified in 'Index' property.

### Properties

**WfAccess Access** - Access type. Can be one of the following: 
- First - get or set item at 0 position in collection.
- Last - get or set item at last position in collection.
- Index - get or set item by index, specified int he 'Index' property

**int Index** - Specifies position in collection. 
