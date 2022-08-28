## WfForEachNode
The WfForEachNode class used to execute sub-branch, connected to the 'ForEach' output connection point, for each item in collection.
Each time in loop, node gets next item in the collection, passes it to the sub-branch, connected to the 'ForEach' connection point.

![WfForEachNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/ForEach.png)

### Input Points

**In** - Input collection object. Passed to the 'Out' connection point.

### Output Points

**Out** - Input collection object.

**Foreach** - Branch, which will be executed for every item in collection.

