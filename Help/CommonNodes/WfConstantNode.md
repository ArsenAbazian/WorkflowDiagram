## WfConstantValueNode
The WfConstantValueNode class used to specify constant value of one of the following types: Decimal, Boolean, String.

![WfConstantValueNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Constant.png)

### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

### Output Points

**Value** - The constant value.

### Properties

**WfValueType ConstantType** - indicates specified value type: Decimal, Boolean, String.

**object Value** - Specified constant value. The node will place this value to a 'Value' output connection point.