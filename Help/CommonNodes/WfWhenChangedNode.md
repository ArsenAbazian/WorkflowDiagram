### WfWhenChangedNode
The WfWhenChangedNode class used to detect wether input value was changed and pass execution to the one of the branches: 'True' if changed or 'False' if not.

![WfWhenChangedNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/WhenChanged.png)

#### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

**In** - Input value

#### Output Points

**Yes** - Connection point for branch when input value changed.

**No** - Connection point for brunch when input value not changed.

