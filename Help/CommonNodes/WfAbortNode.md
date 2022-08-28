## WfAbortNode
The WfAbortNode class used to stops immediately workflow execution and return specified value.

![WfAbortNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Abort.png)

### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

**In** - Input object, the result object from previous node's.

### Output Points

**ResultCode** - The result value which will be returned from WfRunner.RunOnce or WfRunner.Run method.

**Result** - holds object value from 'In' input point. 

### Properties

**bool Success** - The value will indicate whether the execution was succeded.
